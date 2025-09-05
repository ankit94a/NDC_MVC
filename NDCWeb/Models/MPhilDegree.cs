using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class MPhilDegree : BaseEntity
    {
        [Key]
        public int MPhilDegreeId { get; set; }
        public int YearOfAdmission { get; set; }
        public string NameOfApplicant { get; set; }
        public string NameOfSupervisor { get; set; }
        public DateTime DOB { get; set; }
        public string POB { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Community { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }
        public string StudyMode { get; set; }
        public string NameOfExam { get; set; }
        public string RegisterNoMonthYear { get; set; }

        public string MonthAndYearOfDegree { get; set; }
        public string DegreeCollegeName { get; set; }
        public string NoAndDateOfEligibilityCert { get; set; }
        public string AffiliateCollege { get; set; }
        public string IsRecognisedForMphil { get; set; }
        public string ObtainedApproval { get; set; }
        public string NoAndDateOfApproval { get; set; }
        public string NameDesignationOfSupervisor { get; set; }
        public string IsSupervisorRecognisedForCourse { get; set; }
        public string SupervisorSignPath { get; set; }
        public string HoDSignPath { get; set; }
        public string PlaceOfApplication { get; set; }
        public DateTime DateOfApplication { get; set; }
    }
}