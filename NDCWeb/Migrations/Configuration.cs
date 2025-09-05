namespace NDCWeb.Migrations
{
    using NDCWeb.Infrastructure.Constants;
    using NDCWeb.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NDCWeb.Data_Contexts.NDCWebContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "NDCWeb.Data_Contexts.NDCWebContext";
        }

        protected override void Seed(NDCWeb.Data_Contexts.NDCWebContext context)
        {
            context.MenuUrlMstr.AddOrUpdate(x => x.MenuUrlId,
                new MenuUrlMaster { MenuUrlId = 1, UrlPrefix = "HM", Controller = "Home", Action = "Index", PageType = PageType.Static, UrlArea = "NA", MenuLevel = MenuLevel.NA },
                new MenuUrlMaster { MenuUrlId = 2, UrlPrefix = "HM", Controller = "Home", Action = "ContactUs", PageType = PageType.Static, UrlArea = "NA", MenuLevel = MenuLevel.NA },
                new MenuUrlMaster { MenuUrlId = 3, UrlPrefix = "Content1", Controller = "Dynamic", Action = "DynamicPL1", PageType = PageType.Dynamic, UrlArea = "NA", MenuLevel = MenuLevel.Parent },
                new MenuUrlMaster { MenuUrlId = 4, UrlPrefix = "Content1", Controller = "Dynamic", Action = "DynamicL1", PageType = PageType.Dynamic, UrlArea = "NA", MenuLevel = MenuLevel.Child }
                );
        }
    }
}
