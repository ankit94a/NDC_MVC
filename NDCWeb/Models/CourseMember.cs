using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class CourseMember : BaseEntity
    {
        //public CourseMember()
        //{
            //iCountryVisits = new List<CountryVisit>();
            //iHonourAwards = new List<HonourAward>();
            //iCrsMbrHobbies = new List<CrsMbrHobby>();
            //iCrsMbrSports = new List<CrsMbrSport>();
            //iAsgmtAppointments = new List<AsgmtAppointment>();
            //iCrsMbrLanguages = new List<CrsMbrLanguage>();
            //iCrsMbrQualifications = new List<CrsMbrQualification>();
        //}
        [Key]
        public int CourseMemberId { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string NickName { get; set; }
        public string FatherName { get; set; }
        public DateTime DOJoining { get; set; }
        public DateTime DOSeniority { get; set; }
        public DateTime DOBirth { get; set; }
        public DateTime DOMarriage { get; set; }
        public string BloodGroup { get; set; }
        public string EmailId { get; set; }

        public string ApptDesignation { get; set; }
        public string ApptOrganisation { get; set; }
        public string ApptLocation { get; set; }

        public string CurrentAddress { get; set; }
        public string CurrentHomeTelephone { get; set; }
        public string CurrentFax { get; set; }
        public string PermanentAddress { get; set; }
        public string PermanentHomeTelephone { get; set; }
        public string PermanentFax { get; set; }
        public string OffcTelephone { get; set; }

        public string StayBySpouse { get; set; }
        public bool Undertaking { get; set; }

        #region File Path
        public string MemberImgPath { get; set; }
        public string JointImgPath { get; set; }
        #endregion


        public string Service { get; set; }
        public string Branch { get; set; }
        public int RankId { get; set; }
        public virtual RankMaster Ranks { get; set; }
        //public virtual ICollection<CountryVisit> iCountryVisits { get; set; }
        //public virtual ICollection<HonourAward> iHonourAwards { get; set; }
        //public virtual ICollection<CrsMbrHobby> iCrsMbrHobbies { get; set; }
        //public virtual ICollection<CrsMbrSport> iCrsMbrSports { get; set; }
        //public virtual ICollection<AsgmtAppointment> iAsgmtAppointments { get; set; }
        //public virtual ICollection<CrsMbrLanguage> iCrsMbrLanguages { get; set; }
        //public virtual ICollection<CrsMbrQualification> iCrsMbrQualifications { get; set; }
    }
}