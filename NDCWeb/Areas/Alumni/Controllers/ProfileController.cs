using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Models;
using NDCWeb.Persistence;
using NDCWeb.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using NDCWeb.Infrastructure.Extensions;
using System.Threading.Tasks;
using NDCWeb.Infrastructure.Helpers.FileDirectory;
using System.IO;
using NDCWeb.Infrastructure.Helpers.FileExt;
using NDCWeb.Areas.Admin.View_Models;

namespace NDCWeb.Areas.Alumni.Controllers
{
    [Authorize(Roles = CustomRoles.Alumni)]
    [UserMenu(MenuArea = "Alumni")]
    [CSPLHeaders]
    public class ProfileController : Controller
    {
        // GET: Alumni/Profile
        public ActionResult Index()
        {
            string uId = User.Identity.GetUserId();
            ViewBag.fullname = ((ClaimsIdentity)User.Identity).FindFirst("FName").Value;
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
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

                //return View(memberPersonal);
            }
        }
        [HttpGet]
        [EncryptedActionParameter]
        public ActionResult AlumniProfile(int alumniid)
        {
            ViewBag.Service = CustomDropDownList.GetRankService();
            ViewBag.AlumniId = alumniid;
            string uId = User.Identity.GetUserId();

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var memberPersonal = uow.AlumniRepo.Find(x => x.UserId == uId).FirstOrDefault();
                if (memberPersonal != null)
                {
                    var alumnidata = uow.AlumniRepo.GetById(alumniid);
                    if(alumnidata!=null)
                    {
                        if (alumnidata.UserId == memberPersonal.UserId)
                        {
                            AlumniUpVM objVM = new AlumniUpVM();
                            var config = new MapperConfiguration(cfg =>
                            {
                                cfg.CreateMap<AlumniMaster, AlumniUpVM>();
                            });
                            IMapper mapper = config.CreateMapper();
                            AlumniUpVM CreateDto = mapper.Map<AlumniMaster, AlumniUpVM>(alumnidata);
                            return View(CreateDto);
                        }
                        else
                        {
                            this.AddNotification("Not Authorize.", NotificationType.ERROR);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        this.AddNotification("Error!", NotificationType.ERROR);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    this.AddNotification("Not Alummni Member.", NotificationType.ERROR);
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> AlumniProfile(AlumniUpVM ojbAlumnidata)
        {
            string uId = User.Identity.GetUserId();

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var memberPersonal = uow.AlumniRepo.Find(x => x.UserId == uId).FirstOrDefault();
                if (memberPersonal != null)
                {
                    AlumniMaster UpdateDto = uow.AlumniRepo.GetById(ojbAlumnidata.AluminiId);
                    if (UpdateDto != null)
                    {
                        if (UpdateDto.UserId == memberPersonal.UserId)
                        {
                            if (ModelState.IsValid)
                            {
                                //var config = new MapperConfiguration(cfg =>
                                //{
                                //    cfg.CreateMap<AlumniUpVM, AlumniMaster>();
                                //});
                                //IMapper mapper = config.CreateMapper();
                                //AlumniMaster UpdateDto = mapper.Map<AlumniUpVM, AlumniMaster>(ojbAlumnidata);
                                UpdateDto.FirstName = ojbAlumnidata.FirstName;
                                UpdateDto.Surname = ojbAlumnidata.Surname;
                                UpdateDto.ServiceId = ojbAlumnidata.ServiceId;
                                UpdateDto.ServiceRank = ojbAlumnidata.ServiceRank;
                                UpdateDto.CourseYear = ojbAlumnidata.CourseYear;
                                UpdateDto.CourseSerNo = ojbAlumnidata.CourseSerNo;
                                UpdateDto.NdcEqvCourse = ojbAlumnidata.NdcEqvCourse;
                                UpdateDto.YearDone = ojbAlumnidata.YearDone;
                                UpdateDto.ServiceRetd = ojbAlumnidata.ServiceRetd;
                                UpdateDto.Email = ojbAlumnidata.Email;
                                UpdateDto.MobileNo = ojbAlumnidata.MobileNo;
                                UpdateDto.Branch = ojbAlumnidata.Branch;
                                UpdateDto.PermanentAddress = ojbAlumnidata.PermanentAddress;
                                UpdateDto.NdcCommunicationAddress = ojbAlumnidata.NdcCommunicationAddress;
                                UpdateDto.AlumniPhoto = ojbAlumnidata.AlumniImgPath;
                                uow.AlumniRepo.Update(UpdateDto);
                                await uow.CommitAsync();
                                this.AddNotification("Your profile has been successfully updates !", NotificationType.SUCCESS);
                                //return View(ojbAlumnidata);
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                return View(ojbAlumnidata);
                            }
                        }
                        else
                        {
                            this.AddNotification("Not Authorize.", NotificationType.ERROR);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        this.AddNotification("Error!", NotificationType.ERROR);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    this.AddNotification("Not Alummni Member.", NotificationType.ERROR);
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        [HttpPost]
        public JsonResult ImageUpload()
        {
            string id;
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var personal = uow.AlumniRepo.FirstOrDefault(x => x.UserId == uId);
                id = personal.AluminiId.ToString();
              
            }

                string ROOT_PATH = ServerRootConsts.ALUMNI_ROOT;
            string PHOTO_PATH = ROOT_PATH + "photos/";
            //string CURRENT_YEAR = DateTime.Now.Year.ToString();
            //string CURRENT_MONTH = DateTime.Now.ToString("MMM");

            string file_PATH = PHOTO_PATH + "/";
            if (Request.Files.Count > 0)
            {
                DirectoryHelper.CreateFolder(Server.MapPath(file_PATH));
                HttpPostedFileBase file = Request.Files[0];
                //Guid guid = Guid.NewGuid();
                string newFileName = id.ToString() + Path.GetExtension(file.FileName);
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
        #region Password
        private ApplicationUserManager _userManager;
        //public CourseMemberController()
        //{ }
        //public CourseMemberController(ApplicationUserManager userManager)
        //{
        //    UserManager = userManager;
        //}
        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}

        public ActionResult ChangeUserPassword()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ChangeUserPassword(ChangeUserPasswordViewModel model)
        //{
        //    string userName = User.Identity.GetUserName();
        //    //string userId = User.Identity.GetUserId();
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var user = await UserManager.FindByNameAsync(userName);
        //    if (user == null)
        //    {
        //        // Don't reveal that the user does not exist
        //        return RedirectToAction("Login", "Account", new { area = "Admin" });
        //    }
        //    var token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
        //    var result = await UserManager.ResetPasswordAsync(user.Id, token, model.NewPassword);

        //    //var result = await UserManager.ChangePasswordAsync(user.Id, model.CurrentPassword, model.NewPassword);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("ChangeUserPasswordConfirmation");
        //    }
        //    AddErrors(result);
        //    return View();
        //}
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
    }
}