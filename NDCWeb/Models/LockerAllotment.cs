using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class LockerAllotment : BaseEntity
    {
        public int LockerAllotmentId { get; set; }
        public string FullName { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string LockerNo { get; set; }
        public string RolesAssign { get; set; }

        //public int CourseId { get; set; }
        //public virtual Course Courses { get; set; }

        public string IAG { get; set; }

        public int? SDSId { get; set; }
        [ForeignKey("SDSId ")]
        public virtual StaffMaster SDSStaffMasters { get; set; }

        public int CourseMemberId { get; set; }
        public virtual CrsMemberPersonal CrsMemberPersonals { get; set; }
    }
}