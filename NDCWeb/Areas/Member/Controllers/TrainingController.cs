using NDCWeb.Areas.Admin.Models;
using NDCWeb.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Member.Controllers
{
    [Authorize(Roles = CustomRoles.Candidate)]
    [UserMenu(MenuArea = "Member")]
    [CSPLHeaders]
    public class TrainingController : Controller
    {
        // GET: Member/Training
        public ActionResult Index()
        {
            return View();
        }
    }
}