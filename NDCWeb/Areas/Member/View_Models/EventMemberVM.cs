using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Member.View_Models
{
    public class EventMemberVM
    {
        [Key]
        [Required(ErrorMessage = "Event Participant Id Not Supplied")]
        [Display(Name = "Event Participant Id")]
        public int EventMemberId { get; set; }

        [Required(ErrorMessage = "Select Attend Type")]
        [Display(Name = "Attend As a (Host/Guest)")]
        public string AttendType { get; set; }

        [Required(ErrorMessage = "Select Attendance (Self)")]
        [Display(Name = "Self")]
        public string AttendSelf { get; set; }

        [Required(ErrorMessage = "Select Attendance (Spouse)")]
        [Display(Name = "Spouse")]
        public string AttendSpouse { get; set; }
        
        //[Display(Name = "Authorised by Secretary")]
        //public bool SecyPermited { get; set; }

        [Required(ErrorMessage = "Select Dietary Pref (Self)")]
        [Display(Name = "Dietary Preference (Self)")]
        public string DietPrefSelf { get; set; }

        [Required(ErrorMessage = "Select Dietary Pref (Spouse)")]
        [Display(Name = "Dietary Preference (Spouse)")]
        public string DietPrefSpouse { get; set; }

        [Required(ErrorMessage = "Select Liquor Pref")]
        [Display(Name = "Liquor Preference")]
        public string LiquorPref { get; set; }

        [Display(Name = "Remarks (if any)")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string Remarks { get; set; }


        [Required(ErrorMessage = "Fuction Not Selected")]
        [Display(Name = "Name of Party/Function")]
        public int EventId { get; set; }
        public virtual Event Events { get; set; }
    }
    
    public class EventMemberIndxVM : EventMemberVM
    {

    }
    public class EventMemberEnrolVM : EventMemberVM
    {

    }

    public class EventListVM
    {

    }
}