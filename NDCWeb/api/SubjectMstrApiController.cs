using NDCWeb.Data_Contexts;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NDCWeb.api
{
    [RoutePrefix("api/subjectMstr")]
    public class SubjectMstrApiController : ApiController
    {
        [HttpGet]
        [Route("GetTopics/subject/{subjectId}")]
        public IEnumerable<System.Web.Mvc.SelectListItem> GetTopics(int subjectId)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                return uow.TopicMasterRepository.GetTopics(subjectId);
            }
        }
    }
    
}
