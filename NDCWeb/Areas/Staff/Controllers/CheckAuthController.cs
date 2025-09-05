using NDCWeb.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Staff.Controllers
{
    public class CheckAuthController : Controller
    {
        [CSPLHeaders]
        [Authorize(Roles = Admin.Models.CustomRoles.Staff)]
        [StaffStaticUserMenu]
        public ActionResult Index()
        {
            return View();
        }
    }
}