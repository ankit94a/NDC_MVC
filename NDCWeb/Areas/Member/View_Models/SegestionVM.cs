using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Member.View_Models
{
    public class SuggestionVM : BaseEntityVM
    {
        [Required(ErrorMessage = "Feedback Description Not Supplied")]
        [Display(Name = "Feedback Description")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Department Not Supplied")]
        [Display(Name = "Department")]
        public string SuggestionType { get; set; }
    }
    public class SuggestionCrtVM : SuggestionVM
    {

    }
    public class SuggestionIndxVM : SuggestionVM
    {
        [Display(Name = "S.No")]
        public int Sno { get; set; }
        [Display(Name = "Suggestion Id")]
        public int SuggestionId { get; set; }
        
        [Display(Name = "Reply")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Reply { get; set; }

        [Display(Name = "Status")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Status { get; set; }
    }
}