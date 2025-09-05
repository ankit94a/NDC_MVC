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
using NDCWeb.View_Models;
using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Infrastructure.Helpers.Mail;

namespace NDCWeb.Controllers
{
    [CSPLHeaders]
    [UserMenu(MenuArea = "NA")]
    public class AlumnusController : Controller
    {
        // GET: Alumni
        public ActionResult Index()
        {
            return View();
        }
        /*
        public ActionResult Registration()
        {
            ViewBag.Service = CustomDropDownList.GetRankService();
            ViewBag.Gender = CustomDropDownList.GetGender();
            AlumniCrtVM reg = new AlumniCrtVM();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                if (course != null)
                    ViewBag.Course = course.CourseName;

                return View(reg);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Registration(AlumniCrtVM objAlumni)
        {
            if (!this.IsCaptchaValid("Validate your captcha"))
            {
                ViewBag.CaptchaErrorMessage = "Invalid Captcha";
                ModelState.AddModelError("", "Invalid verification code. Please try again.");
                return View(objAlumni);
            }
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AlumniCrtVM, AlumniMaster>();
                });
                IMapper mapper = config.CreateMapper();
                AlumniMaster CreateDto = mapper.Map<AlumniCrtVM, AlumniMaster>(objAlumni);
                CreateDto.RegDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                uow.AlumniRepo.Add(CreateDto);
                await uow.CommitAsync();
                //Mail.AlumniEmail(CreateDto.First_name, "Alumni Registration", CreateDto.Email, "");
                this.AddNotification("Your feedback is successfully submitted", NotificationType.SUCCESS);
                return RedirectToAction("Thankyou");
            }
        }
        */
        //InStep
        //Generalised
        public ActionResult Registration()
        {
            //ViewBag.Service = CustomDropDownList.GetRankService(); 
            ViewBag.Service = CustomDropDownList.GetInstepService();
            ViewBag.Gender = CustomDropDownList.GetGender();
            AlumniCrtVM reg = new AlumniCrtVM();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {

                //LeaveCrtVM objLeaveCrt = new LeaveCrtVM();
                ViewBag.InStepCourseId = uow.InStepCourseRepo.GetInStepCourses();
                var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                if (course != null)
                    ViewBag.Course = course.CourseName;

                return View(reg);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Registration(AlumniCrtVM objAlumni)
        {
            if (!this.IsCaptchaValid("Validate your captcha"))
            {
                ViewBag.CaptchaErrorMessage = "Invalid Captcha";
                ModelState.AddModelError("", "Invalid verification code. Please try again.");
                return View(objAlumni);
            }
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AlumniCrtVM, AlumniMaster>();
                });
                IMapper mapper = config.CreateMapper();
                AlumniMaster CreateDto = mapper.Map<AlumniCrtVM, AlumniMaster>(objAlumni);
                CreateDto.RegDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                uow.AlumniRepo.Add(CreateDto);
                await uow.CommitAsync();
                //Mail.AlumniEmail(CreateDto.First_name, "Alumni Registration", CreateDto.Email, "");
                this.AddNotification("Your registration is successfully submitted", NotificationType.SUCCESS);
                return RedirectToAction("Thankyou");
            }
        }
        public ActionResult Thankyou()
        {

            TempData["alertMessage"] = "Your details are successfuly submitted. However, your details are yet to be verified by NDC.";
            TempData["refNo"] = GenerateNewRandom();
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
        public JsonResult CheckExistingMobile(string MobileNo)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    bool isExist = uow.AlumniRepo.FirstOrDefault(x => x.MobileNo == MobileNo) != null;
                    return Json(!isExist, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public JsonResult CheckExistingEmail(string Email)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    bool isExist = uow.AlumniRepo.FirstOrDefault(x => x.Email == Email) != null;
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