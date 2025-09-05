using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Infrastructure.Constants
{
    public static class CustomDropDownList
    {
        public static IEnumerable<SelectListItem> GetRankService()
        {
            var services = new List<SelectListItem>
            {
                new SelectListItem{ Text="INDIAN ARMY", Value = "INDIAN ARMY" },
                new SelectListItem{ Text="INDIAN NAVY", Value = "INDIAN NAVY" },
                new SelectListItem{ Text="INDIAN AIR FORCE", Value = "INDIAN AIR FORCE" },
                new SelectListItem{ Text="INDIAN CIVIL SERVICES", Value = "INDIAN CIVIL SERVICES" },
                new SelectListItem{ Text="FOREIGN ARMY", Value = "FOREIGN ARMY" },
                new SelectListItem{ Text="FOREIGN NAVY", Value = "FOREIGN NAVY" },
                new SelectListItem{ Text="FOREIGN AIR FORCE", Value = "FOREIGN AIR FORCE" },
                new SelectListItem{ Text="FOREIGN CIVIL SERVICES", Value = "FOREIGN CIVIL SERVICES" },
            };
            return services;
        }

        public static IEnumerable<SelectListItem> GetFacultyType()
        {
            var facultyTypes = new List<SelectListItem>
            {
                new SelectListItem{ Text="NA", Value = "NA" },
                new SelectListItem{ Text="AAQMG", Value = "AAQMG" },
                new SelectListItem{ Text="Admin Officer", Value = "Admin Officer" },
                new SelectListItem{ Text="DAAQMG Records", Value = "DAAQMG Records" },
                new SelectListItem{ Text="DS(Coord)", Value = "DS(Coord)" },
                new SelectListItem{ Text="Dy Director (Adm)", Value = "Dy Director (Adm)" },
                new SelectListItem{ Text="Former Faculties", Value = "Former Faculties" },
                new SelectListItem{ Text="GSO(Systems)", Value = "GSO(Systems)" },
                new SelectListItem{ Text="IFS", Value = "IFS" },
                new SelectListItem{ Text="JDS (Adm)", Value = "JDS (Adm)" },
                new SelectListItem{ Text="JDS (RR)", Value = "JDS (RR)" },
                new SelectListItem{ Text="Oi/c (Univ Div)", Value = "Oi/c (Univ Div)" },
                new SelectListItem{ Text="Research Coordinator", Value = "Research Coordinator" },
                new SelectListItem{ Text="Secretary", Value = "Secretary" },
                new SelectListItem{ Text="SO to Comdt", Value = "SO to Comdt" },
            };
            return facultyTypes;
        }

        public static IEnumerable<SelectListItem> GetStaffType()
        {
            var staffTypes = new List<SelectListItem>
            {
                //new SelectListItem{ Text="Former Faculties", Value = "Former Faculties" },
                //new SelectListItem{ Text="Former Secretaries", Value = "Former Secretaries" },
                //new SelectListItem{ Text="Former Staff", Value = "Former Staff" },
                new SelectListItem{ Text="NA", Value = "NA" },
                new SelectListItem{ Text="Comdt", Value = "Comdt" },
                new SelectListItem{ Text="Secretary", Value = "Secretary" },
                new SelectListItem{ Text="DS (Coord)", Value = "DS (Coord)" },
                new SelectListItem{ Text="JDS (R&R)", Value = "JDS (R&R)" },
                new SelectListItem{ Text="GSO (Sys)", Value = "GSO (Sys)" },
                new SelectListItem{ Text="AQ", Value = "AQ" },
                new SelectListItem{ Text="SDS", Value = "SDS" },
                new SelectListItem{ Text="IAG", Value = "IAG" },

            };
            return staffTypes;
        }

        public static IEnumerable<SelectListItem> GetGender()
        {
            var genders = new List<SelectListItem>
            {
                new SelectListItem{ Text="Male", Value = "Male" },
                new SelectListItem{ Text="Female", Value = "Female" },
            };
            return genders;
        }
        public static IEnumerable<SelectListItem> GetMaritalStatus()
        {
            var genders = new List<SelectListItem>
            {
                new SelectListItem{ Text="Married", Value = "Married" },
                new SelectListItem{ Text="Unmarried", Value = "Unmarried" },
            };
            return genders;
        }
        public static IEnumerable<SelectListItem> GetBloodGroups()
        {
            var genders = new List<SelectListItem>
            {
                new SelectListItem{ Text="A+", Value = "A+" },
                new SelectListItem{ Text="O+", Value = "O+" },
                new SelectListItem{ Text="B+", Value = "B+" },
                new SelectListItem{ Text="AB+", Value = "AB+" },
                new SelectListItem{ Text="A-", Value = "A-" },
                new SelectListItem{ Text="O-", Value = "O-" },
                new SelectListItem{ Text="B-", Value = "B-" },
                new SelectListItem{ Text="AB-", Value = "AB-" },
            };
            return genders;
        }
        public static IEnumerable<SelectListItem> GetYesNoOpt()
        {
            var options = new List<SelectListItem>
            {
                new SelectListItem{ Text="YES", Value = "YES" },
                new SelectListItem{ Text="NO", Value = "NO" },
            };
            return options;
        }
        public static IEnumerable<SelectListItem> GetArea()
        {
            var areas = new List<SelectListItem>
            {
                new SelectListItem{ Text="NA", Value = "NA" },
                new SelectListItem{ Text="Admin", Value = "Admin" },
                new SelectListItem{ Text="Member", Value = "Member" },
                new SelectListItem{ Text="Staff", Value = "Staff" },
                new SelectListItem{ Text="Alumni", Value = "Alumni" },
            };
            return areas;
        }

        public static IEnumerable<SelectListItem> GetCastCommunity()
        {
            var community = new List<SelectListItem>
            {
                new SelectListItem{ Text="SC", Value = "SC" },
                new SelectListItem{ Text="AT", Value = "AT" },
                new SelectListItem{ Text="OBC", Value = "OBC" },
                new SelectListItem{ Text="UR", Value = "UR" },
                new SelectListItem{ Text="MBC", Value = "MBC" },
                new SelectListItem{ Text="DNC", Value = "DNC" },
                new SelectListItem{ Text="OC", Value = "OC" },

            };
            return community;
        }

        public static IEnumerable<SelectListItem> GetVisaEntryType()
        {
            var visaEntryType = new List<SelectListItem>
            {
                new SelectListItem{ Text="Multpiple Entry", Value = "Multpiple Entry" },
                new SelectListItem{ Text="Single Entry", Value = "Single Entry" },
            };
            return visaEntryType;
        }
        public static IEnumerable<SelectListItem> GetHoldingPassport()
        {
            var passportHolding = new List<SelectListItem>
            {
                new SelectListItem{ Text="I am holding a Diplomatic or Official Passport", Value = "I am holding a Diplomatic or Official Passport" },
                new SelectListItem{ Text="I am not holding Diplomatic or Official", Value = "I am not holding Diplomatic or Official Passport" },
            };
            return passportHolding;
        }
        public static IEnumerable<SelectListItem> GetPassportType()
        {
            var visaEntryType = new List<SelectListItem>
            {
                new SelectListItem{ Text="Diplomatic", Value = "Diplomatic" },
                new SelectListItem{ Text="Official", Value = "Official" },
                new SelectListItem{ Text="Service", Value = "Service" },
                new SelectListItem{ Text="Personal", Value = "Personal" },
            };
            return visaEntryType;
        }
        public static IEnumerable<SelectListItem> GetLeaveCategory()
        {
            var leavecategory = new List<SelectListItem>
            {
                new SelectListItem{ Text="Leave for Indian Course Participant", Value = "Leave for Indian Course Participant" },
                new SelectListItem{ Text="Leave for Foreign Country Study Tour", Value = "Leave for Foreign Country Study Tour" },
                new SelectListItem{ Text="Leave for Foreign Course Participant", Value = "Leave for Foreign Course Participant" },

            };
            return leavecategory;
        }
		public static IEnumerable<SelectListItem> GetLeaveIn()
		{
			var leavetype = new List<SelectListItem>
			{
				new SelectListItem{ Text="In India", Value = "InIndia" },
				new SelectListItem{ Text="Out of India", Value = "OutOfIndia" }
			};
			return leavetype;
		}
		public static IEnumerable<SelectListItem> GetLeaveDuration()
		{
			var leavetype = new List<SelectListItem>
			{
				new SelectListItem{ Text="UpTo 02 Days", Value = "Upto2Days" },
				new SelectListItem{ Text="More Than 02 Days", Value = "More2Days" },
				new SelectListItem{ Text="For Weekand", Value = "ForWeek" },
			};
			return leavetype;
		}
		public static IEnumerable<SelectListItem> GetLeaveType()
        {
            var leavetype = new List<SelectListItem>
            {
                new SelectListItem{ Text="Annual Leave", Value = "AL" },
                new SelectListItem{ Text="Earned Leave", Value = "EL" },
                new SelectListItem{ Text="Casual Leave", Value = "CL" },
                new SelectListItem{ Text="Station Leave", Value = "SL" },
            };
            return leavetype;
        }
        public static IEnumerable<SelectListItem> GetEmbassyRecmdType()
        {
            var embassyrecmdtype = new List<SelectListItem>
            {
                new SelectListItem{ Text="Recommended", Value = "Recommended" },
                new SelectListItem{ Text="Not Recommended", Value = "Not Recommended" },
            };
            return embassyrecmdtype;
        }
        public static IEnumerable<SelectListItem> GetForumBlogCategory()
        {
            var forumblogcat = new List<SelectListItem>
            {
                new SelectListItem{ Text="IAG Papers", Value = "IAG Papers" },
                new SelectListItem{ Text="Thesis", Value = "Thesis" },
                new SelectListItem{ Text="ASAT", Value = "ASAT" },
                new SelectListItem{ Text="Other Inputs", Value = "Other Inputs" },
            };
            return forumblogcat;
        }
        public static IEnumerable<SelectListItem> GetMarriedAccnType()
        {
            var marriedaccntype = new List<SelectListItem>
            {
                new SelectListItem{ Text="Yes wef commencement of Course", Value = "Yes wef commencement of Course" },
                new SelectListItem{ Text="Yes wef March/April", Value = "Yes wef March/April" },
            };
            return marriedaccntype;
        }
        public static IEnumerable<SelectListItem> GetAccnArrangeType()
        {
            var accnarrangetype = new List<SelectListItem>
            {
                new SelectListItem{ Text="No. Retaining accn in Delhi", Value = "No. Retaining accn in Delhi" },
                new SelectListItem{ Text="No. Staying under own arrangement", Value = "No. Staying under own arrangement" },
            };
            return accnarrangetype;
        }
        public static IEnumerable<SelectListItem> GetAccnPreference()
        {
            var accnpreference = new List<SelectListItem>
            {
                new SelectListItem{ Text="Raksha Bhawan", Value = "Raksha Bhawan" },
                new SelectListItem{ Text="Vikram Vihar", Value = "Vikram Vihar" },
            };
            return accnpreference;
        }
        public static IEnumerable<SelectListItem> GetAccnFloorRequest()
        {
            var floorrequest = new List<SelectListItem>
            {
                new SelectListItem{ Text="Ground Floor", Value = "Ground Floor" },
                new SelectListItem{ Text="First Floor", Value = "First Floor" },
            };
            return floorrequest;
        }
        //public static IEnumerable<SelectListItem> GetLeaveStatus()
        //{
        //    var floorrequest = new List<SelectListItem>
        //    {
        //        new SelectListItem{ Text="Recommend", Value = "Recommend" },
        //        new SelectListItem{ Text="Not Recommend", Value = "Not Recommend" },
        //        new SelectListItem{ Text="Sanction", Value = "Sanction" },
        //    };
        //    return floorrequest;
        //}
        public static IEnumerable<SelectListItem> GetLeaveStatus()
        {
            var floorrequest = new List<SelectListItem>
            {
                new SelectListItem{ Text="Sanctioned", Value = "Sanction" },
                new SelectListItem{ Text="Not Sanctioned", Value = "Not Recommend" },
            };
            return floorrequest;
        }
        //public static IEnumerable<SelectListItem> GetLeaveOptRecNot()
        //{
        //    var floorrequest = new List<SelectListItem>
        //    {
        //        new SelectListItem{ Text="Recommend", Value = "Recommend" },
        //        new SelectListItem{ Text="Not Recommend", Value = "Not Recommend" },
        //    };
        //    return floorrequest;
        //}
        public static IEnumerable<SelectListItem> GetLeaveOptInitNot()
        {
            var floorrequest = new List<SelectListItem>
            {
                new SelectListItem{ Text="Initiated", Value = "Recommend" },
                new SelectListItem{ Text="Not Initiated", Value = "Not Recommend" },
            };
            return floorrequest;
        }
        public static IEnumerable<SelectListItem> GetLeaveOptRecNot()
        {
            var floorrequest = new List<SelectListItem>
            {
                new SelectListItem{ Text="Recommended", Value = "Recommend" },
                new SelectListItem{ Text="Not Recommended", Value = "Not Recommend" },
            };
            return floorrequest;
        }
        public static IEnumerable<SelectListItem> GetLeaveOptSancNot()
        {
            var floorrequest = new List<SelectListItem>
            {
                new SelectListItem{ Text="Sanction", Value = "Sanction" },
                new SelectListItem{ Text="Not Recommend", Value = "Not Recommend" },
            };
            return floorrequest;
        }

        #region Events Options
        public static IEnumerable<SelectListItem> GetEventAttendTypeOpt()
        {
            var attends = new List<SelectListItem>
            {
                new SelectListItem{ Text="Host", Value = "Host" },
                new SelectListItem{ Text="Guest", Value = "Guest" },
            };
            return attends;
        }
        public static IEnumerable<SelectListItem> GetEventAttendOpt()
        {
            var attends = new List<SelectListItem>
            {
                new SelectListItem{ Text="YES", Value = "YES" },
                new SelectListItem{ Text="NO", Value = "NO" },
            };
            return attends;
        }
        public static IEnumerable<SelectListItem> GetEventDietPreferenceOpt()
        {
            var dietPrefs = new List<SelectListItem>
            {
                new SelectListItem{ Text="VEG", Value = "VEG" },
                new SelectListItem{ Text="NV", Value = "NV" },
            };
            return dietPrefs;
        }
        public static IEnumerable<SelectListItem> GetEventLiquorOpt()
        {
            var liquer = new List<SelectListItem>
            {
                new SelectListItem{ Text="Wishky", Value = "Wishky" },
                new SelectListItem{ Text="Rum", Value = "Rum" },
                new SelectListItem{ Text="Vodka/Gin", Value = "Vodka/Gin" },
                new SelectListItem{ Text="Beer", Value = "Beer" },
                new SelectListItem{ Text="Soft Drink", Value = "Soft Drink" },
            };
            return liquer;
        }
 		public static IEnumerable<SelectListItem> GetParticipationType()
        {
            var floorrequest = new List<SelectListItem>
            {
                new SelectListItem{ Text="Guest", Value = "Guest" },
                new SelectListItem{ Text="Host", Value = "Host" },
            };
            return floorrequest;
        }
        #endregion

        public static IEnumerable<SelectListItem> IAGList()
        {
            var floorrequest = new List<SelectListItem>
            {
                new SelectListItem{ Text="IAG 1", Value = "IAG 1" },
                new SelectListItem{ Text="IAG 2", Value = "IAG 2" },
                new SelectListItem{ Text="IAG 3", Value = "IAG 3" },
                new SelectListItem{ Text="IAG 4", Value = "IAG 4" },
                new SelectListItem{ Text="IAG 5", Value = "IAG 5" },
                new SelectListItem{ Text="IAG 6", Value = "IAG 6" },
                new SelectListItem{ Text="IAG 7", Value = "IAG 7" },
                new SelectListItem{ Text="IAG 8", Value = "IAG 8" },
            };
            return floorrequest;
        }
        public static IEnumerable<SelectListItem> ArrivalModes()
        {
            var floorrequest = new List<SelectListItem>
            {
                new SelectListItem{ Text="By Air", Value = "By Air" },
                new SelectListItem{ Text="By Road", Value = "By Road" },
            };
            return floorrequest;
        }
        public static IEnumerable<SelectListItem> GetInterNetPref()
        {
            var telepref = new List<SelectListItem>
            {
                new SelectListItem{ Text="Internet with Voice", Value = "Internet with Voice" },
                new SelectListItem{ Text="Internet Only", Value = "Internet Only" },
            };
            return telepref;
        }
        public static IEnumerable<SelectListItem> GetCircularCategory()
        {
            var telepref = new List<SelectListItem>
            {
                new SelectListItem{ Text="Circular", Value = "Circular" },
                new SelectListItem{ Text="Order", Value = "Order" },
            };
            return telepref;
        }
        public static IEnumerable<SelectListItem> GetAlumniArticleCategory()
        {
            var articlepref = new List<SelectListItem>
            {
                new SelectListItem{ Text="Article", Value = "Article" },
            };
            return articlepref;
        }
        public static IEnumerable<SelectListItem> GetArrivalAtLocation()
        {
            var arrivalLoc = new List<SelectListItem>
            {
                new SelectListItem{ Text="Nav Raksha Bhawan", Value = "Nav Raksha Bhawan" },
                new SelectListItem{ Text="Vikram Vihar", Value = "Vikram Vihar" },
            };
            return arrivalLoc;
        }
        public static IEnumerable<SelectListItem> GetArrivalMode()
        {
            var arrivalLoc = new List<SelectListItem>
            {
                new SelectListItem{ Text="By Air", Value = "By Air" },
                new SelectListItem{ Text="By Road", Value = "By Road" },
            };
            return arrivalLoc;
        }
        public static IEnumerable<SelectListItem> GetArrivalDietPreference()
        {
            var dietPreference = new List<SelectListItem>
            {
                new SelectListItem{ Text="Veg", Value = "Veg" },
                new SelectListItem{ Text="Non Veg", Value = "Non Veg" },
            };
            return dietPreference;
        }
        public static IEnumerable<SelectListItem> GetDetachmentFoodCharges()
        {
            var foodCharges = new List<SelectListItem>
            {
                new SelectListItem{ Text="Breakfast - 50", Value = "Breakfast - 50" },
                new SelectListItem{ Text="Lunch - 100", Value = "Lunch - 100" },
                new SelectListItem{ Text="Dinner - 100", Value = "Dinner - 100" },
            };
            return foodCharges;
        }
        public static IEnumerable<SelectListItem> GetDepartments()
        {
            var departments = new List<SelectListItem>
            {
                new SelectListItem{ Text="Administration", Value = "Administration" },
                new SelectListItem{ Text="IT & Communication", Value = "IT & Communication" },
                new SelectListItem{ Text="Library", Value = "Library" },
                new SelectListItem{ Text="Research", Value = "Research" },
                new SelectListItem{ Text="Training", Value = "Training" },
                new SelectListItem{ Text="University", Value = "University" },
            };
            return departments;
        }
        public static IEnumerable<SelectListItem> GetForumBlogStatus()
        {
            var telepref = new List<SelectListItem>
            {
                new SelectListItem{ Text="Pending", Value = "Pending" },
                new SelectListItem{ Text="Accept", Value = "Accept" },
                new SelectListItem{ Text="Return", Value = "Return" },
                
            };
            return telepref;
        }
        public static IEnumerable<SelectListItem> GetTrainingContentModule()
        {
            var modules = new List<SelectListItem>
            {
                new SelectListItem{ Text="UIIS", Value = "UIIS" },
                new SelectListItem{ Text="ESST", Value = "ESST" },
                new SelectListItem{ Text="ISE", Value = "ISE" },
                new SelectListItem{ Text="ISN", Value = "ISN" },
                new SelectListItem{ Text="GI", Value = "GI" },
                new SelectListItem{ Text="SSNS", Value = "SSNS" },
                new SelectListItem{ Text="SCRN", Value = "SCRN" },
                new SelectListItem{ Text="SGE-I", Value = "SGE-I" },

            };
            return modules;
        }
        public static IEnumerable<SelectListItem> GetTrainingContentActivity()
        {
            var activities = new List<SelectListItem>
            {
                new SelectListItem{ Text="Module Settings", Value = "Module Settings" },
                new SelectListItem{ Text="IAG Grouping", Value = "IAG Grouping" },
                new SelectListItem{ Text="Amended IAG Grouping", Value = "Amended IAG Grouping" },
                new SelectListItem{ Text="Essential Readings", Value = "Essential Readings" },
                new SelectListItem{ Text="Books Bibliography", Value = "Books Bibliography" },
                new SelectListItem{ Text="Web Links", Value = "Web Links UIIS" },
                new SelectListItem{ Text="Speaker Biodata", Value = "Speaker Biodata" },
                new SelectListItem{ Text="Commandant's Recommended Readings", Value = "Commandant's Recommended Readings" },
                new SelectListItem{Text="Presentation (PPT)", Value="Presentation (PPT)"},
                new SelectListItem{ Text="IAG Assignment", Value="IAG Assignment"},
                new SelectListItem{ Text="Lecture Plan", Value="Lecture Plan"},
                new SelectListItem{ Text="List of Speakers", Value="List of Speakers"},
                new SelectListItem{Text="IAG 1",Value="IAG-1"},
                new SelectListItem{Text="IAG 2",Value="IAG-2"},
                new SelectListItem{Text="IAG 3",Value="IAG-3"},
                new SelectListItem{Text="IAG 4",Value="IAG-4"},
                new SelectListItem{Text="IAG 5",Value="IAG-5"},
                new SelectListItem{Text="IAG 6",Value="IAG-6"},
                new SelectListItem{Text="IAG 7",Value="IAG-7"},
                new SelectListItem{Text="IAG 8",Value="IAG-8"},
            };
            return activities;
        }
        public static IEnumerable<SelectListItem> GetInstepService()
        {
            var services = new List<SelectListItem>
            {
                new SelectListItem{ Text="INDIAN ARMY", Value = "INDIAN ARMY" },
                new SelectListItem{ Text="INDIAN NAVY", Value = "INDIAN NAVY" },
                new SelectListItem{ Text="INDIAN AIR FORCE", Value = "INDIAN AIR FORCE" },
                new SelectListItem{ Text="INDIAN CIVIL SERVICES", Value = "INDIAN CIVIL SERVICES" },
                new SelectListItem{ Text="INTERNATIONAL OFFICER", Value = "INTERNATIONAL OFFICER" },
                new SelectListItem{Text="OTHER", Value="OTHER" }
            };
            return services;
        }
    }
}