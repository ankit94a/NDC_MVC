using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NDCWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();


            routes.MapRoute(
                 name: "Content1",
                 url: "Content1/{slug}",
                 defaults: new { controller = "Dynamic", action = "DynamicPL1", slug = "" }
             );

            routes.MapRoute(
                name: "Content2",
                url: "Content2/{parentSlug}/{childSlug}",
                defaults: new { controller = "Dynamic", action = "DynamicL1", parentSlug = "", childSlug = "" }
            );

            //  routes.MapRoute(
            //    name: "course",
            //    url: "course/{slug}",
            //    defaults: new { controller = "Dynamic", action = "DynamicPL1", slug = "" }
            //);
            routes.MapRoute(
             name: "course",
             url: "course/{parentSlug}/{childSlug}",
             defaults: new { controller = "Dynamic", action = "DynamicL1", parentSlug = "", childSlug = "" }
         );
            routes.MapRoute(
              name: "about",
              url: "about/{parentSlug}/{childSlug}",
              defaults: new { controller = "Dynamic", action = "DynamicL1", parentSlug = "", childSlug = "" }
          );
            routes.MapRoute(
             name: "wings",
             url: "wings/{parentSlug}/{childSlug}",
             defaults: new { controller = "Dynamic", action = "DynamicL1", parentSlug = "", childSlug = "" }
         );
            routes.MapRoute(
            name: "notification",
            url: "notification/{parentSlug}/{childSlug}",
            defaults: new { controller = "Dynamic", action = "DynamicL1", parentSlug = "", childSlug = "" }
        );
            routes.MapRoute(
            name: "cultural",
            url: "cultural/{parentSlug}/{childSlug}",
            defaults: new { controller = "Dynamic", action = "DynamicL1", parentSlug = "", childSlug = "" }
        );
            routes.MapRoute(
          name: "participant",
          url: "participant/{parentSlug}/{childSlug}",
          defaults: new { controller = "Dynamic", action = "DynamicL1", parentSlug = "", childSlug = "" }
      );
            routes.MapRoute(
          name: "alumini",
          url: "alumini/{parentSlug}/{childSlug}",
          defaults: new { controller = "Dynamic", action = "DynamicL1", parentSlug = "", childSlug = "" }
      );
            routes.MapRoute(
         name: "contact",
         url: "contact/{parentSlug}/{childSlug}",
         defaults: new { controller = "Dynamic", action = "DynamicL1", parentSlug = "", childSlug = "" }
     );
            routes.MapRoute(
        name: "quicklinks",
        url: "quicklinks/{parentSlug}/{childSlug}",
        defaults: new { controller = "Dynamic", action = "DynamicL1", parentSlug = "", childSlug = "" }
    );
            routes.MapRoute(
        name: "tenders",
        url: "tenders/{parentSlug}/{childSlug}",
        defaults: new { controller = "Dynamic", action = "DynamicL1", parentSlug = "", childSlug = "" }
    );
            routes.MapRoute(
        name: "rakshika",
        url: "rakshika/{parentSlug}/{childSlug}",
        defaults: new { controller = "Dynamic", action = "DynamicL1", parentSlug = "", childSlug = "" }
    );
            routes.MapRoute(
        name: "faq",
        url: "faq/{slug}",
        defaults: new { controller = "Dynamic", action = "DynamicPL1", slug = "" }
    );
            routes.MapRoute(
        name: "faq2",
        url: "faq/{parentSlug}/{childSlug}",
        defaults: new { controller = "Dynamic", action = "DynamicL1", parentSlug = "", childSlug = "" }
    );
   //         routes.MapRoute(
   //    name: "alumni",
   //    url: "{controller}/{action}",
   //    defaults: new { controller = "alumni", action = "registration", id = UrlParameter.Optional }
   //);
   //         // Participants Area
            routes.MapRoute(
            name: "member",
            url: "member/{slug}",
            defaults: new { controller = "Dynamic", action = "DynamicPL1", slug = "" }
            );
            //For static files
            //routes.MapRoute(
            //           "JSFiles",
            //           "{controller}/{fileName}",
            //           new { controller = "Main", action = "Get" }
            //           );

            // routes.MapRoute(
            //    name: "InstepAlumni",
            //    url: "alumni/{controller}/{action}",
            //    defaults:
            //    new { controller = "Alumnus", action = "InStep" }
            //);
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "NDCWeb.Controllers" }
            );
           
        }
    }
}
