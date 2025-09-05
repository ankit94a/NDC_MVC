using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class FeedbackModule : BaseEntity
    {
        [Key]
        public int ModuleFeedbackId { get; set; }
        public string CoordChairperson { get; set; }
        public string TopicForDelete { get; set; }
        public string TopicForAdition { get; set; }
        public string Suggestions { get; set; }
        public string SuggestChanges { get; set; }
        public string SuggestionOther { get; set; }
        public string CommentsAndRecomedation { get; set; }
        public int SubjectId { get; set; }
        public virtual SubjectMaster Subjects { get; set; }
    }
}