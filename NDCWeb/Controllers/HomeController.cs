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
using NDCWeb.Infrastructure.Helpers.Menu;
using System.Text.RegularExpressions;
using NDCWeb.View_Models;
using System.Data.Entity;
using Newtonsoft.Json;
using NDCWeb.Infrastructure.Helpers.Account;

namespace NDCWeb.Controllers
{
    [CSPLHeaders]
    [UserMenu(MenuArea = "NA")]
    //[SessionTimeout]
    public class HomeController : Controller
    {
        //[OutputCache(Duration = 600)]
        public ActionResult Index()
        {
            ViewBag.WhatsNew = GetWhatsNew();
            ViewBag.Flash = GetFlash();
            ViewBag.Upcoming = Upcoming();
            ViewBag.LastMonth = GetLastMonthHighlights();
            ViewBag.NdcNews = GetNdcNews();
            UserActivityHelper.SaveVisitor(1, "Home-Page");
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var course = uow.CourseRepo.Find(x => x.UnderRegistration == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                var instepcourse = uow.InStepCourseRepo.Find(x => x.UnderRegistration == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                if (course != null)               
                    ViewBag.Course = course.CourseName;              
                else
                    ViewBag.Course = "N";
                if (instepcourse != null)
                    ViewBag.InStepCourse = instepcourse.CourseName;
                else
                    ViewBag.InStepCourse = "N";

            }
            return View();
        }
        public ActionResult ChangeUserPasswordConfirmation()
        {
            return View();
        }
        private List<NewsBulletinVM> GetWhatsNew()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var newsArticle = uow.NewsArticleRepo.GetAll().Where(x => x.NewsCategory == NewsCategory.WhatsNew && x.Archive == false).OrderByDescending(x => x.NewsArticleId);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<NewsArticle>, List<NewsBulletinVM>>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<IEnumerable<NewsArticle>, IEnumerable<NewsBulletinVM>>(newsArticle).ToList();
            }
        }
        private List<NewsBulletinVM> Upcoming()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var newsArticle = uow.NewsArticleRepo.GetAll().Where(x => x.NewsCategory == NewsCategory.Upcoming && x.Archive == false).OrderByDescending(x => x.NewsArticleId);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<NewsArticle>, List<NewsBulletinVM>>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<IEnumerable<NewsArticle>, IEnumerable<NewsBulletinVM>>(newsArticle).ToList();
            }
        }
        private List<NewsBulletinVM> GetFlash()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var newsArticle = uow.NewsArticleRepo.GetAll().Where(x => x.NewsCategory == NewsCategory.Flash && x.Archive == false).OrderByDescending(x => x.NewsArticleId);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<NewsArticle>, List<NewsBulletinVM>>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<IEnumerable<NewsArticle>, IEnumerable<NewsBulletinVM>>(newsArticle).ToList();
            }
        }
        private List<NewsBulletinVM> GetLastMonthHighlights()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var newsArticle = uow.NewsArticleRepo.GetAll().Where(x => x.NewsCategory == NewsCategory.LastMonth && x.Archive == false).OrderByDescending(x => x.NewsArticleId);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<NewsArticle>, List<NewsBulletinVM>>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<IEnumerable<NewsArticle>, IEnumerable<NewsBulletinVM>>(newsArticle).ToList();
            }
        }
        private List<NewsBulletinVM> GetNdcNews()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var newsArticle = uow.NewsArticleRepo.GetAll().Where(x => x.NewsCategory == NewsCategory.NDC && x.Archive == false).OrderByDescending(x => x.NewsArticleId);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<NewsArticle>, List<NewsBulletinVM>>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<IEnumerable<NewsArticle>, IEnumerable<NewsBulletinVM>>(newsArticle).ToList();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Floater()
        {
                       return View();
        }
        public ActionResult pagedesigner()
        {
            ViewBag.Message = "Page Designer. Do not use for Publishing!!";

            return View();
        }
        [HttpPost]
        public JsonResult GetBannerSlider()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                int mediaCategoryId = uow.MediaCategoryMstrRepo.FirstOrDefault(x => x.MediaCategoryName == "Main Home Page Banner").MediaCategoryId;
                var mediaGalry = uow.MediaGalleryRepo.FindAsQuery(x => x.MediaType == MediaType.Image && x.MediaCategoryId == mediaCategoryId && x.Archive == false, np => np.MediaCategoryMasters, np2 => np2.iMediaFiles);
                
                var mediaGalryFiltered = mediaGalry.OrderBy(mg => mg.PublishDate).ToList();

                var config = new MapperConfiguration(cfg => cfg.CreateMap<IEnumerable<MediaGallery>, List<HomeBannerSliderVM>>());
                IMapper mapper = config.CreateMapper();
                var gallery = mapper.Map<IEnumerable<MediaGallery>, IEnumerable<HomeBannerSliderVM>>(mediaGalryFiltered).ToList();

                //var gallerydt = JsonConvert.SerializeObject(gallery);
                return Json(data: new { gallery = gallery }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FAQs()
        {
            // ViewBag.Message = "Your contact page.";

            return View();
        }
        //public string Version()
        //{
        //    return "<h2>The Installed Mvc Version In your System Is : " + typeof(Controller).Assembly.GetName().Version.ToString() + "</h2>";
        //}
        //public ActionResult NewIndex()
        //{
        //    return View();
        //}
    }
}