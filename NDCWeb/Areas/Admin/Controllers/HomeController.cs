using NDCWeb.Areas.Admin.Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Infrastructure.Helpers.Account;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = CustomRoles.Admin)]
    [CSPLHeaders]
    [SessionTimeout]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public async Task<ActionResult> Index()
        {
            UserActivityHelper.SaveUserActivity("Login Successfull for Admin user", Request.Url.ToString());
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Visits = await uow.VisitorRepo.GetVisitStats(0); //newsArticle;
            }
            return View();

        }
        //public static string GetFullName(this System.Security.Principal.IPrincipal usr)
        //{
        //    var fullNameClaim = ((ClaimsIdentity)usr.Identity).FindFirst("FName");
        //    if (fullNameClaim != null)
        //        return fullNameClaim.Value;

        //    return "";
        //}
        [AllowAnonymous]
        public async Task<ActionResult> IndexNew()
        {
            UserActivityHelper.SaveUserActivity("Login Successfull for Admin user", Request.Url.ToString());
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Visits = await uow.VisitorRepo.GetVisitStats(0); //newsArticle;
            }
            return View();
        }
    }
}