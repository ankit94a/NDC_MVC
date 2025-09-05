using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Infrastructure.Helpers.FileDirectory;
using NDCWeb.Models;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace NDCWeb.Areas.Member.Controllers
{
    [Authorize(Roles = CustomRoles.Candidate)]
    [UserMenu(MenuArea = "Member")]
    [CSPLHeaders]
    public class FaqsController : Controller
    {
        // GET: Member/Faqs
        public ActionResult mphil()
        {
            return View();
        }
    }
}