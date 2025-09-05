using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Staff.View_Models
{
    public class MemberLeaveReportVM
    {

    }
    public class LeaveIndianCourseParticipantRptVM : MemberLeaveReportVM
    {
        public string ServiceNo { get; set; }
        public string RankName { get; set; }
        public string FullName { get; set; }
        public string LeaveType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime PrefixDate { get; set; }
        public DateTime SuffixDate { get; set; }
        public string TotalDays { get; set; }
        public string ReasonForLeave { get; set; }
        public string AddressOnLeave { get; set; }
        public string TeleNo { get; set; }
        public string RecommendedByEmbassy { get; set; }
        public string CasualLeaveAvailed { get; set; }
        public string AnnualLeaveAvailed { get; set; }
        public DateTime LeaveDate { get; set; }
        public DateTime AQSignDate { get; set; }
        public DateTime IAGSDSDate { get; set; }
        public DateTime ServiceSDSDate { get; set; }
    }
}