using AutoMapper;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Models;
using NDCWeb.Persistence;
using NDCWeb.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NDCWeb.api
{
    [RoutePrefix("api/home")]
    public class HomeApiController : ApiController
    {
        // GET: api/HomeApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpGet]
        [Route("GetBannerSlider/category/{mediaCategory}")]
        public IEnumerable<SliderVM> GetBannerSlider(string mediaCategory)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                int mediaCategoryId = uow.MediaCategoryMstrRepo.FirstOrDefault(x => x.MediaCategoryName == mediaCategory).MediaCategoryId;
                var mediaGalry = uow.MediaGalleryRepo.FindAsQuery(x => x.MediaType == MediaType.Image && x.MediaCategoryId == mediaCategoryId && x.Archive == false, np => np.MediaCategoryMasters, np2 => np2.iMediaFiles);

                var mediaGalryFiltered = mediaGalry.OrderByDescending(mg => mg.PublishDate).ToList();

                var config = new MapperConfiguration(cfg => cfg.CreateMap<IEnumerable<MediaGallery>, List<SliderVM>>());
                IMapper mapper = config.CreateMapper();
                var gallery = mapper.Map<IEnumerable<MediaGallery>, IEnumerable<SliderVM>>(mediaGalryFiltered).ToList();
                return gallery;
            }
        }
        [HttpGet]
        [Route("GetComdt/category/{mediaCategory}")]
        public IEnumerable<SliderVM> GetComdt(string mediaCategory)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                int mediaCategoryId = uow.MediaCategoryMstrRepo.FirstOrDefault(x => x.MediaCategoryName == mediaCategory).MediaCategoryId;
                var mediaGalry = uow.MediaGalleryRepo.FindAsQuery(x => x.MediaType == MediaType.Image && x.MediaCategoryId == mediaCategoryId && x.Archive == false, np => np.MediaCategoryMasters, np2 => np2.iMediaFiles);

                var mediaGalryFiltered = mediaGalry.OrderByDescending(mg => mg.PublishDate).ToList();

                var config = new MapperConfiguration(cfg => cfg.CreateMap<IEnumerable<MediaGallery>, List<SliderVM>>());
                IMapper mapper = config.CreateMapper();
                var gallery = mapper.Map<IEnumerable<MediaGallery>, IEnumerable<SliderVM>>(mediaGalryFiltered).ToList();
                return gallery;
            }
        }
        [HttpGet]
        [Route("GetTrainingDoc/category/{mediaCategory}")]
        public IEnumerable<SliderVM> GetTrainingDoc(string mediaCategory)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                int mediaCategoryId = uow.MediaCategoryMstrRepo.FirstOrDefault(x => x.MediaCategoryName == mediaCategory).MediaCategoryId;
                var mediaGalry = uow.MediaGalleryRepo.FindAsQuery(x => x.MediaType == MediaType.Image && x.MediaCategoryId == mediaCategoryId && x.Archive == false, np => np.MediaCategoryMasters, np2 => np2.iMediaFiles);

                var mediaGalryFiltered = mediaGalry.OrderByDescending(mg => mg.PublishDate).ToList();

                var config = new MapperConfiguration(cfg => cfg.CreateMap<IEnumerable<MediaGallery>, List<SliderVM>>());
                IMapper mapper = config.CreateMapper();
                var gallery = mapper.Map<IEnumerable<MediaGallery>, IEnumerable<SliderVM>>(mediaGalryFiltered).ToList();
                return gallery;
            }
        }

        [HttpGet]
        [Route("AlumniGetPhohtoSlider/category/{courseid}")]
        public IEnumerable<SliderVM> GetCourseWiseAlumniPhoto(string courseid)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                int mediaCategoryId = uow.MediaCategoryMstrRepo.FirstOrDefault(x => x.MediaCategoryName == "Alumni Group Photo Gallery").MediaCategoryId;
                var mediaGalry = uow.MediaGalleryRepo.FindAsQuery(x => x.MediaType == MediaType.Image && x.Caption==courseid && x.MediaCategoryId == mediaCategoryId && x.Archive == false, np => np.MediaCategoryMasters, np2 => np2.iMediaFiles);

                var mediaGalryFiltered = mediaGalry.OrderByDescending(mg => mg.PublishDate).ToList();

                var config = new MapperConfiguration(cfg => cfg.CreateMap<IEnumerable<MediaGallery>, List<SliderVM>>());
                IMapper mapper = config.CreateMapper();
                var gallery = mapper.Map<IEnumerable<MediaGallery>, IEnumerable<SliderVM>>(mediaGalryFiltered).ToList();
                return gallery;
            }
        }
        // GET: api/HomeApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/HomeApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/HomeApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/HomeApi/5
        public void Delete(int id)
        {
        }
    }
}
