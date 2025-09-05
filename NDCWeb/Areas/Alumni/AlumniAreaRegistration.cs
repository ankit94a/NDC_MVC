using System.Web.Mvc;

namespace NDCWeb.Areas.Alumni
{
    public class AlumniAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Alumni";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                 name: "Alumni_Parent",
                 url: "Alumni/Participant/{slug}",
                 defaults: new { controller = "AlumniDynamic", action = "DynamicPL1", slug = "" }
             );

            context.MapRoute(
                name: "Alumni_Child",
                url: "Alumni/Participant/{parentSlug}/{childSlug}",
                defaults: new { controller = "AlumniDynamic", action = "DynamicL1", parentSlug = "", childSlug = "" }
            );

            context.MapRoute(
                "Alumni_default",
                "Alumni/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "NDCWeb.Areas.Alumni.Controllers" }
            );
        }
    }
}