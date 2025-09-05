using NDCWeb.Areas.Admin.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace NDCWeb.api
{
    [RoutePrefix("api/mediamasters")]
    [Authorize]
    public class MediaMstrApiController : ApiController
    {
        [HttpPost]
        [Route("MediaGalleryAdd")]
        public async Task<HttpResponseMessage> MediaGalleryAdd([FromBody] MediaGallery mediaGallery)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (mediaGallery == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    if (mediaGallery.MediaCategoryId == 8 || mediaGallery.MediaCategoryId == 9)
                    {
                        foreach (var mf in mediaGallery.iMediaFiles)
                        {
                            if (mediaGallery.MediaCategoryId == 8)
                                mf.FileName = "trg_caledar" + mf.Extension;
                            else if (mediaGallery.MediaCategoryId == 9)
                                mf.FileName = "weekly_trg_programme" + mf.Extension;
                        }
                    }
                    uow.MediaGalleryRepo.Add(mediaGallery);
                    await uow.CommitAsync();

                    return Request.CreateResponse(HttpStatusCode.Created);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("MediaGalleryEdit/{id}")]
        public async Task<HttpResponseMessage> MediaGalleryEdit(int id, [FromBody] MediaGalleryUpVM mediaGallery)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (mediaGallery.MediaCategoryId == 8 || mediaGallery.MediaCategoryId == 9)
                    {
                        foreach (var mf in mediaGallery.iMediaFiles)
                        {
                            if (mediaGallery.MediaCategoryId == 8)
                                mf.FileName = "trg_caledar" + mf.Extension;
                            else if (mediaGallery.MediaCategoryId == 9)
                                mf.FileName = "weekly_trg_programme" + mf.Extension;
                        }
                    }

                    if (mediaGallery.iMediaFiles.Count > 0 && mediaGallery.OverWriteFile == true)
                    {
                        var removeOldItem = uow.MediaFileRepo.Find(x => x.MediaGalleryId == mediaGallery.MediaGalleryId).ToList();
                        if (removeOldItem != null)
                        {
                            uow.MediaFileRepo.RemoveRange(removeOldItem);
                            uow.Commit();
                        }
                    }

                    var entity = uow.MediaGalleryRepo.GetById(id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    entity.MediaType = mediaGallery.MediaType;
                    entity.Caption = mediaGallery.Caption;
                    entity.Description = mediaGallery.Description;
                    entity.Archive = mediaGallery.Archive;
                    entity.PublishDate = mediaGallery.PublishDate;
                    entity.ArchiveDate = mediaGallery.ArchiveDate;
                    entity.MediaCategoryId = mediaGallery.MediaCategoryId;
                    
                    if (mediaGallery.iMediaFiles.Count > 0)
                        entity.iMediaFiles = mediaGallery.iMediaFiles;

                    await uow.CommitAsync();
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
