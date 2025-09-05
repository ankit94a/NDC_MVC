using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class TopicMaster:BaseEntity
    {
        [Key]
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public int SubjectId { get; set; }
        public virtual SubjectMaster Subjects { get; set; }
    }
}