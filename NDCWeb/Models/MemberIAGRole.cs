using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class MemberIAGRole : BaseEntity
    {
        public int IAGRoleId { get; set; }
        public string Module { get; set; }
        public string Role { get; set; }

        public int? CourseMemberId { get; set; }
        public virtual CrsMemberPersonal CrsMemberPersonals { get; set; }
    }
}