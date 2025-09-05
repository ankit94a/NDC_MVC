using NDCWeb.Infrastructure.Constants;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Member.View_Models
{
    public class CourseRegisterVM
    {
        [Key]
        public int CourseRegisterId { get; set; }

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

        [Required(ErrorMessage = "Enter Gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Enter Date of Birth")]
        //[RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOBirth { get; set; }

        [Display(Name = "Date of Commissioning")]
        //[RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public DateTime? DOCommissioning { get; set; }

        [Display(Name = "Seniority Year")]
        [MaxLength(4)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Enter years, ie. 2003")]
        public string SeniorityYear { get; set; }

        //[EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "Enter Email")]
        [Display(Name = "Email")]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
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

        [Display(Name = "Honour & Awards")]
        [RegularExpression(@"^[a-zA-Z0-9 ,]*$", ErrorMessage = "Special chars not allowed")]
        public string Honour { get; set; }

        [Display(Name = "Nick Name (For Name Tab)")]
        public string NICName { get; set; }

        [Display(Name = "Designation")]
        [RegularExpression(@"^[a-zA-Z0-9 ,]*$", ErrorMessage = "Special chars not allowed")]
        public string ApptDesignation { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Organisation")]
        public string ApptOrganisation { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Location")]
        public string ApptLocation { get; set; }

        [Display(Name = "Approved")]
        public bool Approved { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Branch")]
        public string Branch { get; set; }

        [Required(ErrorMessage = "Select Rank")]
        [Display(Name = "Rank")]
        public int RankId { get; set; }
        public virtual RankMaster Ranks { get; set; }
    }

    public class CourseRegisterIndxVM : CourseRegisterVM
    {
        [Display(Name = "Create On")]
        public DateTime CreateOn { get; set; }

        [Display(Name = "Verify Date")]
        public DateTime? VerifyDate { get; set; }

        [Display(Name = "User Id")]
        public string UserId { get; set; }
    }
    public class CourseRegisterAckVM : CourseRegisterVM
    {
        [Display(Name = "Create On")]
        public DateTime CreateOn { get; set; }

        [Display(Name = "Verify Date")]
        public DateTime? VerifyDate { get; set; }

        [Display(Name = "User Id")]
        public string UserId { get; set; }

        [Display(Name = "Rank")]
        public string RankName { get; set; }

        [Display(Name = "Service")]
        public string RankService { get; set; }
    }
    public class CourseRegisterCrtVM : CourseRegisterVM
    {
        [Display(Name = "Confirmed E-mail")]
        [NotMapped] // Does not effect with your database
        [Compare("EmailId")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Entered Email does not match")]
        [System.Web.Mvc.Remote("CheckExistingEmail", "Registration", ErrorMessage = "Email Id already exists. Please login to your account or enter a different Email Id.")]
        public string ConfirmEmail { get; set; }

        [Display(Name = "Confirmed Mobile No")]
        [NotMapped] // Does not effect with your database
        [Compare("MobileNo")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Entered MobileNo does not match")]
        [System.Web.Mvc.Remote("CheckExistingMobile", "Registration", ErrorMessage = "Mobile number already exists. Please login to your account or enter a different phone number.")]
        public string ConfirmMobileNo { get; set; }

        [Required(ErrorMessage = "Select Service")]
        [Display(Name = "Service")]
        public string Service { get; set; }

        [Display(Name = "Create On")]
        public DateTime CreateOn { get; set; } = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
    }
    public class VerifyMemberVM
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //[Required]
        //[Display(Name = "Password")]
        //public string Password { get; set; }

        //[Required]
        //[Display(Name = "User Type")]
        //public string Role { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
    public class CourseRegisterPreviewVM : CourseRegisterVM
    {
    }
    public class CourseRegisterAlertVM
    {
        public int CourseRegisterId { get; set; }
        public int memberRegId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string EmailId { get; set; }

        [Display(Name = "Mobile No (with country Code)")]
        public string MobileNo { get; set; }

        [Display(Name = "Approved")]
        public bool Approved { get; set; }

        [Display(Name = "Rank")]
        public int RankId { get; set; }
        public virtual RankMaster Ranks { get; set; }

        [Display(Name = "Service")]
        public string RankService { get; set; }

        [Display(Name = "Create On")]
        public DateTime CreateOn { get; set; }

        [Display(Name = "Verify Date")]
        public DateTime? VerifyDate { get; set; }

        [Display(Name = "User Id")]
        public string UserId { get; set; }

    }
}