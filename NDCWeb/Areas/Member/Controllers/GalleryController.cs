using AutoMapper;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Admin.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Models;
using NDCWeb.Persistence;
using NDCWeb.View_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Member.Controllers
{
    [Authorize(Roles = CustomRoles.Candidate)]
    [UserMenu(MenuArea = "Member")]
    [CSPLHeaders]
    public class GalleryController : Controller
    {
        // GET: Member/Gallery
        public ActionResult Index(string mediaCategory)
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetImageGallery(int mediaGalleryId)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var mediaGalry = uow.MediaGalleryRepo.FindAsQuery(x => x.MediaType == MediaType.Image && x.Archive == false, np => np.MediaCategoryMasters, np2 => np2.iMediaFiles);

                if (mediaGalleryId > 0)
                    mediaGalry = mediaGalry.Where(x => x.MediaGalleryId == mediaGalleryId);

                var mediaGalryFiltered = mediaGalry.OrderBy(mg => mg.MediaGalleryId).ToList();

                var config = new MapperConfiguration(cfg => cfg.CreateMap<MediaGallery, HomeBannerSliderVM>());
                IMapper mapper = config.CreateMapper();
                var gallery = mapper.Map<IEnumerable<MediaGallery>, IEnumerable<HomeBannerSliderVM>>(mediaGalryFiltered).ToList();

                //var gallerydt = JsonConvert.SerializeObject(gallery);
                return Json(data: new { gallery = gallery }, JsonRequestBehavior.AllowGet);
            }

        }
        //[HttpPost]
        //public JsonResult GetImageGallery(int mediaCategoryId)
        //{
        //    using (var uow = new UnitOfWork(new NDCWebContext()))
        //    {
        //        var mediaGalry = uow.MediaGalleryRepo.FindAsQuery(x => x.MediaType == MediaType.Image && x.Archive == false, np => np.MediaCategoryMasters, np2 => np2.iMediaFiles);

        //        if (mediaCategoryId > 0)
        //            mediaGalry = mediaGalry.Where(x => x.MediaCategoryId == mediaCategoryId);

        //        var mediaGalryFiltered = mediaGalry.OrderBy(mg => mg.MediaGalleryId).ToList();

        //        var config = new MapperConfiguration(cfg => cfg.CreateMap<MediaGallery, HomeBannerSliderVM>());
        //        IMapper mapper = config.CreateMapper();
        //        var gallery = mapper.Map<IEnumerable<MediaGallery>, IEnumerable<HomeBannerSliderVM>>(mediaGalryFiltered).ToList();

        //        //var gallerydt = JsonConvert.SerializeObject(gallery);
        //        return Json(data: new { gallery = gallery }, JsonRequestBehavior.AllowGet);
        //    }

        //}

        [HttpPost]
        public JsonResult GetGalleryEvent()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var mediaGallery = uow.MediaGalleryRepo.Find(x => x.MediaType == MediaType.Image && x.Archive == false, np => np.iMediaFiles, np2 => np2.MediaCategoryMasters);
                var gallryCtgry = mediaGallery
                        //.Where(c => c.UserRole == "Admin")
                        .OrderByDescending(n => n.MediaGalleryId)
                        .Select(n =>

                        new MemberMediaGalleryVM
                        {
                            MediaGalleryId = n.MediaGalleryId,
                            MediaCategoryId = n.MediaCategoryId,
                            Caption = n.Caption,
                            FilePath = n.iMediaFiles.First().FilePath,
                        }).ToList();

                if (gallryCtgry == null)
                {
                    return Json("Server not Found", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { galleryEvents = gallryCtgry }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}