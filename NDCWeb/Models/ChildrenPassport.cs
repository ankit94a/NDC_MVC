using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class ChildrenPassport : BaseEntity
    {
        [Key]
        public int ChildPassportId { get; set; }
        public string PassportNo { get; set; }
        public DateTime PassportIssueDate { get; set; }
        public DateTime PassportValidUpto { get; set; }
        public string PassportType { get; set; }
        public string PassportImgPath { get; set; }

        public int PassportId { get; set; }
        public virtual PassportDetail Passports { get; set; }
    }
}