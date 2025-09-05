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

namespace NDCWeb.Areas.Member.Controllers
{
    [Authorize(Roles = CustomRoles.CandidateStaff)]
    [UserMenu(MenuArea = "Member")]
    [CSPLHeaders]
    public class ForumBlogController : Controller
    {
        // GET: Member/ForumBlog
        public ActionResult Index()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var forumBlogs = uow.ForumBlogRepo.Find(x=>x.CreatedBy == uId);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<ForumBlog>, List<ForumBlogIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<ForumBlog>, IEnumerable<ForumBlogIndxVM>>(forumBlogs).ToList();
                return View(indexDto);
            }
        }
        [EncryptedActionParameter]
        public ActionResult ForumBlogEdit(int id)
        {
            ViewBag.GetForumBlogCat = CustomDropDownList.GetForumBlogCategory();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Staff = uow.StaffMasterRepo.GetStaff();
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
            if (!ModelState.IsValid)
            {
                this.AddNotification("Invalid content", NotificationType.WARNING);
                return RedirectToAction("Index");
            }
            ViewBag.GetForumBlogCat = CustomDropDownList.GetForumBlogCategory();
            string path = ServerRootConsts.FORUMBLOG_ROOT;

            //var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".docx", ".docx", ".xlxs", ".xls", ".pptx", ".ppt", ".zip" };

            objForumBlog.iForumBlogMedias = new List<ForumBlogMedia>();
            foreach (var file in Files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    Guid guid = Guid.NewGuid();
                    //Check File
                    CheckBeforeUpload fs = new CheckBeforeUpload();
                    fs.filesize = 6000;
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
                        //this.AddNotification("Invalid file type", NotificationType.WARNING);
                        this.AddNotification(result, NotificationType.WARNING);
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
                var forumBlog = uow.ForumBlogRepo.GetById(objForumBlog.ForumBlogId);
                forumBlog.Category = objForumBlog.Category;
                forumBlog.Description = objForumBlog.Description;
                forumBlog.MemberRemark = objForumBlog.MemberRemark;
                forumBlog.Status = "Pending";

                if (objForumBlog.iForumBlogMedias.Count > 0)
                {
                    forumBlog.iForumBlogMedias = objForumBlog.iForumBlogMedias;
                }
                uow.Commit();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }


        public ActionResult ForumBlogEntry()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.GetForumBlogCat = CustomDropDownList.GetForumBlogCategory();
                ViewBag.Staff = uow.StaffMasterRepo.GetStaff();
                ViewBag.Module = CustomDropDownList.GetTrainingContentModule();  //ADD BY AMITESH
                ForumBlogCrtVM objForumBlogvmNew = new ForumBlogCrtVM();
                return View(objForumBlogvmNew);
            }
        }
        [HttpPost]
        public async Task<ActionResult> ForumBlogEntry(ForumBlogCrtVM objForumBlogvm, HttpPostedFileBase[] Files)
        {
            if (!ModelState.IsValid)
            {
                this.AddNotification("Invalid content", NotificationType.WARNING);
                return RedirectToAction("Index");
            }
            ViewBag.GetForumBlogCat = CustomDropDownList.GetForumBlogCategory();
            string path = ServerRootConsts.FORUMBLOG_ROOT;
            objForumBlogvm.iForumBlogMedias = new List<ForumBlogMedia>();
            foreach (var file in Files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    Guid guid = Guid.NewGuid();
                    //Check File
                    CheckBeforeUpload fs = new CheckBeforeUpload();
                    fs.filesize = 6000;
                    string result = fs.UploadFile(file);
                    if (string.IsNullOrEmpty(result))
                    {
                        ForumBlogMedia objMediaFile = new ForumBlogMedia()
                        {
                            GuId = guid,
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            FilePath = path + guid + Path.GetExtension(fileName)
                        };

                        file.SaveAs(Server.MapPath(objMediaFile.FilePath));
                        objForumBlogvm.iForumBlogMedias.Add(objMediaFile);
                    }
                    else
                    {
                        //this.AddNotification("Invalid file type", NotificationType.WARNING);
                        this.AddNotification(result, NotificationType.WARNING);
                    }

                }
            }

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ForumBlogCrtVM, ForumBlog>();
                });
                IMapper mapper = config.CreateMapper();
                ForumBlog CreateDto = mapper.Map<ForumBlogCrtVM, ForumBlog>(objForumBlogvm);
                CreateDto.Status = "Pending";
                uow.ForumBlogRepo.Add(CreateDto);
                await uow.CommitAsync();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("Index");
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

                CheckBeforeUpload fs = new CheckBeforeUpload();
                fs.filesize = 6000;
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
        [EncryptedActionParameter]
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
                return PartialView("_ShowDocuments", showMediaDto);
                //return PartialView("_Showmedia", showMediaDto);
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
                    foreach (var item in DeleteItem.iForumBlogMedias)
                    {
                        //String path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), item.GuId + item.Extension);
                        String path = Server.MapPath(item.FilePath);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }
                    uow.ForumBlogRepo.Remove(DeleteItem);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}