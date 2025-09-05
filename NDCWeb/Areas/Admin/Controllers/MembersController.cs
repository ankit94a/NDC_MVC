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
using Microsoft.AspNet.Identity.Owin;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.View_Models;
using System.Security.Claims;
using NDCWeb.Infrastructure.Helpers.Account;
using NDCWeb.Areas.Admin.Models;

namespace NDCWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = CustomRoles.Admin)]
    [CSPLHeaders]
    public class MembersController : Controller
    {
        private ApplicationUserManager _userManager;
        public MembersController()
        { }
        public MembersController(ApplicationUserManager userManager)
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
        // GET: Admin/Members
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Participants()
        {
            string UserName = ((ClaimsIdentity)User.Identity).FindFirst("FName").Value;
            UserActivityHelper.SaveUserActivity("Participant List in admin accessed by  " + UserName, Request.Url.ToString());
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                var courseRegisters = uow.CourseRegisterRepo.Find(x => x.CourseId == course.CourseId, np => np.Ranks).OrderByDescending(x => x.CourseRegisterId).ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CourseRegister, CourseRegisterIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<CourseRegister>, List<CourseRegisterIndxVM>>(courseRegisters).ToList();
                return View(indexDto);
            }
        }
        [HttpPost]
        public async Task<JsonResult> CourseMemberResetLogin(int memberRegId)
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
            string UserName = ((ClaimsIdentity)User.Identity).FindFirst("FName").Value;
            UserActivityHelper.SaveUserActivity("Alumni List in admin accessed by  " + UserName, Request.Url.ToString());
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                //var personalDetail = uow.AlumniRepo.Find(x => x.CreatedBy != null);
                var personalDetail = uow.AlumniRepo.GetAll().OrderByDescending(x => x.AluminiId).ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AlumniMaster, AlumniIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var participants = mapper.Map<IEnumerable<AlumniMaster>, List<AlumniIndxVM>>(personalDetail);
                return View(participants);
            }
        }
        [HttpPost]
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
    }
}