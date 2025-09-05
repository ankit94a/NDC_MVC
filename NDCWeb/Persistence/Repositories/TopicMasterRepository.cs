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
    public class TopicMasterRepository : Repository<TopicMaster>, ITopicMasterRepository
    {
        public TopicMasterRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<SelectListItem> GetTopics()
        {
            List<SelectListItem> countries = NDCWebContext.TopicMasters
                    .OrderBy(n => n.TopicName)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.TopicId.ToString(),
                            Text = n.TopicName
                        }).ToList();
            return new SelectList(countries, "Value", "Text");
        }
        
        public IEnumerable<SelectListItem> GetTopics(int subjectId)
        {
            List<SelectListItem> topics = NDCWebContext.TopicMasters
                .Where(n => n.SubjectId == subjectId)
                    .OrderBy(n => n.TopicName)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.TopicId.ToString(),
                            Text = n.TopicName
                        }).ToList();
            return new SelectList(topics, "Value", "Text");
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}