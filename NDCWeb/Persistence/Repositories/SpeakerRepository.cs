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
    public class SpeakerRepository : Repository<Speaker>, ISpeakerRepository
    {
        public SpeakerRepository(DbContext context) : base(context)
        {

        }
        public IEnumerable<SelectListItem> GetSpeakers(int topicId)
        {
            List<SelectListItem> speakers = NDCWebContext.Speakers
                .Where(n => n.TopicId == topicId)
                    .OrderBy(n => n.FullName)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.SpeakerId.ToString(),
                            Text = n.FullName
                        }).ToList();
            return new SelectList(speakers, "Value", "Text");
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}