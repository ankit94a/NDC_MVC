using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class LibraryMembership : BaseEntity
    {
        [Key]
        public int LibraryMembershipId { get; set; }
        public string LockerNo { get; set; }
        public string MemberName { get; set; }
        public string Designation { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }

    }
}