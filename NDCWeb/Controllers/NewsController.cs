using System.Web.Mvc;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Models;
using NDCWeb.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NDCWeb.Areas.Admin.View_Models;
using AutoMapper;
using System;
using System.Data.Entity;
using NDCWeb.View_Models;
using Newtonsoft.Json;

namespace NDCWeb.Controllers
{
    [CSPLHeaders]
    [UserMenu(MenuArea = "NA")]
    public class NewsController : Controller
    {
        // GET: News    
        //public async Task<ActionResult> Index()
        //{
        //    using (var uow = new UnitOfWork(new NDCWebContext()))
        //    {
        //        var newsArticle = await uow.NewsArticleRepo.GetAllAsync();
        //        var newsList = from n in newsArticle
        //                       where n.NewsCategory == NewsCategory.WhatsNew
        //                       select n;
        //        foreach (var news in newsArticle)
        //        {

        //        }
        //        var config = new MapperConfiguration(cfg =>
        //        {
        //            cfg.CreateMap<IEnumerable<NewsArticle>, List<NewsArticleIndxVM>>();
        //        });
        //        IMapper mapper = config.CreateMapper();
        //        var indexDto = mapper.Map<IEnumerable<NewsArticle>, IEnumerable<NewsArticleIndxVM>>(newsArticle).ToList();
        //        await uow.CommitAsync();

        //        return View(indexDto);
        //    }
        //}
        public async Task<ActionResult> Article(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var newsArticle = await uow.NewsArticleRepo.GetByIdAsync(id);
                if (newsArticle == null)
                    return HttpNotFound();
                else
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<NewsArticle, NewsArticleIndxVM>();
                    });
                    IMapper mapper = config.CreateMapper();
                    var indexDto = mapper.Map<NewsArticle, NewsArticleIndxVM>(newsArticle);
                    await uow.CommitAsync();

                    return View(indexDto);
                }  
            }
        }
        public ActionResult Index(NewsCategory ncat)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var newsArticle = uow.NewsArticleRepo.GetAll().Where(x => x.NewsCategory == ncat && x.Archive == false);
                var newsList = from n in newsArticle
                               where n.NewsCategory == ncat
                               select n;
                if (newsArticle == null)
                    return HttpNotFound();
                else
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<IEnumerable<NewsArticle>, List<NewsBulletinVM>>();
                    });
                    IMapper mapper = config.CreateMapper();
                    var indexDto = mapper.Map<IEnumerable<NewsArticle>, IEnumerable<NewsBulletinVM>>(newsArticle);                   

                    return View(indexDto);
                }
            }
        }

        public ActionResult ArticleFilter()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetNewsArticle(int pageIndex, int newsCategoryId, string fromDate, string toDate, bool archive)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var nwsArticle = uow.NewsArticleRepo.FindAsQuery(x => x.Archive == archive);

                if (newsCategoryId > 0)
                {
                    nwsArticle = nwsArticle.Where(x => x.NewsCategory == (NewsCategory)newsCategoryId);
                }
                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    DateTime dfromDate = DateTime.ParseExact(fromDate, "MM/dd/yyyy", null);
                    DateTime dtoDate = DateTime.ParseExact(toDate, "MM/dd/yyyy", null);
                    nwsArticle = nwsArticle.Where(x => DbFunctions.TruncateTime(x.PublishDate) >= DbFunctions.TruncateTime(dfromDate) && DbFunctions.TruncateTime(x.PublishDate) <= DbFunctions.TruncateTime(dtoDate));
                }

                PagingVM pagingParam = new PagingVM();
                pagingParam.PageIndex = pageIndex;
                pagingParam.PageSize = 10;
                pagingParam.RecordCount = nwsArticle.Count();
                int startIndex = (pageIndex - 1) * pagingParam.PageSize;
                var newsArticleFiltered = nwsArticle.OrderBy(mg => mg.NewsArticleId)
                                    .Skip(startIndex)
                                    .Take(pagingParam.PageSize).ToList();

                var config = new MapperConfiguration(cfg => cfg.CreateMap<IEnumerable<NewsArticle>, List<HomeNewsVM>>());
                IMapper mapper = config.CreateMapper();
                var article = mapper.Map<IEnumerable<NewsArticle>, IEnumerable<HomeNewsVM>>(newsArticleFiltered).ToList();

                //pagingParam.RecordCount = article.Count();
                var articledt = JsonConvert.SerializeObject(article);
                return Json(new { article = article, pagingParam = pagingParam }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetNewsContent(int newsArticleId)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var nwsArticle = await uow.NewsArticleRepo.GetByIdAsync(newsArticleId);
                return Json(new { newsContent = nwsArticle }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}