using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Staff.View_Models
{
    public class HolidayCalendarVM
    {
        [Key]
        [Required(ErrorMessage = "Holiday Calendar Id Not Supplied")]
        [Display(Name = "Holiday Calendar Id")]
        public int HolidayCalendarId { get; set; }

        [Required(ErrorMessage = "Description Not Supplied")]
        [Display(Name = "Short Description")]
        [RegularExpression(@"^[\w \.\,\-]*$", ErrorMessage = "Special chars not allowed")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Date Not Supplied")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        public DateTime HolidayDate { get; set; }

        [Required(ErrorMessage = "Holiday Type Not Supplied")]
        [Display(Name = "Holiday Type")]
        public string HolidayType { get; set; }

        [Required(ErrorMessage = "Country Not Supplied")]
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        public virtual CountryMaster Countrys { get; set; }
    }
    public class HolidayCalendarAddVM 
    {
        [Required(ErrorMessage = "Country Not Supplied")]
        [Display(Name = "Country")]
        public int CountryId { get; set; }
    }
    public class HolidayCalendarCrtVM : HolidayCalendarVM
    {

    }
    public class HolidayCalendarUpVM : HolidayCalendarVM
    {
    }
    public class HolidayCalendarIndxVM : HolidayCalendarVM
    {
        [Display(Name = "Month")]
        public int Month { get; set; }

        [Display(Name = "Day")]
        public int Day { get; set; }

        [Display(Name = "Country")]
        public string CountryName { get; set; }

    }
    public class HolidayCalendarDetailsVM : HolidayCalendarVM
    {

    }
    public class GetHolidayCalendarVM
    {
        [Display(Name = "Holiday Calendar Id")]
        public int HolidayCalendarId { get; set; }

        [Display(Name = "Short Description")]
        public string Description { get; set; }

        [Display(Name = "Date")]
        public DateTime StartDate { get; set; }
        
        [Display(Name = "Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Color")]
        public string ColorCode { get; set; }

        [Display(Name = "Holiday Type")]
        public string HolidayType { get; set; }

        [Display(Name = "Country")]
        public string CountryName { get; set; }

    }
    public class HolidayCalendarGroupVM
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        public DateTime HolidayDate { get; set; }

        [Display(Name = "Holiday Type")]
        public string HolidayType { get; set; }
    }
}