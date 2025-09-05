using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Staff.View_Models
{
    public class EventVM
    {
        [Required(ErrorMessage = "Select Event Id")]
        [Display(Name = "Event Id")]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Enter Name of Party/Event")]
        [Display(Name = "Name of Party/Function")]
        public string EventName { get; set; }

        [Required(ErrorMessage = "Enter Venu")]
        [Display(Name = "Venue")]
        [RegularExpression(@"^[\w \.\,\-]*$", ErrorMessage = "Special chars not allowed")]
        public string EventVenu { get; set; }

        [Required(ErrorMessage = "Enter Date")]
        [Display(Name = "Function Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Enter Time")]
        [Display(Name = "Function Time")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EventTime { get; set; }

        [Required(ErrorMessage = "Enter Dress Code")]
        [Display(Name = "Dress Code")]
        [RegularExpression(@"^[\w \.\,\-]*$", ErrorMessage = "Special chars not allowed")]
        public string EventDress { get; set; }

        [Display(Name = "Remarks/Instructions (if any)")]
        [RegularExpression(@"^[\w \.\,\-]*$", ErrorMessage = "Special chars not allowed")]
        public string Remarks { get; set; }
    }
    public class EventCreateVM :EventVM
    { 
    
    }
    public class EventUpdVM : EventVM
    {

    }
    public class EventIndexVM : EventVM
    {

    }

}