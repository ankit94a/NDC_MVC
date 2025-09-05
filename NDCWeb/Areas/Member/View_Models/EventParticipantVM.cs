using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Member.View_Models
{
    public class EventParticipantVM
    {
        [Key]
        [Required(ErrorMessage = "Event Participant Id Not Supplied")]
        [Display(Name = "Event Participant Id")]
        public int EventParticipantId { get; set; }

        [Required(ErrorMessage = "Select Participation Type")]
        [Display(Name = "Participation Type (Host/Guest)")]
        public string ParticipateAs { get; set; }

        [Required(ErrorMessage = "Select Attendance (Self)")]
        [Display(Name = "Self")]
        public bool AttendSelf { get; set; }

        [Required(ErrorMessage = "Select Attendance (Spouse)")]
        [Display(Name = "Spouse")]
        public bool AttendSpouse { get; set; }
        
        [Display(Name = "Authorised by Secretary")]
        public bool SecyPermited { get; set; }

        [Required(ErrorMessage = "Select Dietary Preference (Self)")]
        [Display(Name = "Dietary Preference (Self)")]
        public bool DietaryPrefSelf { get; set; }

        [Required(ErrorMessage = "Select Dietary Preference (Spouse)")]
        [Display(Name = "Dietary Preference (Spouse)")]
        public bool DietaryPrefSpouse { get; set; }

        [Required(ErrorMessage = "Select Liquor Preference")]
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
    public class EventParticipantIndexVM:EventParticipantVM
    {

    }
    public class EventParticipantCreateVM : EventParticipantVM
    {

    }
    public class EventParticipantUpdateVM : EventParticipantVM
    {

    }
}