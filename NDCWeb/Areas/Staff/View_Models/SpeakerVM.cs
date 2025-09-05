using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Staff.View_Models
{
    public class SpeakerVM
    {
        [Key]
        [Required(ErrorMessage = "Circular Id Not Supplied")]
        [Display(Name = "Circular Id")]
        public int SpeakerId { get; set; }

        [Required(ErrorMessage = "Full Name Not Supplied")]
        [Display(Name = "Speaker Name in full")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string FullName { get; set; }

        //[Display(Name = "Speaker Nick Name")]
        //public string NickName { get; set; }

        [Display(Name = "Speach Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> SpeachDate { get; set; }

        //[Required(ErrorMessage = "Email Id Not Supplied")]
        [Display(Name = "Email Id")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid e-mail adress")]
        public string Email { get; set; }

        [Display(Name = "Alternate Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid e-mail adress")]
        public string AlternateEmail { get; set; }

        //[Required(ErrorMessage = "Mobile No Not Supplied")]
        [Display(Name = "Mobile No")]
        [RegularExpression(@"^[0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string MobileNo { get; set; }

        //[Required(ErrorMessage = "Telephone No Not Supplied")]
        [Display(Name = "Telephone No")]
        [RegularExpression(@"^[0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string Telephone { get; set; }

        //[Required(ErrorMessage = "Address No Not Supplied")]
        [Display(Name = "Address (present)")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string CurrentAddress { get; set; }

        //[Required(ErrorMessage = "Photo Not Supplied")]
        [Display(Name = "Photograph")]
        public string PhotoPath { get; set; }

        [Required(ErrorMessage = "Speaker Bio Not Supplied")]
        [Display(Name = "Speaker Bio File")]
        public string FilePath { get; set; }

        [Required(ErrorMessage = "Topic Not Supplied")]
        [Display(Name = "Topic Name")]
        public int TopicId { get; set; }
        public virtual TopicMaster Topics { get; set; }

        [Display(Name = "Year")]
        public DateTime? CreatedAt { get; set; }


    }
    public class SpeakerIndxVM : SpeakerVM
    {

    }
    public class SpeakerCrtVM : SpeakerVM
    {
        [Required(ErrorMessage = "Subject Not Supplied")]
        [Display(Name = "Subject Name")]
        public int SubjectId { get; set; }
        //public virtual SubjectMaster Subjects { get; set; }
    }
    public class SpeakerUpVM : SpeakerVM
    {
        [Required(ErrorMessage = "Subject Not Supplied")]
        [Display(Name = "Subject Name")]
        public int SubjectId { get; set; }
        //public virtual SubjectMaster Subjects { get; set; }
    }
}