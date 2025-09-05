using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Staff.View_Models
{
    public class SpeechEventVM
    {
        [Key]
        public int SpeechEventId { get; set; }

        [Required(ErrorMessage = "Speaker Speech Date")]
        [Display(Name = "Speech Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SpeechDate { get; set; }

        [Required(ErrorMessage = "Enter Feedback Start Date")]
        [Display(Name = "Feedback Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FeedbackStartDate { get; set; }

        [Required(ErrorMessage = "Enter Feedback End Date")]
        [Display(Name = "Feedback End Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FeedbackEndDate { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "Speaker Not Selected")]
        [Display(Name = "Speaker")]
        public int SpeakerId { get; set; }
        public virtual Speaker Speakers { get; set; }
    }
    public class SpeechEventIndxVM : SpeechEventVM
    {
    }
    public class SpeechEventCrtVM : SpeechEventVM
    {
        [Required(ErrorMessage = "Study Not Selected")]
        [Display(Name = "Study")]
        public int SubjectId { get; set; }
        //public virtual SubjectMaster Subjects { get; set; }

        [Required(ErrorMessage = "Topic Not Selected")]
        [Display(Name = "Topic")]
        public int TopicId { get; set; }
        //public virtual TopicMaster Topic { get; set; }
    }
    public class SpeechEventUpVM : SpeechEventVM
    {
    }
    public class SpeechEventDetailVM : SpeechEventVM
    {
    }
    public class SpeechEventAll : SpeechEventVM
    {
        [Display(Name = "Subject")]
        public string SubjectName { get; set; }
  
        [Display(Name = "Topic")]
        public string TopicName { get; set; }
        [Display(Name = "Speaker")]
        public string FullName { get; set; }
    }
}