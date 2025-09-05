using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Infrastructure.Filters;
using System.Threading.Tasks;
using System.Web.Mvc;
using NDCWeb.Persistence;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using AutoMapper;
using NDCWeb.Infrastructure.Constants;
using CaptchaMvc.HtmlHelpers;
using System;
using System.Linq;

namespace NDCWeb.Controllers
{
    [CSPLHeaders]
    [UserMenu(MenuArea = "NA")]
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            ViewBag.Service = CustomDropDownList.GetRankService();
            ViewBag.Gender = CustomDropDownList.GetGender();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var course = uow.CourseRepo.Find(x => x.UnderRegistration == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                if (course != null)
                {
                    ViewBag.Course = course.CourseName;
                    return View();
                }
                else
                    return RedirectToAction("Closed");

                
            }
        }
        
        // GET: Registration/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Closed()
        {
            return View();
        }

        // GET: Registration/Create
        public ActionResult InStep()
        {
            ViewBag.Service = CustomDropDownList.GetRankService();
            ViewBag.Gender = CustomDropDownList.GetGender();

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var course = uow.CourseRepo.Find(x => x.UnderRegistration == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                if (course != null)
                    ViewBag.Course = course.CourseName;

                return View();
            }
        }
        // POST: Registration/Create
        [HttpPost]
        public async Task<ActionResult> Register(CourseRegisterCrtVM objCourseRegisterCvm)
        {
            if (!this.IsCaptchaValid("Validate your captcha"))
            {
                ViewBag.CaptchaErrorMessage = "Invalid Captcha";
                ModelState.AddModelError("", "Invalid verification code. Please try again.");

                #region redirect View With get Data
                ViewBag.Service = CustomDropDownList.GetRankService();
                ViewBag.Gender = CustomDropDownList.GetGender();
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var course = uow.CourseRepo.Find(x => x.UnderRegistration == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                    if (course != null)
                        ViewBag.Course = course.CourseName;
                    return View();
                }
                #endregion
            }
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var course = uow.CourseRepo.Find(x => x.UnderRegistration == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CourseRegisterCrtVM, CourseRegister>();
                });
                IMapper mapper = config.CreateMapper();
                CourseRegister CreateDto = mapper.Map<CourseRegisterCrtVM, CourseRegister>(objCourseRegisterCvm);
                uow.CourseRegisterRepo.Add(CreateDto);
                CreateDto.CourseId =  course.CourseId;
                await uow.CommitAsync();
                TempData["RefrenceNo"] = CreateDto.CourseRegisterId;
                return RedirectToAction("Thankyou");
            }
        }
        [HttpPost]
        public async Task<ActionResult> InStep1()
        {
            ViewBag.Service = CustomDropDownList.GetRankService();
            ViewBag.Gender = CustomDropDownList.GetGender();

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var course = uow.CourseRepo.Find(x => x.UnderRegistration == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                if (course != null)
                    ViewBag.Course = course.CourseName;

                return View();
            }
        }
      
        [HttpPost]
        public ActionResult RegistrationAck(CourseRegisterCrtVM modal)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var courseEnrol = modal;
                var ranks = uow.RankMasterRepo.FirstOrDefault(x=>x.RankId == courseEnrol.RankId);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CourseRegisterCrtVM, CourseRegisterAckVM>();
                });
                IMapper mapper = config.CreateMapper();
                CourseRegisterAckVM courseRegisterAck = mapper.Map<CourseRegisterCrtVM, CourseRegisterAckVM>(courseEnrol);
                courseRegisterAck.RankName = ranks.RankName;
                courseRegisterAck.RankService = ranks.Service;
                return PartialView("_RegistrationAck", courseRegisterAck);
            }
                
            
        }
        
        public ActionResult Thankyou()
        {

            TempData["alertMessage"] = "Your details are successfuly submitted. However, your details are yet to be verified by NDC.";
            TempData["refNo"] = GenerateNewRandom();
            TempData["refNo"] = TempData["RefrenceNo"];
            return View();
        }
        
        private static string GenerateNewRandom()
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateNewRandom();
            }
            return r;
        }
        [HttpGet]
        public JsonResult CheckExistingMobile(string ConfirmMobileNo)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    bool isExist = uow.CourseRegisterRepo.FirstOrDefault(x => x.MobileNo == ConfirmMobileNo) != null;
                    return Json(!isExist, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public JsonResult CheckExistingEmail(string ConfirmEmail)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    bool isExist = uow.CourseRegisterRepo.FirstOrDefault(x => x.EmailId == ConfirmEmail) != null;
                    return Json(!isExist, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
