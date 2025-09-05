using NDCWeb.Areas.Member.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Staff.View_Models
{
    public class TrainingRptVM
    {
    }
    public class ShowSpeechFeedbackSummaryVM
    {
        public ShowSpeechFeedbackSummaryVM()
        {
            iSpeechFeedbackVM = new List<ShowSpeechFeedbackAllVM>();
        }
        public DateTime SpeechDate { get; set; }
        public string FullName { get; set; }
        public string SubjectName { get; set; }
        public string TopicName { get; set; }
        public int SpeechEventId { get; set; }

        public int Excellent { get; set; }
        public int VeryGood { get; set; }
        public int Average { get; set; }
        public int Poor { get; set; }
        public int Recommend { get; set; }
        public int NotRecommend { get; set; }

        public virtual ICollection<ShowSpeechFeedbackAllVM> iSpeechFeedbackVM { get; set; }
    }

    public class ShowSpeechFeedbackNotingVM
    {
        //public DateTime SpeechDate { get; set; }
        //public string FullName { get; set; }
        //public string SubjectName { get; set; }
        //public string TopicName { get; set; }
        //public int SpeechEventId { get; set; }

        #region feedback noting
        public string SpeakerName { get; set; }
        public int NoOfInput { get; set; }
        public double Excellent { get; set; }
        public double VeryGood { get; set; }
        public double Good { get; set; }
        public double Poor { get; set; }
        public double Recommend { get; set; }
        public double NotRecommend { get; set; }
        #endregion
    }
    public class SpeechFeedbackNotingListVM
    {
        //public string SpeakerName { get; set; }
        //public int NoOfInputs { get; set; }
        //public int Excellent { get; set; }
        //public int VeryGood { get; set; }
        //public int Average { get; set; }
        //public int Poor { get; set; }
        //public int Recommend { get; set; }
        //public int NotRecommend { get; set; }
    }
    public class TrainingReportVM
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string Height { get; set; }
        public DateTime DOSeniority { get; set; }
        public DateTime DOJoining { get; set; }
        public string SpouseName { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string MemberPassportNo { get; set; }
        public DateTime MemberPassportValidUpto { get; set; }
        public string VisaNo { get; set; }
        public DateTime VisaValidUpto { get; set; }
    }
}