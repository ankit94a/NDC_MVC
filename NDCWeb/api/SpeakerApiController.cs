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
    [RoutePrefix("api/speaker")]
    public class SpeakerApiController : ApiController
    {
        // GET: api/RankMaster
        [HttpGet]
        [Route("GetSpeakers/topic/{topicId}")]
        public IEnumerable<System.Web.Mvc.SelectListItem> GetSpeakers(int topicId)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                return uow.SpeakerRepo.GetSpeakers(topicId);
            }
        }

        
    }
}
