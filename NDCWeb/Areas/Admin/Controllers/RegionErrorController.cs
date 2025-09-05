using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Admin.Controllers
{
    public class RegionErrorController : Controller
    {
        // GET: Admin/RegionError
        public ActionResult Index()
        {
            string ipAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }; 
            ViewBag.IP = ipAddress;
            return View();
        }
    }
}