using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Helpers.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace NDCWeb.Infrastructure.Filters
{
    public class StaffStaticUserMenuAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //HttpContext.Current.ViewBag.StaffType = staffType;
            //HttpContext.Current.Session["StaffType"] = staffType;

            var staffMenuHelper = new StaffStaticMenuHelper();
            filterContext.Controller.ViewBag.SiteMenuStaffType = staffMenuHelper.GetStaffType();
            base.OnActionExecuting(filterContext);
        }
    }
}