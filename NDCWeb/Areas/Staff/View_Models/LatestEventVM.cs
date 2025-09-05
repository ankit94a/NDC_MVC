using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Staff.View_Models
{
    public class LatestEventVM
    {
        [Display(Name = "Event Id")]
        public int EventId { get; set; }

        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }

        [Display(Name = "Attend Self")]
        public string AttendSelf { get; set; }

        [Display(Name = "Attend Spouse")]
        public string AttendSpouse { get; set; }

        [Display(Name = "Secy Permited")]
        public string SecyPermited { get; set; }

        [Display(Name = "Dietary Pref Self")]
        public string DietaryPrefSelf { get; set; }

        [Display(Name = "Dietary Pref Spouse")]
        public string DietaryPrefSpouse { get; set; }

        [Display(Name = "LiquorPref")]
        public string LiquorPref { get; set; }
    }
}