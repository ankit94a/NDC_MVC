using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class CourseFeedback : BaseEntity
    {
        [Key]
        public int FeedbackId { get; set; }
        public string ConductOfStudy { get; set; }
        public string SpeakerOnTopicScope { get; set; }
        public string CoductOfIAG { get; set; }
        public string ConductOfCDsSGES { get; set; }
        public string InfoCourseLectureSchedule { get; set; }
        public string SuggestionInfraI { get; set; }
        public string SuggestionInfraII { get; set; }
        public string SuggestionInfraIII { get; set; }
        public string OtherComments { get; set; }
        public string ToursI { get; set; }
        public DateTime? TourIDate { get; set; }
        public string VirtualToursI { get; set; }
        public DateTime? VirtualToursIDate { get; set; }
        public string VirtualToursII { get; set; }
        public DateTime? VirtualToursIIDate { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(2)]
        public string AdminTourA { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(2)]
        public string AdminTourB { get; set; }

        public string AdminTourC { get; set; }
        public string AdminSocialFuncA { get; set; }
        public string AdminSocialFuncB { get; set; }
        public string AdminOfficerMessA { get; set; }
        public string AdminOfficerMessB { get; set; }
        public string AdminOfficerMessC { get; set; }

        public string AccountsA { get; set; }
        public string AccountsB { get; set; }
        public string Transport { get; set; }
        public string AnyOtherSuggestion { get; set; }

        public string AdminAccomodationA { get; set; }
        public string AdminAccomodationB { get; set; }
        public string AdminAccomodationReceptionA { get; set; }
        public string AdminAccomodationReceptionB { get; set; }
        public string AdminGym { get; set; }
        public string AdminBeautyParlour { get; set; }
        public string AdminShop { get; set; }
        public string CSDFacilities { get; set; }
        public string AdminDomesticHelp { get; set; }
        public string AdminA { get; set; }
        public string AdminB { get; set; }
        public string AdminC { get; set; }
        public string AdminD { get; set; }
        public string AdminE { get; set; }
        public string AdminF { get; set; }
        public string AdminG { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(2)]
        public string UDThesisA { get; set; }
        public string UDThesisASugg { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(2)]
        public string UDThesisB { get; set; }
        public string UDThesisBSugg { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(2)]
        public string UDThesisC { get; set; } 
        public string UDThesisCSugg { get; set; }
        public string LibraryA { get; set; }
        public string LibraryB { get; set; }
        public string LibraryC { get; set; }   
        public string ASATASuggFeedback { get; set; }
        public bool IsSubmit { get; set; } = false;
        public DateTime? FeedbackDate { get; set; }
        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
    }
}