using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Member.View_Models
{
    public class SocialCalendarVM
    {
        public string Fullname { get; set; }
        public string Coursename { get; set; }
        public string LockerNo { get; set; }
    }
    public class BirthDaysVM: SocialCalendarVM
    {
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }
        public DateTime Dob { get; set; }
        public string AgeNow { get; set; }

    }
    public class AnniversaryVM : SocialCalendarVM
    {
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Anniversary { get; set; }
        public string MarriedAge { get; set; }
    }
}