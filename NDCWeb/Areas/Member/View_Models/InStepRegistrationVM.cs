using NDCWeb.Infrastructure.Filters;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Member.View_Models
{
    public class InStepRegistrationVM
    {
        [Key]
        public int InStepRegId { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        [Display(Name = "Email")]
        [MaxLength(50)]
        //[RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Enter Mobile No")]
        //[RegularExpression(@"^[a-zA-Z0-9-+]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Mobile No (with country Code)")]
        [MaxLength(50)]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Enter Whatsapp No")]
        //[RegularExpression(@"^[a-zA-Z0-9-+]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Whatsapp No")]
        public string WhatsappNo { get; set; }

        [Display(Name = "Upload your recent Photograph")]
        public string PhotoPath { get; set; }

        [Required(ErrorMessage = "Enter First Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your Fathers Name")]
        [Display(Name = "Father Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string FatherName { get; set; }

        [Display(Name = "Honour & Awards")]
        [RegularExpression(@"^[a-zA-Z0-9 \*\,\.\:\-\\_\'\(\)""\/]+$", ErrorMessage = "Sorry! Special chars are not allowed")]
        public string HonourAward { get; set; }

        [Required(ErrorMessage = "Enter Date of Birth")]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }
       
        [Required(ErrorMessage = "Enter Gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Select Service Category")]
        [Display(Name = "Service")]
        public string ServicesCategory { get; set; }

        //[Required(ErrorMessage = "Please enter your Service / Personnel No")]
        [Display(Name = "Service / Personnel No")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string ServiceNo { get; set; }

        [Display(Name = "Branch /Department")]
        [RegularExpression(@"^[a-zA-Z0-9 \*\,\.\:\-\\_\'\(\)""\/]+$", ErrorMessage = "Sorry! Special chars are not allowed")]
        public string BranchDepartment { get; set; }

        //[Required(ErrorMessage = "Please enter your Date of Commissioning")]
        [Display(Name = "Date of Commissioning")]
        public DateTime? DateOfCommissioning { get; set; }

        [Display(Name = "Seniority Year")]
        [MaxLength(4)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Enter years, ie. 2003")]
        public string SeniorityYear { get; set; }

        #region Address
        [Required(ErrorMessage = "Please enter your Local Address")]
        [Display(Name = "Local Address")]
        //[RegularExpression(@"^[a-zA-Z0-9 \*\,\.\:\-\\_\'\(\)""\/]+$", ErrorMessage = "Sorry! Special chars are not allowed")]
        public string AddressLocal { get; set; }

        [Required(ErrorMessage = "Please enter your Permanent Address")]
        [Display(Name = "Permanent Address")]
        //[RegularExpression(@"^[a-zA-Z0-9 \*\,\.\:\-\\_\'\(\)""\/]+$", ErrorMessage = "Sorry! Special chars are not allowed")]
        public string AddressPermanent { get; set; }
        #endregion

        #region NOK

        [Required(ErrorMessage = "Please enter your Next of Kin (NOK) Name")]
        [Display(Name = "Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string NOKName { get; set; }

        [Required(ErrorMessage = "Please enter your Next of Kin (NOK) Contact No")]
        [Display(Name = "Contact No")]
        public string NOKContact { get; set; }
        #endregion

        #region passport

        [Required(ErrorMessage = "Please enter your Passport No")]
        [RequiredIf(nameof(StudentType), "OC")]
        [Display(Name = "Passport No")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string PassportNo { get; set; }

        [Display(Name = "Name as per Passport")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string PassportName { get; set; }

        [Required(ErrorMessage = "Please enter your Passport Issuing Authority")]
        [Display(Name = "Passport Issuing Authority")]
        [RequiredIf(nameof(StudentType), "OC")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string PassportAuthority { get; set; }

        [Display(Name = "Upload Passport PDF")]
        [RequiredIf(nameof(StudentType), "OC")]
        [Required(ErrorMessage = "Please upload a Copy of Passport in PDF format only. Max size upto 500 KB")]
        public string PassportDocPath { get; set; }
        #endregion

        #region Aadhaar

        
        [Display(Name = "AADHAAR No")]
        [RequiredIf(nameof(StudentType), "IN")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string AadhaarNo { get; set; }
        
        [RequiredIf(nameof(StudentType), "IN")]
        [Display(Name = "Upload AADHAAR/e-AADHAAR PDF")]
        [Required(ErrorMessage = "Please upload a Copy of AADHAAR in PDF format only. Max size upto 500 KB")]
        public string AadhaarDocPath { get; set; }
        #endregion

        #region BioData
        //[RegularExpression(@"^[a-zA-Z0-9 \*\,\.\:\-\\_\'\(\)""\/]+$", ErrorMessage = "Sorry! Special chars are not allowed")]
        [Display(Name = "Bio-Data/Pen Picture")]
        public string BioData { get; set; }
        #endregion

        #region Travel Details /Flight Details
        [Display(Name = "E.T.A")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ArrivalTime { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9 \*\,\.\:\-\\_\'\(\)""\/]+$", ErrorMessage = "Sorry! Special chars are not allowed")]
        [Display(Name = "Arrival Flight Name")]
        public string ArrivalMode { get; set; }

        [Display(Name = "E.T.D")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DepartureTime { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9 \*\,\.\:\-\\_\'\(\)""\/]+$", ErrorMessage = "Sorry! Special chars are not allowed")]
        [Display(Name = "Departure Flight Name")]
        public string DepartureMode { get; set; }
        #endregion

        #region Approval
        [Display(Name = "Upload Approval/Nomination ink Signed Letter Copy")]
        [Required(ErrorMessage = "Please upload a Copy of Approval/Nomination in PDF format only. Max size upto 500 KB")]
        public string ApprovedNominationDocPath { get; set; }
        public bool Approved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        #endregion
        [RegularExpression(@"^[a-zA-Z0-9 \*\,\.\:\-\\_\'\(\)""\/]+$", ErrorMessage = "Sorry! Special chars are not allowed")]
        [Display(Name = "Any Other Requirement")]
        public string AnyOtherRequirement { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Rank/Designation")]
        [Required(ErrorMessage = "Please select your Rank")]
        public int RankId { get; set; }
        public virtual RankMaster Ranks { get; set; }

        [Display(Name = "Other Rank/Designation")]
        [RequiredIf(nameof(RankId), 105)]
        [RegularExpression(@"^[a-zA-Z0-9 \*\,\.\:\-\\_\'\(\)""\/]+$", ErrorMessage = "Sorry! Special chars are not allowed")]
        public string OtherRank { get; set; }

        public int? CourseId { get; set; }
        public virtual InStepCourse InStepCourses { get; set; }
       
        [NotMapped]
        public string StudentType { get; set; }
    }
    public class InStepRegistrationCrtVM : InStepRegistrationVM
    {
        [RegularExpression(@"^\s*(\S+\s+|\S+$){50,500}$", ErrorMessage = "Please enter minimum 50 words and maximum 200 words to proceed")]
        [Display(Name = "Bio-Data/Pen Picture")]
        public string BioData { get; set; }

        [Display(Name = "Confirmed E-mail")]
        [NotMapped] // Does not effect with your database
        [Compare("EmailId")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Entered Email does not match")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        // [System.Web.Mvc.Remote("CheckExistingEmail", "InStep", ErrorMessage = "Email Id already exists. Please login to your account or enter a different Email Id.")]
        public string ConfirmEmail { get; set; }

        [Display(Name = "Confirmed Mobile No")]
        [NotMapped] // Does not effect with your database
        [Compare("MobileNo")]
        [DataType(DataType.Password)]
        //[RegularExpression(@"^[a-zA-Z0-9-+]*$", ErrorMessage = "Special chars not allowed")]
        [Required(ErrorMessage = "Entered MobileNo does not match")]
        //[System.Web.Mvc.Remote("CheckExistingMobile", "InStep", ErrorMessage = "Mobile number already exists. Please login to your account or enter a different phone number.")]
        public string ConfirmMobileNo { get; set; }
        
        [NotMapped]
        public string BaseId { get; set; }
        [NotMapped]
        public string hdns { get; set; }
        //[Display(Name = "Create On")]
        //public DateTime CreateOn { get; set; } = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
    }
    public class InStepRegistrationIndexVM : InStepRegistrationVM
    {
        public string RankName { get; set; }


    }
}