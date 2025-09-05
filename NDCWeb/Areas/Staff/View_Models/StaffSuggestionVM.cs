using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Staff.View_Models
{
    public class StaffSuggestionVM : BaseEntityVM
    {
        [Display(Name = "Suggestion Id")]
        public int SuggestionId { get; set; }

        [Required(ErrorMessage = "Reply Not Supplied")]
        [Display(Name = "Reply")]
        [RegularExpression(@"^[\w \.\,\-]*$", ErrorMessage = "Special chars not allowed")]
        public string Reply { get; set; }

        [Required(ErrorMessage = "Status Not Supplied")]
        [Display(Name = "Status")]
        [RegularExpression(@"^[\w \.\,\-]*$", ErrorMessage = "Special chars not allowed")]
        public string Status { get; set; }
    }
    public class StaffSuggestionUpdVM : StaffSuggestionVM
    {

    }
    public class StaffSuggestionIndxVM : StaffSuggestionVM
    {
        [Display(Name = "S.No")]
        public int Sno { get; set; }

        [Display(Name = "Feedback Description")]
        [RegularExpression(@"^[\w \.\,\-]*$", ErrorMessage = "Special chars not allowed")]
        public string Description { get; set; }

        [Display(Name = "Department")]
        public string SuggestionType { get; set; }

        [Display(Name = "Locker No.")]
        public string LockerNo { get; set; }

    }
}