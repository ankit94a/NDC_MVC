using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.View_Models
{
    public class VisitorVM
    {
        [Key]
        [Required(ErrorMessage = "Please Enter Visitor ID")]
        [Display(Name = "Visitor Id")]
        public int VisitorId { get; set; }
        [Display(Name = "Menu Id")]
        public int MenuId { get; set; }
        [Display(Name = "Page-Name")]
        public string Slug { get; set; }
        [Display(Name = "Visitor IP Address")]
        public string IpAddress { get; set; }
        [Display(Name = "Visit Date")]
        public DateTime VisitDate { get; set; }
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
    }
    public class LatestVisit:VisitorVM
    {
        
        [Display(Name = "Today")]
        public int Today { get; set; }
        [Display(Name = "Yesturday")]
        public int Yesturday { get; set; }
        [Display(Name = "Page with Highest Visits")]
        public string HighestRankedPage { get; set; }
        [Display(Name = "Page with Minimum Visits")]
        public string LowestRankedPage { get; set; }
        [Display(Name = "Avarage")]
        public string AvaragePage { get; set; }

        [Display(Name = "Overall")]
        public int Overall { get; set; }
        public int TotalPages { get; set; }
        public int TotalMediaUp { get; set; }

    }
}