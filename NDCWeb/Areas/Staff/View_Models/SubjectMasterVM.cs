using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Staff.View_Models
{
    public class SubjectMasterVM
    {
        [Key]
        [Required(ErrorMessage = "Subject Id Not Supplied")]
        [Display(Name = "Subject Id")]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Subject Name Not Supplied")]
        [Display(Name = "Subject Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string SubjectName { get; set; }

        [Required(ErrorMessage = "Subject Code Not Supplied")]
        [Display(Name = "Subject Code")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Code { get; set; }
    }
    public class SubjectMasterIndexVM : SubjectMasterVM
    {

    }
    public class SubjectMasterCrtVM : SubjectMasterVM
    {

    }
    public class SubjectMasterUpVM : SubjectMasterVM
    {

    }
}