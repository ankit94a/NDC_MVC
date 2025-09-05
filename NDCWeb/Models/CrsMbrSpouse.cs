using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class CrsMbrSpouse : BaseEntity
    {
        public CrsMbrSpouse()
        {
            //iSpouseChildrens = new List<SpouseChildren>();
            iSpouseLanguages = new List<SpouseLanguage>();
            iSpouseQualifications = new List<SpouseQualification>();
        }
        [Key]
        public int SpouseId { get; set; }
        public string SpouseName { get; set; }
        public string SpouseBloodGroup { get; set; }
        public DateTime SpouseDOBirth { get; set; }
        public string Occupation { get; set; }
        public string ContactNo { get; set; }
        public bool SpouseStay { get; set; }
        
        #region Qualification
        public string EduHigher { get; set; }
        public string EduSubject { get; set; }
        public string EduDivision { get; set; }
        public string EduUniversity { get; set; }
        #endregion

        #region passport
        public string SpousePassportNo { get; set; }
        public string SpousePassportName { get; set; }
        public DateTime? SpousePassportIssueDate { get; set; }
        public DateTime? SpousePassportValidUpto { get; set; }
        public string SpousePassportType { get; set; }
        public string SpousePassportCountryIssued { get; set; }

        public string SpousePassportImgPath { get; set; }
        public string SpousePassportBackImgPath { get; set; }

        //Added on 28/11/23 by CP
        public string HoldingPassport { get; set; }
        //End Added on 28/11/23 by CP
        #endregion

        #region Visa
        public string SpouseVisaNo { get; set; }
        public DateTime? SpouseVisaIssueDate { get; set; }
        public DateTime? SpouseVisaValidUpto { get; set; }
        public string SpouseVisaPath { get; set; }
        public string SpouseFRRONo { get; set; }
        public DateTime? SpouseFRROIssueDate { get; set; }
        public DateTime? SpouseFRROValidUpto { get; set; }
        public string SpouseFRROPath { get; set; }
        #endregion

        //public virtual ICollection<SpouseChildren> iSpouseChildrens { get; set; }
        public virtual ICollection<SpouseLanguage> iSpouseLanguages { get; set; }
        public virtual ICollection<SpouseQualification> iSpouseQualifications { get; set; }
    }
}