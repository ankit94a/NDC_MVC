using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Staff.View_Models;
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
    //[UserMenu(MenuArea = "Staff")]
    [StaffStaticUserMenu]
    [EncryptedActionParameter]
    public class CircularController : Controller
    {
        // GET: Staff/Circular
        public ActionResult Index()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                string staffType = "NA";
                string category = "NA";
                CircularCrtVM objCircularvmNew = new CircularCrtVM();
                var memberPersonal = uow.StaffMasterRepo.Find(x => x.LoginUserId == uId, fk => fk.Faculties).FirstOrDefault();
                if (memberPersonal != null)
                { staffType = memberPersonal.Faculties.StaffType; }

                if (staffType == "AQ")
                    category = "Order";
                else if (staffType == "DS (Coord)")
                    category = "Circular";
                          

                var circulars = uow.CircularRepo.Find(x=>x.Category== category);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<Circular>, List<CircularIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<Circular>, IEnumerable<CircularIndxVM>>(circulars).ToList();
                return View(indexDto);
            }
        }
        public ActionResult CircularEdit(int id)
        {
            ViewBag.GetCircularCat = CustomDropDownList.GetCircularCategory();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var Circulardata = uow.CircularRepo.GetById(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Circular, CircularUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                CircularUpVM CreateDto = mapper.Map<Circular, CircularUpVM>(Circulardata);
                return View(CreateDto);
            }
        }
        [HttpPost]
        public ActionResult CircularEdit(CircularUpVM objCircularUvm, HttpPostedFileBase[] Files)
        {
            ViewBag.GetCircularCat = CustomDropDownList.GetCircularCategory();

            string path = ServerRootConsts.CIRCULAR_ROOT;
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".docx", ".doc", ".xlxs", ".xls", ".pptx", ".ppt" };

            objCircularUvm.iCircularMedias = new List<CircularMedia>();
            foreach (var file in Files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var extension = Path.GetExtension(file.FileName);
                    if (allowedExtensions.Contains(extension.ToLower()))
                    { }
                    else
                    {
                        this.AddNotification("You should choose valid file", NotificationType.ERROR);
                        return RedirectToAction("CircularEntry");
                    }
                }
            }
            foreach (var file in Files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    Guid guid = Guid.NewGuid();
                    CheckBeforeUpload fs = new CheckBeforeUpload();
                    fs.filesize = 3000;
                    string result = fs.UploadFile(file);
                    if (string.IsNullOrEmpty(result))
                    {
                        CircularMedia objMediaFile = new CircularMedia()
                        {
                            GuId = guid,
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            FilePath = path + guid + Path.GetExtension(fileName),
                            CircularId = objCircularUvm.CircularId
                        };
                        file.SaveAs(Server.MapPath(objMediaFile.FilePath));
                        objCircularUvm.iCircularMedias.Add(objMediaFile);
                    }
                    else
                    {
                        //this.AddNotification("You should choose valid file", NotificationType.ERROR);
                        this.AddNotification(result, NotificationType.ERROR);
                    }

                }
            }

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                if (objCircularUvm.iCircularMedias.Count > 0)
                {
                    var removeOldItem = uow.CircularMediaRepo.Find(x => x.CircularId == objCircularUvm.CircularId).ToList();
                    if (removeOldItem != null)
                    {
                        uow.CircularMediaRepo.RemoveRange(removeOldItem);
                        uow.Commit();
                    }
                }

                var circular = uow.CircularRepo.GetById(objCircularUvm.CircularId);
                circular.Category = objCircularUvm.Category;
                circular.Description = objCircularUvm.Description;

                if (objCircularUvm.iCircularMedias.Count > 0)
                {
                    circular.iCircularMedias = objCircularUvm.iCircularMedias;
                }
                uow.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult CircularEntry()
        {
            ViewBag.GetCircularCat = CustomDropDownList.GetCircularCategory();
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                string staffType = "NA";
                CircularCrtVM objCircularvmNew = new CircularCrtVM();
                var memberPersonal = uow.StaffMasterRepo.Find(x => x.LoginUserId == uId, fk => fk.Faculties).FirstOrDefault();
                if (memberPersonal != null)
                { staffType = memberPersonal.Faculties.StaffType; }

                if (staffType == "AQ")
                    objCircularvmNew.Category = "Order";
                else if (staffType == "DS (Coord)")
                    objCircularvmNew.Category = "Circular";
                    objCircularvmNew.Category = "Misc";

                return View(objCircularvmNew);
            }
        }
        [HttpPost]
        public JsonResult GetCommunityList(int OrderId)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var menurole = uow.CommunityRepo.GetAll().ToList();
                if (menurole != null)
                {
                   
                }
                //uow.Commit();
                if (menurole == null)
                    return Json(data: new { flag = 0 }, JsonRequestBehavior.AllowGet);
                return Json(data: new { menus = menurole, flag = 1 }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult CircularEntry(CircularCrtVM objCircularvm, HttpPostedFileBase[] Files, int[] selChkbxDesignationId, int[] selChkShow)
        {
            ViewBag.GetCircularCat = CustomDropDownList.GetCircularCategory();
            string path = ServerRootConsts.CIRCULAR_ROOT;
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".docx", ".doc", ".xlxs", ".xls", ".pptx", ".ppt" };

            objCircularvm.iCircularMedias = new List<CircularMedia>();
            foreach (var file in Files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var extension = Path.GetExtension(file.FileName);
                    if (allowedExtensions.Contains(extension.ToLower()))
                    { }
                    else
                    {
                        this.AddNotification("You should choose valid file", NotificationType.ERROR);
                        return RedirectToAction("CircularEntry");
                    }
                }
            }
            foreach (var file in Files)
            {
                var extension = Path.GetExtension(file.FileName);
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    Guid guid = Guid.NewGuid();
                    CheckBeforeUpload fs = new CheckBeforeUpload();
                    fs.filesize = 3000;
                    string result = fs.UploadFile(file);
                    if (string.IsNullOrEmpty(result))
                    {
                        CircularMedia objMediaFile = new CircularMedia()
                        {
                            GuId = guid,
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            FilePath = path + guid + Path.GetExtension(fileName)
                        };

                        file.SaveAs(Server.MapPath(objMediaFile.FilePath));
                        objCircularvm.iCircularMedias.Add(objMediaFile);
                    }
                    else
                        //this.AddNotification("Invalid file type", NotificationType.WARNING);
                        this.AddNotification(result, NotificationType.WARNING);

                }
            }

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                string staffType = "NA";
                string uId = User.Identity.GetUserId();
                //CircularCrtVM objCircularvm = new CircularCrtVM();
                if (selChkbxDesignationId != null)
                {
                    int cn = selChkbxDesignationId.Count();
                    List<CircularDetail> objCircularDetailList = new List<CircularDetail>();
                    for (int i = 0; i < cn; i++)
                    {
                        CircularDetail objCircularDetail = new CircularDetail();
                        objCircularDetail.CircularId = objCircularvm.CircularId;
                        objCircularDetail.DesignationId = selChkbxDesignationId[i];
                        if (selChkShow != null)
                        {
                            if (selChkShow.Contains(selChkbxDesignationId[i])) objCircularDetail.Show = true;
                            else objCircularDetail.Show = false;
                            objCircularDetailList.Add(objCircularDetail);
                        }
                    }

                    var CircularDetailList = uow.CircularDetailRepo.Find(x => x.CircularId == objCircularvm.CircularId);
                    if (CircularDetailList != null)
                    {
                        uow.CircularDetailRepo.RemoveRange(CircularDetailList);
                        uow.Commit();
                    }
                    uow.CircularDetailRepo.AddRange(objCircularDetailList);
                }
                //Existing Code
                var memberPersonal = uow.StaffMasterRepo.Find(x => x.LoginUserId == uId, fk => fk.Faculties).FirstOrDefault();
                if (memberPersonal != null)
                { staffType = memberPersonal.Faculties.StaffType; }

                if (staffType == "AQ")
                    objCircularvm.Category = "Order";
                else if (staffType == "DS (Coord)")
                    objCircularvm.Category = "Circular";

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CircularCrtVM, Circular>();
                });
                IMapper mapper = config.CreateMapper();
                Circular CreateDto = mapper.Map<CircularCrtVM, Circular>(objCircularvm);
                uow.CircularRepo.Add(CreateDto);
                uow.Commit();
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> ShowMediaFiles(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var mediaGalry = await uow.CircularRepo.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Circular, CircularIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var showMediaDto = mapper.Map<Circular, CircularIndxVM>(mediaGalry);
                await uow.CommitAsync();
                //return View(indexDto);
                return PartialView("_ShowCircularMediaFiles", showMediaDto);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<JsonResult> DeleteOnConfirm(int id)
        public JsonResult DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var DeleteItem =  uow.CircularRepo.GetById(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    foreach (var item in DeleteItem.iCircularMedias)
                    {
                        //String path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), item.GuId + item.Extension);
                        String path = Server.MapPath(item.FilePath);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }
                    uow.CircularRepo.Remove(DeleteItem);
                    uow.Commit();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
        [HttpPost]
        public JsonResult DocumentUpload()
        {
            string ROOT_PATH = ServerRootConsts.CIRCULAR_ROOT;
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
    }
}