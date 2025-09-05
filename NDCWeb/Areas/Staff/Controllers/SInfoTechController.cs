using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Infrastructure.Helpers.FileDirectory;
using NDCWeb.Models;
using NDCWeb.Infrastructure.Helpers.FileExt;
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
    //[UserMenu(MenuArea = "Staff")]
    public class SInfoTechController : Controller
    {
        // GET: Staff/SInfoTech
        public async Task<ActionResult> Index()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var infoTeches = await uow.InfotechRepo.GetInfoTechComplete();
                return View(infoTeches);
            }
        }
        public async Task<ActionResult> Edit(int id)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var infotech = await uow.InfotechRepo.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<Infotech>, List<InfoTechUpVM>>();
                });
                IMapper mapper = config.CreateMapper();
                InfoTechUpVM indexDto = mapper.Map<Infotech, InfoTechUpVM>(infotech);
                #region Member Detail
                var crsMemberPersonal = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == infotech.CreatedBy);
                var appointment = uow.CrsMbrAppointmentRepo.FirstOrDefault(x => x.CreatedBy == infotech.CreatedBy, np => np.Ranks);
                var regMember = uow.CourseRegisterRepo.FirstOrDefault(x => x.UserId == infotech.CreatedBy);
                ViewBag.FullName = crsMemberPersonal.FirstName + ", " + crsMemberPersonal.MiddleName + ", " + crsMemberPersonal.Surname + ", " + regMember.Honour;
                ViewBag.Rank = appointment.Ranks.RankName;
                #endregion
                return View(indexDto);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Edit(InfoTechUpVM objInfotechUp)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<InfoTechUpVM, Infotech>();
                });
                IMapper mapper = config.CreateMapper();
                Infotech UpdateDto = mapper.Map<InfoTechUpVM, Infotech>(objInfotechUp);
                uow.InfotechRepo.Update(UpdateDto);
                await uow.CommitAsync();
                return RedirectToAction("Index");
            }
        }

        #region Helper Action
        [HttpPost]
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
                CheckBeforeUpload fs = new CheckBeforeUpload();
                fs.filesize = 3000;
                string result = fs.UploadFile(file);
                if (string.IsNullOrEmpty(result))
                {
                    file.SaveAs(Server.MapPath(location));
                    return Json(new { message = "File Uploaded Successfully!", status = 1, filePath = location });
                }
                else
                    //return Json(new { message = "File Could not be Uploaded!", status = 0, filePath = location });
                    return Json(new { message = result, status = 0, filePath = location });
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
                CheckBeforeUpload fs = new CheckBeforeUpload();
                fs.filesize = 3000;
                string result = fs.UploadFile(file);
                if (string.IsNullOrEmpty(result))
                {
                    file.SaveAs(Server.MapPath(location));
                    return Json(new { message = "File Uploaded Successfully!", status = 1, filePath = location });
                }
                else
                    //return Json(new { message = "File Could not be Uploaded!", status = 0, filePath = location });
                    return Json(new { message = result, status = 0, filePath = location });
            }
            else
                return Json(new { message = "No files selected.", status = 0, filePath = "#" });
        }
        #endregion
    }
}