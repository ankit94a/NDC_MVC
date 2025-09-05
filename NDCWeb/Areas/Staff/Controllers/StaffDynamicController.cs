using NDCWeb.Areas.Admin.Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Staff.Controllers
{
    [CSPLHeaders]
    [Authorize(Roles = CustomRoles.Staff)]
    [StaffStaticUserMenu]
    //[UserMenu(MenuArea = "Staff")]
    public class StaffDynamicController : Controller
    {
        // GET: Staff/StaffDynamic
        public async Task<ActionResult> DynamicPL1(string slug)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var menuMstr = await uow.MenuItemMstrRepo.FirstOrDefaultAsync(x => x.SlugMenu == slug);
                if (menuMstr == null)
                    return HttpNotFound();
                else
                {
                    var pageContent = await uow.PageContentRepo.FirstOrDefaultAsync(x => x.MenuId == menuMstr.MenuId);
                    if (pageContent == null)
                        return HttpNotFound();
                    ViewBag.Title1 = pageContent.MenuItemMasters.PageTitle;
                    ViewBag.Heading = pageContent.MenuItemMasters.PageHeading;
                    ViewBag.Body = pageContent.Content;
                    return View();
                }
            }
        }
        public async Task<ActionResult> DynamicL1(string parentSlug, string childSlug)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                //var parentMenu = uow.MenuItemMstrRepo.FirstOrDefaultAsync(x => x.SlugMenu == childSlug);
                var childMenu = await uow.MenuItemMstrRepo.FirstOrDefaultAsync(x => x.SlugMenu == childSlug);
                if (childMenu == null)
                    return HttpNotFound();
                else
                {
                    var pageContent = await uow.PageContentRepo.FirstOrDefaultAsync(x => x.MenuId == childMenu.MenuId);
                    if (pageContent == null)
                        return HttpNotFound();
                    ViewBag.Title1 = pageContent.MenuItemMasters.PageTitle;
                    ViewBag.Heading = pageContent.MenuItemMasters.PageHeading;
                    ViewBag.Body = pageContent.Content;
                    return View();
                }
            }
        }
    }
}