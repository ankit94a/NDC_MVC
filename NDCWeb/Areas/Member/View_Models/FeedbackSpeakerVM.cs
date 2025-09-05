using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Member.View_Models
{
    public class FeedbackSpeakerVM : BaseEntityVM
    {
        [Key]
        [Required(ErrorMessage = "Id Not Supplied")]
        [Display(Name = "Id")]
        public int SpeakerFeedbackId { get; set; }

        //[Required(ErrorMessage = "Enter Speaker Name")]
        //[Display(Name = "Name of the Speaker")]
        //public string SpeakerName { get; set; }

        [Required(ErrorMessage = "Quality of Talk is a must")]
        [Display(Name = "Quality of Talk")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string QualityTalk { get; set; }

        //[Required(ErrorMessage = "Recomendation is a must")]
        [Display(Name = "Do you recomend the speaker for next course")]
        public string RecomendForNextCourse { get; set; }

        [Display(Name = "Any other suggestion")]
        //[RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string Suggetions { get; set; }

        //[Required(ErrorMessage = "Enter Date of Speech")]
        //[Display(Name = "Date of Speech")]
        //[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime FeedbackDate { get; set; }

        public int SpeechEventId { get; set; }
        public virtual SpeechEvent SpeechEvents { get; set; }

        //[Required(ErrorMessage = "Speaker Not Selected")]
        //[Display(Name = "Speaker")]
        //public int SpeakerId { get; set; }
        //public virtual Speaker Speakers { get; set; }

        [Display(Name = "Lecture Attended")]
        public string LectureAttend { get; set; }
    }
    public class FeedbackSpeakerIndexVM : FeedbackSpeakerVM
    {

    }    
    public class FeedbackSpeakerCreateVM : FeedbackSpeakerVM
    {
        //[Required(ErrorMessage = "Study Not Selected")]
        //[Display(Name = "Study")]
        //public int SubjectId { get; set; }
        ////public virtual SubjectMaster Subjects { get; set; }

        //[Required(ErrorMessage = "Topic Not Selected")]
        //[Display(Name = "Topic")]
        //public int TopicId { get; set; }
        ////public virtual TopicMaster Topic { get; set; }
    }
    public class FeedbackSpeakerUpdateVM : FeedbackSpeakerVM
    {

    }
    public class ShowSpeechFeedbackAllVM
    {
        #region speech feedback
        public int SpeakerFeedbackId { get; set; }
        public string QualityTalk { get; set; }
        public string RecomendForNextCourse { get; set; }
        public string Suggetions { get; set; }
        public string LectureAttend { get; set; }
        #endregion

        #region speech event
        public int SpeechEventId { get; set; }
        public DateTime SpeechDate { get; set; }
        public DateTime FeedbackStartDate { get; set; }
        public DateTime FeedbackEndDate { get; set; }
        public bool Active { get; set; }
        #endregion

        #region speaker
        public int SpeakerId { get; set; }
        public string FullName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Telephone { get; set; }
        public string CurrentAddress { get; set; }
        public string PhotoPath { get; set; }
        public string FilePath { get; set; }
        #endregion

        #region Topic
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        #endregion

        #region Subject
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        #endregion

        #region Member
        public string MemberFullName { get; set; }
        public string LockerNo { get; set; }
        public string ServiceNo { get; set; }
        #endregion
    }
}