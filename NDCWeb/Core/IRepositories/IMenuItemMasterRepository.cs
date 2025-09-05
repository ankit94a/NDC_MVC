using NDCWeb.Areas.Admin.View_Models;
using NDCWeb.Models;
using NDCWeb.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NDCWeb.Core.IRepositories
{
    public interface IMenuItemMasterRepository : IRepository<MenuItemMaster>
    {
        Task<IEnumerable<MenuItemMasterCompleteIndxVM>> GetMenuItemListAsync();
        IEnumerable<SelectListItem> GetParentMenus();
        IEnumerable<SelectListItem> GetParentMenus(string area);
        IEnumerable<SelectListItem> GetParentMenus(int parentmenuid);
        void UpdateMenuSortOrder(MenuItemMaster menuItem);
    }
}
