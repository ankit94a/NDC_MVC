using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Staff.View_Models
{
    public class MemberIAGRoleVM
    {
        public int IAGRoleId { get; set; }
        public string Module { get; set; }
        public string Role { get; set; }

        public int? CourseMemberId { get; set; }
        public virtual CrsMemberPersonal CrsMemberPersonals { get; set; }
    }
    public class MemberIAGRoleIndxVM : MemberIAGRoleVM 
    {
    }
    public class MemberIAGRoleCrtVM : MemberIAGRoleVM
    {
    }
    public class MemberIAGRoleUpVM : MemberIAGRoleVM
    {
    }
}