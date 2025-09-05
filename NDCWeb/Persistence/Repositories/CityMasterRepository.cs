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
    public class CityMasterRepository : Repository<CityMaster>, ICityMasterRepository
    {
        public CityMasterRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<SelectListItem> GetCities(int stateid)
        {
            List<SelectListItem> cities = NDCWebContext.CityMasters
                .Where(n => n.StateId == stateid)
                    .OrderBy(n => n.CityName)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.CityId.ToString(),
                            Text = n.CityName
                        }).ToList();
            return new SelectList(cities, "Value", "Text");
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}