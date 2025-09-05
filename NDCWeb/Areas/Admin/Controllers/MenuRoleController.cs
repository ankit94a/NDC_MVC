using AutoMapper;
using NDCWeb.Areas.Admin.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Models;
using NDCWeb.Persistence;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Filters;

namespace NDCWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [CSPLHeaders]
    public class MenuRoleController : Controller
    {
        private ApplicationRoleManager _roleManager;
        public MenuRoleController()
        {
        }
        public MenuRoleController(ApplicationRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        
        // GET: Admin/MenuRoles/Create
        public ActionResult Create()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.MenuArea = CustomDropDownList.GetArea();
                ViewBag.Roles = RoleManager.Roles.Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }).ToList();
                //ViewBag.UrlPrefixs = uow.MenuUrlMstrRepo.GetUrlPrefixs();
                return View();
            }
        }
        
        // POST: Admin/MenuRoles/Create
        [HttpPost]
        public ActionResult Create(MenuRoleCrtVM objMenuRoleCvm, int[] selChkbxMenuIds, int[] selChkRead, int[] selChkWrite)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (selChkbxMenuIds != null)
                    {
                        int cn = selChkbxMenuIds.Count();
                        List<MenuRole> objmenuRoleList = new List<MenuRole>();
                        for (int i = 0; i < cn; i++)
                        {
                            MenuRole objMenu = new MenuRole();
                            objMenu.RoleId = objMenuRoleCvm.RoleId;
                            objMenu.MenuId = selChkbxMenuIds[i];
                            if (selChkRead != null)
                            {
                                if (selChkRead.Contains(selChkbxMenuIds[i])) objMenu.Read = true;
                                else objMenu.Read = false;
                            }
                            if (selChkWrite != null)
                            {
                                if (selChkWrite.Contains(selChkbxMenuIds[i])) objMenu.Write = true;
                                else objMenu.Write = false;
                            }
                            objmenuRoleList.Add(objMenu);
                        }
                        
                        var menurolelist = uow.MenuRoleRepo.Find(x => x.RoleId == objMenuRoleCvm.RoleId && x.MenuItemMasters.MenuArea==objMenuRoleCvm.MenuArea);
                        if(menurolelist!=null)
                        {
                            uow.MenuRoleRepo.RemoveRange(menurolelist);
                            uow.Commit();
                        }
                        uow.MenuRoleRepo.AddRange(objmenuRoleList);
                        uow.Commit();
                    }
                    return RedirectToAction("Create");
                }
            }
            finally { }
        }
        
        [HttpPost]
        public JsonResult GetMenuViewList(int roleId, string menuArea)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var menuViewData = uow.MenuItemMstrRepo.GetAllAsQuery();
                var filterMenuView = (from p in menuViewData
                            join q in menuViewData on p.ParentId equals q.MenuId
                            into ps
                            from q in ps.DefaultIfEmpty()
                            where p.MenuArea == menuArea
                            orderby q.MenuName
                            select new MenuRoleMenuitem
                            {
                                MenuId = p.MenuId,
                                ParentId = p.ParentId,
                                MenuName = (p.MenuUrlMasters.PageType != PageType.Static ? (p.MenuUrlMasters.UrlPrefix + "/") : "")
                                            + (q.MenuName != null ? (q.MenuName + "/") : "")
                                            + p.MenuName,
                                SlugMenu = p.SlugMenu,
                                SortOrder = p.SortOrder
                            }).ToList();

                var menurole = uow.MenuRoleRepo.Find(x => x.RoleId == roleId && x.MenuItemMasters.MenuArea == menuArea);
                if (menurole != null)
                {
                    foreach (var mr in menurole)
                    {
                        var itemToChange = filterMenuView.FirstOrDefault(d => d.MenuId == mr.MenuId);
                        if (itemToChange != null)
                        {
                            itemToChange.Read = mr.Read;
                            itemToChange.Write = mr.Write;
                        }
                    }
                }
                //uow.Commit();
                if(filterMenuView ==null)
                    return Json(data: new { flag = 0 }, JsonRequestBehavior.AllowGet);
                return Json(data: new { menus = filterMenuView, flag=1}, JsonRequestBehavior.AllowGet);
            }
        }
        
    }
}
