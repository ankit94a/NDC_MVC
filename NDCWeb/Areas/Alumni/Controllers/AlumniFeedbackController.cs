using NDCWeb.Areas.Admin.Models;
using AutoMapper;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Alumni.Controllers
{
    [Authorize(Roles = CustomRoles.Alumni)]
    [UserMenu(MenuArea = "Alumni")]
    [CSPLHeaders]
    public class AlumniFeedbackController : Controller
    {
        // GET: Alumni/AlumniFeedback
        public ActionResult Feedback()
        {
            return View();
        }
        public async Task<ActionResult> AlumniFeedback()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var alumniFeedbacks = await uow.AlumniFeedbackRepo.AlumniFeedbackGetAll();
                return View(alumniFeedbacks);
            }
        }
    }
}