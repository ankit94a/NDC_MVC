using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Member.View_Models
{
    public class RakshikaVM
    {
        public int RakshikaId { get; set; }

        [Required(ErrorMessage = "Enter Spouse Name")]
        [Display(Name = "Spouse Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string SpouseName { get; set; }

        [Required(ErrorMessage = "Enter CourseMember Name")]
        [Display(Name = "CourseMember Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string CourseMemberName { get; set; }

        [Required(ErrorMessage = "Enter Spouse NickName")]
        [Display(Name = "Spouse NickName")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string SpouseNickName { get; set; }

        [Required(ErrorMessage = "Enter Date of Birth")]
        [Display(Name = "Date of Birth")]
        public DateTime DOBirth { get; set; }

        [Required(ErrorMessage = "Enter Qualification")]
        [Display(Name = "Qualification")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Qualification { get; set; }
    }
    public class RakshikaIndxVM : RakshikaVM
    {
    }
    public class RakshikaCrtVM : RakshikaVM
    {
    }
}