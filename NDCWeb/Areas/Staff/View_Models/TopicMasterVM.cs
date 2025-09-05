using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Staff.View_Models
{
    public class TopicMasterVM
    {
        [Key]
        public int TopicId { get; set; }

        [Required(ErrorMessage = "Enter Topic")]
        [Display(Name = "Topic")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string TopicName { get; set; }

        [Required(ErrorMessage = "Select Subject")]
        [Display(Name = "Subject")]
        public int SubjectId { get; set; }
        public virtual SubjectMaster Subjects { get; set; }
    }
    public class TopicMasterIndxVM : TopicMasterVM
    {
    }
    public class TopicMasterCrtVM : TopicMasterVM
    {
    }
    public class TopicMasterUpVM : TopicMasterVM
    {
    }
}