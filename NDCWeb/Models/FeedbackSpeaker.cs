using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class FeedbackSpeaker:BaseEntity
    {
        [Key]
        public int SpeakerFeedbackId { get; set; }
        //public string SpeakerName { get; set; }
        public string QualityTalk { get; set; }
        public string RecomendForNextCourse { get; set; }
        public string Suggetions { get; set; }
        //public DateTime FeedbackDate { get; set; }

        public int SpeechEventId { get; set; }
        public virtual SpeechEvent SpeechEvents { get; set; }

        public string LectureAttend { get; set; }
        //public int SpeakerId { get; set; }
        //public virtual Speaker Speakers { get; set; }
    }
}