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
    [RoutePrefix("api/countrystatecity")]
    public class CountryStateCityApiController : ApiController
    {
        // GET: api/CountryStateCityApi
        [HttpGet]
        [Route("GetCountries")]
        public IEnumerable<System.Web.Mvc.SelectListItem> GetCountries()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                return uow.CountryMasterRepo.GetCountries();
            }
        }
        [HttpGet]
        [Route("GetStates/country/{countryId}")]
        public IEnumerable<System.Web.Mvc.SelectListItem> GetStates(int countryId)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                return uow.StateMasterRepo.GetStates(countryId);
            }
        }
        [HttpGet]
        [Route("GetCities/state/{stateId}")]
        public IEnumerable<System.Web.Mvc.SelectListItem> GetCities(int stateId)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                return uow.CityMasterRepo.GetCities(stateId);
            }
        }

        [HttpGet]
        [Route("GetCountriesNameDDL")]
        public IEnumerable<System.Web.Mvc.SelectListItem> GetCountriesNameDDL()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                return uow.CountryMasterRepo.GetCountriesOnlyName();
            }
        }
    }
}
