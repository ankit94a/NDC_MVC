using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class InStepRegistration : BaseEntity
    {
        [Key]
        public int InStepRegId { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string WhatsappNo { get; set; }
        public string PhotoPath { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string HonourAward { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string ServicesCategory { get; set; }
        public string ServiceNo { get; set; }
        public string BranchDepartment { get; set; }
        public DateTime? DateOfCommissioning { get; set; }
        public string SeniorityYear { get; set; }
       
        #region Address
        public string AddressLocal { get; set; }
        public string AddressPermanent { get; set; }
        #endregion

        #region NOK
        public string NOKName { get; set; }
        public string NOKContact { get; set; }
        #endregion

        #region passport
        public string PassportNo { get; set; }
        public string PassportName { get; set; }
        public string PassportAuthority { get; set; }
        public string PassportDocPath { get; set; }
        #endregion

        #region Aadhaar
        public string AadhaarNo { get; set; }
        public string AadhaarDocPath { get; set; }
        #endregion
       
        #region BioData
        public string BioData { get; set; }
        #endregion

        #region Travel Details /Flight Details
        public DateTime? ArrivalTime { get; set; }
        public string ArrivalMode { get; set; }
        public DateTime? DepartureTime { get; set; }
        public string DepartureMode { get; set; }
        #endregion

        #region Approval
        public string ApprovedNominationDocPath { get; set; }
        public bool Approved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        #endregion
        public string AnyOtherRequirement { get; set; }
        public string UserId { get; set; }
        public int RankId { get; set; }
        public virtual RankMaster Ranks { get; set; }
        public string OtherRank { get; set; }
        public int? CourseId { get; set; }
        public virtual InStepCourse InStepCourses { get; set; }

        [NotMapped]
        public string StudentType { get; set; }
    }
}