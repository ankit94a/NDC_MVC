using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Alumni.View_Models
{
    public class AlumniProfileVM
    {
        [Key]
        public int AluminiId { get; set; }


        [Required(ErrorMessage = "First Name is Required")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Decoration (for Service Personnel)")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Decoration { get; set; }

        [Required(ErrorMessage = "Serving/Retired is Required")]
        [Display(Name = "Serving/Retired")]
        public string ServiceRetd { get; set; }

        [Display(Name = "Service")]
        public string ServiceId { get; set; }
        [Required(ErrorMessage = "Rank/Designation is Required")]
        [Display(Name = "Rank/Designation")]
        public string ServiceRank { get; set; }

        [Display(Name = "Branch/Department/Ministry")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Branch { get; set; }

        [Display(Name = "NDC Course Number")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string CourseSerNo { get; set; }

        [Display(Name = "Course Year")]
        public string CourseYear { get; set; }

        [Display(Name = "Name of the Foreign Course (Course/Country/Year of Commencement)")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string NdcEqvCourse { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }
        [Display(Name = "Year of Commencement")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string YearDone { get; set; }

        [Display(Name = "Mobile No")]
        [RegularExpression(@"^[0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string MobileNo { get; set; }
        [Display(Name = "E-mail Id")]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }
        [Display(Name = "Permanent Address")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string PermanentAddress { get; set; }
        [Display(Name = "Correspondence Address")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string NdcCommunicationAddress { get; set; }
        [Display(Name = "Any other relevant information (May include details of important appointments held while in service/Post retirement)")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string OtherInfo { get; set; }

        public string ServiceNo { get; set; }
        public string LandlineNo { get; set; }
        public string FaxNo { get; set; }
        public string Spouse { get; set; }
        public string AlumniPhoto { get; set; }
        public bool Verified { get; set; }
        public bool IsDelete { get; set; }
        public string Appointment { get; set; }


        public string OtherService { get; set; }
        public string IsProminent { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? RegDate { get; set; }
        public DateTime? VerifyDate { get; set; }

        public string UserId { get; set; }
    }

    public class AlumniUpVM
    {
        public int AluminiId { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Decoration")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string Decoration { get; set; }

        [Required(ErrorMessage = "Serving/Retired is Required")]
        [Display(Name = "Serving/Retired")]
        public string ServiceRetd { get; set; }

        [Display(Name = "Service")]
        public string ServiceId { get; set; }
        [Required(ErrorMessage = "Rank/Designation is Required")]
        [Display(Name = "Rank/Designation")]
        public string ServiceRank { get; set; }

        [Display(Name = "Branch/Service")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Branch { get; set; }

        [Display(Name = "NDC Course Number")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string CourseSerNo { get; set; }

        [Display(Name = "Course Year")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string CourseYear { get; set; }

        [Display(Name = "Name of Foreign Course")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string NdcEqvCourse { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }
        [Display(Name = "Year of Commencement")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string YearDone { get; set; }

        [Display(Name = "Mobile No")]
        [RegularExpression(@"^[0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string MobileNo { get; set; }
        [Display(Name = "E-mail Id")]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }

        [Display(Name = "Permanent Address")]
        [RegularExpression(@"^[\w \.\,\-]*$", ErrorMessage = "Special chars not allowed")]
        public string PermanentAddress { get; set; }

        [Display(Name = "Correspondence Address")]
        [RegularExpression(@"^[\w \.\,\-]*$", ErrorMessage = "Special chars not allowed")]
        public string NdcCommunicationAddress { get; set; }
    }
}