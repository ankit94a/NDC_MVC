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
    public class StateMasterRepository : Repository<StateMaster>, IStateMasterRepository
    {
        public StateMasterRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<SelectListItem> GetInStepCourses()
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

        public IEnumerable<SelectListItem> GetStates(int countryid)
        {
            List<SelectListItem> states = NDCWebContext.StateMasters
                .Where(n => n.CountryId == countryid)
                    .OrderBy(n => n.StateName)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.StateId.ToString(),
                            Text = n.StateName
                        }).ToList();
            return new SelectList(states, "Value", "Text");
        }

        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}