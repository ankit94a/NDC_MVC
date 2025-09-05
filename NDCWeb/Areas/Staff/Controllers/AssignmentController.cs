using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Member.View_Models;
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
    [Authorize(Roles = CustomRoles.CandidateStaff)]
    [StaffStaticUserMenu]
    //[UserMenu(MenuArea = "Staff")]
    [EncryptedActionParameter]

    public class AssignmentController : Controller
    {
        // GET: Staff/Assignment
        public async Task<ActionResult> Index(string category)
        {
            category = category ?? "All";
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var memberUploadAll = await uow.ForumBlogRepo.GetMemberUploadsForStaff(uId, category);
                return View(memberUploadAll);
            }
        }
        public ActionResult ForumBlogEdit(int id)
        {
            ViewBag.GetForumBlogCat = CustomDropDownList.GetForumBlogCategory();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Staff = uow.StaffMasterRepo.GetStaff();
                ViewBag.Status = CustomDropDownList.GetForumBlogStatus();
                var forumblogdata = uow.ForumBlogRepo.GetById(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ForumBlog, ForumBlogUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                ForumBlogUpVM CreateDto = mapper.Map<ForumBlog, ForumBlogUpVM>(forumblogdata);
                return View(CreateDto);
            }
        }
        [HttpPost]
        public ActionResult ForumBlogEdit(ForumBlogUpVM objForumBlog, HttpPostedFileBase[] Files)
        {
            ViewBag.GetForumBlogCat = CustomDropDownList.GetForumBlogCategory();
            string path = ServerRootConsts.FORUMBLOG_ROOT;
            objForumBlog.iForumBlogMedias = new List<ForumBlogMedia>();
            foreach (var file in Files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    Guid guid = Guid.NewGuid();
                    //Check File
                    CheckBeforeUpload fs = new CheckBeforeUpload();
                    fs.filesize = 3000;
                    string result = fs.UploadFile(file);
                    if (string.IsNullOrEmpty(result))
                    {
                        ForumBlogMedia objMediaFile = new ForumBlogMedia()
                        {
                            GuId = guid,
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            FilePath = path + guid + Path.GetExtension(fileName),
                            ForumBlogId = objForumBlog.ForumBlogId
                        };
                        file.SaveAs(Server.MapPath(objMediaFile.FilePath));
                        objForumBlog.iForumBlogMedias.Add(objMediaFile);
                    }
                    else
                    {
                        this.AddNotification(result, NotificationType.WARNING);
                    }

                }
            }
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                if (objForumBlog.iForumBlogMedias.Count > 0)
                {
                    var removeOldItem = uow.ForumBlogMediaRepo.Find(x => x.ForumBlogId == objForumBlog.ForumBlogId).ToList();
                    if (removeOldItem != null)
                    {
                        uow.ForumBlogMediaRepo.RemoveRange(removeOldItem);
                        uow.Commit();
                    }
                }
                var forumblogdata = uow.ForumBlogRepo.GetById(objForumBlog.ForumBlogId);
                forumblogdata.StaffRemark = objForumBlog.StaffRemark;
                forumblogdata.Status = objForumBlog.Status;
                forumblogdata.IsReaded = "Y";
                if (objForumBlog.iForumBlogMedias.Count > 0)
                {
                    forumblogdata.iForumBlogMedias = objForumBlog.iForumBlogMedias;
                }

                uow.Commit();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("Index", new { category = objForumBlog.Category });
            }
        }
        [HttpPost]
        public JsonResult DocumentUpload()
        {
            string ROOT_PATH = ServerRootConsts.FORUMBLOG_ROOT;
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
                file.SaveAs(Server.MapPath(location));
                return Json(new { message = "File Uploaded Successfully!", status = 1, filePath = location });
            }
            else
                return Json(new { message = "No files selected.", status = 0, filePath = "#" });
        }
        public async Task<ActionResult> ShowMediaFiles(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var mediaGalry = await uow.ForumBlogRepo.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ForumBlog, ForumBlogIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var showMediaDto = mapper.Map<ForumBlog, ForumBlogIndxVM>(mediaGalry);
                await uow.CommitAsync();
                //return View(indexDto);
                return PartialView("_Showmedia", showMediaDto);
            }
        }
        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var DeleteItem = await uow.ForumBlogRepo.GetByIdAsync(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    
                    uow.ForumBlogRepo.Remove(DeleteItem);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
        [HttpPost]
        public async Task<JsonResult> MarkAsRead(int Id)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var SelectedUsers = await uow.ForumBlogRepo.FirstOrDefaultAsync(x => x.ForumBlogId == Id);
                    if (SelectedUsers == null)
                        return Json(data: "Not Found", behavior: JsonRequestBehavior.AllowGet);
                    else
                    {
                        SelectedUsers.IsReaded = "Y";
                        uow.ForumBlogRepo.Update(SelectedUsers);
                        await uow.CommitAsync();
                        //await uow.Complete();
                        return Json(data: "Red", behavior: JsonRequestBehavior.AllowGet);
                    }
                }
            }
            finally { }
        }
    }
}