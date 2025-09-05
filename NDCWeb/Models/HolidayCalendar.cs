using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class HolidayCalendar : BaseEntity
    {
        [Key]
        public int HolidayCalendarId { get; set; }
        public string Description { get; set; }
        public string HolidayType { get; set; }
        public DateTime HolidayDate { get; set; }
        public int Month { get; set; }
        public int Day {get; set; }
        public string ColorCode { get; set; }
        public int CountryId { get; set; }
        public virtual CountryMaster Countrys { get; set; }
    }
}