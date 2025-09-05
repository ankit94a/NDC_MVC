using NDCWeb.Areas.Admin.Models;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Admin.View_Models
{
    #region Staff Master
    public class StaffVM
    { }
    public class StaffMasterVM
    {
        public StaffMasterVM()
        {
            iStaffDocument = new List<StaffDocument>();
        }
        [Key]
        [Required(ErrorMessage = "Please Enter Staff Id")]
        [Display(Name = "Staff Id")]
        public int StaffId { get; set; }

        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        [Required(ErrorMessage = "Please Enter Full Name")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        //[RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        [Required(ErrorMessage = "Please Enter Decoration")]
        [Display(Name = "Decoration")]
        public string Decoration { get; set; }

        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        [Required(ErrorMessage = "Please Enter EmailId")]
        [Display(Name = "Email")]
        public string EmailId { get; set; }

        [Display(Name = "Phone No")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string PhoneNo { get; set; }

        [Required(ErrorMessage = "Enter Date of Birth")]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOBirth { get; set; }

        [Display(Name = "Date of Marriage")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DOMarriage { get; set; }

        [Display(Name = "Date of Appointment")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DOAppointment { get; set; }

        [Display(Name = "Posting Out")]
        public bool PostingOut { get; set; }

        [Display(Name = "Is Login User")]
        public bool IsLoginUser { get; set; }

        public string SelfImage { get; set; }
        #region nav
        //public Guid PhotoId { get; set; }
        public virtual ICollection<StaffDocument> iStaffDocument { get; set; }

        [Required(ErrorMessage = "Select Appointment")]
        [Display(Name = "Appointment")]
        public int FacultyId { get; set; }
        public virtual Faculty Faculties { get; set; }

        [Required(ErrorMessage = "Select Rank")]
        [Display(Name = "Rank")]
        public int RankId { get; set; }
        public virtual RankMaster Ranks { get; set; }
        #endregion
    }
    public class StaffMasterIndxVM : StaffMasterVM
    {
    }
    public class StaffMasterCrtVM : StaffMasterVM
    {
        [Required(ErrorMessage = "Select Service")]
        [Display(Name = "Service")]
        public string Service { get; set; }

    }
    public class StaffMasterUpVM : StaffMasterVM
    {
        [Required(ErrorMessage = "Select Service")]
        [Display(Name = "Service")]
        public string Service { get; set; }

        public string LoginUserId { get; set; }
    }

    public class StaffUserIndxListVM
    {
        public int StaffId { get; set; }

        public string FullName { get; set; }

        public string Decoration { get; set; }

        public string EmailId { get; set; }

        public string PhoneNo { get; set; }

        [Required(ErrorMessage = "Enter Date of Birth")]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DOBirth { get; set; }

        public DateTime? DOMarriage { get; set; }

        public DateTime? DOAppointment { get; set; }

        public bool? LockoutEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public int? LoginId { get; set; }
        
        [RegularExpression(@"^[\w,.!?@ ]*$", ErrorMessage = "Special chars not allowed")]
        public string RankName { get; set; }
        
        [RegularExpression(@"^[\w,.!?@ ]*$", ErrorMessage = "Special chars not allowed")]
        public string FacultyName { get; set; }

        public string SelfImage { get; set; }
    }

    #endregion

    #region User Document
    public class UserDocumentVM
    {
        public Guid GuId { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string FilePath { get; set; }
        public bool Verify { get; set; }
        public int UserId { get; set; }
    }
    #endregion

    public class ResetLoginUserVM
    {
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public string LoginUserId { get; set; }
    }
}