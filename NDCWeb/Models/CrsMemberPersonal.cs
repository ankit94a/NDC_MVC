using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class CrsMemberPersonal : BaseEntity
    {
        [Key]
        public int CourseMemberId { get; set; }

        #region Names
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string FatherName { get; set; }
        public string FatherMiddleName { get; set; }
        public string FatherSurname { get; set; }
        public string MotherName { get; set; }
        public string MotherMiddleName { get; set; }
        public string MotherSurname { get; set; }
        public string NickName { get; set; }
        #endregion

        public string MobileNo { get; set; }
        public string AlternateMobileNo { get; set; }
        public string IndentificationMark { get; set; }
        public string EmailId { get; set; }
        public string AlternateEmailId { get; set; }
        public DateTime DOBirth { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime? DOMarriage { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public string Height { get; set; }
        public string VoterIdNo { get; set; }
        public string PANCardNo { get; set; }

        #region Address
        public string CommunicationAddress { get; set; }
        public string OfficeHouseNo { get; set; }
        public string OfficePremisesName { get; set; }
        public string OfficeStreet { get; set; }
        public string OfficeArea { get; set; }
        public string OfficeCity { get; set; }
        public string OfficeZipCode { get; set; }

        //public string ResidentHouseNo { get; set; }
        //public string ResidentPremisesName { get; set; }
        //public string ResidentStreet { get; set; }
        //public string ResidentArea { get; set; }
        //public string ResidentCity { get; set; }
        //public string ResidentZipCode { get; set; }
        #endregion

        public string BioSketch { get; set; }
        public bool Undertaking { get; set; }
        public string StayBySpouse { get; set; }

        #region passport
        public string HoldingPassport { get; set; }
        public string MemberPassportNo { get; set; }
        public string MemberPassportName { get; set; }
        public DateTime? MemberPassportIssueDate { get; set; }
        public DateTime? MemberPassportValidUpto { get; set; }
        public string MemberPassportType { get; set; }
        public string CountryIssued { get; set; }
        public string MemberPassportImgPath { get; set; }
        public string MemberPassportBackImgPath { get; set; }
        #endregion

        #region personal passport
        public bool HoldingPersonalPassportSelf { get; set; } = false;
        public string MemberPersonalPassportNo { get; set; }
        public string MemberPersonalPassportName { get; set; }
        public DateTime? MemberPersonalPassportIssueDate { get; set; }
        public DateTime? MemberPersonalPassportValidUpto { get; set; }
        public string CountryIssuedPersonalPassport { get; set; }

        public string MemberPersonalPassportImgPath { get; set; }
        public string MemberPersonalPassportBackImgPath { get; set; }
        #endregion

        #region visa
        public string VisaNo { get; set; }
        public DateTime? VisaIssueDate { get; set; }
        public DateTime? VisaValidUpto { get; set; }
        public string VisaPath { get; set; }
        #endregion

        #region FRRO
        public string SelfFRRONo { get; set; }
        public DateTime? SelfIssueDate { get; set; }
        public DateTime? SelfValidUpto { get; set; }
        public string SelfFRROPath { get; set; }
        #endregion

        #region File Path
        public string MemberImgPath { get; set; }
        public string JointImgPath { get; set; }
        public string AadhaarPath { get; set; }
        #endregion
        public string DietaryPref { get; set; }
        public string MedicalCategory { get; set; }

        public string SpouseName { get; set; }
        public string NOK { get; set; }
        public int? OfficeStateId { get; set; }
        [ForeignKey("OfficeStateId ")]
        public virtual StateMaster OfficeStates { get; set; }

        //public int? ResidentStateId { get; set; }
        //[ForeignKey("ResidentStateId ")]
        //public virtual StateMaster ResidentStates { get; set; }

        public int CitizenshipCountryId { get; set; }
        [ForeignKey("CitizenshipCountryId")]
        public virtual CountryMaster CitizenshipCountries { get; set; }

        public int CourseId { get; set; }
        public virtual Course Courses { get; set; }
    }
}