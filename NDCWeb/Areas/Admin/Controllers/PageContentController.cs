using NDCWeb.Data_Contexts;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using System.IO;
using NDCWeb.Models;
using NDCWeb.Areas.Admin.View_Models;
using NDCWeb.Infrastructure.Helpers.Routing;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Helpers.FileDirectory;
using NDCWeb.Infrastructure.Helpers.Editor;
using NDCWeb.Infrastructure.Helpers.Account;
using NDCWeb.Infrastructure.Filters;
//using Ganss.Xss;
using NDCWeb.Infrastructure.Extensions;
using System.Web.Http.Results;

namespace NDCWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [CSPLHeaders]
    public class PageContentController : Controller
    {
        // GET: PageContent
        public async Task<ActionResult> Index()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                int roleid = UserAccountHelper.getCurrentUserRoleId();
                var MenuRole = uow.MenuRoleRepo.Find(x => x.RoleId == roleid && x.Read == true).Select(x => x.MenuId);
                var pageContent = await uow.PageContentRepo.GetPageContentListAsync();
                var pageContentList = pageContent.Where(x => MenuRole.Contains(x.MenuId));
                return View(pageContentList);
            }
            //using (var uow = new UnitOfWork(new NDCWebContext()))
            //{
            //    int roleid = UserAccountHelper.getCurrentUserRoleId();
            //    var MenuRole = uow.MenuRoleRepo.Find(x => x.RoleId == roleid && x.Read == true).Select(x => x.MenuId);
            //    var pageContent = uow.PageContentRepo.Find(x => MenuRole.Contains(x.MenuId));
            //    var config = new MapperConfiguration(cfg =>
            //    {
            //        cfg.CreateMap<IEnumerable<PageContent>, List<PageContentIndxVM>>();
            //    });
            //    IMapper mapper = config.CreateMapper();
            //    var indexDto = mapper.Map<IEnumerable<PageContent>, IEnumerable<PageContentIndxVM>>(pageContent).ToList();
            //    return View(indexDto);
            //}
        }

        // GET: PageContent/Create
        public async Task<ActionResult> Create()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.MenuArea = CustomDropDownList.GetArea();
                int roleid = UserAccountHelper.getCurrentUserRoleId();
                var MenuRole = uow.MenuRoleRepo.Find(x => x.RoleId == roleid && x.Write == true).Select(x => (x.MenuId).ToString());
                var ParentMenus = uow.MenuItemMstrRepo.GetParentMenus();
                ViewBag.ParentMenus = ParentMenus.Where(x => MenuRole.Contains(x.Value));
                await uow.CommitAsync();
                return View();
            }
        }

        // POST: PageContent/Create
        [ValidateInput(false)]  //Set true for audit
        [HttpPost]
        public async Task<ActionResult> Create(PageContentCrtVM objPageContCvm)
        {
            //HtmlSanitizer sanitizer = new HtmlSanitizer();
            if (TinyEditorHelper.CheckEmailExists(objPageContCvm.Content))
            {
                this.AddNotification("Email address detected. Please ensure you use '[dot]' for '.', '[at]' for '@', and '[hyphen]' for '-'.", NotificationType.WARNING);
                return RedirectToAction("Create");
            }
            else
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<PageContentCrtVM, PageContent>();
                    });
                    IMapper mapper = config.CreateMapper();
                    PageContent CreateDto = mapper.Map<PageContentCrtVM, PageContent>(objPageContCvm);
                    //string sanitized = sanitizer.Sanitize(objPageContCvm.Content);
                    //CreateDto.Content = sanitized.Replace("&lt;script&gt", "").Replace("&lt;/script&gt;", "");

                    uow.PageContentRepo.Add(CreateDto);
                    await uow.CommitAsync();
                    return RedirectToAction("Create");
                }
            }
        }

        // GET: PageContent/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                int roleid = UserAccountHelper.getCurrentUserRoleId();
                var MenuRole = uow.MenuRoleRepo.Find(x => x.RoleId == roleid && x.Write == true).Select(x => (x.MenuId).ToString());
                var ParentMenus = uow.MenuItemMstrRepo.GetParentMenus();
                ViewBag.ParentMenus = ParentMenus.Where(x => MenuRole.Contains(x.Value));
                var pgContntMstr = uow.PageContentRepo.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<PageContent, PageContentUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                PageContentUpVM UpdateDto = mapper.Map<PageContent, PageContentUpVM>(await pgContntMstr);
                await uow.CommitAsync();
                return View(UpdateDto);
            }
        }

        // POST: PageContent/Edit/5
        [ValidateInput(false)]  //Set true for audit
        [HttpPost]
        public async Task<ActionResult> Edit(PageContentUpVM objPageContUvm)
        {
            //HtmlSanitizer sanitizer = new HtmlSanitizer();
            //if (TinyEditorHelper.CheckEmailExists(objPageContUvm.Content))
            //{
            //    this.AddNotification("Email address detected. Please ensure you use '[dot]' for '.', '[at]' for '@', and '[hyphen]' for '-'.", NotificationType.WARNING);
            //    return RedirectToAction("Edit");
            //}
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<PageContentUpVM, PageContent>();
                });
                IMapper mapper = config.CreateMapper();
                PageContent UpdateDTO = mapper.Map<PageContentUpVM, PageContent>(objPageContUvm);
                //string sanitized = sanitizer.Sanitize(objPageContUvm.Content);
                //UpdateDTO.Content = sanitized.Replace("&lt;script&gt", "").Replace("&lt;/script&gt;", "");
                uow.PageContentRepo.Update(UpdateDTO);
                await uow.CommitAsync();
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var dataModal = await uow.PageContentRepo.GetByIdAsync(id);
                if (dataModal == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.PageContentRepo.Remove(dataModal);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
        public async Task<ActionResult> PagePreview(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var pageContent = await uow.PageContentRepo.GetByIdAsync(id);
                ViewBag.Content = pageContent.Content;
                await uow.CommitAsync();
                return View();
            }
        }

        [HttpPost]
        public ActionResult ImageUpload(HttpPostedFileBase file)
        {
            string currentYear = DateTime.Now.Year.ToString();
            string currentMonth = DateTime.Now.ToString("MMM");
            string path = Path.Combine(ServerRootConsts.NEWS_CONTENT_ROOT, "images", currentYear, currentMonth);
            var location = TinyEditorHelper.SaveImageFile(Server.MapPath(path), file, path);
            return Json(new { location }, JsonRequestBehavior.AllowGet);
        }
    }
}
