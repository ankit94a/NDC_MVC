using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class SpouseChildren : BaseEntity
    {
        [Key]
        public int ChildId { get; set; }
        public string ChildName { get; set; }
        public string ChildGender { get; set; }
        public DateTime ChildDOBirth { get; set; }
        public string ChildOccupation { get; set; }
        public string ChildContactNo { get; set; }
        public string ChildStayWithMember { get; set; }

        #region passport
        public string ChildPassportNo { get; set; }
        public string ChildPassportName { get; set; }
        public DateTime? ChildPassportIssueDate { get; set; }
        public DateTime? ChildPassportValidUpto { get; set; }
        public string ChildPassportType { get; set; }
        public string ChildPassportCountryIssued { get; set; }

        public string ChildPassportImgPath { get; set; }
        public string ChildPassportBackImgPath { get; set; }

        //Added on 28/11/23 by CP
        public string HoldingPassport { get; set; }
        //End Added on 28/11/23 by CP
        #endregion

        #region Visa
        public string ChildVisaNo { get; set; }
        public DateTime? ChildVisaIssueDate { get; set; }
        public DateTime? ChildVisaValidUpto { get; set; }
        public string ChildVisaPath { get; set; }
        #endregion

    }
}