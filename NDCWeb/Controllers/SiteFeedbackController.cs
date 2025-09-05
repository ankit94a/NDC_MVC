using AutoMapper;
using CaptchaMvc.HtmlHelpers;
using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Infrastructure.Helpers.Mail;
using NDCWeb.Models;
using NDCWeb.Persistence;
using NDCWeb.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Controllers
{
    [CSPLHeaders]
    [UserMenu(MenuArea = "NA")]
    public class SiteFeedbackController : Controller
    {
        // GET: SiteFeedback
        public ActionResult Index()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var feedBacks = uow.SiteFeedbackRepo.FindAsQuery(x => x.Approved == true);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<SiteFeedback>, List<AlumniFeedbackVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<SiteFeedback>, IEnumerable<AlumniFeedbackVM>>(feedBacks).ToList();
                return View(indexDto);
            }
        }
        public ActionResult Feedback()
        {
            SiteFeedbackVM objVM = new SiteFeedbackVM();
            ViewBag.Department = CustomDropDownList.GetDepartments();
            return View(objVM);
        }

        [HttpPost]
        public async Task<ActionResult> Feedback(SiteFeedbackVM ojbFeedback)
        {
            if (!this.IsCaptchaValid("Validate your captcha"))
            {
                ViewBag.CaptchaErrorMessage = "Invalid Captcha";
                ModelState.AddModelError("", "Invalid verification code. Please try again.");
                return View(ojbFeedback);
            }
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<SiteFeedbackVM, SiteFeedback>();
                });
                IMapper mapper = config.CreateMapper();
                SiteFeedback CreateDto = mapper.Map<SiteFeedbackVM, SiteFeedback>(ojbFeedback);
                uow.SiteFeedbackRepo.Add(CreateDto);
                await uow.CommitAsync();
                //Mail.FeedbackEmail(CreateDto.FullName, "Website Feedback", CreateDto.EmailId, CreateDto.Comment, CreateDto.DepartmentSubject);
                this.AddNotification("Your feedback is successfully submitted", NotificationType.SUCCESS);
                return RedirectToAction("Feedback");
            }
        }
    }
}