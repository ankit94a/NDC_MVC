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
    public class HolidayCalendarRepository : Repository<HolidayCalendar>, IHolidayCalendarRepository
    {
        public HolidayCalendarRepository(DbContext context) : base(context)
        {

        }
        public IEnumerable<SelectListItem> GetHolidayTypeIndia()
        {
            var HolidayOptions = new List<SelectListItem>
                {
                    new SelectListItem{ Text="Closed Holidays of India", Value = "Closed Holidays of India"},
                    new SelectListItem{ Text="Restricted  Holidays of India", Value = "Restricted  Holidays of India" },
                    new SelectListItem{ Text="Independence Day", Value = "Independence Day" },
                    new SelectListItem{ Text="National Day", Value = "National Day" },
                };
            return HolidayOptions;
        }
        public IEnumerable<SelectListItem> GetHolidayTypeOther()
        {
            var HolidayOptions = new List<SelectListItem>
            {
                new SelectListItem{ Text="Independence Day", Value = "Independence Day" },
                new SelectListItem{ Text="National Day", Value = "National Day" },
            };
            return HolidayOptions;
        }
    }
}