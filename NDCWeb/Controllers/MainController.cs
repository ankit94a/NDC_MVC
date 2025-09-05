using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }
        //option 1. Have one method for all the files.
        public ActionResult Get(string fileName)
        {
            return File(Server.MapPath("Areas/Admin/Component/scripts/" + fileName + ".js"), "text/javascript");
        }
        //option 2:  have a method for each file 
        public ActionResult main()
        {
            return File(Server.MapPath("~/media/js/main.js"), "text/javascript");
        }
    }
}