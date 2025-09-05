using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
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
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [CSPLHeaders]
    public class StaffMasterController : Controller
    {
        private ApplicationUserManager _userManager;
        public StaffMasterController()
        { }
        public StaffMasterController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Admin/StaffMaster
        public async Task<ActionResult> Index()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                //var staffs = await uow.StaffMasterRepo.GetAllAsync(np1 => np1.Ranks, np2 => np2.Faculties);
                //var config = new MapperConfiguration(cfg =>
                //{
                //    cfg.CreateMap<IEnumerable<StaffMaster>, List<StaffMasterIndxVM>>();
                //});
                //IMapper mapper = config.CreateMapper();
                //var indexDto = mapper.Map<IEnumerable<StaffMaster>, IEnumerable<StaffMasterIndxVM>>(staffs);
                var indexDto = await uow.StaffMasterRepo.GetStaffUserListAsync();
                return View(indexDto);
            }
        }
        public async Task<ActionResult> Index2()
        {
            string UserName = ((ClaimsIdentity)User.Identity).FindFirst("FName").Value;
            UserActivityHelper.SaveUserActivity("Staff List in admin accessed by  " + UserName, Request.Url.ToString());
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var staffs = await uow.StaffMasterRepo.GetAllAsync(np1=>np1.Ranks, np2=>np2.Faculties);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<StaffMaster>, List<StaffMasterIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<StaffMaster>, IEnumerable<StaffMasterIndxVM>>(staffs);
                return View(indexDto);
            }
        }

        // GET: Admin/StaffMaster/Create
        [HttpGet]
        public ActionResult Create()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Service = CustomDropDownList.GetRankService();
                ViewBag.Appointment = uow.AppointmentDetailRepo.GetAppointments();
                ViewBag.Faculty = uow.FacultyRepo.GetFaculties();
                return View();
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create(StaffMasterCrtVM objStaffMstrCvm, HttpPostedFileBase[] docFiles)
        {
            string path = ServerRootConsts.USER_ROOT;
            string DocPath = path + "documents/";
            string PhotoPath = path + "photos/";

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StaffMasterCrtVM, StaffMaster>();
                cfg.CreateMap<StaffMasterCrtVM, AppUserLoginViewModel>()
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.EmailId))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.EmailId))
                .ForMember(dest => dest.FName, opts => opts.MapFrom(src => src.FullName))
                .ForMember(dest => dest.PhoneNumber, opts => opts.MapFrom(src => src.PhoneNo));
            });
            IMapper mapper = config.CreateMapper();

            if (objStaffMstrCvm.IsLoginUser)
            {
                AppUserLoginViewModel appUser = mapper.Map<StaffMasterCrtVM, AppUserLoginViewModel>(objStaffMstrCvm);
                var user = new ApplicationUser
                {
                    UserName = appUser.Email,
                    FName = appUser.FName,
                    Email = appUser.Email,
                    PhoneNumber = appUser.PhoneNumber
                };
                var result = await UserManager.CreateAsync(user, AppSettingsKeyConsts.DefPassKey);
                if (result.Succeeded)
                {
                    await this.UserManager.AddToRoleAsync(user.Id, "Staff");
                    using (var uow = new UnitOfWork(new NDCWebContext()))
                    {
                        objStaffMstrCvm.iStaffDocument = new List<StaffDocument>();
                        foreach (var file in docFiles)
                        {
                            if (file != null && file.ContentLength > 0)
                            {
                                var fileName = Path.GetFileName(file.FileName);
                                Guid guid = Guid.NewGuid();
                                //Check File
                                CheckBeforeUpload fs = new CheckBeforeUpload();
                                fs.filesize = 3000;
                                string result_ = fs.UploadFile(file);
                                if (string.IsNullOrEmpty(result_))
                                {
                                    StaffDocument objEntrDocs = new StaffDocument()
                                    {
                                        GuId = guid,
                                        FileName = fileName,
                                        Extension = Path.GetExtension(fileName),
                                        FilePath = DocPath + guid + Path.GetExtension(fileName)
                                    };
                                    file.SaveAs(Server.MapPath(objEntrDocs.FilePath));
                                    objStaffMstrCvm.iStaffDocument.Add(objEntrDocs);
                                }
                                else
                                    //this.AddNotification("Invalid file type", NotificationType.WARNING);
                                    this.AddNotification(result_, NotificationType.WARNING);
                            }
                        }
                        StaffMaster CreateDto = mapper.Map<StaffMasterCrtVM, StaffMaster>(objStaffMstrCvm);
                        CreateDto.LoginUserId = user.Id.ToString();
                        uow.StaffMasterRepo.Add(CreateDto);
                        uow.Commit();
                    }
                    return RedirectToAction("Index", "StaffMaster");
                }
                //AddErrors(result);
            }
            else
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    objStaffMstrCvm.iStaffDocument = new List<StaffDocument>();
                    foreach (var file in docFiles)
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
                                StaffDocument objEntrDocs = new StaffDocument()
                                {
                                    GuId = guid,
                                    FileName = fileName,
                                    Extension = Path.GetExtension(fileName),
                                    FilePath = DocPath + guid + Path.GetExtension(fileName)
                                };
                                file.SaveAs(Server.MapPath(objEntrDocs.FilePath));
                                objStaffMstrCvm.iStaffDocument.Add(objEntrDocs);
                            }
                            else
                            {
                                //this.AddNotification("Invalid file type", NotificationType.WARNING);
                                this.AddNotification(result, NotificationType.WARNING);
                            }
                        }
                    }
                    StaffMaster CreateDto = mapper.Map<StaffMasterCrtVM, StaffMaster>(objStaffMstrCvm);
                    uow.StaffMasterRepo.Add(CreateDto);
                    uow.Commit();
                }
                return RedirectToAction("Index", "StaffMaster");
            }
            return RedirectToAction("Create", "StaffMaster");
        }

        // POST: Admin/StaffMaster/Create
        //[HttpPost]
        //public async Task<ActionResult> Create(StaffMasterCrtVM objStaffMstrCvm, HttpPostedFileBase[] docFiles)
        //{
        //    using (var uow = new UnitOfWork(new NDCWebContext()))
        //    {

        //        string path = ServerRootConsts.USER_ROOT;
        //        string DocPath = path + "documents/";
        //        string PhotoPath = path + "photos/";

        //        objStaffMstrCvm.iStaffDocument = new List<StaffDocument>();
        //        foreach (var file in docFiles)
        //        {
        //            if (file != null && file.ContentLength > 0)
        //            {
        //                var fileName = Path.GetFileName(file.FileName);
        //                Guid guid = Guid.NewGuid();
        //                StaffDocument objEntrDocs = new StaffDocument()
        //                {
        //                    GuId = guid,
        //                    FileName = fileName,
        //                    Extension = Path.GetExtension(fileName),
        //                    FilePath = DocPath + guid + Path.GetExtension(fileName)
        //                };
        //                file.SaveAs(Server.MapPath(objEntrDocs.FilePath));
        //                objStaffMstrCvm.iStaffDocument.Add(objEntrDocs);
        //            }
        //        }
        //        //StaffPhoto objStaffPhoto = new StaffPhoto();
        //        //objStaffPhoto = getPhoto(picfile, PhotoPath);

        //        var config = new MapperConfiguration(cfg =>
        //        {
        //            cfg.CreateMap<StaffMasterCrtVM, StaffMaster>();
        //            cfg.CreateMap<StaffMasterCrtVM, AppUserLoginViewModel>();
        //        });
        //        IMapper mapper = config.CreateMapper();
        //        StaffMaster CreateDto = mapper.Map<StaffMasterCrtVM, StaffMaster>(objStaffMstrCvm);
        //        AppUserLoginViewModel appUser = mapper.Map<StaffMasterCrtVM, AppUserLoginViewModel>(objStaffMstrCvm);
        //        //CreateDto.PhotoId = objStaffPhoto.GuId;

        //        //uow.StaffPhotoRepo.Add(objStaffPhoto);
        //        uow.StaffMasterRepo.Add(CreateDto);
        //        await uow.CommitAsync();
        //        return RedirectToAction("Create");
        //    }
        //}

        // GET: Admin/StaffMaster/Edit/5
        public ActionResult Edit(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Service = CustomDropDownList.GetRankService();
                ViewBag.Appointment = uow.AppointmentDetailRepo.GetAppointments();
                ViewBag.Faculty = uow.FacultyRepo.GetFaculties();
                var staffmaster = uow.StaffMasterRepo.Find(x=>x.StaffId == id, np => np.Ranks).FirstOrDefault();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<StaffMaster, StaffMasterUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                StaffMasterUpVM UpdateDto = mapper.Map<StaffMaster, StaffMasterUpVM>(staffmaster);
                UpdateDto.Service = UpdateDto.Ranks.Service;
                ViewData["SelectedRank"] = UpdateDto.RankId;
                return View(UpdateDto);
            }
        }

        // POST: Admin/StaffMaster/Edit/5
        [HttpPost]
        public ActionResult Edit(StaffMasterUpVM objStaffMasterUvm, HttpPostedFileBase[] docFiles)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                string path = ServerRootConsts.USER_ROOT;
                string DocPath = path + "documents/";
                string PhotoPath = path + "photos/";

                var staff = uow.StaffMasterRepo.GetById(objStaffMasterUvm.StaffId);
                staff.StaffId = objStaffMasterUvm.StaffId;
                staff.FullName = objStaffMasterUvm.FullName;
                staff.EmailId = objStaffMasterUvm.EmailId;
                staff.PhoneNo = objStaffMasterUvm.PhoneNo;
                staff.Decoration = objStaffMasterUvm.Decoration;
                staff.DOBirth = objStaffMasterUvm.DOBirth;
                staff.DOMarriage = objStaffMasterUvm.DOMarriage;
                staff.DOAppointment = objStaffMasterUvm.DOAppointment;
                staff.PostingOut = objStaffMasterUvm.PostingOut;
                
                staff.FacultyId = objStaffMasterUvm.FacultyId;
                staff.RankId = objStaffMasterUvm.RankId;
                //staff.IsLoginUser = objStaffMasterUvm.IsLoginUser;
                //staff.LoginUserId = objStaffMasterUvm.LoginUserId;

                if (!string.IsNullOrEmpty(objStaffMasterUvm.SelfImage))
                {
                    staff.SelfImage = objStaffMasterUvm.SelfImage;
                }
                foreach (var file in docFiles)
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
                            StaffDocument objEntrDocs = new StaffDocument()
                            {
                                GuId = guid,
                                FileName = fileName,
                                Extension = Path.GetExtension(fileName),
                                FilePath = DocPath + guid + Path.GetExtension(fileName)
                            };
                            file.SaveAs(Server.MapPath(objEntrDocs.FilePath));
                            staff.iStaffDocument.Add(objEntrDocs);
                        }
                        else
                        {
                            //this.AddNotification("Invalid file type", NotificationType.WARNING);
                            this.AddNotification(result, NotificationType.WARNING);
                        }
                    }
                }
                uow.Commit();
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/StaffMaster/Delete/5
        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var dataModal = await uow.StaffMasterRepo.GetByIdAsync(id);
                if (dataModal == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //bool isUserDelete;
                    if (dataModal.LoginUserId != null)
                    {
                        await AccountDeleteConfirmed(int.Parse(dataModal.LoginUserId));
                    }
                    uow.StaffMasterRepo.Remove(dataModal);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
        [HttpPost]
        public async Task<JsonResult> CreateLoginUser(int staffId)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var dataModal = await uow.StaffMasterRepo.GetByIdAsync(staffId);
                if (dataModal == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<StaffMaster, AppUserLoginViewModel>()
                        .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.EmailId))
                        .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.EmailId))
                        .ForMember(dest => dest.FName, opts => opts.MapFrom(src => src.FullName))
                        .ForMember(dest => dest.PhoneNumber, opts => opts.MapFrom(src => src.PhoneNo));
                    });
                    IMapper mapper = config.CreateMapper();
                    AppUserLoginViewModel appUser = mapper.Map<StaffMaster, AppUserLoginViewModel>(dataModal);
                    var user = new ApplicationUser
                    {
                        UserName = appUser.Email,
                        FName = appUser.FName,
                        Email = appUser.Email,
                        PhoneNumber = appUser.PhoneNumber
                    };
                    var result = await UserManager.CreateAsync(user, AppSettingsKeyConsts.DefPassKey);
                    if (result.Succeeded)
                    {
                        await this.UserManager.AddToRoleAsync(user.Id, "Staff");
                        dataModal.LoginUserId = user.Id.ToString();
                        await uow.CommitAsync();
                    }
                    return Json(data: "Login User Created", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
        [HttpPost]
        public async Task<JsonResult> ResetLoginUser(int staffId)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var staff = await uow.StaffMasterRepo.GetByIdAsync(staffId);
                if (staff == null)
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                else
                {
                    var user = await UserManager.FindByIdAsync(int.Parse(staff.LoginUserId));
                    user.UserName = staff.EmailId;
                    user.FName = staff.FullName;
                    user.Email = staff.EmailId;
                    user.PhoneNumber = staff.PhoneNo;
                    await UserManager.UpdateAsync(user);

                    var token = await UserManager.GeneratePasswordResetTokenAsync(int.Parse(staff.LoginUserId));
                    var pwdResult = await UserManager.ResetPasswordAsync(int.Parse(staff.LoginUserId), token, AppSettingsKeyConsts.DefPassKey);
                    return Json(data: "Login Detail Updated", behavior: JsonRequestBehavior.AllowGet);
                }

                //int result = uow.StaffMasterRepo.DMLUpdateStaffAndAccount(staffId);
                //if (result==1)
                //{
                //    return Json(data: "Login Detail Updated", behavior: JsonRequestBehavior.AllowGet);
                //}
                //else if (result == 2)
                //{
                //    return Json(data: "Email already Register", behavior: JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    return Json(data: "Login Detail Opration Failed", behavior: JsonRequestBehavior.AllowGet);
                //}


                //var dataModal = await uow.StaffMasterRepo.GetByIdAsync(staffId);
                //if (dataModal == null)
                //{
                //    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    var config = new MapperConfiguration(cfg =>
                //    {
                //        cfg.CreateMap<StaffMaster, ResetLoginUserVM>();
                //    });
                //    IMapper mapper = config.CreateMapper();
                //    ResetLoginUserVM appUser = mapper.Map<StaffMaster, ResetLoginUserVM>(dataModal);

                //    //var config = new MapperConfiguration(cfg =>
                //    //{
                //    //    cfg.CreateMap<StaffMaster, AppUserLoginViewModel>()
                //    //    .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.EmailId))
                //    //    .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.EmailId))
                //    //    .ForMember(dest => dest.FName, opts => opts.MapFrom(src => src.FullName))
                //    //    .ForMember(dest => dest.PhoneNumber, opts => opts.MapFrom(src => src.PhoneNo));
                //    //});
                //    //IMapper mapper = config.CreateMapper();
                //    //AppUserLoginViewModel appUser = mapper.Map<StaffMaster, AppUserLoginViewModel>(dataModal);
                //    //var user = new ApplicationUser
                //    //{
                //    //    UserName = appUser.Email,
                //    //    FName = appUser.FName,
                //    //    Email = appUser.Email,
                //    //    PhoneNumber = appUser.PhoneNumber
                //    //};
                //    bool result = await AccountUpdateConfirmed(appUser);
                //    if (result == true)
                //        return Json(data: "Login Detail Updated", behavior: JsonRequestBehavior.AllowGet);
                //    else
                //        return Json(data: "Login Detail Opration Failed", behavior: JsonRequestBehavior.AllowGet);
                //}
            }
        }
        public async Task<bool> AccountDeleteConfirmed(int id)
        {
            var user = await UserManager.FindByIdAsync(id);
            var logins = user.Logins;
            var rolesForUser = await UserManager.GetRolesAsync(id);

            using (var context = new ApplicationDbContext())
            {
                foreach (var login in logins.ToList())
                {
                    await UserManager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                }

                if (rolesForUser.Count() > 0)
                {
                    foreach (var item in rolesForUser.ToList())
                    {
                        // item should be the name of the role
                        var result = await UserManager.RemoveFromRoleAsync(user.Id, item);
                    }
                }
                await UserManager.DeleteAsync(user);
                int returnResult = context.SaveChanges();
                if (returnResult > 0)
                    return true;
                else
                    return false;
            }
        }
        public async Task<bool> AccountUpdateConfirmed(ResetLoginUserVM staffMaster)
        {
            using (var context = new ApplicationDbContext())
            {
                var user = await UserManager.FindByIdAsync(int.Parse(staffMaster.LoginUserId));
                //user.UserName = staffMaster.EmailId;
                user.FName = staffMaster.FullName;
                //user.Email = staffMaster.EmailId;
                //user.PhoneNumber = staffMaster.PhoneNo;

                UserManager.Update(user);
                int returnResult = context.SaveChanges();
                if (returnResult > 0)
                    return true;
                else
                    return false;
            }
        }
        [HttpPost]
        public JsonResult GetRanks(string serviceType)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var ranks = uow.RankMasterRepo.GetRanks(serviceType);
                return Json(ranks, JsonRequestBehavior.AllowGet);
            }
        }

        private StaffPhoto getPhoto(HttpPostedFileBase picfile, string path)
        {
            var fileName = Path.GetFileName(picfile.FileName);
            Guid guid = Guid.NewGuid();
            StaffPhoto objStaffPhoto = new StaffPhoto()
            {
                GuId = guid,
                FileName = fileName,
                Extension = Path.GetExtension(fileName),
                FilePath = path + guid + Path.GetExtension(fileName)
            };
            picfile.SaveAs(Server.MapPath(objStaffPhoto.FilePath));
            return objStaffPhoto;
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
                    //return Json(new { message = "File Could not be Uploaded!", status = 0 });
                    return Json(new { message = result, status = 0 });
            }
            else
                return Json(new { message = "No files selected.", status = 0, filePath = "#" });
        }
        #endregion
    }
}
