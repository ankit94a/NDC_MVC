using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.View_Models
{
    public class AlumniVM
    {
        [Key]
        public int AluminiId { get; set; }


        [Required(ErrorMessage = "First Name is Required")]
        [RegularExpression(@"^[a-zA-Z0-9_ ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        //[RegularExpression(@"^[a-zA-Z0-9_ ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        //[RegularExpression(@"^[a-zA-Z0-9_ ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Decoration (for Service Personnel)")]
        public string Decoration { get; set; }

        [Required(ErrorMessage = "Serving/Retired is Required")]
        [Display(Name = "Serving/Retired")]
        [RegularExpression(@"^[a-zA-Z0-9_ ]*$", ErrorMessage = "Special chars not allowed")]
        public string ServiceRetd { get; set; }

        [Display(Name = "Service")]
        public string ServiceId { get; set; }
        [Required(ErrorMessage = "Rank/Designation is Required")]
        [Display(Name = "Rank/Designation")]
        public string ServiceRank { get; set; }

        [Display(Name = "Branch/Department/Ministry")]
        public string Branch { get; set; }

        [Display(Name = "NDC Course Number")]
        [RegularExpression(@"^[a-zA-Z0-9_ ]*$", ErrorMessage = "Special chars not allowed")]
        public string CourseSerNo { get; set; }

        [Display(Name = "Course Year")]
        [RegularExpression(@"^[a-zA-Z0-9_ ]*$", ErrorMessage = "Special chars not allowed")]
        public string CourseYear { get; set; }

        [Display(Name = "Name of the Foreign Course (Course/Country/Year of Commencement)")]
        [RegularExpression(@"^[a-zA-Z0-9_ ]*$", ErrorMessage = "Special chars not allowed")]
        public string NdcEqvCourse { get; set; }

        [Display(Name = "Country")]
        [RegularExpression(@"^[a-zA-Z0-9_ ]*$", ErrorMessage = "Special chars not allowed")]
        public string Country { get; set; }
        
        [RegularExpression(@"^[a-zA-Z0-9_ ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Year of Commencement")]
        public string YearDone { get; set; }

        [Display(Name = "Mobile No")]
        [Required(ErrorMessage = "Entered MobileNo does not match")]
        [System.Web.Mvc.Remote("CheckExistingMobile", "Alumnus", ErrorMessage = "Mobile number already exists. Please login to your account or enter a different phone number.")]
        public string MobileNo { get; set; }
        
        [Display(Name = "E-mail Id")]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        [System.Web.Mvc.Remote("CheckExistingEmail", "Alumnus", ErrorMessage = "Email Id already exists. Please login to your account or enter a different Email Id.")]
        public string Email { get; set; }
        //[RegularExpression(@"^[a-zA-Z0-9_ ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }

        //[RegularExpression(@"^[a-zA-Z0-9_ ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Correspondence Address")]
        public string NdcCommunicationAddress { get; set; }
        //[RegularExpression(@"^[a-zA-Z0-9_ ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Any other relevant information (May include details of important appointments held while in service/Post retirement)")]
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

        [Display(Name = "Last Updated")]
        public DateTime? LastUpdatedAt { get; set; }

        [Display(Name = "In-Step Course Name")]
        public int? InStepCourseId { get; set; }

        //public int AluminiId { get; set; }

        //[Required(ErrorMessage = "First Name is Required")]
        //[Display(Name = "First Name")]
        //public string FirstName { get; set; }
        //[Required(ErrorMessage = "Surname is Required")]
        //[Display(Name = "Surname")]
        //public string Surname { get; set; }

        //[Display(Name = "Decoration (for Service Persons)")]
        //public string Decoration { get; set; }

        //[Required(ErrorMessage = "Serving/Retired is Required")]
        //[Display(Name = "Serving/Retired")]
        //public string ServiceRetd { get; set; }

        //[Required(ErrorMessage = "Rank/Designation is Required")]
        //[Display(Name = "Rank/Designation")]
        //public string OherRank { get; set; }
        //public string NdcEqvCourse { get; set; }
        //public string YearDone { get; set; }
        //public string a_country { get; set; }
        //public string Branch { get; set; }
        //public string CourseSerNo { get; set; }
        //public string CourseYear { get; set; }

        //[Required(ErrorMessage = "Mobile No is Required")]
        //[Display(Name = "Mobile No")]
        //public string Mobile_no { get; set; }

        //[Required(ErrorMessage = "Email is Required")]
        //[Display(Name = "Email Id")]
        //public string Email { get; set; }

        //[Required(ErrorMessage = "Address is Required")]
        //[Display(Name = "Permanent Address")]
        //public string Permanent_Address { get; set; }
        //public string Ndc_commun_address { get; set; }
        //public string OtherInfo { get; set; }
        //public string a_service { get; set; }
        //public string Service_id { get; set; }
        //public string Service_no { get; set; }

        //public string Landline_no { get; set; }

        //public string Fax_no { get; set; }
        //public string Spouse { get; set; }
        //public string AlumniPhoto { get; set; }
        //public bool Verified { get; set; }
        //public bool IsDelete { get; set; }
        //public string Appointment { get; set; }

        //public string IsProminent { get; set; }
        //[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime? RegDate { get; set; }
        //[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime? VerifyDate { get; set; }

        //[Required(ErrorMessage = "Rank/Designation is Required")]
        //[Display(Name = "Rank/Designation")]
        //public int RankId { get; set; }
        //public virtual RankMaster Ranks { get; set; }

        //public string NdcEqvCourse { get; set; }
        //public string YearDone { get; set; }
        //public string ServiceId { get; set; }
        //public string ServiceNo { get; set; }
        //public string PermanentAddress { get; set; }
        //public string NdcCommunicationAddress { get; set; }
        //public string LandlineNo { get; set; }
        //public string MobileNo { get; set; }
        //public string FaxNo { get; set; }
        //public string Country { get; set; }
        //public string ServiceRank { get; set; }
        //public string OtherService { get; set; }
        //public string UserId { get; set; }
    }

    public class AlumniIndxVM : AlumniVM
    {

    }
    public class AlumniCrtVM : AlumniVM
    {

    }
    public class AlumniUpVM
    {
        public int AluminiId { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        [RegularExpression(@"^[a-zA-Z0-9_ ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_ ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Decoration")]
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
        public string Branch { get; set; }

        [Display(Name = "NDC Course Number")]
        public string CourseSerNo { get; set; }

        [Display(Name = "Course Year")]
        public string CourseYear { get; set; }

        [Display(Name = "Foreign Course ")]
        public string NdcEqvCourse { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Year of Commencement")]
        public string YearDone { get; set; }

        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }

        [Display(Name = "E-mail Id")]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }

        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }

        [Display(Name = "Correspondence Address")]
        public string NdcCommunicationAddress { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RegDate { get; set; }
        public bool? Verified { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? VerifyDate { get; set; }
        public string UserId { get; set; }
        public string AlumniImgPath { get; set; }
    }
    public class AlumniDetailVM : AlumniVM
    {

    }
    public class InstepAlumniIndxVM : AlumniVM
    {

    }
}