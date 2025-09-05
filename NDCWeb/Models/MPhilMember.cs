using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NDCWeb.Models
{
    public class MPhilMember : BaseEntity
    {
        public MPhilMember()
        {
            iMPhilPostGraduates = new List<MPhilPostGraduate>();
        }
        [Key]
        public int MPhilId { get; set; }
        public string CollegeApplied { get; set; }
        public string Subject { get; set; }
        public string ApplicantNameEnglish { get; set; }
        public string ApplicantNameVernacular { get; set; }
        public string EmailId { get; set; }
        public DateTime DOBirth { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Gender { get; set; }


        public string CommunicationAddress { get; set; }
        public string CommunicationMob { get; set; }
        public string CommunicationPin { get; set; }
        public string PermanentAddress { get; set; }
        public string PermanentMob { get; set; }
        public string PermanentPin { get; set; }


        public string Nationality { get; set; }
        public string Community { get; set; }
        public bool IsDisabled { get; set; }
        public string PhysicalHandicap { get; set; }

        public string YearsStudies { get; set; }
        public string DegreeName { get; set; }
        public string SubjectMain { get; set; }
        public string UniversityCollegeStudied { get; set; }
        public string AnyOtherInformation { get; set; }

        #region File Path
        public string MemberImgPath { get; set; }
        public string MarksStatementDocPath { get; set; }
        public string PostGradDegreeDocPath { get; set; }
        public string CourseCertificateDocPath { get; set; }
        public string TranslatedCopyEngDocPath { get; set; }
        #endregion

        public virtual ICollection<MPhilPostGraduate> iMPhilPostGraduates { get; set; }
    }
}