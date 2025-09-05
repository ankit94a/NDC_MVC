using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Member.View_Models
{
    public class FeedbackModuleVM
    {
        [Key]

        [Required(ErrorMessage = "Id Not Supplied")]
        [Display(Name = "Id")]
        public int ModuleFeedbackId { get; set; }

        [Required(ErrorMessage = "Enter Coordinating Chairperson")]
        [Display(Name = "Coordinating Chairperson")]
        //[RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string CoordChairperson { get; set; }

        [Required(ErrorMessage = "Topics for Deletion")]
        [Display(Name = "Lecture (Topics) for Deletion")]
        //[RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string TopicForDelete { get; set; }

        [Required(ErrorMessage = "Topics for Addition")]
        [Display(Name = "Lecture (Topic) for Addition")]
        //[RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string TopicForAdition { get; set; }

        [Required(ErrorMessage = "Suggestion on Books... is a must")]
        [Display(Name = "Suggestion on Books, periodicals, articles and other study materials")]
        //[RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string Suggestions { get; set; }

        [Required(ErrorMessage = "Suggested changes in... is a must")]
        [Display(Name = "Suggested changes in framing of IAG Assignments (including changes of IAG Assignments)")]
        //[RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string SuggestChanges { get; set; }

        [Display(Name = "Any other Suggestion")]
        //[RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string SuggestionOther { get; set; }

        [Display(Name = "Comments / Recomendations of SDS I/c IAG")]
        //[RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string CommentsAndRecomedation { get; set; }

        [Required(ErrorMessage = "Module No Selected")]
        [Display(Name = "Module")]
        public int SubjectId { get; set; }
        public virtual SubjectMaster Subjects { get; set; }

    }
    public class StaffFeedbackModuleVM : FeedbackModuleVM
    {
        public string LockerNo { get; set; }
        public bool IsSDS { get; set; }
    }
    public class FeedbackModuleIndexVM : FeedbackModuleVM
    {

    }
    public class FeedbackModuleCreateVM : FeedbackModuleVM
    {
    }
    public class FeedbackModuleUpdateVM 
    {
        [Required(ErrorMessage = "Id Not Supplied")]
        [Display(Name = "Id")]
        public int ModuleFeedbackId { get; set; }
        
        [Display(Name = "Comments/Recomendations of SDS I/c IAG")]
        [Required(ErrorMessage = "Comments And Recomedation Not Supplied")]
        public string CommentsAndRecomedation { get; set; }
        public string Choice { get; set; }
    }
}