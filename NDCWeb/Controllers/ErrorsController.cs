using NDCWeb.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Controllers
{
    [CSPLHeaders]
    [UserMenu(MenuArea = "NA")]
    public class ErrorsController : Controller
    {
        // GET: Errors
        public ViewResult Index()
        {
            return View("Error");
        }
        public ViewResult NotFound()
        {
            //Response.StatusCode = 404;  //you may want to set this to 200
            return View("Error404");
        }
        public ViewResult ServerError()
        {
            //Response.StatusCode = 500;
            return View("Error");
        }
    }
}