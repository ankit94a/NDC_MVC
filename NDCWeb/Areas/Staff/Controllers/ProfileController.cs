using AngleSharp.Io;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
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
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.PeerToPeer;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;


namespace NDCWeb.Areas.Staff.Controllers
{
    [CSPLHeaders]
    [Authorize(Roles = CustomRoles.Staff)]
    [StaffStaticUserMenu]
    //[UserMenu(MenuArea = "Staff")]
    [EncryptedActionParameter]
    public class ProfileController : Controller
    {
        private ApplicationUserManager _userManager;
        public ProfileController()
        { }
        public ProfileController(ApplicationUserManager userManager)
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
        // GET: Staff/Profile
        public ActionResult Index()
        {
            string uId = User.Identity.GetUserId();
            //ViewBag.fullname = ((ClaimsIdentity)User.Identity).FindFirst("FName").Value;
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var memberPersonal = uow.StaffMasterRepo.Find(x => x.LoginUserId == uId, fk => fk.Faculties).FirstOrDefault();
                if (memberPersonal != null)
                {
                    ViewBag.fullname = memberPersonal.FullName;
                    ViewBag.ProfilePic = memberPersonal.SelfImage;
                    ViewBag.Appointment = memberPersonal.Faculties.FacultyName;
                    string staffType = memberPersonal.Faculties.StaffType;
                    if (staffType.IsExistsInList("AQ", "SDS", "Secretary", "Comdt"))
                    {
                        ViewBag.ShowLeave = true;
                    }
                }
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<StaffMaster>, List<StaffMasterIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<StaffMaster, StaffMasterIndxVM>(memberPersonal);
                return View(indexDto);

                //return View(memberPersonal);
            }
        }

        public ActionResult Edit(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Service = CustomDropDownList.GetRankService();
                ViewBag.Appointment = uow.AppointmentDetailRepo.GetAppointments();
                ViewBag.Faculty = uow.FacultyRepo.GetFaculties();
                var staffmaster = uow.StaffMasterRepo.Find(x => x.StaffId == id, np => np.Ranks).FirstOrDefault();
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
                staff.EmailId = staff.EmailId;
                staff.PhoneNo = objStaffMasterUvm.PhoneNo;
                staff.Decoration = objStaffMasterUvm.Decoration;
                staff.DOBirth = objStaffMasterUvm.DOBirth;
                staff.DOMarriage = objStaffMasterUvm.DOMarriage;
                //staff.DOAppointment = objStaffMasterUvm.DOAppointment;

                //staff.FacultyId = objStaffMasterUvm.FacultyId;
                staff.RankId = objStaffMasterUvm.RankId;

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

        #region Change Password
        private static Random RNG = new Random();
        public string GetSalt()
        {
            var builder = new StringBuilder();
            while (builder.Length < 16)
            {
                builder.Append(RNG.Next(10).ToString());
            }
            return builder.ToString();
        }
        public ActionResult ChangeUserPassword()
        {
            secConst.cSalt = GetSalt();
            ChangeUserPasswordViewModel msm = new ChangeUserPasswordViewModel();
            msm.hdns = secConst.cSalt.ToString();
            return View(msm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeUserPassword(ChangeUserPasswordViewModel model)
        {
            string userName = User.Identity.GetUserName();
            bool pwdOK = false;
            string Msg = "";
            //string userId = User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(userName);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("Login", "Account", new { area = "Admin" });
            }
            var token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var _pwdhistory = await uow.UserPwdMangerRepo.Validatepwdhistory(userName, AESEncrytDecry.DecryptStringAES(model.NewPassword).Replace(secConst.cSalt, ""));
                if (_pwdhistory)
                {
                    pwdOK = false; //"fail";
                    Msg = "You have already used this password in last 3 transaction. Please use different password.";
                }
                else
                {
                    pwdOK = true;
                    var userPwdMgr = new UserPwdManger
                    {
                        Username = userName,
                        Password = AESEncrytDecry.DecryptStringAES(model.NewPassword).Replace(secConst.cSalt, ""),
                        ModifyDate = DateTime.Now,
                    };
                    uow.UserPwdMangerRepo.Add(userPwdMgr);
                    uow.Commit();
                }
            }
            if (pwdOK)
            {
                var result = await UserManager.ChangePasswordAsync(user.Id, AESEncrytDecry.DecryptStringAES(model.CurrentPassword).Replace(secConst.cSalt, ""), AESEncrytDecry.DecryptStringAES(model.NewPassword).Replace(secConst.cSalt, ""));
                if (result.Succeeded)
                {
                    string[] myCookies = Request.Cookies.AllKeys;
                    //var user = await UserManager.FindByNameAsync(User.Identity.Name);
                    try
                    {
                        if (user != null)
                        {
                            UserActivityHelper.SaveUserActivity("Change Password by user  " + User.Identity.Name, Request.Url.ToString());
                            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                            await UserManager.UpdateSecurityStampAsync(user.Id);
                            Session.Abandon();
                        }

                        foreach (string cookie in myCookies)
                        {
                            Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
                        }
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                    foreach (string cookie in myCookies)
                    {
                        Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
                    }
                    Session.Abandon();
                    return RedirectToAction("ChangeUserPasswordConfirmation", "Home", new { Area = "" });
                    //return RedirectToAction("ChangeUserPasswordConfirmation");
                }
                AddErrors(result);
                return View();
            }
            else
            {
                this.AddNotification(Msg, NotificationType.WARNING);
                return View();
            }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        public ActionResult ChangeUserPasswordConfirmation()
        {
            return View();
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        #endregion

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