using NDCWeb.Infrastructure.Constants;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NDCWeb.View_Models
{
    public class MenuControlVM
    {
        public int MenuId { get; set; }
        public int ParentId { get; set; }
        public string SlugMenu { get; set; }
        public int SortOrder { get; set; }
        public string MenuName { get; set; }
        public string HMenuName { get; set; }
        public string PageTitle { get; set; }
        public string PageHeading { get; set; }
        public string HPageHeading { get; set; }
        public bool IsVisible { get; set; }
        public PositionType MenuType { get; set; }
        public int? MenuUrlId { get; set; }
        //public virtual MenuUrlMaster MenuUrlMasters { get; set; }
        public string UrlPrefix { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public PageType PageType { get; set; }

        public bool ExternalLink { get; set; }
        public string ExternalUrl { get; set; }

        public string MenuArea { get; set; }
        public MenuLevel MenuLevel { get; set; }
        public string UrlArea { get; set; }
    }
}