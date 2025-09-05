using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NDCWeb.api
{
    [RoutePrefix("api/rankMasters")]

    public class RankMasterApiController : ApiController    
    {
        // GET: api/RankMaster
        [HttpGet]
        [Route("GetRanks/service/{service}")]
        public IEnumerable<System.Web.Mvc.SelectListItem> GetRanks(string service)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                return uow.RankMasterRepo.GetRanks(service);
            }
        }
        [HttpGet]
        [Route("GetParticipantRanks/service/{service}")]
        public IEnumerable<System.Web.Mvc.SelectListItem> GetRanksForParticipants(string service)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                return uow.RankMasterRepo.GetRanksParticipants(service);
            }
        }
        [HttpGet]
        public IEnumerable<RankMaster> Get()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                return uow.RankMasterRepo.GetAll();
            }
        }
        [HttpGet]
        [Route("GetInstepRanks/service/{service}")]
        public IEnumerable<System.Web.Mvc.SelectListItem> GetInstepRanks(string service)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                return uow.RankMasterRepo.GetRanksInstep(service);
            }
        }
        // GET: api/RankMasterApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/RankMasterApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/RankMasterApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/RankMasterApi/5
        public void Delete(int id)
        {
        }
    }
}
