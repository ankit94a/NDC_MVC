using NDCWeb.Areas.Member.View_Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using NDCWeb.Persistence;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using AutoMapper;
using NDCWeb.Infrastructure.Constants;
using System.Collections.Generic;
using System.Linq;
using NDCWeb.Infrastructure.Helpers.FileDirectory;
using System;
using System.Web;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using Microsoft.AspNet.Identity.Owin;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.View_Models;
using NDCWeb.Infrastructure.Extensions;
using System.Security.Claims;
using NDCWeb.Infrastructure.Helpers.Account;
using NDCWeb.Areas.Staff.View_Models;
using System.Web.Services.Description;
using NDCWeb.Areas.Admin.View_Models;
using NDCWeb.Infrastructure.Helpers.Email;

namespace NDCWeb.Areas.Staff.Controllers
{
    [CSPLHeaders]
    [Authorize(Roles = CustomRoles.Staff)]
    [StaffStaticUserMenu]
    //[UserMenu(MenuArea = "Staff")]
    [EncryptedActionParameter]
    public class MemberVerifyController : Controller
    {
        #region forAuth
        private ApplicationUserManager _userManager;
        public MemberVerifyController()
        { }
        public MemberVerifyController(ApplicationUserManager userManager)
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
        #endregion
        // GET: Staff/MemberVerify
        public ActionResult Participants()
        {
            string UserName = ((ClaimsIdentity)User.Identity).FindFirst("FName").Value;
            UserActivityHelper.SaveUserActivity("Verify Participant List accessed by  " + UserName, Request.Url.ToString());
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                //x => x.IsCurrent == false && x.UnderRegistration == true

                var course = uow.CourseRepo.Find(x => x.UnderRegistration == true || x.IsCurrent == true ).OrderByDescending(x => x.CourseId).FirstOrDefault();
                var courseRegisters = uow.CourseRegisterRepo.Find(x=>x.CourseId==course.CourseId, np => np.Ranks).OrderByDescending(x => x.CourseRegisterId).ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CourseRegister, CourseRegisterIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<CourseRegister>, List<CourseRegisterIndxVM>>(courseRegisters).ToList();
                return View(indexDto);
            }
        }
        
        public ActionResult ParticipantsPending()
        {
            string uId = User.Identity.GetUserId();
            string UserName = ((ClaimsIdentity)User.Identity).FindFirst("FName").Value;
            UserActivityHelper.SaveUserActivity("Participant Pending verification List accessed by  " + UserName, Request.Url.ToString());
            using (NDCWebContext db = new NDCWebContext())
            {
                List<CourseRegister> crs = db.CourseRegisters.ToList();
                List<RankMaster> rms = db.RankMasters.ToList();
                var verificationrecord = (from a in crs
                                          join b in rms on a.RankId equals b.RankId into table1
                                          from b in table1.ToList() where a.Approved == false
                                          orderby a.CourseRegisterId descending
                                          select new CourseRegisterAlertVM()
                                          {
                                              CourseRegisterId = a.CourseRegisterId,
                                              EmailId = a.EmailId,
                                              Ranks = b,
                                              RankId = b.RankId,
                                              FirstName = a.FirstName,
                                              MiddleName = a.MiddleName,
                                              LastName = a.LastName,
                                              MobileNo = a.MobileNo,
                                              CreateOn = a.CreateOn,
                                              Approved = a.Approved,
                                              UserId = a.UserId,
                                              memberRegId = a.CourseRegisterId,
                                          }).ToList();
                //var config = new MapperConfiguration(cfg =>
                //{
                //    cfg.CreateMap<CourseRegister, CourseRegisterAlertVM>();
                //});
                //IMapper mapper = config.CreateMapper();
                //var vr = mapper.Map<IEnumerable<CourseRegister>, List<CourseRegisterAlertVM>>(verificationrecord).ToList();
                return View(verificationrecord);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //R on productions
        public async Task<JsonResult> ResetLoginUser(int memberRegId)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var regCrsMember = await uow.CourseRegisterRepo.GetByIdAsync(memberRegId);
                if (regCrsMember == null || regCrsMember.UserId == null)
                    return Json(data: "Not Found", behavior: JsonRequestBehavior.AllowGet);
                else
                {
                    var token = await UserManager.GeneratePasswordResetTokenAsync(int.Parse(regCrsMember.UserId));
                    var pwdResult = await UserManager.ResetPasswordAsync(int.Parse(regCrsMember.UserId), token, AppSettingsKeyConsts.DefPassKey);
                    return Json(data: "Login Detail Updated", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult AlumniList()
        {
            string uId = User.Identity.GetUserId();
            string UserName = ((ClaimsIdentity)User.Identity).FindFirst("FName").Value;
            UserActivityHelper.SaveUserActivity("Alumni  List accessed by  " + UserName, Request.Url.ToString());
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                //var personalDetail = uow.AlumniRepo.GetAll().OrderBy(x => x.Verified);
                var personalDetail = uow.AlumniRepo.Find(x => x.InStepCourseId == null).OrderBy(x => x.Verified);
                if (uId.ToString() == "1026")
                {
                    personalDetail = uow.AlumniRepo.Find(x => x.Verified == true && x.InStepCourseId == null).OrderByDescending(x => x.AluminiId);
                }                
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AlumniMaster, AlumniIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var participants = mapper.Map<IEnumerable<Models.AlumniMaster>, List<AlumniIndxVM>>(personalDetail);
                return View(participants.OrderBy(x => x.Verified));
            }
        }
        public async Task<ActionResult> InStepCourse()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var mediaCtgry = await uow.InStepCourseRepo.GetAllAsync();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<InStepCourse>, List<InStepCourseIndexVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<InStepCourse>, IEnumerable<InStepCourseIndexVM>>(mediaCtgry);
                return View(indexDto);
            }

        }
        [EncryptedActionParameter]
        public ActionResult InstepAlumniList( int id)
        {
            string uId = User.Identity.GetUserId();
            string UserName = ((ClaimsIdentity)User.Identity).FindFirst("FName").Value;
            UserActivityHelper.SaveUserActivity("Alumni  List accessed by  " + UserName, Request.Url.ToString());
            using (NDCWebContext db = new NDCWebContext())
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    //var personalDetail = uow.AlumniRepo.GetAll().OrderBy(x => x.Verified);
                    //var personalDetail = uow.AlumniRepo.Find(x => x.InStepCourseId != null).OrderBy(x => x.Verified);
                    List<InStepCourse> courseMasters = db.InStepCourses.ToList();
                    List<AlumniMaster> alumniMaster = db.Alumnis.ToList();

                    var personalDetail = (from am in alumniMaster
                                          join cm in courseMasters on am.InStepCourseId equals cm.CourseId
                                          where cm.CourseId == id
                                          orderby am.AluminiId descending
                                          select new AlumniIndxVM()
                                          {
                                              AluminiId = am.AluminiId,
                                              CourseSerNo = cm.CourseName,
                                              Email = am.Email,
                                              ServiceId=am.ServiceId,
                                              ServiceRank=am.ServiceRank,
                                              FirstName=am.FirstName,
                                              Surname= am.Surname   ,
                                              MobileNo= am.MobileNo,
                                              RegDate= am.RegDate,
                                              Verified=am.Verified,
                                              VerifyDate = am.VerifyDate,

                                          }).ToList();
                    return View(personalDetail);
                    //if (uId.ToString() == "1026")
                    //{
                    //    personalDetail = uow.AlumniRepo.Find(x => x.Verified == true).OrderByDescending(x => x.AluminiId);
                    //}
                    //var config = new MapperConfiguration(cfg =>
                    //{
                    //    cfg.CreateMap<AlumniMaster, AlumniIndxVM>();
                    //});
                    //IMapper mapper = config.CreateMapper();
                    //var participants = mapper.Map<IEnumerable<Models.AlumniMaster>, List<AlumniIndxVM>>(personalDetail);
                    //return View(participants.OrderBy(x => x.Verified));
                }
            }
        }

        public ActionResult AlumniDetail(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                //var personalDetail = uow.AlumniRepo.Find(x => x.CreatedBy != null);
                var alumni = uow.AlumniRepo.GetById(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AlumniMaster, AlumniDetailVM>();
                });
                IMapper mapper = config.CreateMapper();
                var DetailDto = mapper.Map<AlumniMaster, AlumniDetailVM>(alumni);
                return View(DetailDto);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //R on productions
        public async Task<JsonResult> AlumniLoginReset(int alumniRegId)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var regAlumniMember = await uow.AlumniRepo.GetByIdAsync(alumniRegId);
                if (regAlumniMember == null || regAlumniMember.UserId == null)
                    return Json(data: "Not Found", behavior: JsonRequestBehavior.AllowGet);
                else
                {
                    var token = await UserManager.GeneratePasswordResetTokenAsync(int.Parse(regAlumniMember.UserId));
                    var pwdResult = await UserManager.ResetPasswordAsync(int.Parse(regAlumniMember.UserId), token, AppSettingsKeyConsts.DefPassKey);
                    return Json(data: "Login Detail Updated", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
        [ValidateAntiForgeryToken] //R on productions
        public async Task<JsonResult> VerifyAlumni(int regId, VerifyMemberVM member)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = member.UserName,
                    FName = member.FName,
                    Email = member.Email,
                    PhoneNumber = member.PhoneNumber
                };
                var result = await UserManager.CreateAsync(user, AppSettingsKeyConsts.DefPassKey);
                if (result.Succeeded)
                {
                    await this.UserManager.AddToRoleAsync(user.Id, "Alumni");
                    using (var uow = new UnitOfWork(new NDCWebContext()))
                    {
                        var alumniReg = await uow.AlumniRepo.GetByIdAsync(regId);
                        if (alumniReg == null)
                            return Json(data: "Not Found", behavior: JsonRequestBehavior.AllowGet);

                        alumniReg.UserId = user.Id.ToString();
                        alumniReg.VerifyDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                        alumniReg.Verified = true;
                        await uow.CommitAsync();
                    }
                    return Json(data: "Alumni verified successfully!", behavior: JsonRequestBehavior.AllowGet);
                }
                return Json(data: "Bad Request", behavior: JsonRequestBehavior.AllowGet);
            }
            return Json(data: "State Invalid", behavior: JsonRequestBehavior.AllowGet);
        }
        //public async Task<JsonResult> VerifyInstepMember(int regId, VerifyMemberVM member)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser
        //        {
        //            UserName = member.UserName,
        //            FName = member.FName,
        //            Email = member.Email,
        //            PhoneNumber = member.PhoneNumber
        //        };
        //        var result = await UserManager.CreateAsync(user, AppSettingsKeyConsts.DefPassKey);
        //        if (result.Succeeded)
        //        {
        //            await this.UserManager.AddToRoleAsync(user.Id, "Alumni");
        //            //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

        //            using (var uow = new UnitOfWork(new NDCWebContext()))
        //            {
        //                //var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
        //                var courseRegister = await uow.CourseRegisterRepo.GetByIdAsync(regId);
        //                if (courseRegister == null)
        //                    return Json(data: "Not Found", behavior: JsonRequestBehavior.AllowGet);

        //                courseRegister.UserId = user.Id.ToString();
        //                //courseRegister.CourseId = course.CourseId;
        //                courseRegister.VerifyDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        //                courseRegister.Approved = true;
        //                await uow.CommitAsync();
        //                SendEmailHelper.EmailSend(courseRegister.EmailId, "NDC User Verification", "You are now verified user!", false);
        //            }

        //            return Json(data: "Participant verified successfully!", behavior: JsonRequestBehavior.AllowGet);
        //        }
        //        return Json(data: "Bad Request", behavior: JsonRequestBehavior.AllowGet);
        //    }
        //    return Json(data: "State Invalid", behavior: JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken] //R on productions
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var dataModal = await uow.AlumniRepo.GetByIdAsync(id);
                if (dataModal == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.AlumniRepo.Remove(dataModal);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
        [HttpPost]
        //[ValidateAntiForgeryToken] //R on productions
        public async Task<JsonResult> ParticipantDeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var dataModal = await uow.CourseRegisterRepo.GetByIdAsync(id);
                if (dataModal == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.CourseRegisterRepo.Remove(dataModal);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
        [EncryptedActionParameter]

        public ActionResult AccomodationPrint(int id)
        {
            string UserName = ((ClaimsIdentity)User.Identity).FindFirst("FName").Value;
            UserActivityHelper.SaveUserActivity("Accommodation Report print by  " + UserName, Request.Url.ToString());
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var accomodationdatacheck = uow.AccomodationRepo.FirstOrDefault(x => x.CreatedBy == id.ToString());
                var accomodationdata = uow.AccomodationRepo.FirstOrDefault(x => x.CreatedBy == id.ToString());
                var coursereg = uow.CourseRegisterRepo.Find(x => x.UserId == id.ToString()).FirstOrDefault();
                if (accomodationdatacheck != null)
                {


                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<Accomodation, AccomodationIndexVM>();
                    });

                    ViewBag.FullName = coursereg.FirstName + " " + coursereg.MiddleName + " " + coursereg.LastName;
                    ViewBag.Rank = coursereg.Ranks.RankName;
                    ViewBag.Decoration = coursereg.Honour;
                    ViewBag.DOC = coursereg.DOCommissioning;
                    ViewBag.DOS = coursereg.SeniorityYear;
                    ViewBag.Address = coursereg.ApptLocation;
                    ViewBag.MobileNo = coursereg.MobileNo;
                    ViewBag.EmailId = coursereg.EmailId;

                    IMapper mapper = config.CreateMapper();
                    AccomodationIndexVM CreateDto = mapper.Map<Accomodation, AccomodationIndexVM>(accomodationdata);
                    return View(CreateDto);
                }
                else
                {
                    //return null;
                    this.AddNotification("Accommodation form not submitted.", NotificationType.WARNING);
                    return RedirectToAction("participants", "MemberVerify");
                }
            }
        }
    }
}