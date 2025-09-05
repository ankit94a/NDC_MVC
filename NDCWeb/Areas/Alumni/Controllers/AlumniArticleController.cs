using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Alumni.View_Models;
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

namespace NDCWeb.Areas.Alumni.Controllers
{
    [Authorize(Roles = CustomRoles.Alumni)]
    [UserMenu(MenuArea = "Alumni")]
    [CSPLHeaders]
    public class AlumniArticleController : Controller
    {
        // GET: Member/AlumniArticle
        public ActionResult Index()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var AlumniArticles = uow.AlumniArticleRepo.Find(x => x.CreatedBy == uId);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<AlumniArticle>, List<AlumniArticleIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<AlumniArticle>, IEnumerable<AlumniArticleIndxVM>>(AlumniArticles).ToList();
                return View(indexDto);
            }
        }
        [EncryptedActionParameter]
        public ActionResult AlumniArticleEdit(int id)
        {
            ViewBag.GetAlumniArticleCat = CustomDropDownList.GetAlumniArticleCategory();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Staff = uow.StaffMasterRepo.GetStaff();
                var AlumniArticledata = uow.AlumniArticleRepo.GetById(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AlumniArticle, AlumniArticleUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                AlumniArticleUpVM CreateDto = mapper.Map<AlumniArticle, AlumniArticleUpVM>(AlumniArticledata);
                return View(CreateDto);
            }
        }
        [HttpPost]
        public ActionResult AlumniArticleEdit(AlumniArticleUpVM objAlumniArticle, HttpPostedFileBase[] Files)
        {
            ViewBag.GetAlumniArticleCat = CustomDropDownList.GetAlumniArticleCategory();
            string path = ServerRootConsts.ALUMNIARTICLE_ROOT;

            //var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".docx", ".docx", ".xlxs", ".xls", ".pptx", ".ppt", ".zip" };

            objAlumniArticle.iAlumniArticleMedias = new List<AlumniArticleMedia>();
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
                        AlumniArticleMedia objMediaFile = new AlumniArticleMedia()
                        {
                            GuId = guid,
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            FilePath = path + guid + Path.GetExtension(fileName),
                            ArticleId = objAlumniArticle.ArticleId
                        };

                        file.SaveAs(Server.MapPath(objMediaFile.FilePath));
                        objAlumniArticle.iAlumniArticleMedias.Add(objMediaFile);
                    }
                    else
                    {
                        this.AddNotification(result, NotificationType.WARNING);
                    }

                }
            }

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                if (objAlumniArticle.iAlumniArticleMedias.Count > 0)
                {
                    var removeOldItem = uow.AlumniArticleMediaRepo.Find(x => x.ArticleId == objAlumniArticle.ArticleId).ToList();
                    if (removeOldItem != null)
                    {
                        uow.AlumniArticleMediaRepo.RemoveRange(removeOldItem);
                        uow.Commit();
                    }
                }
                var AlumniArticle = uow.AlumniArticleRepo.GetById(objAlumniArticle.ArticleId);
                AlumniArticle.Category = objAlumniArticle.Category;
                AlumniArticle.Description = objAlumniArticle.Description;

                if (objAlumniArticle.iAlumniArticleMedias.Count > 0)
                {
                    AlumniArticle.iAlumniArticleMedias = objAlumniArticle.iAlumniArticleMedias;
                }
                uow.Commit();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }


        public ActionResult AlumniArticleEntry()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.GetAlumniArticleCat = CustomDropDownList.GetAlumniArticleCategory();
                ViewBag.Staff = uow.StaffMasterRepo.GetStaff();
                AlumniArticleCrtVM objAlumniArticlevmNew = new AlumniArticleCrtVM();
                return View(objAlumniArticlevmNew);
            }
        }
        [HttpPost]
        public async Task<ActionResult> AlumniArticleEntry(AlumniArticleCrtVM objAlumniArticlevm, HttpPostedFileBase[] Files)
        {
            ViewBag.GetAlumniArticleCat = CustomDropDownList.GetAlumniArticleCategory();
            string path = ServerRootConsts.ALUMNIARTICLE_ROOT;
            objAlumniArticlevm.iAlumniArticleMedias = new List<AlumniArticleMedia>();
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
                        AlumniArticleMedia objMediaFile = new AlumniArticleMedia()
                        {
                            GuId = guid,
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            FilePath = path + guid + Path.GetExtension(fileName)
                        };

                        file.SaveAs(Server.MapPath(objMediaFile.FilePath));
                        objAlumniArticlevm.iAlumniArticleMedias.Add(objMediaFile);
                    }
                    else
                    {
                        this.AddNotification(result, NotificationType.WARNING);
                    }

                }
            }

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AlumniArticleCrtVM, AlumniArticle>();
                });
                IMapper mapper = config.CreateMapper();
                AlumniArticle CreateDto = mapper.Map<AlumniArticleCrtVM, AlumniArticle>(objAlumniArticlevm);
                uow.AlumniArticleRepo.Add(CreateDto);
                await uow.CommitAsync();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public JsonResult DocumentUpload()
        {
            string ROOT_PATH = ServerRootConsts.ALUMNIARTICLE_ROOT;
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
        [EncryptedActionParameter]
        public async Task<ActionResult> ShowMediaFiles(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var mediaGalry = await uow.AlumniArticleRepo.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AlumniArticle, AlumniArticleIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var showMediaDto = mapper.Map<AlumniArticle, AlumniArticleIndxVM>(mediaGalry);
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
                var DeleteItem = await uow.AlumniArticleRepo.GetByIdAsync(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    foreach (var item in DeleteItem.iAlumniArticleMedias)
                    {
                        //String path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), item.GuId + item.Extension);
                        String path = Server.MapPath(item.FilePath);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }
                    uow.AlumniArticleRepo.Remove(DeleteItem);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}