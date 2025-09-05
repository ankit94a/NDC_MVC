using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using NDCWeb.Persistence;
using NDCWeb.Data_Contexts;
using NDCWeb.Areas.Admin.View_Models;
using NDCWeb.Models;
using AutoMapper;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Infrastructure.Helpers.Menu;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Areas.Admin.Models;

namespace NDCWeb.Areas.Admin.Controllers
{
    //[UserMenu]
    [Authorize(Roles = CustomRoles.Admin)]
    [CSPLHeaders]
    public class MenuItemMasterController : Controller
    {
        // GET: MenuItemMaster
        public async Task<ActionResult> Index()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var indexDto = await uow.MenuItemMstrRepo.GetMenuItemListAsync();
                return View(indexDto);

                //var menuItmMstr = await uow.MenuItemMstrRepo.GetAllAsync();
                //var config = new MapperConfiguration(cfg =>
                //{
                //    cfg.CreateMap<MenuItemMaster, MenuItemMasterIndxVM>();
                //});
                //IMapper mapper = config.CreateMapper();
                //var indexDto = mapper.Map<IEnumerable<MenuItemMaster>, IEnumerable<MenuItemMasterIndxVM>>(menuItmMstr).ToList();
                //return View(indexDto);
            }
        }

        // GET: MenuItemMaster/Create
        public async Task<ActionResult> Create()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                //ViewBag.ParentMenus = uow.MenuItemMstrRepo.GetParentMenus();
                ViewBag.MenuArea = CustomDropDownList.GetArea();
                ViewBag.UrlPrefixs = uow.MenuUrlMstrRepo.GetUrlPrefixs();
                await uow.CommitAsync();
                return View();
            }
        }

        // POST: MenuItemMaster/Create
        [HttpPost]
        public async Task<ActionResult> Create(MenuItemMasterCrtVM objMenuItmMstrCvm)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MenuItemMasterCrtVM, MenuItemMaster>();
                });
                IMapper mapper = config.CreateMapper();
                MenuItemMaster CreateDto = mapper.Map<MenuItemMasterCrtVM, MenuItemMaster>(objMenuItmMstrCvm);
                uow.MenuItemMstrRepo.Add(CreateDto);
                await uow.CommitAsync();
                return RedirectToAction("Create");
            }
        }

        // GET: MenuItemMaster/Edit/5
        public ActionResult Edit(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.ParentMenus = uow.MenuItemMstrRepo.GetParentMenus();
                ViewBag.UrlPrefixs = uow.MenuUrlMstrRepo.GetUrlPrefixs();
                ViewBag.MenuArea = CustomDropDownList.GetArea();
                var menuItemMstr = uow.MenuItemMstrRepo.GetById(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MenuItemMaster, MenuItemMasterUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                MenuItemMasterUpVM UpdateDto = mapper.Map<MenuItemMaster, MenuItemMasterUpVM>(menuItemMstr);
                uow.Commit();
                return View(UpdateDto);
            }
        }

        // POST: MenuItemMaster/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(MenuItemMasterUpVM objMenuItemUvm)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MenuItemMasterUpVM, MenuItemMaster>();
                });
                IMapper mapper = config.CreateMapper();
                MenuItemMaster UpdateDto = mapper.Map<MenuItemMasterUpVM, MenuItemMaster>(objMenuItemUvm);
                uow.MenuItemMstrRepo.Update(UpdateDto);
                await uow.CommitAsync();
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> MenuSortOrder()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                //ViewBag.menus = uow.MenuItemMstrRepo.GetParentMenus();
                ViewBag.MenuArea = CustomDropDownList.GetArea();
                await uow.CommitAsync();
                return View();
            }
        }
        [HttpPost]
        public ActionResult MenuSortOrder(int[] menuIds)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                int sortorder = 1;
                foreach (int id in menuIds)
                {
                    MenuItemMaster menuitem = uow.MenuItemMstrRepo.GetById(id);
                    menuitem.SortOrder = sortorder;

                    uow.MenuItemMstrRepo.UpdateMenuSortOrder(menuitem);
                    uow.Commit();
                    sortorder += 1;
                }
                //return View(uow.MenuItemMstrRepo.GetAll().OrderBy(p => p.SortOrder).ToList());
                return RedirectToAction("MenuSortOrder");
            }
        }
        [HttpPost]
        public JsonResult GetMenuSortData(int menuId, string menuArea)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var menuSortData = uow.MenuItemMstrRepo.Find(x => x.ParentId == menuId && x.MenuArea == menuArea).OrderBy(x=>x.SortOrder);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<MenuItemMaster>, List<MenuSortOrderVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var data = mapper.Map<IEnumerable<MenuItemMaster>, IEnumerable<MenuSortOrderVM>>(menuSortData).ToList();
                //uow.Commit();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var dataModal = await uow.MenuItemMstrRepo.GetByIdAsync(id);
                if (dataModal == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.MenuItemMstrRepo.Remove(dataModal);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
        [HttpPost]
        public JsonResult GetParentMenus(string area)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var menus = uow.MenuItemMstrRepo.GetParentMenus(area);
                return Json(menus, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Nodes()
        {
            var nodes = TreeViewHelper.GetMenuTree().ToList();
            return Json(nodes, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CollapseTable()
        {
            return View();
        }
    }
}
