using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Core;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Infrastructure.Helpers.FileDirectory;
using NDCWeb.Infrastructure.Helpers.FileExt;
using NDCWeb.Models;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Staff.Controllers
{
    [CSPLHeaders]
    [Authorize(Roles = CustomRoles.Staff)]
    [StaffStaticUserMenu]
    [EncryptedActionParameter]  
    public class InStepParticipantsController : Controller
    {
        [EncryptedActionParameter]
        public async Task<ActionResult> Index(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {

                var instepCourseRegistration = uow.InStepRegistrationRepo.GetInStepCourseMemberList(id);
                return View(instepCourseRegistration);
            }

        }
        [EncryptedActionParameter]
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.Service = CustomDropDownList.GetRankService();
            ViewBag.Gender = CustomDropDownList.GetGender();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var evnt = await uow.InStepRegistrationRepo.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<InStepRegistration>, List<InStepRegistrationVM>>();
                });
                IMapper mapper = config.CreateMapper();
                InStepRegistrationVM indexDto = mapper.Map<InStepRegistration, InStepRegistrationVM>(evnt);
                ViewData["SelectedRank"] = indexDto.RankId;
                return View(indexDto);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(InStepRegistrationVM objInfotechUp)
        {
            ViewBag.Service = CustomDropDownList.GetRankService();
            ViewBag.Gender = CustomDropDownList.GetGender();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<InStepRegistrationVM, InStepRegistration>();
                });
                IMapper mapper = config.CreateMapper();
                InStepRegistration UpdateDto = mapper.Map<InStepRegistrationVM, InStepRegistration>(objInfotechUp);
                uow.InStepRegistrationRepo.Update(UpdateDto);
                await uow.CommitAsync();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("Index", new {id= objInfotechUp.CourseId });
                //return View();
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var DeleteItem = await uow.InStepRegistrationRepo.GetByIdAsync(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.InStepRegistrationRepo.Remove(DeleteItem);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
        public FileResult Download(String file)
        {
            String filepath = Server.MapPath(file);
            //String filepath = Server.UrlEncode(file);
            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file);
        }
        #region File Download
        //[EncryptedActionParameter]
        [HttpPost]
        public async Task<JsonResult> DownloadFile(int id, int tId)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var client = await uow.InStepRegistrationRepo.FirstOrDefaultAsync(cp => cp.InStepRegId == id);
                if (client == null)
                    return Json(data: "No file(s) found", behavior: JsonRequestBehavior.AllowGet);
                string filePath = "";
                if (tId == 1)
                {
                    if (client.PassportDocPath != null)
                        filePath = client.PassportDocPath;
                    else
                        return Json(data: "No file(s) found", behavior: JsonRequestBehavior.AllowGet);
                }
                else if (tId == 2) 
                {
                    if (client.AadhaarDocPath != null)
                        filePath = client.AadhaarDocPath;
                    else
                        return Json(data: "No file(s) found", behavior: JsonRequestBehavior.AllowGet);
                }
                else if (tId == 3)
                {
                    if (client.ApprovedNominationDocPath != null)
                        filePath = client.ApprovedNominationDocPath;
                    else
                        return Json(data: "No file(s) found", behavior: JsonRequestBehavior.AllowGet);
                }
                var fileInfo = new FileInfo(filePath);
                var fileName = fileInfo.Name;

                if (!fileInfo.Exists)
                    return Json(data: "No file(s) found", behavior: JsonRequestBehavior.AllowGet);

                //var path = Path.Combine(Server.MapPath("~/writereaddata/" + clientId + "/"), fileName);
                var path = Server.MapPath(filePath);
                var contentType = MimeMapping.GetMimeMapping(path);
                try
                {
                    var contentDisposition = new System.Net.Mime.ContentDisposition
                    {
                        FileName = fileName,
                        Inline = false,
                    };
                    Response.AppendHeader("Content-Disposition", contentDisposition.ToString());
                    //return File(path, contentType, fileName);
                    return Json(data: path, contentType, behavior: JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(data: "No file(s) found", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
        #endregion
        #region Helper Action
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult ImageUpload()
        {
            string ROOT_PATH = ServerRootConsts.USER_ROOT;
            string PHOTO_PATH = ROOT_PATH + "photos/";
            string CURRENT_YEAR = DateTime.Now.Year.ToString();
            string CURRENT_MONTH = DateTime.Now.ToString("MMM");

            string file_PATH = PHOTO_PATH + CURRENT_YEAR + "/" + CURRENT_MONTH + "/";
            if (Request.Files.Count > 0)
            {
                DirectoryHelper.CreateFolder(Server.MapPath(file_PATH));
                HttpPostedFileBase file = Request.Files[0];
                Guid guid = Guid.NewGuid();
                string newFileName = guid + Path.GetExtension(file.FileName);
                string location = file_PATH + newFileName;
                //Check File
                CheckBeforeUpload fs = new CheckBeforeUpload();
                fs.filesize = 3000;
                string result = fs.UploadFile(file);
                if (string.IsNullOrEmpty(result))
                {
                    file.SaveAs(Server.MapPath(location));
                    return Json(new { message = "File Uploaded Successfully!", status = 1, filePath = location });
                }
                else
                    //return Json(new { message = "File Could not be Uploaded!", status = 0 });
                    return Json(new { message = result, status = 0 });
            }
            else
                return Json(new { message = "No files selected.", status = 0, filePath = "#" });
        }
        [HttpPost]
        public JsonResult DocumentUpload()
        {
            string ROOT_PATH = ServerRootConsts.USER_ROOT;
            string DOC_PATH = ROOT_PATH + "documents/";
            string CURRENT_YEAR = DateTime.Now.Year.ToString();
            string CURRENT_MONTH = DateTime.Now.ToString("MMM");

            string file_PATH = DOC_PATH + CURRENT_YEAR + "/" + CURRENT_MONTH + "/";
            if (Request.Files.Count > 0)
            {
                DirectoryHelper.CreateFolder(Server.MapPath(file_PATH));
                HttpPostedFileBase file = Request.Files[0];
                Guid guid = Guid.NewGuid();
                string newFileName = guid + Path.GetExtension(file.FileName);
                string location = file_PATH + newFileName;
                //Check File
                CheckBeforeUpload fs = new CheckBeforeUpload();
                fs.filesize = 3000;
                string result = fs.UploadFile(file);
                if (string.IsNullOrEmpty(result))
                {
                    file.SaveAs(Server.MapPath(location));
                    return Json(new { message = "File Uploaded Successfully!", status = 1, filePath = location });
                }
                else
                    return Json(new { message = result, status = 0 });
            }
            else
                return Json(new { message = "No files selected.", status = 0, filePath = "#" });
        }
        #endregion
    }
}