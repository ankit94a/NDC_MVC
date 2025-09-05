using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Member.View_Models
{
    #region MPhil Member
    public class MPhilMemberVM
    {
        [Key]
        [Display(Name = "MPhil Id")]
        public int MPhilId { get; set; }

        [Required(ErrorMessage = "Enter College Applied")]
        [Display(Name = "Name of the College Applied to")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string CollegeApplied { get; set; }

        [Required(ErrorMessage = "Enter Subject")]
        [Display(Name = "Subject")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Enter Applicant Name (In English)")]
        [Display(Name = "Name of the Applicant (In English)")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string ApplicantNameEnglish { get; set; }

        [Required(ErrorMessage = "Enter Applicant Name (in Vernacular)")]
        [Display(Name = "Name of the Applicant (in Vernacular)")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string ApplicantNameVernacular { get; set; }
        
        #region Location
        [Required(ErrorMessage = "Enter Communication Address")]
        [Display(Name = "Address")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string CommunicationAddress { get; set; }

        [Required(ErrorMessage = "Enter Communication Mob")]
        [Display(Name = "Mob")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string CommunicationMob { get; set; }

        [Required(ErrorMessage = "Enter Communication Pin")]
        [Display(Name = "Pin")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string CommunicationPin { get; set; }

        [Required(ErrorMessage = "Enter Permanent Address")]
        [Display(Name = "Address")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string PermanentAddress { get; set; }

        [Required(ErrorMessage = "Enter Permanent Mob")]
        [Display(Name = "Mob")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string PermanentMob { get; set; }

        [Required(ErrorMessage = "Enter Permanent Pin")]
        [Display(Name = "Pin")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string PermanentPin { get; set; }
        #endregion

        [Required(ErrorMessage = "Enter Email")]
        [Display(Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid e-mail adress")]

        public string EmailId { get; set; }

        [Required(ErrorMessage = "Enter Date of Birth")]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOBirth { get; set; }

        [Required(ErrorMessage = "Enter Father's Name")]
        [Display(Name = "Father's Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "Enter Mother's Name")]
        [Display(Name = "Mother's Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string MotherName { get; set; }

        [Required(ErrorMessage = "Enter Gender")]
        [Display(Name = "Gender")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Special chars not allowed")]
        public string Gender { get; set; }


        [Required(ErrorMessage = "Enter Nationality")]
        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "Enter Community")]
        [Display(Name = "Community")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Community { get; set; }

        [Display(Name = "Physically challenged")]
        public bool IsDisabled { get; set; }

        [Required(ErrorMessage = "Specify Physical Disability")]
        [Display(Name = "Specify Physical Handicap, if any ")]
        public string PhysicalHandicap { get; set; }

        [Required(ErrorMessage = "Specify No of Years Studied in School and College")]
        [Display(Name = "No of Years Studied in School and College:")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string YearsStudies { get; set; }

        [Required(ErrorMessage = "Enter Your Qualification")]
        [Display(Name = "Qualification of the Applicant. Degree")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string DegreeName { get; set; }

        [Required(ErrorMessage = "Enter Subject")]
        [Display(Name = "Subject (Main)")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string SubjectMain { get; set; }

        [Required(ErrorMessage = "Enter Name of College/Institute and University")]
        [Display(Name = "Name of College/Institute and University where studied")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string UniversityCollegeStudied { get; set; }

        [Display(Name = "Any other information which you would like to present in support of your application")]
        [RegularExpression(@"^[a-zA-Z0-9., ]*$", ErrorMessage = "Special chars not allowed")]
        public string AnyOtherInformation { get; set; }

        [Required(ErrorMessage = "Please agree to the terms & conditions.")]
        [Display(Name = "I hereby declare at the particulars furnished above are correct.")]
        public bool Undertaking { get; set; }

        #region File Path
        public string MemberImgPath { get; set; }
        public string MarksStatementDocPath { get; set; }
        public string PostGradDegreeDocPath { get; set; }
        public string CourseCertificateDocPath { get; set; }
        public string TranslatedCopyEngDocPath { get; set; }
        #endregion
    }

    public class MPhilMemberIndxVM : MPhilMemberVM
    {
        
    }
    public class MPhilMemberEnrolVM : MPhilMemberVM
    {
    }
    public class MPhilMemberCrtVM : MPhilMemberVM
    {
    }
    public class MPhilMemberUpVM : MPhilMemberVM
    {
    }
    public class MPhilMemberEnrolACKVM : MPhilMemberEnrolVM
    {
        public virtual ICollection<MPhilPostGraduate> iMPhilPostGraduates { get; set; }
        public virtual string LockerNo { get; set; }
        #region File Path
        public bool IsMemberImgPath
        {
            get
            {
                if (string.IsNullOrEmpty(MemberImgPath) || MemberImgPath == "#")
                    return false;
                else
                    return true;
            }
        }
        public bool IsMarksStatementDocPath
        {
            get
            {
                if (string.IsNullOrEmpty(MarksStatementDocPath) || MarksStatementDocPath == "#")
                    return false;
                else
                    return true;
            }
        }
        public bool IsPostGradDegreeDocPath
        {
            get
            {
                if (string.IsNullOrEmpty(PostGradDegreeDocPath) || PostGradDegreeDocPath == "#")
                    return false;
                else
                    return true;
            }
        }
        public bool IsCourseCertificateDocPath
        {
            get
            {
                if (string.IsNullOrEmpty(CourseCertificateDocPath) || CourseCertificateDocPath == "#")
                    return false;
                else
                    return true;
            }
        }
        public bool IsTranslatedCopyEngDocPath
        {
            get
            {
                if (string.IsNullOrEmpty(TranslatedCopyEngDocPath) || TranslatedCopyEngDocPath == "#")
                    return false;
                else
                    return true;
            }
        }
        #endregion
    }
    #endregion

    #region Arrival
   public class ArrivalDetailAck: ArrivalDetailVM
    {

    }
    #endregion
    #region M.Phil Application to MU
    public class MPhilApplicationVM
    {
        [Key]
        [Display(Name = "MPhil Id")]
        public int MPhilId { get; set; }

        [Required(ErrorMessage = "Enter Year of Admission to M.Phil Degreee")]
        [Display(Name = "Year of Admission to M.Phil Degreee")]
        [RegularExpression(@"^[0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string YearOfAdmission { get; set; }

        [Required(ErrorMessage = "Enter College Applied")]
        [Display(Name = "Name of the College Applied to")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string CollegeApplied { get; set; }

        [Required(ErrorMessage = "Enter Subject")]
        [Display(Name = "Subject")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Enter Applicant Name (In English)")]
        [Display(Name = "Name of the Applicant (In English)")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string ApplicantNameEnglish { get; set; }

        [Required(ErrorMessage = "Enter Applicant Name (in Vernacular)")]
        [Display(Name = "Name of the Applicant (in Vernacular)")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string ApplicantNameVernacular { get; set; }

        #region Location
        [Required(ErrorMessage = "Enter Communication Address")]
        [Display(Name = "Address")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string CommunicationAddress { get; set; }

        [Required(ErrorMessage = "Enter Communication Mob")]
        [Display(Name = "Mob")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string CommunicationMob { get; set; }

        [Required(ErrorMessage = "Enter Communication Pin")]
        [Display(Name = "Pin")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string CommunicationPin { get; set; }

        [Required(ErrorMessage = "Enter Permanent Address")]
        [Display(Name = "Address")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string PermanentAddress { get; set; }

        [Required(ErrorMessage = "Enter Permanent Mob")]
        [Display(Name = "Mob")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string PermanentMob { get; set; }

        [Required(ErrorMessage = "Enter Permanent Pin")]
        [Display(Name = "Pin")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string PermanentPin { get; set; }
        #endregion

        [Required(ErrorMessage = "Enter Email")]
        [Display(Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid e-mail adress")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Enter Date of Birth")]
        [Display(Name = "Date of Birth")]
        public DateTime DOBirth { get; set; }

        [Required(ErrorMessage = "Enter Father Name")]
        [Display(Name = "Father Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "Enter Mother Name")]
        [Display(Name = "Mother Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string MotherName { get; set; }

        [Required(ErrorMessage = "Enter Gender")]
        [Display(Name = "Gender")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Gender { get; set; }


        [Required(ErrorMessage = "Enter Nationality")]
        [Display(Name = "Nationality")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "Enter Community")]
        [Display(Name = "Community")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Community { get; set; }

        [Display(Name = "Physically challenged")]
        public bool IsDisabled { get; set; }

        [Required(ErrorMessage = "Specify Physical Disability")]
        [Display(Name = "Specify Physical Handicap, if any ")]
        public string PhysicalHandicap { get; set; }

        [Required(ErrorMessage = "Specify No of Years Studied in Scholl and College")]
        [Display(Name = "No of Years Studied in Scholl and College:")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string YearsStudies { get; set; }

        [Required(ErrorMessage = "Enter Your Qualification")]
        [Display(Name = "Qualification of the Applicant. Degree")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string DegreeName { get; set; }

        [Required(ErrorMessage = "Enter Subject")]
        [Display(Name = "Subject (Main)")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string SubjectMain { get; set; }

        [Required(ErrorMessage = "Enter Name of College/Institute and University")]
        [Display(Name = "Name of College/Institute and University where studied")]
        public string UniversityCollegeStudied { get; set; }

        [Display(Name = "Any other information which you would like to present in support of your application")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string AnyOtherInformation { get; set; }

        [Required(ErrorMessage = "Please agree to the terms & conditions.")]
        [Display(Name = "I hereby declare tat the particulars furnished above are correct.")]
        public bool Undertaking { get; set; }

        #region File Path
        public string MemberImgPath { get; set; }
        public string MarksStatementDocPath { get; set; }
        public string PostGradDegreeDocPath { get; set; }
        public string CourseCertificateDocPath { get; set; }
        public string TranslatedCopyEngDocPath { get; set; }
        #endregion
    }

    #endregion
}