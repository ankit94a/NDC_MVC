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
    public class UnderConstructionController : Controller
    {
        // GET: Staff/UnderConstruction
        public ActionResult Index()
        {
            return View();
        }
    }
}