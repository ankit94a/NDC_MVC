using AutoMapper;
using CaptchaMvc.HtmlHelpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Admin.View_Models;
using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Infrastructure.Helpers.Account;
using NDCWeb.Infrastructure.Helpers.FileDirectory;
using NDCWeb.Infrastructure.Helpers.FileExt;
using NDCWeb.Infrastructure.Helpers.Mail;
using NDCWeb.Models;
using NDCWeb.Persistence;
using NDCWeb.View_Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Alumni.Controllers
{
    [Authorize(Roles = CustomRoles.Alumni)]
    [UserMenu(MenuArea = "Alumni")]
    [CSPLHeaders]
    [SessionTimeout]
    public class HomeController : Controller
    {
        // GET: Alumni/Home
       public ActionResult Index()
        {
            ViewBag.Flash = GetFlash();
            string uId = User.Identity.GetUserId();
            ViewBag.fullname = ((ClaimsIdentity)User.Identity).FindFirst("FName").Value;

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var courseid = uow.AlumniRepo.FirstOrDefault(x => x.UserId == uId);
                ViewBag.CourseId = courseid.CourseSerNo;
                ViewBag.TrainingCalendar = uow.MediaFileRepo.FirstOrDefault(x => x.FileName.Contains("trg_caledar")).FilePath;
                ViewBag.WeeklyTrainingProgramme = uow.MediaFileRepo.FirstOrDefault(x => x.FileName.Contains("weekly_trg_programme")).FilePath;

                if (courseid.InStepCourseId == null)
                {
                    ViewBag.alumniCat = "NDCCourse";
                    var memberPersonal = uow.AlumniRepo.Find(x => x.UserId == uId).FirstOrDefault();
                    if (memberPersonal != null)
                    {
                        ViewBag.ProfilePic = "/writereaddata/alumni/photos/" + memberPersonal.AluminiId.ToString() + ".jpg";
                    }
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<IEnumerable<Models.AlumniMaster>, List<AlumniIndxVM>>();
                    });
                    IMapper mapper = config.CreateMapper();
                    var indexDto = mapper.Map<Models.AlumniMaster, AlumniIndxVM>(memberPersonal);
                    return View(indexDto);
                }
                else
                {
                    ViewBag.alumniCat = "InstepCourse";
                    var memberPersonal = uow.AlumniRepo.Find(x => x.UserId == uId && x.InStepCourseId != null).FirstOrDefault();
                    if (memberPersonal != null)
                    {
                        ViewBag.ProfilePic = "/writereaddata/alumni/photos/" + memberPersonal.AluminiId.ToString() + ".jpg";
                    }
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<IEnumerable<Models.AlumniMaster>, List<AlumniIndxVM>>();
                    });
                    IMapper mapper = config.CreateMapper();
                    var indexDto = mapper.Map<Models.AlumniMaster, AlumniIndxVM>(memberPersonal);
                    return View(indexDto);
                }
            }            
        }
        private List<NewsBulletinVM> GetFlash()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var newsArticle = uow.NewsArticleRepo.GetAll().Where(x => x.NewsCategory == NewsCategory.Flash && x.Archive == false && x.DisplayArea == NewsDisplayArea.Alumni).OrderByDescending(x => x.NewsArticleId);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<NewsArticle>, List<NewsBulletinVM>>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<IEnumerable<NewsArticle>, IEnumerable<NewsBulletinVM>>(newsArticle).ToList();
            }
        }
        public async Task<ActionResult> Article(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var newsArticle = await uow.NewsArticleRepo.GetByIdAsync(id);
                if (newsArticle == null)
                    return HttpNotFound();
                else
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<NewsArticle, NewsArticleIndxVM>();
                    });
                    IMapper mapper = config.CreateMapper();
                    var indexDto = mapper.Map<NewsArticle, NewsArticleIndxVM>(newsArticle);
                    await uow.CommitAsync();

                    return View(indexDto);
                }
            }
        }
        public ActionResult CircularAlert()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                //var circular = uow.CircularRepo.GetAlertCircularList("Circular");
                string uId = User.Identity.GetUserId();
                var circular = uow.CircularDetailRepo.GetOrderAsPerDesignation("Alumni", uId);
                return View(circular);
            }
        }
        public ActionResult SocialEvents()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var eventDetail = uow.EventRepo.GetAll();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Event, EventIndexVM>();
                });
                IMapper mapper = config.CreateMapper();
                var evenTs = mapper.Map<IEnumerable<Event>, List<EventIndexVM>>(eventDetail);
                return PartialView("_Events", evenTs);
            }
        }
        public ActionResult AlumniFeedback()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                string uId = User.Identity.GetUserId();
                var memberPersonal = uow.AlumniRepo.Find(x => x.UserId == uId).FirstOrDefault();
                AlumniFeedbackCrtVM objVM = new AlumniFeedbackCrtVM();
                //objVM.FullName = memberPersonal.ServiceRank + " " + memberPersonal.FirstName + " " + memberPersonal.Surname;
                //objVM.EmailId = memberPersonal.Email;
                ViewBag.Department = CustomDropDownList.GetDepartments();
                return View(objVM);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AlumniFeedback(AlumniFeedbackCrtVM ojbFeedback)
        {
            //if (!this.IsCaptchaValid("Validate your captcha"))
            //{
            //    ViewBag.CaptchaErrorMessage = "Invalid Captcha";
            //    ModelState.AddModelError("", "Invalid verification code. Please try again.");
            //    return View(ojbFeedback);
            //}
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AlumniFeedbackCrtVM, AlumniFeedback>();
                });
                IMapper mapper = config.CreateMapper();
                AlumniFeedback CreateDto = mapper.Map<AlumniFeedbackCrtVM, AlumniFeedback>(ojbFeedback);
                uow.AlumniFeedbackRepo.Add(CreateDto);
                await uow.CommitAsync();
                this.AddNotification("Your feedback is successfully submitted", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }
        public ActionResult Memento()
        {
            return View();
        }
        public ActionResult Faq()
        {
            return View();
        }
        public ActionResult AlumniNews()
        {
            return View();
        }
        #region Submit Articles
        public ActionResult SubmitArticle()
        {
            ForumBlogCrtVM objForumBlogvmNew = new ForumBlogCrtVM();
            objForumBlogvmNew.Category = "Alumni";
            return View(objForumBlogvmNew);
        }
        [HttpPost]
        public async Task<ActionResult> SubmitArticle(ForumBlogCrtVM objForumBlogvm, HttpPostedFileBase[] Files)
        {
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
                    fs.filesize = 3000;
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
                uow.ForumBlogRepo.Add(CreateDto);
                await uow.CommitAsync();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }
        public ActionResult SubmitThinkTank()
        {
            ForumBlogCrtVM objForumBlogvmNew = new ForumBlogCrtVM();
            objForumBlogvmNew.Category = "Think Tank";
            return View(objForumBlogvmNew);
        }
        [HttpPost]
        public async Task<ActionResult> SubmitThinkTank(ForumBlogCrtVM objForumBlogvm, HttpPostedFileBase[] Files)
        {
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
                    fs.filesize = 3000;
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
                return PartialView("_ShowDocuments", showMediaDto);
                //return PartialView("_ShowBlogMediaFiles", showMediaDto);
            }
        }
        #endregion
        #region Socials
        public ActionResult Birthdays()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var getallBdays = uow.SocialCalendarRepo.GetBirthdayList();
                return PartialView("_Birthdays", getallBdays);

                //return View(getallBdays);
            }
        }
        #endregion
        #region ChangePassword
        private ApplicationUserManager _userManager;
        public HomeController()
        { }
        public HomeController(ApplicationUserManager userManager)
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
                return RedirectToAction("Login", "Auth", new { area = "" });
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
            //string userName = User.Identity.GetUserName();
            ////string userId = User.Identity.GetUserId();
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}
            //var user = await UserManager.FindByNameAsync(userName);
            //if (user == null)
            //{
            //    // Don't reveal that the user does not exist
            //    return RedirectToAction("Login", "Account", new { area = "Admin" });
            //}
            //var token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            ////var result = await UserManager.ResetPasswordAsync(user.Id, token, model.NewPassword);
            ////var result = await UserManager.ChangePasswordAsync(user.Id, model.CurrentPassword, model.NewPassword);
            ////var result = await UserManager.ChangePasswordAsync(user.Id, AESEncrytDecry.DecryptStringAES(model.CurrentPassword).Replace(secConst.cSalt, ""), AESEncrytDecry.DecryptStringAES(model.NewPassword).Replace(secConst.cSalt, ""));
            //var result = await UserManager.ChangePasswordAsync(user.Id, AESEncrytDecry.DecryptStringAES(model.CurrentPassword), AESEncrytDecry.DecryptStringAES(model.NewPassword));
            //if (result.Succeeded)
            //{
            //    string[] myCookies = Request.Cookies.AllKeys;
            //    //var user = await UserManager.FindByNameAsync(User.Identity.Name);
            //    try
            //    {
            //        if (user != null)
            //        {
            //            UserActivityHelper.SaveUserActivity("Change Password by user  " + User.Identity.Name, Request.Url.ToString());
            //            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //            await UserManager.UpdateSecurityStampAsync(user.Id);
            //            Session.Abandon();
            //        }

            //        foreach (string cookie in myCookies)
            //        {
            //            Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //        throw;
            //    }
            //    foreach (string cookie in myCookies)
            //    {
            //        Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            //    }
            //    Session.Abandon();
            //    return RedirectToAction("ChangeUserPasswordConfirmation","Home", new { Area = "" });
            //}
            //AddErrors(result);
            //return View();
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);

            }
        }
        #endregion
    }
}