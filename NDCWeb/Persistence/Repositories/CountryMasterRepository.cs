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
    public class CountryMasterRepository : Repository<CountryMaster>, ICountryMasterRepository
    {
        public CountryMasterRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<SelectListItem> GetCountries()
        {
            List<SelectListItem> countries = NDCWebContext.CountryMasters
                    .OrderBy(n => n.CountryName)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.CountryId.ToString(),
                            Text = n.CountryName
                        }).ToList();
            return new SelectList(countries, "Value", "Text");
        }

        public IEnumerable<SelectListItem> GetCountriesOnlyName()
        {
            List<SelectListItem> countries = NDCWebContext.CountryMasters
                    .OrderBy(n => n.CountryName)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.CountryName,
                            Text = n.CountryName
                        }).ToList();
            return new SelectList(countries, "Value", "Text");
        }
        public IEnumerable<SelectListItem> GetSelectedCountries(int id)
        {
            var CountriesOptions = NDCWebContext.CountryMasters.Where(x => x.CountryId == id)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.CountryId.ToString(),
                            Text = n.CountryName
                        }).ToList();
            return new SelectList(CountriesOptions, "Value", "Text");
        }

        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}