using System.Web.Mvc;

namespace NDCWeb.Areas.Staff
{
    public class StaffAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Staff";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Staff_Assignment",
                "Staff/Assignment/index/{category}",
                new { controller = "Assignment", action = "Index", category = UrlParameter.Optional },
                new[] { "NDCWeb.Areas.Staff.Controllers" }
            );
            context.MapRoute(
                "StaffModule_Assignment",
                "Staff/Feedbacks/module/{category}",
                new { controller = "Feedbacks", action = "module", category = UrlParameter.Optional },
                new[] { "NDCWeb.Areas.Staff.Controllers" }
            );
            context.MapRoute(
                "Staff_Feedback",
                "staff/feedbacks/index/{category}",
                new { controller = "Feedbacks", action = "Index", category = UrlParameter.Optional },
                new[] { "NDCWeb.Areas.Staff.Controllers" }
            );
            context.MapRoute(
                 name: "Staff_Parent",
                 url: "Staff/Emp/{slug}",
                 defaults: new { controller = "StaffDynamic", action = "DynamicPL1", slug = "" }
             );

            context.MapRoute(
                name: "Staff_Child",
                url: "Staff/Emp/{parentSlug}/{childSlug}",
                defaults: new { controller = "StaffDynamic", action = "DynamicL1", parentSlug = "", childSlug = "" }
            );
            context.MapRoute(
                "Staff_TrainingActivity",
                "Staff/TrainingActivity/ModuleCategory/module/{module}",
                new { controller = "TrainingActivity", action = "ModuleCategory", module = UrlParameter.Optional },
                new[] { "NDCWeb.Areas.Staff.Controllers" }
            );
            context.MapRoute(
                "Staff_default",
                "Staff/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "NDCWeb.Areas.Staff.Controllers" }
            );
        }
    }
}