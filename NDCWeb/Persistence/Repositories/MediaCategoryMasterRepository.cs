using NDCWeb.Core.IRepositories;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Persistence.Repositories
{
    public class MediaCategoryMasterRepository : Repository<MediaCategoryMaster>, IMediaCategoryMasterRepository
    {
        public MediaCategoryMasterRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<SelectListItem> GetMediaCategories()
        {
            List<SelectListItem> mediaCategories = NDCWebContext.MediaCategoryMstr
                .OrderBy(n => n.MediaCategoryName)
                .Select(n =>
                new SelectListItem
                {
                    Value = n.MediaCategoryId.ToString(),
                    Text = n.MediaCategoryName
                }).ToList();
            var ddltip = new SelectListItem()
            {
                Value = null,
                Text = "-- Select --"
            };
            mediaCategories.Insert(0, ddltip);
            return new SelectList(mediaCategories, "Value", "Text");
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}