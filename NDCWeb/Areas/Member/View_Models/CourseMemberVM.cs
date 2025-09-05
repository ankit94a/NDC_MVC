using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Member.View_Models
{
    #region Course Member
    public class CourseMemberVM
    {
        [Key]
        [Required(ErrorMessage = "Select Member Id")]
        [Display(Name = "Member Id")]
        public int CourseMemberId { get; set; }

        [Required(ErrorMessage = "Enter Surname")]
        [Display(Name = "Family/Surname")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        [Display(Name = "Given Name/Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter Nick Name")]
        [Display(Name = "Nick Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "Select Gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Select Marital Status")]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        [Required(ErrorMessage = "Enter Father Name")]
        [Display(Name = "Father's Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "Enter Mother Name")]
        [Display(Name = "Mothers's Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string MothersName { get; set; }

        [Required(ErrorMessage = "Select Country Name")]
        [Display(Name = "Citizenship Country Name")]
        public string CitizenshipCountry { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        [Display(Name = "E-mail ID")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid e-mail adress")]
        public string EmailId { get; set; }

        #region Dates
        [Required(ErrorMessage = "Enter Date of Joining")]
        [Display(Name = "Date of Joining")]
        public DateTime DOJoining { get; set; }

        [Required(ErrorMessage = "Enter Date of Seniority")]
        [Display(Name = "Date of Seniority")]
        public DateTime DOSeniority { get; set; }

        [Required(ErrorMessage = "Enter Date of Birth")]
        [Display(Name = "Date of Birth")]
        public DateTime DOBirth { get; set; }

        [Required(ErrorMessage = "Enter Date of Marriage")]
        [Display(Name = "Date of Marriage")]
        public DateTime? DOMarriage { get; set; }
        #endregion

        #region Appointment
        [Required(ErrorMessage = "Enter Designation")]
        [Display(Name = "Designation")]
        public string ApptDesignation { get; set; }

        [Required(ErrorMessage = "Enter Organisation")]
        [Display(Name = "Organisation")]
        public string ApptOrganisation { get; set; }

        [Required(ErrorMessage = "Enter Location")]
        [Display(Name = "Location")]
        public string ApptLocation { get; set; }
        #endregion

        #region Location
        [Required(ErrorMessage = "Enter Address")]
        [Display(Name = "Address")]
        public string CurrentAddress { get; set; }

        [Required(ErrorMessage = "Enter Telephone")]
        [Display(Name = "Telephone")]
        public string CurrentHomeTelephone { get; set; }

        [Required(ErrorMessage = "Enter Fax")]
        [Display(Name = "Fax")]
        public string CurrentFax { get; set; }

        [Required(ErrorMessage = "Enter Address")]
        [Display(Name = "Address")]
        public string PermanentAddress { get; set; }

        [Required(ErrorMessage = "Enter Telephone")]
        [Display(Name = "Telephone")]
        public string PermanentHomeTelephone { get; set; }

        [Required(ErrorMessage = "Enter Fax")]
        [Display(Name = "Fax")]
        public string PermanentFax { get; set; }

        [Required(ErrorMessage = "Enter Office Telephone")]
        [Display(Name = "Office Telephone")]
        public string OffcTelephone { get; set; }
        #endregion

        [Required(ErrorMessage = "Enter Blood Group")]
        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }

        [Required(ErrorMessage = "Select Stay By Spouse")]
        [Display(Name = "Stay By Spouse")]
        public string StayBySpouse { get; set; }

        [Display(Name = "Agree to terms and conditions")]
        public bool Undertaking { get; set; }

        [Required(ErrorMessage = "Select Service")]
        [Display(Name = "Service")]
        public string Service { get; set; }

        #region File Path
        public string MemberImgPath { get; set; }
        public string JointImgPath { get; set; }
        public string AadhaarPath { get; set; }
        #endregion

        [Display(Name = "Dietery Preferences (V/NV)")]
        public string DietaryPref { get; set; }
        [Display(Name = "Medical Category")]
        public string MedicalCategory { get; set; }

        #region Navigation
        [Display(Name = "Branch")]
        public string Branch { get; set; }

        [Required(ErrorMessage = "Select Rank")]
        [Display(Name = "Rank")]
        public int RankId { get; set; }
        public virtual RankMaster Ranks { get; set; }
        #endregion
    }
    public class CourseMemberCrtVM : CourseMemberVM
    {

    }
    #endregion

    #region Course Member Personal
    public class CrsMemberPersonalVM
    {
        [Key]
        [Required(ErrorMessage = "Select Member Id")]
        [Display(Name = "Member Id")]
        public int CourseMemberId { get; set; }

        #region Names

        [Required(ErrorMessage = "Enter First Name")]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string MiddleName { get; set; }

        [Display(Name = "Surname")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Enter Father Name")]
        [Display(Name = "Father's Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string FatherName { get; set; }

        [Display(Name = "Middle Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string FatherMiddleName { get; set; }

        [Display(Name = "Surname")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string FatherSurname { get; set; }

        [Required(ErrorMessage = "Enter Mother Name")]
        [Display(Name = "Mother's Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string MotherName { get; set; }

        [Display(Name = "Middle Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string MotherMiddleName { get; set; }

        [Display(Name = "Surname")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string MotherSurname { get; set; }

        [Display(Name = "Nick Name")]
        //[RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string NickName { get; set; }
        #endregion

        [Required(ErrorMessage = "Enter Mobile Number")]
        [Display(Name = "Mobile Number")]
        //[RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        [MaxLength(15)]
        //[RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string MobileNo { get; set; }

        [Display(Name = "Alternate Mobile Number")]
        //[RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        //[RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        [MaxLength(15)]
        public string AlternateMobileNo { get; set; }

        [Display(Name = "Identification Mark")]
        //[RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string IndentificationMark { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        [Display(Name = "E-mail ID")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid e-mail adress")]
        public string EmailId { get; set; }

        [Display(Name = "Alternate E-mail ID")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid e-mail adress")]
        public string AlternateEmailId { get; set; }

        [Required(ErrorMessage = "Enter Date of Birth")]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOBirth { get; set; }

        [Required(ErrorMessage = "Select Marital Status")]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        [Display(Name = "Date of Marriage")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DOMarriage { get; set; }

        [Required(ErrorMessage = "Select Gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Enter Blood Group")]
        [Display(Name = "Blood Group")]
        [RegularExpression(@"^[a-zA-Z0-9+-]*$", ErrorMessage = "Special chars not allowed")]
        public string BloodGroup { get; set; }

        [Display(Name = "Height (in cm)")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string Height { get; set; }

        [Required(ErrorMessage = "Enter Voter ID/ Any ID")]
        [Display(Name = "Voter ID/ Any ID")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string VoterIdNo { get; set; }

       // [Required(ErrorMessage = "Enter PAN Number")]
        [Display(Name = "PAN Number")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string PANCardNo { get; set; }

        #region Address
        [Required(ErrorMessage = "Enter Communication Address")]
        [Display(Name = "Communication Address")]
        public string CommunicationAddress { get; set; }

        //[Required(ErrorMessage = "Enter Office House No")]
        [Display(Name = "Office House No")]
        //[RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string OfficeHouseNo { get; set; }

        //[Required(ErrorMessage = "Enter Office Premises Name")]
        //[RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Office Premises Name")]
        public string OfficePremisesName { get; set; }

        //[Required(ErrorMessage = "Enter Office Street")]
        [Display(Name = "Office Street")]
        //[RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string OfficeStreet { get; set; }

        [Required(ErrorMessage = "Enter Office Area")]
        //[RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Office Area")]
        public string OfficeArea { get; set; }

        [Required(ErrorMessage = "Enter Office City")]
        [Display(Name = "Office City")]
        public string OfficeCity { get; set; }

        [Required(ErrorMessage = "Enter Office ZipCode")]
        [Display(Name = "Office ZipCode")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string OfficeZipCode { get; set; }


        //[Required(ErrorMessage = "Enter Resident House No")]
        [Display(Name = "Resident House No")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string ResidentHouseNo { get; set; }

        //[Required(ErrorMessage = "Enter Resident Premises Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Resident Premises Name")]
        public string ResidentPremisesName { get; set; }

        //[Required(ErrorMessage = "Enter Resident Street")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Resident Street")]
        public string ResidentStreet { get; set; }

        //[Required(ErrorMessage = "Enter Resident Area")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Resident Area")]
        public string ResidentArea { get; set; }

        //[Required(ErrorMessage = "Enter Resident City")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Resident City")]
        public string ResidentCity { get; set; }

        //[Required(ErrorMessage = "Enter Resident ZipCode")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Resident ZipCode")]
        public string ResidentZipCode { get; set; }
        #endregion

        #region File Path
        public string MemberImgPath { get; set; }
        public string JointImgPath { get; set; }
        public string AadhaarPath { get; set; }
        #endregion
        [Display(Name = "Dietery Preferences (V/NV)")]
        public string DietaryPref { get; set; }
        [Display(Name = "Medical Condition/Category")]
        public string MedicalCategory { get; set; }

        [Display(Name = "Spouse Name")]
        public string SpouseName { get; set; }

        [Display(Name = "NOK")]
        public string NOK { get; set; }

        [Required(ErrorMessage = "Enter Bio Sketch")]
        //[RegularExpression(@"^\s*(\S+\s+|\S+$){50,500}[a-zA-Z0-9 ]*$", ErrorMessage = "Please enter minimum 50 words and maximum 100 words to proceed")]
        [Display(Name = "Biographical Sketch (Not more than 100 words)")]
        public string BioSketch { get; set; }

        [Display(Name = "I hereby certify that details filled by me are correct")]
        public bool Undertaking { get; set; }

        //[Required(ErrorMessage = "Select Stay By Spouse")]
        [Display(Name = "Stay By Spouse")]
        public string StayBySpouse { get; set; }

        #region passport
        //[Required(ErrorMessage = "Enter Holding Passport")]
        [Display(Name = "Holding Passport")]
        public string HoldingPassport { get; set; }

        //[Required(ErrorMessage = "Enter Self Passport No")]
        [Display(Name = "Passport No")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string MemberPassportNo { get; set; }

        //[Required(ErrorMessage = "Enter Name On Passport")]
        [Display(Name = "Name On Passport")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string MemberPassportName { get; set; }

        //[Required(ErrorMessage = "Enter Self Passport Issue Date")]
        [Display(Name = "Passport Issue Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MemberPassportIssueDate { get; set; }

        //[Required(ErrorMessage = "Enter Self Passport Valid Upto")]
        [Display(Name = "Passport Valid Upto")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MemberPassportValidUpto { get; set; }

        //[Required(ErrorMessage = "Enter Self Passport Type")]
        [Display(Name = "Passport Type")]
        public string MemberPassportType { get; set; }

        //[Required(ErrorMessage = "Enter Contry that issued passport")]
        [Display(Name = "Passport Issued Country")]
        public string CountryIssued { get; set; }

        public string MemberPassportImgPath { get; set; }
        public string MemberPassportBackImgPath { get; set; }
        #endregion

        #region personal passport
        //[Required(ErrorMessage = "Enter Holding Passport")]
        [Display(Name = "Holding Passport")]
        public bool HoldingPersonalPassportSelf { get; set; }

        //[Required(ErrorMessage = "Enter Self Passport No")]
        [Display(Name = "Passport No")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string MemberPersonalPassportNo { get; set; }

        //[Required(ErrorMessage = "Enter Name On Passport")]
        [Display(Name = "Name On Passport")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string MemberPersonalPassportName { get; set; }

        //[Required(ErrorMessage = "Enter Self Passport Issue Date")]
        [Display(Name = "Passport Issue Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MemberPersonalPassportIssueDate { get; set; }

        //[Required(ErrorMessage = "Enter Self Passport Valid Upto")]
        [Display(Name = "Passport Valid Upto")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MemberPersonalPassportValidUpto { get; set; }

        //[Required(ErrorMessage = "Enter Contry that issued passport")]
        [Display(Name = "Passport Issued Country")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string CountryIssuedPersonalPassport { get; set; }

        public string MemberPersonalPassportImgPath { get; set; }
        public string MemberPersonalPassportBackImgPath { get; set; }
        #endregion

        #region visa
        //[Required(ErrorMessage = "Enter Visa No")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Visa No")]
        public string VisaNo { get; set; }

        //[Required(ErrorMessage = "Enter Visa Issue Date")]
        [Display(Name = "Visa Issue Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? VisaIssueDate { get; set; }

        //[Required(ErrorMessage = "Enter Visa Valid Upto")]
        [Display(Name = "Visa Valid Upto")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? VisaValidUpto { get; set; }

        public string VisaPath { get; set; }
        #endregion

        #region FRRO
        //[Required(ErrorMessage = "Enter FRRO No")]
        [Display(Name = "FRRO No")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string SelfFRRONo { get; set; }

        //[Required(ErrorMessage = "Enter Issue Date")]
        [Display(Name = "Issue Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SelfIssueDate { get; set; }

        //[Required(ErrorMessage = "Enter Valid Upto")]
        [Display(Name = "Valid Upto")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SelfValidUpto { get; set; }

        public string SelfFRROPath { get; set; }
        #endregion

        #region Navigation
        public int? OfficeStateId { get; set; }
        public virtual StateMaster OfficeStates { get; set; }

        public int? ResidentStateId { get; set; }
        public virtual StateMaster ResidentStates { get; set; }

        //[Required(ErrorMessage = "Enter Citizenship Country")]
        [Display(Name = "Citizenship Country")]
        public int CitizenshipCountryId { get; set; }
        [ForeignKey("CitizenshipCountryId")]
        public CountryMaster CitizenshipCountries { get; set; }

        public int CourseId { get; set; }
        public virtual Course Courses { get; set; }
        #endregion
    }
    public class CrsMemberPersonalIndxVM : CrsMemberPersonalVM
    {
    }
    public class CrsMemberPersonalCrtVM : CrsMemberPersonalVM
    {
        //[Required(ErrorMessage = "Select Country")]
        [Display(Name = "Country")]
        public int? OfficeCountryId { get; set; }

        //[Required(ErrorMessage = "Select Country")]
        [Display(Name = "Country")]
        public int? ResidentCountryId { get; set; }
    }
    public class CrsMemberPersonalUpVM 
    {
        //[Key]
        //[Required(ErrorMessage = "Select Member Id")]
        [Display(Name = "Member Id")]
        public int CourseMemberId { get; set; }

        #region Names

        [Required(ErrorMessage = "Enter First Name")]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string MiddleName { get; set; }

        [Display(Name = "Surname")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Enter Father Name")]
        [Display(Name = "Father's Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string FatherName { get; set; }

        [Display(Name = "Middle Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string FatherMiddleName { get; set; }

        [Display(Name = "Surname")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string FatherSurname { get; set; }

        [Required(ErrorMessage = "Enter Mother Name")]
        [Display(Name = "Mother's Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string MotherName { get; set; }

        [Display(Name = "Middle Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string MotherMiddleName { get; set; }

        [Display(Name = "Surname")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string MotherSurname { get; set; }

        [Display(Name = "Nick Name")]
        //[RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string NickName { get; set; }
        #endregion

        [Required(ErrorMessage = "Enter Mobile Number")]
        [Display(Name = "Mobile Number")]
        //[RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        [MaxLength(15)]
        //[RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string MobileNo { get; set; }

        [Display(Name = "Alternate Mobile Number")]
        //[RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        //[RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        [MaxLength(15)]
        public string AlternateMobileNo { get; set; }

        [Display(Name = "Identification Mark")]
        //[RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string IndentificationMark { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        [Display(Name = "E-mail ID")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid e-mail adress")]
        public string EmailId { get; set; }

        [Display(Name = "Alternate E-mail ID")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid e-mail adress")]
        public string AlternateEmailId { get; set; }

        [Required(ErrorMessage = "Enter Date of Birth")]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOBirth { get; set; }

        [Required(ErrorMessage = "Select Marital Status")]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        [Display(Name = "Date of Marriage")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DOMarriage { get; set; }

        [Required(ErrorMessage = "Select Gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Enter Blood Group")]
        [Display(Name = "Blood Group")]
        [RegularExpression(@"^[a-zA-Z0-9+-]*$", ErrorMessage = "Special chars not allowed")]
        public string BloodGroup { get; set; }

        [Display(Name = "Height (in cm)")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string Height { get; set; }

        [Required(ErrorMessage = "Enter Voter ID/ Any ID")]
        [Display(Name = "Voter ID/ Any ID")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string VoterIdNo { get; set; }

        //[Required(ErrorMessage = "Enter PAN Number")]
        [Display(Name = "PAN Number")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string PANCardNo { get; set; }

        #region Address
        [Required(ErrorMessage = "Enter Communication Address")]
        [Display(Name = "Communication Address")]
        public string CommunicationAddress { get; set; }

        //[Required(ErrorMessage = "Enter Office House No")]
        [Display(Name = "Office House No")]
        //[RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string OfficeHouseNo { get; set; }

        //[Required(ErrorMessage = "Enter Office Premises Name")]
        //[RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Office Premises Name")]
        public string OfficePremisesName { get; set; }

        //[Required(ErrorMessage = "Enter Office Street")]
        [Display(Name = "Office Street")]
        //[RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string OfficeStreet { get; set; }

        [Required(ErrorMessage = "Enter Office Area")]
        //[RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Office Area")]
        public string OfficeArea { get; set; }

        [Required(ErrorMessage = "Enter Office City")]
        [Display(Name = "Office City")]
        public string OfficeCity { get; set; }

        [Required(ErrorMessage = "Enter Office ZipCode")]
        [Display(Name = "Office ZipCode")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string OfficeZipCode { get; set; }


        //[Required(ErrorMessage = "Enter Resident House No")]
        [Display(Name = "Resident House No")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string ResidentHouseNo { get; set; }

        //[Required(ErrorMessage = "Enter Resident Premises Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Resident Premises Name")]
        public string ResidentPremisesName { get; set; }

        //[Required(ErrorMessage = "Enter Resident Street")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Resident Street")]
        public string ResidentStreet { get; set; }

        //[Required(ErrorMessage = "Enter Resident Area")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Resident Area")]
        public string ResidentArea { get; set; }

        //[Required(ErrorMessage = "Enter Resident City")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Resident City")]
        public string ResidentCity { get; set; }

        //[Required(ErrorMessage = "Enter Resident ZipCode")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Resident ZipCode")]
        public string ResidentZipCode { get; set; }
        #endregion

        #region File Path
        public string MemberImgPath { get; set; }
        public string JointImgPath { get; set; }
        public string AadhaarPath { get; set; }
        #endregion
        [Display(Name = "Dietery Preferences (V/NV)")]
        public string DietaryPref { get; set; }
        [Display(Name = "Medical Condition/ Category")]
        public string MedicalCategory { get; set; }

        [Display(Name = "Spouse Name")] 
        public string SpouseName { get; set; }
       
        [Display(Name = "NOK")]
        public string NOK { get; set; }

        [Required(ErrorMessage = "Enter Bio Sketch")]
        //[RegularExpression(@"^[a-zA-Z0-9 .]*$", ErrorMessage = "Please enter minimum 50 words and maximum 100 words to proceed")]
        [Display(Name = "Biographical Sketch (Not more than 100 words)")]
        public string BioSketch { get; set; }

        [Display(Name = "I hereby certify that details filled by me are correct")]
        public bool Undertaking { get; set; }

        //[Required(ErrorMessage = "Select Stay By Spouse")]
        [Display(Name = "Stay By Spouse")]
        public string StayBySpouse { get; set; }

        #region passport
        //[Required(ErrorMessage = "Enter Holding Passport")]
        [Display(Name = "Holding Passport")]
        public string HoldingPassport { get; set; }

        //[Required(ErrorMessage = "Enter Self Passport No")]
        [Display(Name = "Passport No")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string MemberPassportNo { get; set; }

        //[Required(ErrorMessage = "Enter Name On Passport")]
        [Display(Name = "Name On Passport")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string MemberPassportName { get; set; }

        //[Required(ErrorMessage = "Enter Self Passport Issue Date")]
        [Display(Name = "Passport Issue Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MemberPassportIssueDate { get; set; }

        //[Required(ErrorMessage = "Enter Self Passport Valid Upto")]
        [Display(Name = "Passport Valid Upto")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MemberPassportValidUpto { get; set; }

        //[Required(ErrorMessage = "Enter Self Passport Type")]
        [Display(Name = "Passport Type")]
        public string MemberPassportType { get; set; }

        //[Required(ErrorMessage = "Enter Contry that issued passport")]
        [Display(Name = "Passport Issued Country")]
        public string CountryIssued { get; set; }

        public string MemberPassportImgPath { get; set; }
        public string MemberPassportBackImgPath { get; set; }
        #endregion

        #region personal passport
        //[Required(ErrorMessage = "Enter Holding Passport")]
        [Display(Name = "Holding Passport")]
        public bool HoldingPersonalPassportSelf { get; set; }

        //[Required(ErrorMessage = "Enter Self Passport No")]
        [Display(Name = "Passport No")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string MemberPersonalPassportNo { get; set; }

        //[Required(ErrorMessage = "Enter Name On Passport")]
        [Display(Name = "Name On Passport")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string MemberPersonalPassportName { get; set; }

        //[Required(ErrorMessage = "Enter Self Passport Issue Date")]
        [Display(Name = "Passport Issue Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MemberPersonalPassportIssueDate { get; set; }

        //[Required(ErrorMessage = "Enter Self Passport Valid Upto")]
        [Display(Name = "Passport Valid Upto")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MemberPersonalPassportValidUpto { get; set; }

        //[Required(ErrorMessage = "Enter Contry that issued passport")]
        [Display(Name = "Passport Issued Country")]
        //[RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string CountryIssuedPersonalPassport { get; set; }

        public string MemberPersonalPassportImgPath { get; set; }
        public string MemberPersonalPassportBackImgPath { get; set; }
        #endregion

        #region visa
        //[Required(ErrorMessage = "Enter Visa No")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Visa No")]
        public string VisaNo { get; set; }

        //[Required(ErrorMessage = "Enter Visa Issue Date")]
        [Display(Name = "Visa Issue Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? VisaIssueDate { get; set; }

        //[Required(ErrorMessage = "Enter Visa Valid Upto")]
        [Display(Name = "Visa Valid Upto")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? VisaValidUpto { get; set; }

        public string VisaPath { get; set; }
        #endregion

        #region FRRO
        //[Required(ErrorMessage = "Enter FRRO No")]
        [Display(Name = "FRRO No")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string SelfFRRONo { get; set; }

        //[Required(ErrorMessage = "Enter Issue Date")]
        [Display(Name = "Issue Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SelfIssueDate { get; set; }

        //[Required(ErrorMessage = "Enter Valid Upto")]
        [Display(Name = "Valid Upto")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SelfValidUpto { get; set; }

        public string SelfFRROPath { get; set; }
        #endregion

        #region Navigation
        public int? OfficeStateId { get; set; }
        public virtual StateMaster OfficeStates { get; set; }

        public int? ResidentStateId { get; set; }
        public virtual StateMaster ResidentStates { get; set; }

        //[Required(ErrorMessage = "Enter Citizenship Country")]
        [Display(Name = "Citizenship Country")]
        public int CitizenshipCountryId { get; set; }
        [ForeignKey("CitizenshipCountryId")]
        public CountryMaster CitizenshipCountries { get; set; }

        public int CourseId { get; set; }
        public virtual Course Courses { get; set; }
        #endregion
        //[Required(ErrorMessage = "Select Country")]
        [Display(Name = "Country")]
        public int? OfficeCountryId { get; set; }

        //[Required(ErrorMessage = "Select Country")]
        [Display(Name = "Country")]
        public int? ResidentCountryId { get; set; }
    }
    #endregion

    #region Address Detail
    public class CrsMbrAddressVM
    {
        [Key]
        public int MemberAddressId { get; set; }

        [Required(ErrorMessage = "Enter Address")]
        [Display(Name = "Address")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string CurrentAddress { get; set; }

        [Required(ErrorMessage = "Enter Telephone")]
        [Display(Name = "Telephone")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string CurrentTelephone { get; set; }

        [Required(ErrorMessage = "Enter Fax")]
        [Display(Name = "Fax")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string CurrentFax { get; set; }

        [Required(ErrorMessage = "Enter Address")]
        [Display(Name = "Address")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string PermanentAddress { get; set; }

        [Required(ErrorMessage = "Enter Telephone")]
        [Display(Name = "Telephone")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string PermanentTelephone { get; set; }

        [Required(ErrorMessage = "Enter Fax")]
        [Display(Name = "Fax")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string PermanentFax { get; set; }

        [Required(ErrorMessage = "Enter Office Telephone")]
        [Display(Name = "Office Telephone")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string OffcTelephone { get; set; }

        [Required(ErrorMessage = "Enter City")]
        [Display(Name = "City")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string City { get; set; }

        [Required(ErrorMessage = "Enter ZipCode")]
        [Display(Name = "ZipCode")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Select State")]
        [Display(Name = "State")]
        public int StateId { get; set; }
        public virtual StateMaster States { get; set; }
    }
    public class CrsMbrAddressIndxVM : CrsMbrAddressVM
    {
    }
    public class CrsMbrAddressCrtVM : CrsMbrAddressVM
    {
        [Required(ErrorMessage = "Select Country")]
        [Display(Name = "Country")]
        public int CountryId { get; set; }
    }
    public class CrsMbrAddressUpVM : CrsMbrAddressVM
    {
        [Required(ErrorMessage = "Select Country")]
        [Display(Name = "Country")]
        public int CountryId { get; set; }
    }
    #endregion

    #region Biography
    public class BiographyVM
    {
        [Display(Name = "Bio Id")]
        public int BiographyId { get; set; }

        [Required(ErrorMessage = "Enter Pen Picture")]
        [Display(Name = "Pen Picture")]
        public string PenPicture { get; set; }

        [Display(Name = "Family Background")]
        public string FamilyBackground { get; set; }

        [Display(Name = "Early Schooling")]
        public string EarlySchooling { get; set; }

        [Display(Name = "Academic Achievement")]
        public string AcademicAchievement { get; set; }

        [Display(Name = "Personal Value System")]
        public string PersonalValueSystem { get; set; }
    }

    public class BiographyIndxVM : BiographyVM
    {
    }
    public class BiographyCrtVM : BiographyVM
    {
    }
    public class BiographyUpVM : BiographyVM
    {
    }
    #endregion

    #region Passport Detail
    public class PassportDetailVM
    {
        public PassportDetailVM()
        {
            iChildrenPassports = new List<ChildrenPassport>();
        }
        public int PassportId { get; set; }

        [Required(ErrorMessage = "Enter Holding Passport")]
        [Display(Name = "Holding Passport")]
        public string HoldingPassport { get; set; }

        [Required(ErrorMessage = "Enter Self Passport No")]
        [Display(Name = "Passport No")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string MemberPassportNo { get; set; }

        [Required(ErrorMessage = "Enter Self Passport Issue Date")]
        [Display(Name = "Passport Issue Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime MemberPassportIssueDate { get; set; }

        [Required(ErrorMessage = "Enter Self Passport Valid Upto")]
        [Display(Name = "Passport Valid Upto")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime MemberPassportValidUpto { get; set; }

        [Required(ErrorMessage = "Enter Self Passport Type")]
        [Display(Name = "Passport Type")]
        public string MemberPassportType { get; set; }


        [Required(ErrorMessage = "Enter Spouse Passport No")]
        [Display(Name = "Passport No")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string SpousePassportNo { get; set; }

        [Required(ErrorMessage = "Enter Spouse Passport Issue Date")]
        [Display(Name = "Passport Issue Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SpousePassportIssueDate { get; set; }

        [Required(ErrorMessage = "Enter Spouse Passport Valid Upto")]
        [Display(Name = "Passport Valid Upto")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SpousePassportValidUpto { get; set; }

        [Required(ErrorMessage = "Enter Spouse Passport Type")]
        [Display(Name = "Passport Type")]
        public string SpousePassportType { get; set; }

        public string MemberPassportImgPath { get; set; }
        public string SpousePassportImgPath { get; set; }
        public virtual ICollection<ChildrenPassport> iChildrenPassports { get; set; }
    }
    public class PassportDetailIndxVM : PassportDetailVM
    {
    }
    public class PassportDetailCrtVM : PassportDetailVM
    {
    }
    public class PassportDetailUpVM : PassportDetailVM
    {
    }
    #endregion

    #region Passport Children
    public class ChildrenPassportVM
    {
        public int ChildPassportId { get; set; }

        [Required(ErrorMessage = "Enter Child Passport No")]
        [Display(Name = "Passport No")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string PassportNo { get; set; }

        [Required(ErrorMessage = "Enter Child Passport Issue Date")]
        [Display(Name = "Passport Issue Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PassportIssueDate { get; set; }

        [Required(ErrorMessage = "Enter Child Passport Issue Date")]
        [Display(Name = "Passport Issue Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PassportValidUpto { get; set; }

        [Required(ErrorMessage = "Enter Child Passport Issue Date")]
        [Display(Name = "Passport Issue Date")]
        public string PassportType { get; set; }

        public string PassportImgPath { get; set; }

        public int PassportId { get; set; }
        public virtual PassportDetail Passports { get; set; }
    }
    public class PassportChildrenIndxVM : ChildrenPassportVM
    {
    }
    public class PassportChildrenCrtVM : ChildrenPassportVM
    {
    }
    public class PassportChildrenUpVM : ChildrenPassportVM
    {
    }
    #endregion

    #region Visa Detail
    public class VisaDetailVM
    {
        public int VisaId { get; set; }

        [Required(ErrorMessage = "Enter Rank Abbr.")]
        [Display(Name = "Rank Abbr.")]
        public string VisaEntryType { get; set; }

        [Required(ErrorMessage = "Enter Visa No")]
        [Display(Name = "Visa No")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string VisaNo { get; set; }

        [Required(ErrorMessage = "Enter Visa Issue Date")]
        [Display(Name = "Visa Issue Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime VisaIssueDate { get; set; }

        [Required(ErrorMessage = "Enter Visa Valid Upto")]
        [Display(Name = "Visa Valid Upto")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime VisaValidUpto { get; set; }

        public string VisaPath { get; set; }

        [Required(ErrorMessage = "Enter Self FRRO No")]
        [Display(Name = "Self FRRO No")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string SelfFRRONo { get; set; }

        [Required(ErrorMessage = "Enter Self Issue Date")]
        [Display(Name = "Issue Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SelfIssueDate { get; set; }

        [Required(ErrorMessage = "Enter Self Valid Upto")]
        [Display(Name = "Valid Upto")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SelfValidUpto { get; set; }

        public string SelfFRROPath { get; set; }

        [Required(ErrorMessage = "Enter Spouse FRRO No")]
        [Display(Name = "FRRO No")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string SpouseFRRONo { get; set; }

        [Required(ErrorMessage = "Enter Spouse Issue Date")]
        [Display(Name = "Issue Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SpouseIssueDate { get; set; }

        [Required(ErrorMessage = "Enter Spouse Valid Upto")]
        [Display(Name = "Valid Upto")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SpouseValidUpto { get; set; }

        public string SpouseFRROPath { get; set; }
    }
    public class VisaDetailIndxVM : VisaDetailVM
    {
    }
    public class VisaDetailCrtVM : VisaDetailVM
    {
    }
    public class VisaDetailUpVM : VisaDetailVM
    {
    }
    #endregion

    #region Tally
    public class TallyDetailVM : BaseEntityVM
    {
        public int TallyId { get; set; }

        [Required(ErrorMessage = "Enter Rank Abbr.")]
        [Display(Name = "Rank Abbr.")]
        public string RankAbbr { get; set; }

        [Required(ErrorMessage = "Enter Passport Name")]
        [Display(Name = "Passport Name")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string PassportName { get; set; }

        [Required(ErrorMessage = "Enter Tab Name")]
        [Display(Name = "Tab Name")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string TabName { get; set; }

        [Required(ErrorMessage = "Enter Nick Name")]
        [Display(Name = "Nick Name")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "Enter Country/Service")]
        [Display(Name = "Country/Service")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string CountryService { get; set; }

        [Required(ErrorMessage = "Enter Name/Rank")]
        [Display(Name = "Name/Rank")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string NameORRank { get; set; }

        [Required(ErrorMessage = "Enter Residential Address")]
        [Display(Name = "Residential Address")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string ResidentialAddress { get; set; }

        [Required(ErrorMessage = "Enter Mobile No")]
        [Display(Name = "Mobile No")]
        //[RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Enter Telephone No")]
        [Display(Name = "Telephone No")]
        //[RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Invalid Telephone Number.")]
        public string TelephoneNo { get; set; }

        #region Vehicle Detail
        //[Display(Name = "Brand Model No")]
        //public string BrandModelNo { get; set; }

        //[Display(Name = "Colour")]
        //public string Colour { get; set; }

        //[Display(Name = "Registration No")]
        //public string RegistrationNo { get; set; }

        //[Display(Name = "Driving License No")]
        //public string DrivingLicenseNo { get; set; }

        //[Display(Name = "No Of Vehicle")]
        //public string NoOfVehicle { get; set; }
        //public string RegistrationCertificatePath { get; set; }
        //public string DrivingLicensePath { get; set; }
        #endregion
    }
    public class TallyDetailIndxVM : TallyDetailVM
    {
    }
    public class TallyDetailCrtVM : TallyDetailVM
    {
    }
    public class TallyDetailUpVM : TallyDetailVM
    {
    }
    public class TallyVehicleAddVM
    {
        public TallyDetail Tally { get; set; }
        public List<CrsMbrVehicleSticker> VehicleStickers { get; set; }
    }
    #endregion

    #region Vehicle Detail
    public class CrsMbrVehicleStickerVM
    {
        [Key]
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "Please Enter Brand Model No")]
        [Display(Name = "Brand Model No")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string BrandModelNo { get; set; }

        [Required(ErrorMessage = "Please Enter Colour")]
        [Display(Name = "Colour")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string Colour { get; set; }

        [Required(ErrorMessage = "Please Enter Registration No")]
        [Display(Name = "Registration No")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string RegistrationNo { get; set; }

        [Required(ErrorMessage = "Please Enter Driving License No")]
        [Display(Name = "Driving License No")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string DrivingLicenseNo { get; set; }

        //[Required(ErrorMessage = "Please Enter NoOfVehicle")]
        //[Display(Name = "NoOfVehicle")]
        //public string NoOfVehicle { get; set; }

        public string RegistrationCertificatePath { get; set; }
        public string DrivingLicensePath { get; set; }
    }
    public class CrsMbrVehicleStickerIndxVM : CrsMbrVehicleStickerVM
    {
    }
    public class CrsMbrVehicleStickerCrtVM : CrsMbrVehicleStickerVM
    {
    }
    #endregion

    #region AccountInfo
    public class AccountInfoVM
    {
        public int AccInfoId { get; set; }
        
        #region CDA Ac
        //[Required(ErrorMessage = "Enter CDA Account No")]
        [Display(Name = "CDA Account No")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string CDAAcNo { get; set; }

        //[Required(ErrorMessage = "Enter Basic Pay")]
        [Display(Name = "Basic Pay")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string BasicPay { get; set; }

       // [Required(ErrorMessage = "Enter MSP")]
        [Display(Name = "MSP")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string MSP { get; set; }

        //[Required(ErrorMessage = "Enter Pay Level")]
        [Display(Name = "Pay Level")]
        [RegularExpression(@"^[a-zA-Z0-9-]*$", ErrorMessage = "Special chars not allowed")]
        public string PayLevel { get; set; }

        //[Required(ErrorMessage = "Enter Address of Pay Account")]
        [Display(Name = "Address of Pay Account")]
        //[RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string AddressOfPayAc { get; set; }

        //Added on 29 Nov 2023
        //[Required(ErrorMessage = "Enter PAO Nodal Office Name")]
        [Display(Name = "PAO Nodal Officer Name")]
        public string NodalOfficeName { get; set; }

        //[Required(ErrorMessage = "Enter PAO Nodal Contact No")]
        [Display(Name = "PAO Nodal Contact No")]
        public string NodalOfficeContactNo { get; set; }

        //Added on 29 Nov 2024
        //[Required(ErrorMessage = "Enter PAO Nodal Email")]
        [Display(Name = "PAO Nodal Email")]
        public string NodalOfficeEmail { get; set; }
        [Display(Name = "Pay Account No")]
        public string CivilServiceAcNo { get; set; }
        [Display(Name = "Address of Pay Account")]
        public string CivilServiceAddressOfPayAc { get; set; }
        [Display(Name = "PAO Nodal Officer Name")]
        public string CivilServiceNodalOfficeName { get; set; }
        [Display(Name = "PAO Nodal Contact No")]
        public string CivilServiceNodalOfficeContactNo { get; set; }
        [Display(Name = "PAO Nodal Email")]
        public string CivilServiceNodalOfficeEmail { get; set; }
        #endregion

        #region  Bank Ac
        [Required(ErrorMessage = "Enter Account No")]
        [Display(Name = "Account No")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string AccountNo { get; set; }

        [Required(ErrorMessage = "Select Account Type")]
        [Display(Name = "Account Type")]
        public string AccountType { get; set; }

        [Required(ErrorMessage = "Enter IFSC")]
        [Display(Name = "IFSC")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string IFSC { get; set; }

        [Required(ErrorMessage = "Enter MICR")]
        [Display(Name = "MICR")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string MICR { get; set; }

        [Required(ErrorMessage = "Passbook/Cheque Required")]
        [Display(Name = "Passbook or Cheque Slip")]
        public string PassbookPath { get; set; }


        [Required(ErrorMessage = "Name and Address of Banker Required")]
        [Display(Name = "Name and Address of Banker")]
        public string NameAndAddressOfBanker { get; set; }
        #endregion
    }
    public class AccountInfoIndxVM : AccountInfoVM
    {
    }
    public class AccountInfoCrtVM : AccountInfoVM
    {
    }
    public class AccountInfoUpVM : AccountInfoVM
    {
    }
    #endregion

    #region Arrival
    public class ArrivalDetailVM : BaseEntityVM
    {
        public ArrivalDetailVM()
        {
            iArrivalMeals = new List<ArrivalMeal>();
            iArrivalAccompanied = new List<ArrivalAccompanied>();
        }
        public int ArrivalId { get; set; }

        [Required(ErrorMessage = "Select Arrival Place")]
        [Display(Name = "Arrival at")]
        public string ArrivaAt { get; set; }

        [Required(ErrorMessage = "Enter Date")]
        [Display(Name = "Date")]
        public DateTime ArrivalDate { get; set; }

        [Required(ErrorMessage = "Enter Time")]
        [Display(Name = "Time")]
        public TimeSpan ArrivalTime { get; set; }

        [Required(ErrorMessage = "Select Arrival Mode")]
        [Display(Name = "Mode of Arrival")]
        public string ArrivalMode { get; set; }

        [Required(ErrorMessage = "Enter Transportation Mode")]
        [Display(Name = "Transportation Mode")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string TransportationMode { get; set; }

        [Required(ErrorMessage = "Enter Assistance Required")]
        [Display(Name = "Assistance Required at..for")]
        public string AssistanceRequired { get; set; }


        #region Meals Required

        [Display(Name = "Meals Required")]
        public bool MealRequired { get; set; }

        [Required(ErrorMessage = "Enter No of Meals")]
        [Display(Name = "No of Meals")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string NoofMeals { get; set; }

        [Required(ErrorMessage = "Enter From Date")]
        [Display(Name = "From Date")]
        public DateTime? MealFromDate { get; set; }

        [Required(ErrorMessage = "Enter To Date")]
        [Display(Name = "To Date")]
        public DateTime? MealToDate { get; set; }

        [Required(ErrorMessage = "Enter Dietary Preference of the family")]
        [Display(Name = "Dietary Preference of the family")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string MealDietPreference { get; set; }
        #endregion

        #region Food Detachment

        [Display(Name = "Food Detachment")]
        public bool FoodDetachment { get; set; }

        [Required(ErrorMessage = "Enter From Date")]
        [Display(Name = "From Date")]
        public DateTime? DetachFromDate { get; set; }

        [Required(ErrorMessage = "Enter To Date")]
        [Display(Name = "To Date")]
        public DateTime? DetachToDate { get; set; }

        [Required(ErrorMessage = "Enter Meal Info")]
        [Display(Name = "Any other specific info pertaining to meal requirement")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string DetachMealInfo { get; set; }

        [Required(ErrorMessage = "Enter Meals Charges")]
        [Display(Name = "Meals Charges")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string DetachCharges { get; set; }
        #endregion

        public virtual ICollection<ArrivalMeal> iArrivalMeals { get; set; }
        public virtual ICollection<ArrivalAccompanied> iArrivalAccompanied { get; set; }
    }

    public class ArrivalDetailIndxVM : ArrivalDetailVM
    {
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
    }
    public class ArrivalDetailCrtVM : ArrivalDetailVM
    {

    }
    public class ArrivalDetailUpVM : ArrivalDetailVM
    {

    }
    public class ArrivalDetailCompletePreviewVM
    {
        public ArrivalDetailIndxVM ArrivalMasterVM { get; set; }
        public ArrivalMeal ArrivalMealVM { get; set; }
        public ArrivalAccompanied ArrivalAccomapniedVM { get; set; }
    }
    #endregion

    #region Ration
    public class ServiceRationVM : BaseEntityVM
    {
        public int RationId { get; set; }

        [Required(ErrorMessage = "Enter Personal No")]
        [Display(Name = "Personal No")]
        public string PersonalNo { get; set; }

        //[Required(ErrorMessage = "Enter CDA Account No.")]
        //[Display(Name = "CDAAcNo")]
        //public string CDAAcNo { get; set; }

        [Required(ErrorMessage = "Enter Eater")]
        [Display(Name = "Eater")]
        public string Eater { get; set; }

        [Required(ErrorMessage = "Enter LRC")]
        [Display(Name = "LRC")]
        public string LRC { get; set; }
    }
    public class ServiceRationIndxVM : ServiceRationVM
    {
    }
    public class ServiceRationCrtVM : ServiceRationVM
    {
    }
    #endregion

    #region Complete Preview
    public class MemberCompletePreviewVM
    {
        public CrsMemberPersonalIndxVM PersonalVM { get; set; }
        public CrsMbrAppointmentIndxVM AppointmentVM { get; set; }
        public CrsMbrAddressIndxVM AddressVM { get; set; }

        public List<CrsMbrQualificationIndxVM> QualificationsVM { get; set; }
        public List<CountryVisitIndxVM> CountryVisitsVM { get; set; }
        public List<CrsMbrLanguageIndxVM> LanguagesVM { get; set; }
        public List<ImportantAssignmentIndxVM> ImportantAssignmentsVM { get; set; }

        public BiographyIndxVM BiographyVM { get; set; }

        public SpouseIndxVM SpouseVM { get; set; }
        public List<ChildrenIndxVM> ChildrensVM { get; set; }
        //public List<SpouseQualificationIndxVM> SpouseQualificationsVM { get; set; }
        //public List<SpouseLanguageIndxVM> SpouseLanguagesVM { get; set; }
        public PassportDetailIndxVM PassportVM { get; set; }
        public List<PassportChildrenIndxVM> PassportChildrensVM { get; set; }

        public VisaDetailIndxVM VisaVM { get; set; }
        public TallyDetailIndxVM TallyVM { get; set; }
        public List<CrsMbrVehicleStickerIndxVM> VehicleStickerVM { get; set; }
        public AccountInfoIndxVM AccountInfoVM { get; set; }
    }
    #endregion

}