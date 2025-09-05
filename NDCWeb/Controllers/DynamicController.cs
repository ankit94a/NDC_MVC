using System.Web.Mvc;
using System.Threading.Tasks;
using NDCWeb.Persistence;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Infrastructure.Helpers.Account;

namespace NDCWeb.Controllers
{
    [CSPLHeaders]
    [UserMenu(MenuArea = "NA")]
    public class DynamicController : Controller
    {
        // GET: Dynamic

        public async Task<ActionResult> Index()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                await uow.CommitAsync();
                return View();
            }
        }
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
                    //Visitor Tracking
                    UserActivityHelper.SaveVisitor(menuMstr.MenuId, pageContent.MenuItemMasters.PageTitle);

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
                    //Visitor Tracking
                    UserActivityHelper.SaveVisitor(childMenu.MenuId, pageContent.MenuItemMasters.PageTitle);
                    return View();
                }
            }
        }
        public async Task<ActionResult> aboutndc(string parentSlug, string childSlug)
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
                    //Visitor Tracking
                    UserActivityHelper.SaveVisitor(childMenu.MenuId, pageContent.MenuItemMasters.PageTitle);
                    return View();
                }
            }
        }
        public async Task<ActionResult> DynamicL2(string parentSlug, string childSlug)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                //var parentMenu = uow.MenuItemMstrRepo.FirstOrDefaultAsync(x => x.SlugMenu == childSlug);
                var childMenu = await uow.MenuItemMstrRepo.FirstOrDefaultAsync(x => x.SlugMenu == childSlug);
                if (childMenu == null)
                {
                    return HttpNotFound();
                }
                var pageContent = await uow.PageContentRepo.FirstOrDefaultAsync(x => x.MenuId == childMenu.MenuId);
                ViewBag.Title1 = pageContent.MenuItemMasters.PageTitle;
                ViewBag.Heading = pageContent.MenuItemMasters.PageHeading;
                ViewBag.Body = pageContent.Content;
                //Visitor Tracking
                UserActivityHelper.SaveVisitor(childMenu.MenuId, pageContent.MenuItemMasters.PageTitle);
                return View();
            }
        }
        public ActionResult TestyView()
        {
            return View();
        }
        #region Comment Code
        //public ActionResult DynamicAction(string parent, string child)
        //{
        //    //int id;
        //    //var data=GetData From Table Where Id = 1;
        //    PageContent objPgContent = GetPageData(1);
        //    ViewBag.Title1 = objPgContent.PgTitle;
        //    ViewBag.Heading = objPgContent.PgHeading;
        //    ViewBag.Body = objPgContent.PgBody;
        //    return View();
        //}
        #endregion
    }
}