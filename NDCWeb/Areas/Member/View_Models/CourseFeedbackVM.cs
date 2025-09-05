using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Member.View_Models
{
    public class CourseFeedbackVM
    {
        [Key]
        [Required(ErrorMessage = "Feedback ID Not Supplied")]
        [Display(Name = "Feedback ID")]
        public int FeedbackId { get; set; }
        
        [Display(Name = "(a) Conduct of the study (to include lectures and panel discussions)")]
        [Required(ErrorMessage = "Please enter the feedback for Conduct Of Study")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string ConductOfStudy { get; set; }
        
        [Display(Name = "(b) Speakers (Topics and Scope)")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string SpeakerOnTopicScope { get; set; }
        
        [Display(Name = "(c) Feedback on conduct of IAGs and written work")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string CoductOfIAG { get; set; }

        //[Display(Name = "(d) Feedback on conduct of CD's SGE's and Presentation")]
        [Display(Name = "(d) Feedback on conduct of Central discussion, SGE's, and various capsules")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string ConductOfCDsSGES { get; set; }
        
        //[Display(Name = "(e) Feedback on information on Course lecture schedule/urricular, itinerary, program etc.")]
        [Display(Name = "(e) Feedback on information on Course lecture schedule, itinerary, program etc.")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string InfoCourseLectureSchedule { get; set; }

        [Display(Name = "(a) Kautilya")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string SuggestionInfraI { get; set; }

        //[Display(Name = "(ii) Air Force Briefing Hall")]
        [Display(Name = "(b) Aviator Hall")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string SuggestionInfraII { get; set; }
        
        [Display(Name = "(c)	IAG Rooms")]
        public string SuggestionInfraIII { get; set; }
        
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "(d)  Any other Comments/Suggestions")] 
        public string OtherComments { get; set; }
        
        [Display(Name = "(a) Domestic Tours")]
        public string ToursI { get; set; }
        
        [Display(Name = "Virtual Tour Date From")]
        public DateTime? TourIDate { get; set; }
        
        [Display(Name = "(i) FCST")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string VirtualToursI { get; set; }
        
        [Display(Name = "Virtual Tour Date From")]
        public DateTime? VirtualToursIDate { get; set; }
        
        [Display(Name = "(ii) SNST")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string VirtualToursII { get; set; }
        
        [Display(Name = "Virtual Tour Date to")]
        public DateTime? VirtualToursIIDate { get; set; }
        
        [Display(Name = "(c) Were the services of Travel Agent satisfactory?")]
        public string AdminTourA { get; set; }
        
        [Display(Name = "(d) Were the dispatch/pick up arrangements satisfactory?")]
        public string AdminTourB { get; set; }
        
        [Display(Name = "(e) Any suggestions for improvement")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string AdminTourC { get; set; }
        
        [Display(Name = "(a) Number of organised functions : Adequate/Less/Excessive.")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string AdminSocialFuncA { get; set; }
        
        [Display(Name = "(b) Any suggestions in this regard?")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string AdminSocialFuncB { get; set; }
        
        [Display(Name = "(a) Feedback on lunch and mess services")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string AdminOfficerMessA { get; set; }
        
        [Display(Name = "(b) Are tea arrangements adequate? Any comments?")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string AdminOfficerMessB { get; set; }
        
        [Display(Name = "(c) Any suggestions/comments.")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string AdminOfficerMessC { get; set; }
        
        [Display(Name = "(a) Were your claims dealt with promptly?	Yes/No")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string AccountsA { get; set; }
        
        [Display(Name = "(b) Any suggestions in this regard?")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string AccountsB { get; set; }
        
        [Display(Name = "(a) Mechanical Transport")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string Transport { get; set; }
        
        [Display(Name = "(b) Any other suggestions on Adm")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string AnyOtherSuggestion { get; set; }

        [Display(Name = "(a) Standards of Maintenance")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string AdminAccomodationA { get; set; }
        
        [Display(Name = "(b) Suggestions")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string AdminAccomodationB { get; set; }
        
        [Display(Name = "(a) Service at Reception")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string AdminAccomodationReceptionA { get; set; }
        
        [Display(Name = "(b) Suggestions")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string AdminAccomodationReceptionB { get; set; }
        
        [Display(Name = " ")] //10 Gym facilities
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string AdminGym { get; set; }
        [Display(Name = " ")] //11 Beauty Parlour
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string AdminBeautyParlour { get; set; }
        [Display(Name = " ")] //12 Rakshika True Value Shop
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string AdminShop { get; set; }
        
        [Display(Name = " ")] //13 CSD Facilities
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string CSDFacilities { get; set; }
        
        [Display(Name = "(a) Name of your domestic servant")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string AdminDomesticHelp { get; set; }
        
        [Display(Name = "(b) Were you satisfied with his/her services?")]
       // [RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string AdminA { get; set; }
        
        [Display(Name = "(c) Name of your Dhobi/Washerman.")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string AdminB { get; set; }
        
        [Display(Name = "(d) Were you satisfied with his/her services?")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string AdminC { get; set; }
        
        [Display(Name = "(e) Name of your Sweeper:")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string AdminD { get; set; }
        
        [Display(Name = "(f) Were you satisfied with his/her services?")]
       // [RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string AdminE { get; set; }
        
        [Display(Name = "(g) Any other suggestion in this regard?")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string AdminF { get; set; }       
        
        [Display(Name = "")]
        public string AdminG { get; set; }

        [Display(Name = "(a) Are the number of subjects for making a choice adequate?")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string UDThesisA { get; set; }

        [Display(Name = "Any suggestions regarding subjects to be added?")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string UDThesisASugg { get; set; }
        
        [Display(Name = "(b) Was the briefing on your selected subject adequate?")]
        public string UDThesisB { get; set; }
        
        [Display(Name = "Suggestions if any.")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string UDThesisBSugg { get; set; }
        
        [Display(Name = "(c) Was the time allotted adequate?")]
        public string UDThesisC { get; set; }

        [Display(Name = "Suggestions if any.")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string UDThesisCSugg { get; set; }

        [Display(Name = "Suggestions/ feedback.")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string ASATASuggFeedback { get; set; }
        [Display(Name = "(a) Books availability")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string LibraryA { get; set; }
        
        [Display(Name = "(b) Refread and other Diqital Resource")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string LibraryB { get; set; }
        
        [Display(Name = "Any other suggestions")]
        //[RegularExpression(@"^[\w,.!?]*$", ErrorMessage = "Special chars not allowed")]
        public string LibraryC { get; set; }
        public bool IsSubmit { get; set; } = false;
    }

    public class CourseFeedbackIndxVM : CourseFeedbackVM
    {
        [Display(Name = "Name")]
        public string FullName { get; set; }
        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }
        [Display(Name = "Email ID")]
        public string Email { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

    }

    public class CourseFeedbackIndxListVM
    {
        public int FeedbackId { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }

        [Display(Name = "Email ID")]
        public string EmailId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class CourseFeedbackCrtVM : CourseFeedbackVM
    {
    }
    public class CourseFeedbackUpVM : CourseFeedbackVM
    {
    }
}