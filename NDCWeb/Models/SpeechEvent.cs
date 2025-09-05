using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class SpeechEvent : BaseEntity
    {
        [Key]
        public int SpeechEventId { get; set; }
        public DateTime SpeechDate { get; set; }
        public DateTime FeedbackStartDate { get; set; }
        public DateTime FeedbackEndDate { get; set; }
        public bool Active { get; set; }

        public int SpeakerId { get; set; }
        public virtual Speaker Speakers { get; set; }
    }
}