using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.View_Models
{
    public class SiteFeedbackVM
    {
        [Key]

        [Required(ErrorMessage = "Feedback Id is Must")]
        [Display(Name = "Feedback Id")]

        public int FeedbackId { get; set; }

        [Required(ErrorMessage = "Department Selection is Must")]
        [Display(Name = "Department")]
        public string DepartmentSubject { get; set; }

        [Required(ErrorMessage = "Full Name is Must")]
        [Display(Name = "Name (in full)")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email Id is Must")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "e-Mail")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Comment is Must")]
        [Display(Name = "Feedback/Comment")]
        public string Comment { get; set; }
        public bool Approved { get; set; }
    }
    public class AlumniFeedbackVM
    {
        [Key]
        [Required(ErrorMessage = "Feedback Id is Must")]
        [Display(Name = "Feedback Id")]
        public int FeedbackId { get; set; }

        //[Required(ErrorMessage = "Department Selection is Must")]
        [Display(Name = "Department")]
        public string DepartmentSubject { get; set; }

        [Required(ErrorMessage = "Comment is Must")]
        [Display(Name = "Feedback/Comment")]
        public string Comment { get; set; }
    }
    public class AlumniFeedbackIndxVM : AlumniFeedbackVM
    {
        [Required(ErrorMessage = "Full Name is Must")]
        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email Id is Must")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "e-Mail")]
        public string EmailId { get; set; }

        [Display(Name = "Status")]
        public bool Approved { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public string CreatedAt { get; set; }
    }
    public class AlumniFeedbackCrtVM : AlumniFeedbackVM
    {

    }
    public class AlumniFeedbackUpVM : AlumniFeedbackVM
    {

    }
    public class AlumniFeedbackListVM : AlumniFeedbackVM
    {
        [Required(ErrorMessage = "Full Name is Must")]
        [Display(Name = "Name (in full)")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email Id is Must")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "e-Mail")]
        public string EmailId { get; set; }
    }


}