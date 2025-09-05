using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class PassportDetail : BaseEntity
    {
        public PassportDetail()
        {
            iChildrenPassports = new List<ChildrenPassport>();
        }
        [Key]
        public int PassportId { get; set; }
        public string HoldingPassport { get; set; }
        
        public string MemberPassportNo { get; set; }
        public DateTime MemberPassportIssueDate { get; set; }
        public DateTime MemberPassportValidUpto { get; set; }
        public string MemberPassportType { get; set; }
        
        public string SpousePassportNo { get; set; }
        public DateTime SpousePassportIssueDate { get; set; }
        public DateTime SpousePassportValidUpto { get; set; }
        public string SpousePassportType { get; set; }

        public string MemberPassportImgPath { get; set; }
        public string SpousePassportImgPath { get; set; }

        public virtual ICollection<ChildrenPassport> iChildrenPassports { get; set; }
    }
}