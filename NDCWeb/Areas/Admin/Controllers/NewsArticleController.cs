using AutoMapper;
using NDCWeb.Areas.Admin.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Infrastructure.Helpers.Editor;
using NDCWeb.Models;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [CSPLHeaders]
    public class NewsArticleController : Controller
    {
        // GET: NewsArticle
        public async Task<ActionResult> Index()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var newsArticle = await uow.NewsArticleRepo.GetAllAsync();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<NewsArticle>, List<NewsArticleIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<NewsArticle>, IEnumerable<NewsArticleIndxVM>>(newsArticle).ToList();
                await uow.CommitAsync();
                return View(indexDto);
            }
        }

        // GET: NewsArticle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsArticle/Create
        [ValidateInput(true)] // [ValidateInput(true)]
        [HttpPost]
        public async Task<ActionResult> Create(NewsArticleCrtVM objNewsArtclCvm)
        {
            
            if (TinyEditorHelper.CheckEmailExists(objNewsArtclCvm.Description))
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
                        cfg.CreateMap<NewsArticleCrtVM, NewsArticle>();
                    });
                    IMapper mapper = config.CreateMapper();
                    NewsArticle CreateDto = mapper.Map<NewsArticleCrtVM, NewsArticle>(objNewsArtclCvm);
                    
                    //CreateDto.Description = sanitized.Replace("&lt;script&gt", "").Replace("&lt;/script&gt;", "");
                    uow.NewsArticleRepo.Add(CreateDto);
                    await uow.CommitAsync();
                    return RedirectToAction("Create");
                }
            }
        }

        // GET: NewsArticle/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var newsArticle = await uow.NewsArticleRepo.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<NewsArticle, NewsArticleUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                NewsArticleUpVM UpdateDto = mapper.Map<NewsArticle, NewsArticleUpVM>(newsArticle);
                uow.Commit();
                return View(UpdateDto);
            }
        }

        // POST: NewsArticle/Edit/5
        [ValidateInput(false)] // [ValidateInput(true)]
        [HttpPost]
        public async Task<ActionResult> Edit(NewsArticleUpVM objNewsArtclUvm)
        {
            //HtmlSanitizer sanitizer = new HtmlSanitizer();
            if (TinyEditorHelper.CheckEmailExists(objNewsArtclUvm.Description))
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
                        cfg.CreateMap<NewsArticleUpVM, NewsArticle>();
                    });
                    IMapper mapper = config.CreateMapper();
                    NewsArticle UpdateDto = mapper.Map<NewsArticleUpVM, NewsArticle>(objNewsArtclUvm);
                    //string sanitized = sanitizer.Sanitize(objNewsArtclUvm.Description);
                    //UpdateDto.Description = sanitized.Replace("&lt;script&gt", "").Replace("&lt;/script&gt;", "");
                    uow.NewsArticleRepo.Update(UpdateDto);
                    await uow.CommitAsync();
                    return RedirectToAction("Index");
                }
            }
        }
        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var DeleteItem = await uow.NewsArticleRepo.GetByIdAsync(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.NewsArticleRepo.Remove(DeleteItem);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
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
