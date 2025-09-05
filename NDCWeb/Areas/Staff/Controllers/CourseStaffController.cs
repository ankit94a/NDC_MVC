using NDCWeb.Areas.Admin.Models;
using NDCWeb.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Staff.Controllers
{
    [CSPLHeaders]
    [Authorize(Roles = CustomRoles.Staff)]
    [StaffStaticUserMenu]
    //[UserMenu(MenuArea = "Staff")]
    public class CourseStaffController : Controller
    {
        // GET: Staff/CourseStaff
        public ActionResult CourseHandbook()
        {
            return View();
        }
        public ActionResult TrainingCalendar()
        {
            return View();
        }
        public ActionResult IAGPaperGuidline()
        {
            return View();
        }
        public ActionResult ThesisGuidline()
        {
            return View();
        }
        public ActionResult TourReportSample()
        {
            return View();
        }
        public ActionResult NdcPublications()
        {
            return View();
        }
        public ActionResult OpacLib()
        {
            return View();
        }
        public ActionResult Faq()
        {
            return View();
        }
    }
}