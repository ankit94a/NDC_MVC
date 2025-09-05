using NDCWeb.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace NDCWeb.Areas.Admin.View_Models
{
    public class InStepCourseVM
    {
        [Key]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Please enter name of the Course")]
        [Display(Name = "Name of the Course")]
        [RegularExpression(@"^[a-zA-Z0-9 \*\,\.\:\-\\_\'\(\)""\/]+$", ErrorMessage = "Sorry! Special chars are not allowed")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Please the Start date of the Course")]
        [Display(Name = "Course Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please the End date of the Course")]
        [Display(Name = "Course End Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Display(Name = "This Course is running")]
        public bool IsCurrent { get; set; }

        [Display(Name = "Open registration")]
        public bool UnderRegistration { get; set; }

        [Required(ErrorMessage = "Please the set registration Start date")]
        [Display(Name = "Registration Start Date")]
        [RequiredIf(nameof(UnderRegistration), true)]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? RegistrationStartDate { get; set; }

        [Required(ErrorMessage = "Please the set registration End date")]
        [Display(Name = "Registration End Date")]
        [RequiredIf(nameof(UnderRegistration), true)]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? RegistrationEndDate { get; set; }
        public int TotalStrength { get; set; }
    }
    public class InStepCourseCreateVM: InStepCourseVM
    {

    }
    public class InStepCourseUpVM : InStepCourseVM
    {

    }
    public class InStepCourseIndexVM : InStepCourseVM
    {

    }
}