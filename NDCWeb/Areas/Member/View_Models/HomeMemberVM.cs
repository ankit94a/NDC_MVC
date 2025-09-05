using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Member.View_Models
{
    public class HomeMemberVM
    {
    }
    public class StaffCrsMbrBirthdayAlertVM
    {
        public string FullName { get; set; }
        public string DOB { get; set; }
    }
    public class CrsMbrFamilyBirthdayAlertVM
    {
        public string MemberFullName { get; set; }
        public string Relation { get; set; }
        public string Name { get; set; }
        //public string spouseDOBirth { get; set; }
        //public string ChildDOBirth { get; set; }
    }
    public class MarriageAnniversaryAlertVM
    {
        public string FullName { get; set; }
        //public string DOMarriage { get; set; }
        public string DateTime { get; set; }
    }
    #region Old
    //public class CourseMemberBirthdayAlertVM
    //{
    //    public string MemberFullName { get; set; }
    //    public string LockerNo { get; set; }
    //}
    //public class CrsMbrSpouseBirthdayAlertVM
    //{
    //    public string MemberFullName { get; set; }
    //    public string LockerNo { get; set; }
    //    public string SpouseName { get; set; }
    //}
    //public class CrsMbrChildrenBirthdayAlertVM
    //{
    //    public string MemberFullName { get; set; }
    //    public string LockerNo { get; set; }
    //    public string ChildName { get; set; }
    //}
    //public class StaffBirthdayAlertVM
    //{
    //    public string StaffFullName { get; set; }
    //    public string Appointment { get; set; }
    //}
    #endregion
}