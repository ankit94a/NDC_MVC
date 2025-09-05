using AutoMapper;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Infrastructure.Filters;
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

namespace NDCWeb.Areas.Admin.Controllers
{
    [CSPLHeaders]
    [Authorize(Roles = CustomRoles.Admin)]
    public class TrainingActivityController : Controller
    {
        // GET: Staff/TrainingActivity
        public ActionResult Index()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var trainingActivities = uow.TrainingActivityRepo.GetAll();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TrainingActivity, TrainingActivityIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<TrainingActivity>, IEnumerable<TrainingActivityIndxVM>>(trainingActivities).ToList();
                return View(indexDto);
            }
        }

        // GET: Staff/TrainingActivity/Create
        public ActionResult Create()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Module = CustomDropDownList.GetTrainingContentModule();
                ViewBag.Activity = CustomDropDownList.GetTrainingContentActivity();
                //ViewBag.ModuleMenu = uow.MenuItemMstrRepo.GetParentMenus(108);
                return View();
            }
        }

        // POST: Staff/TrainingActivity/Create
        [HttpPost]
        public ActionResult Create(TrainingActivityCrtVM objTrainingActivityCvm, HttpPostedFileBase[] Files)
        {
            string path = ServerRootConsts.TRAINING_DOC_ROOT;
            objTrainingActivityCvm.iTrainingActivityMedias = new List<TrainingActivityMedia>();
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
                        TrainingActivityMedia objMediaFile = new TrainingActivityMedia()
                        {
                            GuId = guid,
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            FilePath = path + guid + Path.GetExtension(fileName)
                        };

                        file.SaveAs(Server.MapPath(objMediaFile.FilePath));
                        objTrainingActivityCvm.iTrainingActivityMedias.Add(objMediaFile);
                    }
                    else
                        //this.AddNotification("Invalid file type", NotificationType.WARNING);
                        this.AddNotification(result, NotificationType.WARNING);

                }
            }

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var trainigContent = uow.TrainingActivityRepo.Find(x => x.Module == objTrainingActivityCvm.Module && x.Activity == objTrainingActivityCvm.Activity);
                if (trainigContent.Count() > 0)
                {
                    this.AddNotification("Record Already Created..! Please change Module and Activity", NotificationType.WARNING);
                    return RedirectToAction("Index");
                }
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TrainingActivityCrtVM, TrainingActivity>();
                });
                IMapper mapper = config.CreateMapper();
                TrainingActivity CreateDto = mapper.Map<TrainingActivityCrtVM, TrainingActivity>(objTrainingActivityCvm);
                uow.TrainingActivityRepo.Add(CreateDto);
                uow.Commit();
                return RedirectToAction("Index");
            }
        }

        // GET: Staff/TrainingActivity/Edit/5
        public ActionResult Edit(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                //ViewBag.ModuleMenu = uow.MenuItemMstrRepo.GetParentMenus(108);
                ViewBag.Module = CustomDropDownList.GetTrainingContentModule();
                ViewBag.Activity = CustomDropDownList.GetTrainingContentActivity();
                var Trgdata = uow.TrainingActivityRepo.FirstOrDefault(x => x.TrainingActivityId == id);
                //ViewData["SelectedParent"] = Trgdata.MenuItemMasters.ParentId;
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TrainingActivity, TrainingActivityUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                TrainingActivityUpVM UpdateDto = mapper.Map<TrainingActivity, TrainingActivityUpVM>(Trgdata);
                //UpdateDto.ParentId = Trgdata.MenuItemMasters.ParentId;
                //ViewData["SelectedMenu"] = UpdateDto.MenuId;
                return View(UpdateDto);
            }
        }

        // POST: Staff/TrainingActivity/Edit/5
        [HttpPost]
        public ActionResult Edit(TrainingActivityUpVM objTrgupvm, HttpPostedFileBase[] Files)
        {
            string path = ServerRootConsts.TRAINING_DOC_ROOT;

            //var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".docx", ".docx", ".xlxs", ".xls", ".pptx", ".ppt", ".zip" };
            objTrgupvm.iTrainingActivityMedias = new List<TrainingActivityMedia>();
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
                        TrainingActivityMedia objMediaFile = new TrainingActivityMedia()
                        {
                            GuId = guid,
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            FilePath = path + guid + Path.GetExtension(fileName),
                            TrainingActivityId = objTrgupvm.TrainingActivityId
                        };

                        file.SaveAs(Server.MapPath(objMediaFile.FilePath));
                        objTrgupvm.iTrainingActivityMedias.Add(objMediaFile);
                    }
                    else
                        //this.AddNotification("Invalid file type", NotificationType.WARNING);
                        this.AddNotification(result, NotificationType.WARNING);
                }
            }

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var trainigContent = uow.TrainingActivityRepo.Find(x => x.Module == objTrgupvm.Module && x.Activity == objTrgupvm.Activity && x.TrainingActivityId != objTrgupvm.TrainingActivityId);
                if (trainigContent.Count() > 0)
                {
                    this.AddNotification("Record Already Exists..! Please change Module and Activity", NotificationType.WARNING);
                    return RedirectToAction("Index");
                }
                if (objTrgupvm.iTrainingActivityMedias.Count > 0)
                {
                    var removeOldItem = uow.TrainingActivityMediaRepo.Find(x => x.TrainingActivityId == objTrgupvm.TrainingActivityId).ToList();
                    if (removeOldItem != null)
                    {
                        uow.TrainingActivityMediaRepo.RemoveRange(removeOldItem);
                        uow.Commit();
                    }
                }
                var trainingActivity = uow.TrainingActivityRepo.GetById(objTrgupvm.TrainingActivityId);
                trainingActivity.Module = objTrgupvm.Module;
                trainingActivity.Activity = objTrgupvm.Activity;
                trainingActivity.Description = objTrgupvm.Description;
                trainingActivity.FromDate = objTrgupvm.FromDate;
                trainingActivity.ToDate = objTrgupvm.ToDate;
                trainingActivity.Active = objTrgupvm.Active;

                if (objTrgupvm.iTrainingActivityMedias.Count > 0)
                {
                    trainingActivity.iTrainingActivityMedias = objTrgupvm.iTrainingActivityMedias;
                }
                uow.Commit();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var DeleteItem = await uow.TrainingActivityRepo.GetByIdAsync(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    foreach (var item in DeleteItem.iTrainingActivityMedias)
                    {
                        //String path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), item.GuId + item.Extension);
                        String path = Server.MapPath(item.FilePath);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }
                    uow.TrainingActivityRepo.Remove(DeleteItem);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
        public async Task<ActionResult> ShowMediaFiles(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var trainingActivities = await uow.TrainingActivityRepo.GetByIdAsync(id);
                var trainingMedia = trainingActivities.iTrainingActivityMedias.OrderBy(x=>x.FileName).ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TrainingActivityMedia, TrainingActivityMediaIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var showMediaDto = mapper.Map<IEnumerable<TrainingActivityMedia>, List<TrainingActivityMediaIndxVM>>(trainingMedia);
                return PartialView("_ShowDocuments", showMediaDto);
            }
        }
        [HttpPost]
        public JsonResult GetModuleSubMenus(int parentMenuId)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var menus = uow.MenuItemMstrRepo.GetParentMenus(parentMenuId);
                return Json(menus, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
