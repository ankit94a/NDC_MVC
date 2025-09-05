using NDCWeb.Core.IRepositories;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class MediaGalleryRepository : Repository<MediaGallery>, IMediaGalleryRepository
    {
        public MediaGalleryRepository(DbContext context) : base(context)
        {
        }
        public void UpdateRecord(MediaGallery objmediaGallery)
        {
            NDCWebContext.MediaGalleries.Attach(objmediaGallery);
            NDCWebContext.Entry(objmediaGallery).State = EntityState.Modified;
            if (objmediaGallery.iMediaFiles.Count >= 1)
            {
                var removeOldItem = NDCWebContext.MediaFiles.Where(x => x.MediaGalleryId == objmediaGallery.MediaGalleryId).ToList();
                if (removeOldItem != null)
                {
                    NDCWebContext.MediaFiles.RemoveRange(removeOldItem);
                    //NDCWebContext.SaveChanges();
                }
                foreach (var up in objmediaGallery.iMediaFiles)
                {
                    NDCWebContext.MediaFiles.Attach(up);
                    NDCWebContext.Entry(up).State = EntityState.Added;
                    //NDCWebContext.Entry(up).Property(x => x.IsAttend).IsModified = true;
                }
            }
        }

        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }

    }
}