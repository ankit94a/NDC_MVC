using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class VisaDetail : BaseEntity
    {
        [Key]
        public int VisaId { get; set; }
        public string VisaEntryType { get; set; }
        public string VisaNo { get; set; }
        public DateTime VisaIssueDate { get; set; }
        public DateTime VisaValidUpto { get; set; }
        
        public string SelfFRRONo { get; set; }
        public DateTime SelfIssueDate { get; set; }
        public DateTime SelfValidUpto { get; set; }
        
        public string SpouseFRRONo { get; set; }
        public DateTime SpouseIssueDate { get; set; }
        public DateTime SpouseValidUpto { get; set; }

        public string VisaPath { get; set; }
        public string SelfFRROPath { get; set; }
        public string SpouseFRROPath { get; set; }
    }
}