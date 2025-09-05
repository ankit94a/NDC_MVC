using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Staff.View_Models
{
    public class ParticipantVM
    {
    }
    public class ParticipantIndxVM : BaseEntityVM
    {
        [Required(ErrorMessage = "Select Member Id")]
        [Display(Name = "Member Id")]
        public int CourseMemberId { get; set; }

        [Required(ErrorMessage = "Enter Full Name")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Select Gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Enter Father Name")]
        [Display(Name = "Father's Name")]
        public string FatherName { get; set; }

        public string FatherMiddleName { get; set; }
        public string FatherSurname { get; set; }

        [Required(ErrorMessage = "Enter Date of Birth")]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOBirth { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        [Display(Name = "E-mail")]
        public string EmailId { get; set; }

        [Display(Name = "Alternate E-mail")]
        public string AlternateEmailId { get; set; }

        [Required(ErrorMessage = "Enter Mobile No")]
        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }
        [Display(Name = "Alternate Mobile Number")]
        public string AlternateMobileNo { get; set; }

        [Required(ErrorMessage = "Enter Passport No")]
        [Display(Name = "Passport No")]
        public string MemberPassportNo { get; set; }

        [Required(ErrorMessage = "Select Service No")]
        [Display(Name = "Service No")]
        public string ServiceNo { get; set; }
        public string LockerNo { get; set; }

    }

    public class ParticipantBioVM
    {

    }

    #region LockerAllotment
    public class LockerAllotmentVM
    {
        public int LockerAllotmentId { get; set; }

        [Required(ErrorMessage = "Enter Locker No")]
        [Display(Name = "Locker No")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string LockerNo { get; set; }

        [Required(ErrorMessage = "Enter Roles Assign")]
        [Display(Name = "Roles Assign")]
        public string RolesAssign { get; set; }

        [Required(ErrorMessage = "Select SDS")]
        [Display(Name = "SDS")]
        public int? SDSId { get; set; }
        public virtual StaffMaster SDSStaffMasters { get; set; }

        [Required(ErrorMessage = "Select ADS")]
        [Display(Name = "ADS")]
        public int? ADSId { get; set; }
        public virtual StaffMaster ADSStaffMasters { get; set; }

        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public int CourseMemberId { get; set; }
        public virtual CrsMemberPersonal CrsMemberPersonals { get; set; }
        //public int CourseRegisterId { get; set; }
        //public virtual CourseRegister CourseRegisters { get; set; }

    }
    public class LockerAllotmentIndxVM : LockerAllotmentVM
    {
    }
    public class LockerAllotmentReadVM
    {
        public int LockerAllotmentId { get; set; }

        [Required(ErrorMessage = "Enter Locker No")]
        [Display(Name = "Locker No")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string LockerNo { get; set; }

        [Required(ErrorMessage = "Enter Roles Assign")]
        [Display(Name = "Roles Assign")]
        public string RolesAssign { get; set; }

        public string IAG { get; set; }

        [Required(ErrorMessage = "Select SDS")]
        [Display(Name = "SDS")]
        public int? SDSId { get; set; }

        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }

        public int CourseMemberId { get; set; }
    }
    public class LockerAllotmentAddVM
    {
        [Required(ErrorMessage = "Select Course")]
        [Display(Name = "Course")]
        public int CourseId { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public int CourseMemberId { get; set; }
    }
    public class LockerAllotmentEditVM
    {
        public int LockerAllotmentId { get; set; }

        [Required(ErrorMessage = "Enter Locker No")]
        [Display(Name = "Locker No")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string LockerNo { get; set; }

        public string IAG { get; set; }

        [Required(ErrorMessage = "Select SDS")]
        [Display(Name = "SDS")]
        public int? SDSId { get; set; }
        //public StaffMaster SDSStaffMasters { get; set; }

        [Required(ErrorMessage = "Enter Roles Assign")]
        [Display(Name = "Roles Assign")]
        public string RolesAssign { get; set; }

        public string FullName { get; set; }

        public int CourseMemberId { get; set; }
    }
    #endregion

    #region Accommodation
    public class TrainingSectionVM
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string MobileNo { get; set; }
        public string AlternateMobileNo { get; set; }
        public string EmailId { get; set; }
        public string AlternateEmailId { get; set; }
        public string Height { get; set; }
        public string MemberPassportNo { get; set; }
        public DateTime? MemberPassportValidUpto { get; set; }
        public string VisaNo { get; set; }
        public DateTime? VisaValidUpto { get; set; }

        public DateTime DOJoining { get; set; }
        public DateTime DOSeniority { get; set; }

        public string SpouseName { get; set; }
    }
    #endregion
}