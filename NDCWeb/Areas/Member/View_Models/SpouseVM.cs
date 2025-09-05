using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Member.View_Models
{
    #region Spouse
    public class SpouseVM
    {
        public SpouseVM()
        {
            //iSpouseChildrens = new List<SpouseChildren>();
            iSpouseLanguages = new List<SpouseLanguage>();
            iSpouseQualifications = new List<SpouseQualification>();
        }
        public int SpouseId { get; set; }

        [Required(ErrorMessage = "Enter Spouse Name")]
        [Display(Name = "Spouse Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string SpouseName { get; set; }

        [Required(ErrorMessage = "Enter Blood Group")]
        [Display(Name = "Blood Group")]
        [RegularExpression(@"^[a-zA-Z-+]*$", ErrorMessage = "Special chars not allowed")]
        public string SpouseBloodGroup { get; set; }

        [Required(ErrorMessage = "Enter Contact No")]
        [Display(Name = "Contact No")]
        //[RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Enter Date of Birth")]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SpouseDOBirth { get; set; }

        //[Required(ErrorMessage = "Enter Service/Occupation")]
        [Display(Name = "Service/Occupation")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Occupation { get; set; }

        [Display(Name = "Spouse Stay")]
        public bool SpouseStay { get; set; }=false;

        //[Required(ErrorMessage = "Enter E-mail")]
        //[Display(Name = "E-mail")]
        //public string EmailId { get; set; }

        //[Required(ErrorMessage = "Enter Food Preference")]
        //[Display(Name = "Food Preference")]
        //public string FoodPreference { get; set; }

        //[Required(ErrorMessage = "Enter Biography")]
        //[Display(Name = "Biography")]
        //public string Biography { get; set; }

        #region passport
        //Added on 28/11/23 by CP

        [Required(ErrorMessage = "Enter Holding Passport")]
        [Display(Name = "Holding Passport")]
        public string HoldingPassport { get; set; }
        //End Added on 28/11/23 by CP
        [Display(Name = "Passport No")]
        public string SpousePassportNo { get; set; }

        //[Required(ErrorMessage = "Enter Name On Passport")]
        [Display(Name = "Name On Passport")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string SpousePassportName { get; set; }

        [Display(Name = "Passport Issue Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SpousePassportIssueDate { get; set; }

        [Display(Name = "Passport Valid Upto")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SpousePassportValidUpto { get; set; }

        [Display(Name = "Passport Type")]
        public string SpousePassportType { get; set; }

        [Display(Name = "Passport Issued Country")]
        public string SpousePassportCountryIssued { get; set; }

        public string SpousePassportImgPath { get; set; }
        public string SpousePassportBackImgPath { get; set; }
        #endregion

        #region Visa
        [Display(Name = "Visa No")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string SpouseVisaNo { get; set; }

        [Display(Name = "Visa Issue Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SpouseVisaIssueDate { get; set; }

        [Display(Name = "Visa Valid Upto")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SpouseVisaValidUpto { get; set; }
        public string SpouseVisaPath { get; set; }


        [Display(Name = "FRRO No")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string SpouseFRRONo { get; set; }

        [Display(Name = "Issue Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SpouseFRROIssueDate { get; set; }

        [Display(Name = "Valid Upto")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SpouseFRROValidUpto { get; set; }
        public string SpouseFRROPath { get; set; }
        #endregion

        //[Required(ErrorMessage = "Enter Qualification")]
        //[Display(Name = "Qualification")]
        //public string Qualification { get; set; }

        #region Qualification
        [Display(Name = "Higher Education")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string EduHigher { get; set; }

        [Display(Name = "Subject specification")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string EduSubject { get; set; }

        [Display(Name = "Grade/Division")]
        [RegularExpression(@"^[a-zA-Z0-9+ ]*$", ErrorMessage = "Special chars not allowed")]
        public string EduDivision { get; set; }

        [Display(Name = "Board/University")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string EduUniversity { get; set; }
        #endregion

        //public virtual ICollection<SpouseChildren> iSpouseChildrens { get; set; }
        public virtual ICollection<SpouseLanguage> iSpouseLanguages { get; set; }
        public virtual ICollection<SpouseQualification> iSpouseQualifications { get; set; }
    }
    public class SpouseIndxVM : SpouseVM
    {
    }
    public class SpouseCrtVM : SpouseVM
    {
    }
    public class SpouseUpVM : SpouseVM
    {
    }
    #endregion

    #region Spouse Children
    public class SpouseChildrenVM
    {
        public int ChildId { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        [Display(Name = "Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string ChildName { get; set; }

        [Required(ErrorMessage = "Enter Date of Birth")]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOBirth { get; set; }

        [Required(ErrorMessage = "Enter Gender")]
        [Display(Name = "Gender")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string ChildGender { get; set; }

        [Required(ErrorMessage = "Enter Occupation")]
        [Display(Name = "Occupation")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Occupation { get; set; }

        [Required(ErrorMessage = "Enter Living With You")]
        [Display(Name = "Living With You")]
        public string StayWithMember { get; set; }

        public string ChildBloodGroup { get; set; }
        public DateTime ChildDOBirth { get; set; }
        public string ChildOccupation { get; set; }
        public string ChildContactNo { get; set; }
        public string ChildStayWithMember { get; set; }

        #region passport
        [Required(ErrorMessage = "Enter Passport No")]
        [Display(Name = "Passport No")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string ChildPassportNo { get; set; }

        [Required(ErrorMessage = "Enter Passport Issue Date")]
        [Display(Name = "Passport Issue Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ChildPassportIssueDate { get; set; }

        [Required(ErrorMessage = "Enter Passport Valid Upto")]
        [Display(Name = "Passport Valid Upto")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ChildPassportValidUpto { get; set; }

        [Required(ErrorMessage = "Enter Passport Type")]
        [Display(Name = "Passport Type")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string ChildPassportType { get; set; }

        [Required(ErrorMessage = "Enter country that issued passport")]
        [Display(Name = "Passport Issued Country")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string ChildPassportCountryIssued { get; set; }

        public string ChildPassportImgPath { get; set; }
        public string ChildPassportBackImgPath { get; set; }
        #endregion

        #region Visa
        [Required(ErrorMessage = "Enter Visa No")]
        [Display(Name = "Visa No")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string ChildVisaNo { get; set; }

        [Required(ErrorMessage = "Enter Visa Issue Date")]
        [Display(Name = "Visa Issue Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ChildVisaIssueDate { get; set; }

        [Required(ErrorMessage = "Enter Visa Valid Upto")]
        [Display(Name = "Visa Valid Upto")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ChildVisaValidUpto { get; set; }
        public string ChildVisaPath { get; set; }

        #endregion
    }
    public class SpouseChildrenIndxVM : SpouseChildrenVM
    {
    }
    public class SpouseChildrenCrtVM : SpouseChildrenVM
    {
    }
    public class SpouseChildrenUpVM : SpouseChildrenVM
    {
    }

    #endregion

    #region Spouse Language
    public class SpouseLanguageVM
    {
        public int LanguageId { get; set; }

        //[Required(ErrorMessage = "Enter Language")]
        [Display(Name = "Language")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Language { get; set; }

        [Display(Name = "Read")]
        public bool Read { get; set; }

        [Display(Name = "Write")]
        public bool Write { get; set; }

        [Display(Name = "Speak")]
        public bool Speak { get; set; }

        [Required(ErrorMessage = "Enter Qualification")]
        [Display(Name = "Qualification")]
        public string Qualification { get; set; }

        public int SpouseId { get; set; }
        public virtual CrsMbrSpouse Spouses { get; set; }
    }
    public class SpouseLanguageIndxVM : SpouseLanguageVM
    {
    }
    public class SpouseLanguageCrtVM : SpouseLanguageVM
    {
    }
    public class SpouseLanguageUpVM : SpouseLanguageVM
    {
    }
    #endregion

    #region Spouse Qualification
    public class SpouseQualificationVM
    {
        [Required(ErrorMessage = "Enter Qualification")]
        [Display(Name = "Qualification")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public int SpouseEduId { get; set; }

        //[Required(ErrorMessage = "Enter Professional Qualification")]
        [Display(Name = "Professional Qualification")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string ProfessionalEdu { get; set; }

        //[Required(ErrorMessage = "Enter Academic Achievement")]
        [Display(Name = "Academic Achievement")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string AcademicAchievement { get; set; }

        //[Required(ErrorMessage = "Enter Grade/Division")]
        [Display(Name = "Grade/Division")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Division { get; set; }

        //[Required(ErrorMessage = "Enter Institute")]
        [Display(Name = "Institute")]
        public string Institute { get; set; }

        public int SpouseId { get; set; }
        public virtual CrsMbrSpouse Spouses { get; set; }
    }
    public class SpouseQualificationIndxVM : SpouseQualificationVM
    {
    }
    public class SpouseQualificationCrtVM : SpouseQualificationVM
    {
    }
    public class SpouseQualificationUpVM : SpouseQualificationVM
    {
    }
    #endregion

    #region Children
    public class ChildrenVM
    {
        public int ChildId { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        [Display(Name = "Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string ChildName { get; set; }

        [Required(ErrorMessage = "Enter Gender")]
        [Display(Name = "Gender")]
        public string ChildGender { get; set; }

        [Required(ErrorMessage = "Enter Date of Birth")]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ChildDOBirth { get; set; }

        //[Required(ErrorMessage = "Enter Occupation")]
        [Display(Name = "Occupation")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string ChildOccupation { get; set; }

        //[Required(ErrorMessage = "Enter Contact No")]
        [Display(Name = "Contact No")]
        //[RegularExpression(@"^([0-9]{15})$", ErrorMessage = "Invalid Mobile Number.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string ChildContactNo { get; set; }

        [Required(ErrorMessage = "Enter Living With You")]
        [Display(Name = "Living With You")]
        public string ChildStayWithMember { get; set; }

        #region passport
        //[Required(ErrorMessage = "Enter Passport No")]
        [Display(Name = "Passport No")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string ChildPassportNo { get; set; }

        //[Required(ErrorMessage = "Enter Name On Passport")]
        [Display(Name = "Name On Passport")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string ChildPassportName { get; set; }

        //[Required(ErrorMessage = "Enter Passport Issue Date")]
        [Display(Name = "Passport Issue Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ChildPassportIssueDate { get; set; }

        //[Required(ErrorMessage = "Enter Passport Valid Upto")]
        [Display(Name = "Passport Valid Upto")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ChildPassportValidUpto { get; set; }

        //[Required(ErrorMessage = "Enter Passport Type")]
        [Display(Name = "Passport Type")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string ChildPassportType { get; set; }

        //[Required(ErrorMessage = "Enter country that issued passport")]
        [Display(Name = "Passport Issued Country")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string ChildPassportCountryIssued { get; set; }

        public string ChildPassportImgPath { get; set; }
        public string ChildPassportBackImgPath { get; set; }
        #endregion

        #region Visa
        [Display(Name = "Visa No")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string ChildVisaNo { get; set; }

        [Display(Name = "Visa Issue Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ChildVisaIssueDate { get; set; }

        [Display(Name = "Visa Valid Upto")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ChildVisaValidUpto { get; set; }
        public string ChildVisaPath { get; set; }

        #endregion
    }
    public class ChildrenIndxVM : ChildrenVM
    {
    }
    public class ChildrenCrtVM : ChildrenVM
    {
    }
    public class ChildrenUpVM : ChildrenVM
    {
    }
    #endregion
}