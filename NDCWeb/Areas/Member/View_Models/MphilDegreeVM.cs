using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Member.View_Models
{
    public class MphilDegreeVM
    {
        [Key]
        [Required(ErrorMessage = "MPhilDegree Id Not Supplied")]
        [Display(Name = "MPhil Degree Id")]
        public int MPhilDegreeId { get; set; }

        [Required(ErrorMessage = "Year of Admission is Required")]
        [Display(Name = "Year of Admission")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Special chars not allowed")]
        public int YearOfAdmission { get; set; }

        [Required(ErrorMessage = "Name of Applicant is Required")]
        [Display(Name = "Name of Applicant as entered in Degree Certificate")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string NameOfApplicant { get; set; }

        [Required(ErrorMessage = "Name of Supervisor is Required")]
        [Display(Name = "Supervisor's Name")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string NameOfSupervisor { get; set; }

        [Required(ErrorMessage = "DOB is Required")]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Place is Required")]
        [Display(Name = "Place of Birth")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string POB { get; set; }

        [Required(ErrorMessage = "Age is Required")]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Community is Required")]
        [Display(Name = "Community")]
        public string Community { get; set; }


        [Required(ErrorMessage = "Present Occupation is Required")]
        [Display(Name = "Present Occupation")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string Occupation { get; set; }

        [Required(ErrorMessage = "Present Address is Required")]
        [Display(Name = "Present Address")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Mode is Required")]
        [Display(Name = "Full-time research student or a Teacher Candidate (Part-time")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string StudyMode { get; set; }
  
        [Required(ErrorMessage = "Examination is Required")]
        [Display(Name = "Name of Examination Passed with Branch Offered and name of University")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string NameOfExam { get; set; }

        [Required(ErrorMessage = "Register No with month and year is Required")]
        [Display(Name = "Register No with month and year of Passing")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string RegisterNoMonthYear { get; set; }

        [Required(ErrorMessage = "Register No with month and year is Required")]
        [Display(Name = "Month & Year of  in which Degree was taken at a convocation")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string MonthAndYearOfDegree { get; set; }

        [Required(ErrorMessage = "College of Institute is Required")]
        [Display(Name = "College or Institution through which the applicant studied for Degree")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string DegreeCollegeName { get; set; }

        [Required(ErrorMessage = "No and Date of Eligibility Certificate is Required")]
        [Display(Name = "If the examination passed is from another University state the No and Date of Eligibility Certificate")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string NoAndDateOfEligibilityCert { get; set; }

        [Required(ErrorMessage = "Affiliatet college is Required")]
        [Display(Name = "Department of the University of Madras of of the College affiliated to this University in which the applicant proposes to undergo the M.Phil Course")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string AffiliateCollege { get; set; }

        [Required(ErrorMessage = "Whether degree is recognised is Required")]
        [Display(Name = "Whether the Department has beenrecognised previously by the University for Conducting M.Phil")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string IsRecognisedForMphil { get; set; }

        [Required(ErrorMessage = "Whether candidate has obtained approval is Required")]
        [Display(Name = "Whether the candidate has obtained neccessary approval of admission fro the University to undergo the course in the University departments/colleges")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string ObtainedApproval { get; set; }

        [Required(ErrorMessage = "Quote the No and Date is Required")]
        [Display(Name = "If so, quote the number and date of this office communication conveying approval of admission to undergo the course in the University departments/colleges")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string NoAndDateOfApproval { get; set; }

        [Required(ErrorMessage = "Name and designation of supervisor is Required")]
        [Display(Name = "Name and designation of the supervisor who conducts the M.Phil Course")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string NameDesignationOfSupervisor { get; set; }

        [Required(ErrorMessage = "Whether the supervisor conducting the course is recognised Required")]
        [Display(Name = "Whether the supervisor conducting the course has been provisionally recognised by this University for guiding the research work of candidate for research degree(M.Phil/Ph.D) No and date of Communication should be quoted")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string IsSupervisorRecognisedForCourse { get; set; }

        [Required(ErrorMessage = "Signature of the supervisor is Required")]
        [Display(Name = "Signature of the supervisor conducting the M.Phil Degree course with full name and designation ")]
        public string SupervisorSignPath { get; set; }

        [Required(ErrorMessage = "Signature of the HoD is Required")]
        [Display(Name = "Signature of the Head of the Department with full name and designation")]
        public string HoDSignPath { get; set; }

        [Required(ErrorMessage = "Station is Required")]
        [Display(Name = "Station")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string PlaceOfApplication { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfApplication { get; set; }
    }
    public class MphilDegreeIndxVM: MphilDegreeVM
    {

    }
    public class MphilDegreeCrtVM : MphilDegreeVM
    {

    }
    public class MphilDegreeUpVM : MphilDegreeVM
    {

    }
}