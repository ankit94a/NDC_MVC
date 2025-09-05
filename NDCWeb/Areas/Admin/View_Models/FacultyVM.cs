using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Admin.View_Models
{
    public class FacultyVM : BaseEntityVM
    {
        [Required(ErrorMessage = "Please Enter Faculty Id")]
        [Display(Name = "Faculty Id")]
        public int FacultyId { get; set; }

        [Required(ErrorMessage = "Please Enter Faculty")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Faculty")]
        public string FacultyName { get; set; }

        [Required(ErrorMessage = "Please Enter Designation")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Select Type")]
        [Display(Name = "Type")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Select Staff Type")]
        [Display(Name = "Staff Type")]
        public string StaffType { get; set; }
    }
    public class FacultyIndxVM : FacultyVM
    {
    }
    public class FacultyCrtVM : FacultyVM
    {
    }
    public class FacultyUpVM : FacultyVM
    {
    }
}