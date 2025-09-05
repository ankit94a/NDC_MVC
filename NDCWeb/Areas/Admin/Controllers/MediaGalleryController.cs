using AutoMapper;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Admin.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Infrastructure.Helpers.Account;
using NDCWeb.Infrastructure.Helpers.FileDirectory;
using NDCWeb.Infrastructure.Helpers.FileExt;
using NDCWeb.Models;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = CustomRoles.AdminStaff)]
    //[Authorize(Roles = CustomRoles.Admin)]
    [CSPLHeaders]
    public class MediaGalleryController : Controller
    {
        // GET: MediaGallery
        public async Task<ActionResult> Index()
        {
            var role = UserAccountHelper.getCurrentUserRole();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                //var mediaGalry = await uow.MediaGalleryRepo.GetAllAsync();
                //var mediaGalry = await uow.MediaGalleryRepo.FindAsync(x => x.UserRole == role);
                var mediaGalry = await uow.MediaGalleryRepo.GetAllAsync();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<MediaGallery>, List<MediaGalleryIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<MediaGallery>, IEnumerable<MediaGalleryIndxVM>>(mediaGalry).ToList();
                await uow.CommitAsync();
                return View(indexDto);
            }
        }
        public async Task<ActionResult> ShowMediaFiles(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var mediaGalry = await uow.MediaGalleryRepo.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MediaGallery, MediaGalleryIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var showMediaDto = mapper.Map<MediaGallery, MediaGalleryIndxVM>(mediaGalry);
                await uow.CommitAsync();
                //return View(indexDto);
                return PartialView("_ShowMediaFiles", showMediaDto);
            }
        }
        public async Task<ActionResult> Create()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.MediaCategories = uow.MediaCategoryMstrRepo.GetMediaCategories();
                await uow.CommitAsync();
                return View();
            }
        }

        // POST: MediaGallery/Create
        [HttpPost]
        public async Task<ActionResult> Create(MediaGalleryCrtVM objMediaGalryCvm, HttpPostedFileBase[] Files)
        {
            //Ensure model state is valid
            var role = UserAccountHelper.getCurrentUserRole();
            string path = ServerRootConsts.MEDIA_ROOT;
            if (objMediaGalryCvm.MediaType == MediaType.Image)
                path = path + "Images/";
            else if (objMediaGalryCvm.MediaType == MediaType.Video)
                path = path + "Videos/";
            else if (objMediaGalryCvm.MediaType == MediaType.Document)
                path = path + "Documents/";

            objMediaGalryCvm.iMediaFiles = new List<MediaFile>();
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".docx", ".docx", ".xlxs", ".xls", ".pptx", ".ppt", ".zip" };
            foreach (var file in Files)
            {
                var extension = Path.GetExtension(file.FileName);
                if (file != null && file.ContentLength > 0 && allowedExtensions.Contains(extension.ToLower()))
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var fileExtension = Path.GetExtension(fileName);
                    Guid guid = Guid.NewGuid();
                    if (objMediaGalryCvm.MediaCategoryId == 8)
                        fileName = "trg_caledar" + fileExtension;
                    else if (objMediaGalryCvm.MediaCategoryId == 9)
                        fileName = "weekly_trg_programme" + fileExtension;
                    //Check File
                    CheckBeforeUpload fs = new CheckBeforeUpload();
                    fs.filesize = 3000;
                    string result = fs.UploadFile(file);
                    if (string.IsNullOrEmpty(result))
                    {
                        MediaFile objMediaFile = new MediaFile()
                        {
                            GuId = guid,
                            FileName = fileName,
                            Extension = fileExtension,
                            FilePath = path + guid + fileExtension
                        };
                        file.SaveAs(Server.MapPath(objMediaFile.FilePath));
                        objMediaGalryCvm.iMediaFiles.Add(objMediaFile);
                    }
                    else
                        //this.AddNotification("Invalid file type", NotificationType.WARNING);
                        this.AddNotification(result, NotificationType.WARNING);
                }
                else
                {
                    TempData["notice"] = "You should choose valid file less than 2ΜB";
                    return View("Create"); // show the error in create View
                }
            }

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MediaGalleryCrtVM, MediaGallery>();
                });
                IMapper mapper = config.CreateMapper();
                MediaGallery CreateDto = mapper.Map<MediaGalleryCrtVM, MediaGallery>(objMediaGalryCvm);
                CreateDto.UserRole = role;
                uow.MediaGalleryRepo.Add(CreateDto);
                await uow.CommitAsync();
                return RedirectToAction("Create");
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.MediaCategories = uow.MediaCategoryMstrRepo.GetMediaCategories();

                var mediaGalry = await uow.MediaGalleryRepo.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MediaGallery, MediaGalleryUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                MediaGalleryUpVM UpdateDto = mapper.Map<MediaGallery, MediaGalleryUpVM>(mediaGalry);
                uow.Commit();
                return View(UpdateDto);
            }
        }

        // POST: MediaGallery/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(MediaGalleryUpVM objMediaGalryUvm, HttpPostedFileBase[] Files)
        {
            string path = ServerRootConsts.MEDIA_ROOT;
            if (objMediaGalryUvm.MediaType == MediaType.Image)
                path = path + "images/";
            else if (objMediaGalryUvm.MediaType == MediaType.Video)
                path = path + "videos/";
            else if (objMediaGalryUvm.MediaType == MediaType.Document)
                path = path + "documents/";

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".docx", ".docx", ".xlxs", ".xls", ".pptx", ".ppt", ".zip" };

            //objMediaGalryUvm.iMediaFiles = new List<MediaFile>();
            foreach (var file in Files)
            {
                var extension = Path.GetExtension(file.FileName);
                if (file != null && file.ContentLength > 0 && allowedExtensions.Contains(extension.ToLower()))
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var fileExtension = Path.GetExtension(fileName);
                    Guid guid = Guid.NewGuid();
                    if (objMediaGalryUvm.MediaCategoryId == 8)
                        fileName = "trg_caledar" + fileExtension;
                    else if (objMediaGalryUvm.MediaCategoryId == 9)
                        fileName = "weekly_trg_programme" + fileExtension;

                    //Check File
                    CheckBeforeUpload fs = new CheckBeforeUpload();
                    fs.filesize = 3000;
                    string result = fs.UploadFile(file);
                    if (string.IsNullOrEmpty(result))
                    {
                        MediaFile objMediaFile = new MediaFile()
                        {
                            GuId = guid,
                            FileName = fileName,
                            Extension = fileExtension,
                            FilePath = path + guid + fileExtension,
                            MediaGalleryId = objMediaGalryUvm.MediaGalleryId
                        };

                        file.SaveAs(Server.MapPath(objMediaFile.FilePath));
                        objMediaGalryUvm.iMediaFiles.Add(objMediaFile);
                    }
                    else
                        //this.AddNotification("Invalid file type", NotificationType.WARNING);
                        this.AddNotification(result, NotificationType.WARNING);

                }
                else
                {
                    TempData["notice"] = "You should choose valid file less than 2ΜB";
                    return View("Edit"); // show the error in Edit View
                }
            }

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                if (objMediaGalryUvm.iMediaFiles.Count > 0)
                {
                    var removeOldItem = uow.MediaFileRepo.Find(x => x.MediaGalleryId == objMediaGalryUvm.MediaGalleryId).ToList();
                    if (removeOldItem != null)
                    {
                        uow.MediaFileRepo.RemoveRange(removeOldItem);
                        uow.Commit();
                    }
                }

                var mediaGallery = uow.MediaGalleryRepo.GetById(objMediaGalryUvm.MediaGalleryId);
                mediaGallery.MediaType = objMediaGalryUvm.MediaType;
                mediaGallery.Caption = objMediaGalryUvm.Caption;
                mediaGallery.Description = objMediaGalryUvm.Description;
                mediaGallery.Archive = objMediaGalryUvm.Archive;
                mediaGallery.PublishDate = objMediaGalryUvm.PublishDate;
                mediaGallery.ArchiveDate = objMediaGalryUvm.ArchiveDate;
                mediaGallery.MediaCategoryId = objMediaGalryUvm.MediaCategoryId;

                if (objMediaGalryUvm.iMediaFiles.Count > 0)
                {
                    mediaGallery.iMediaFiles = objMediaGalryUvm.iMediaFiles;
                }
                await uow.CommitAsync();
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var DeleteItem = await uow.MediaGalleryRepo.GetByIdAsync(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    foreach (var item in DeleteItem.iMediaFiles)
                    {
                        //String path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), item.GuId + item.Extension);
                        String path = Server.MapPath(item.FilePath);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }
                    uow.MediaGalleryRepo.Remove(DeleteItem);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }


        [HttpPost]
        public JsonResult DocumentUpload()
        {
            //string path = ServerRootConsts.MEDIA_ROOT;
            //if (objMediaGalryUvm.MediaType == MediaType.Image)
            //    path = path + "images/";
            //else if (objMediaGalryUvm.MediaType == MediaType.Video)
            //    path = path + "videos/";
            //else if (objMediaGalryUvm.MediaType == MediaType.Document)
            //    path = path + "documents/";

            string ROOT_PATH = ServerRootConsts.MEDIA_ROOT;
            string DOC_PATH = ROOT_PATH + "files/";
            string CURRENT_YEAR = DateTime.Now.Year.ToString();
            string CURRENT_MONTH = DateTime.Now.ToString("MMM");

            string file_PATH = DOC_PATH + CURRENT_YEAR + "/" + CURRENT_MONTH + "/";
            if (Request.Files.Count > 0)
            {
                DirectoryHelper.CreateFolder(Server.MapPath(file_PATH));
                HttpPostedFileBase file = Request.Files[0];


                var fileName = Path.GetFileName(file.FileName);
                var fileExtension = Path.GetExtension(fileName);
                Guid guid = Guid.NewGuid();
                //if (objMediaGalryUvm.MediaCategoryId == 8)
                //    fileName = "trg_caledar" + fileExtension;
                //else if (objMediaGalryUvm.MediaCategoryId == 9)
                //    fileName = "weekly_trg_programme" + fileExtension;
                //Check File
                CheckBeforeUpload fs = new CheckBeforeUpload();
                fs.filesize = 5000;
                string result = fs.UploadFile(file);
                if (string.IsNullOrEmpty(result))
                {
                    MediaFile objMediaFile = new MediaFile()
                    {
                        GuId = guid,
                        FileName = fileName,
                        Extension = fileExtension,
                        FilePath = file_PATH + guid + fileExtension,
                        MediaGalleryId = 0
                    };
                    file.SaveAs(Server.MapPath(objMediaFile.FilePath));
                    return Json(new { message = "File Uploaded Successfully!", status = 1, mediaFile = objMediaFile });
                }
                else
                {
                    //return Json(new { message = "Invalid file type.", status = 0 });
                    return Json(new { message = result, status = 0 });
                }


                //Guid guid = Guid.NewGuid();
                //string newFileName = guid + Path.GetExtension(file.FileName);
                //string location = file_PATH + newFileName;
                //file.SaveAs(Server.MapPath(location));


            }
            else
                return Json(new { message = "No files selected.", status = 0 });
        }

        public class MediaFileUploadVM
        {
            public Guid GuId { get; set; }
            public string FileName { get; set; }
            public string Extension { get; set; }
            public string FilePath { get; set; }
        }
    }
}
