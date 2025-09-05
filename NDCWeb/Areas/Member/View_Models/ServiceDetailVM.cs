using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Member.View_Models
{
    public class ServiceDetailVM
    {
    }

    #region Current Appointment
    public class CrsMbrAppointmentVM
    {
        [Key]
        public int AppointmentId { get; set; }

        [Required(ErrorMessage = "Enter Designation")]
        [Display(Name = "Designation")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Enter Organisation")]
        [Display(Name = "Organisation")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Organisation { get; set; }

        [Required(ErrorMessage = "Enter Location")]
        [Display(Name = "Location (Office Address)")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Location { get; set; }

        #region Personal Servive
        [Required(ErrorMessage = "Enter Date of Joining")]
        [Display(Name = "Date of Joining")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DOJoining { get; set; }

        [Required(ErrorMessage = "Enter Date of Seniority")]
        [Display(Name = "Date of Seniority")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DOSeniority { get; set; }

        [Required(ErrorMessage = "Enter Service No")]
        [Display(Name = "Service No")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string ServiceNo { get; set; }

        [Required(ErrorMessage = "Select Service")]
        [Display(Name = "Service")]
        public string Service { get; set; }

        [Display(Name = "Branch")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Branch { get; set; }

        [Required(ErrorMessage = "Select Rank")]
        [Display(Name = "Rank")]
        public int RankId { get; set; }
        public virtual RankMaster Ranks { get; set; }
        #endregion
        #region Added as requested on 24/11/23
        //[Required(ErrorMessage = "Enter Holding Passport")]
        [Display(Name = "Are you working as DA/MA ?")]
        public string WorkingAsDAMA { get; set; }

        //[Required(ErrorMessage = "Enter Self Passport No")]
        [Display(Name = "Please mention details")]
        public string WorkingAsDAMADetails { get; set; }
        #endregion
    }
    public class CrsMbrAppointmentIndxVM : CrsMbrAppointmentVM
    {
    }
    public class CrsMbrAppointmentCrtVM : CrsMbrAppointmentVM
    {
    }
    public class CrsMbrAppointmentUpVM : CrsMbrAppointmentVM
    {
    }
    #endregion

    #region Qualifications
    public class CrsMbrQualificationVM
    {
        [Key]

        public int QualificationId { get; set; }

        [Required(ErrorMessage = "Enter Course Name")]
        [Display(Name = "Course Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Course { get; set; }

        [Required(ErrorMessage = "Enter Qualification Type")]
        [Display(Name = "Qualification Type")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string QualificationType { get; set; }

        [Required(ErrorMessage = "Enter Qualification Year")]
        [Display(Name = "Year")]
        [RegularExpression(@"^[0-9- ]*$", ErrorMessage = "Special chars not allowed")]
        public string Year { get; set; }

        [Required(ErrorMessage = "Enter Location")]
        [Display(Name = "Location")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Enter Country Name")]
        [Display(Name = "Country")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Country { get; set; }

    }
    public class CrsMbrQualificationIndxVM : CrsMbrQualificationVM
    {
    }
    public class CrsMbrQualificationCrtVM : CrsMbrQualificationVM
    {
    }
    public class CrsMbrQualificationUpVM : CrsMbrQualificationVM
    {
    }
    #endregion

    #region Country Visited
    public class CountryVisitVM
    {
        [Key]
        public int CountryVisitId { get; set; }

        [Required(ErrorMessage = "Enter Country Name")]
        [Display(Name = "Country")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Enter Year of Visit")]
        [Display(Name = "Year of Visit")]
        [RegularExpression(@"^[0-9- ]*$", ErrorMessage = "Special chars not allowed")]
        public string Visit { get; set; }

        [Required(ErrorMessage = "Enter Duration")]
        [Display(Name = "Duration")]
        [RegularExpression(@"^[0-9- ]*$", ErrorMessage = "Special chars not allowed")]
        public string Duration { get; set; }

        [Required(ErrorMessage = "Enter Purpose")]
        [Display(Name = "Purpose")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string Purpose { get; set; }

    }
    public class CountryVisitIndxVM : CountryVisitVM
    {
    }
    public class CountryVisitCrtVM : CountryVisitVM
    {
    }
    public class CountryVisitUpVM : CountryVisitVM
    {
    }
    #endregion

    #region Language
    public class CrsMbrLanguageVM
    {
        [Key]
        public int LanguageId { get; set; }

        [Required(ErrorMessage = "Enter Language")]
        [Display(Name = "Language")]
        public string Language { get; set; }

        [Display(Name = "Read")]
        public bool Read { get; set; }

        [Display(Name = "Write")]
        public bool Write { get; set; }

        [Display(Name = "Speak")]
        public bool Speak { get; set; }

        [Display(Name = "Qualification")]
        public string Qualification { get; set; }
    }
    public class CrsMbrLanguageIndxVM : CrsMbrLanguageVM
    {
    }
    public class CrsMbrLanguageCrtVM : CrsMbrLanguageVM
    {
    }
    public class CrsMbrLanguageUpVM : CrsMbrLanguageVM
    {
    }
    #endregion

    #region Assignment & Appointment
    public class AsgmtAppointmentVM
    {
        [Key]
        public int AsgmtAppointmentId { get; set; }

        [Required(ErrorMessage = "Enter Appointment")]
        [Display(Name = "Appointment")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Appointment { get; set; }

        [Required(ErrorMessage = "Enter Organisation")]
        [Display(Name = "Organisation")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string Organisation { get; set; }

        [Required(ErrorMessage = "Enter Duration")]
        [Display(Name = "Duration (months)")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string Duration { get; set; }

        [Required(ErrorMessage = "Enter Location")]
        [Display(Name = "Location")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Enter From date")]
        [Display(Name = "From")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime From { get; set; }

        [Required(ErrorMessage = "Enter Date till")]
        [Display(Name = "To")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime To { get; set; }
    }
    public class ImportantAssignmentIndxVM : AsgmtAppointmentVM
    {
    }
    public class ImportantAssignmentCrtVM : AsgmtAppointmentVM
    {
    }
    public class ImportantAssignmentUpVM : AsgmtAppointmentVM
    {
    }
    #endregion

    #region Service Multiple
    public class ServiceMultipleVM
    {

    }
    public class ServiceMultipleReadVM
    {

    }
    public class ServiceMultipleAddVM
    {
        public CrsMbrAppointment Appointment { get; set; }
        public List<CrsMbrQualification> Qualifications { get; set; }
        public List<CountryVisit> CountryVisits { get; set; }
        public List<CrsMbrLanguage> Languages { get; set; }
        public List<AsgmtAppointment> ImportantAssignments { get; set; }
    }
    #endregion
}