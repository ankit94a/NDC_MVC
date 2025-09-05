using NDCWeb.Core.IRepositories;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Persistence.Repositories
{
    public class MenuUrlMasterRepository : Repository<MenuUrlMaster>, IMenuUrlMasterRepository
    {
        public MenuUrlMasterRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<SelectListItem> GetUrlPrefixs()
        {
            List<SelectListItem> menuUrls = NDCWebContext.MenuUrlMstr.ToList()
                .OrderBy(n => n.MenuUrlId)
                .Select(n =>
                new SelectListItem
                {
                    Value = n.MenuUrlId.ToString(),
                    Text = GetUrlPattern(n)
                    //Text = n.UrlArea + "/" + n.UrlPrefix + "(" + n.MenuLevel + ")"
                }).ToList();
            var ddltip = new SelectListItem()
            {
                Value = null,
                Text = "# (Not Link)"
            };
            menuUrls.Insert(0, ddltip);
            return new SelectList(menuUrls, "Value", "Text");
        }
        private string GetUrlPattern(MenuUrlMaster n)
        {
            string pattern = "";
            string urlarea = "";
            if (n.UrlArea != "NA")
            {
                urlarea = n.UrlArea + "/";
            }
            if (n.PageType == PageType.Static)
                pattern = urlarea + n.Controller + "/" + n.Action + "(Static)";
            else
                pattern = urlarea + n.UrlPrefix + "(" + n.MenuLevel + ")";

            return pattern;
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}