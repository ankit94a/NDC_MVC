using NDCWeb.Areas.Admin.View_Models;
using NDCWeb.Core.IRepositories;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Persistence.Repositories
{
    public class MenuItemMasterRepository : Repository<MenuItemMaster>, IMenuItemMasterRepository
    {
        public MenuItemMasterRepository(DbContext context) : base(context)
        {
        }
        public void UpdateMenuSortOrder(MenuItemMaster menuItem)
        {
            NDCWebContext.MenuItemMstr.Attach(menuItem);
            NDCWebContext.Entry(menuItem).Property(x => x.SortOrder).IsModified = true;
        }
        public IEnumerable<SelectListItem> GetParentMenus()
        {
            List<SelectListItem> parentMenus = NDCWebContext.MenuItemMstr
                    .OrderByDescending(n => n.SortOrder)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.MenuId.ToString(),
                            Text = n.MenuName
                        }).ToList();
            var ddltip = new SelectListItem()
            {
                Value = "0",
                Text = "Root"
            };
            parentMenus.Insert(0, ddltip);
            return new SelectList(parentMenus, "Value", "Text");
        }
        public IEnumerable<SelectListItem> GetParentMenus(string area)
        {
            List<SelectListItem> parentMenus = NDCWebContext.MenuItemMstr
                .Where(n => n.MenuArea == area)
                    .OrderByDescending(n => n.SortOrder)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.MenuId.ToString(),
                            Text = n.MenuName
                        }).ToList();
            var ddltip = new SelectListItem()
            {
                Value = "0",
                Text = "Root"
            };
            parentMenus.Insert(0, ddltip);
            return new SelectList(parentMenus, "Value", "Text");
        }
        public IEnumerable<SelectListItem> GetParentMenus(int parentmenuid)
        {
            List<SelectListItem> parentMenus = NDCWebContext.MenuItemMstr
                .Where(n => n.ParentId == parentmenuid)
                    .OrderByDescending(n => n.SortOrder)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.MenuId.ToString(),
                            Text = n.MenuName
                        }).ToList();
            //var ddltip = new SelectListItem()
            //{
            //    Value = "0",
            //    Text = "Root"
            //};
            //parentMenus.Insert(0, ddltip);
            return new SelectList(parentMenus, "Value", "Text");
        }

        public IEnumerable<SelectListItem> GetMenuWithParent(string area)
        {
            List<SelectListItem> parentMenus = NDCWebContext.MenuItemMstr
                .Where(n => n.MenuArea == area)
                    .OrderByDescending(n => n.SortOrder)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.MenuId.ToString(),
                            Text = n.MenuName
                        }).ToList();
            var ddltip = new SelectListItem()
            {
                Value = "0",
                Text = "Root"
            };
            parentMenus.Insert(0, ddltip);
            return new SelectList(parentMenus, "Value", "Text");
        }

        public async Task<IEnumerable<MenuItemMasterCompleteIndxVM>> GetMenuItemListAsync()
        {
            return await NDCWebContext.Database.SqlQuery<MenuItemMasterCompleteIndxVM>("Get_MenuItemMaster").ToListAsync();
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}