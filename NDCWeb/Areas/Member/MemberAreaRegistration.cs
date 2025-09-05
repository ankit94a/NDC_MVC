using System.Web.Mvc;

namespace NDCWeb.Areas.Member
{
    public class MemberAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Member";
            }
        }
        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                 name: "Member_Parent",
                 url: "Member/Participant/{slug}",
                 defaults: new { controller = "MemberDynamic", action = "DynamicPL1", slug = "" }
             );

            context.MapRoute(
                name: "Member_Child",
                url: "Member/Participant/{parentSlug}/{childSlug}",
                defaults: new { controller = "MemberDynamic", action = "DynamicL1", parentSlug = "", childSlug = "" }
            );

            context.MapRoute(
                "Member_Event",
                "Member/EventMember/Enrol/{attendType}/{eventId}",
                new { controller = "EventMember", action = "Enrol", attendType = UrlParameter.Optional, eventId = UrlParameter.Optional },
                new[] { "NDCWeb.Areas.Member.Controllers" }
            );

            context.MapRoute(
                "Member_ModuleActivity",
                "Member/ModuleActivity/ModuleCategory/module/{module}",
                new { controller = "ModuleActivity", action = "ModuleCategory", module = UrlParameter.Optional },
                new[] { "NDCWeb.Areas.Member.Controllers" }
            );

            context.MapRoute(
                "Member_default",
                "Member/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "NDCWeb.Areas.Member.Controllers" }
            );
        }
    }
}