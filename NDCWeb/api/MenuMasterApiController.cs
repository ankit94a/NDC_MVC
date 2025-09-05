using NDCWeb.Areas.Admin.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Helpers.Account;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages.Html;

namespace NDCWeb.api
{
    [RoutePrefix("api/menuMasters")]
    public class MenuMasterApiController : ApiController
    {
        // GET: api/MenuMasterApi
        [HttpGet]
        [Route("GetMenuWithParent/menuArea/{menuArea}")]
        public IEnumerable<System.Web.Mvc.SelectListItem> GetMenuWithParent(string menuArea)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var menus = uow.MenuItemMstrRepo.Find(x => x.MenuArea == menuArea);

                var menuViewData = uow.MenuItemMstrRepo.GetAllAsQuery();
                var filterMenuView = (from p in menuViewData
                                      join q in menuViewData on p.ParentId equals q.MenuId
                                      into ps
                                      from q in ps.DefaultIfEmpty()
                                      where p.MenuArea == menuArea
                                      orderby q.MenuName
                                      select new SelectListItem
                                      {
                                          Value = p.MenuId.ToString(),
                                          Text = (q.MenuName != null ? (q.MenuName + "/") : "")
                                                      + p.MenuName
                                      }).ToList();
                var ddltip = new SelectListItem()
                {
                    Value = "0",
                    Text = "Root"
                };
                filterMenuView.Insert(0, ddltip);
                return new System.Web.Mvc.SelectList(filterMenuView, "Value", "Text");
            }
        }

        [HttpGet]
        [Route("GetSubMenus/parentMenu/{parentMenuId}")]
        public IEnumerable<System.Web.Mvc.SelectListItem> GetSubMenus(int parentMenuId)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                return uow.MenuItemMstrRepo.GetParentMenus(parentMenuId);
            }
        }
        //public IEnumerable<System.Web.Mvc.SelectListItem> GetMenuWithParent(string menuArea, string role)
        //{
        //    using (var uow = new UnitOfWork(new NDCWebContext()))
        //    {


        //        //var menus = uow.MenuItemMstrRepo.Find(x => x.MenuArea == menuArea);

        //        int roleid = UserAccountHelper.getCurrentUserRoleId();
        //        var MenuRole = uow.MenuRoleRepo.Find(x => x.RoleId == roleid && x.Write == true).Select(x => (x.MenuId).ToString());
        //        //var ParentMenus = uow.MenuItemMstrRepo.GetParentMenus();

        //        var menuViewData = uow.MenuItemMstrRepo.GetAllAsQuery().Where(x => MenuRole.Contains(x.Value);
        //        var filterMenuView = (from p in menuViewData
        //                              join q in menuViewData on p.ParentId equals q.MenuId
        //                              into ps
        //                              from q in ps.DefaultIfEmpty()
        //                              where p.MenuArea == menuArea
        //                              orderby q.MenuName
        //                              select new SelectListItem
        //                              {
        //                                  Value = p.MenuId.ToString(),
        //                                  Text = (q.MenuName != null ? (q.MenuName + "/") : "")
        //                                              + p.MenuName
        //                              }).ToList();

        //        ViewBag.ParentMenus = ParentMenus.Where(x => MenuRole.Contains(x.Value));




        //        var ddltip = new SelectListItem()
        //        {
        //            Value = "0",
        //            Text = "Root"
        //        };
        //        filterMenuView.Insert(0, ddltip);
        //        return new System.Web.Mvc.SelectList(filterMenuView, "Value", "Text");
        //    }
        //}
    }
}
