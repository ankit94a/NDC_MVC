using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Staff.View_Models
{
    public class ArrivalAllVM
    {
        public int ArrivalId { get; set; }
        public DateTime ArrivalDate { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public string ArrivalMode { get; set; }
        public string TransportationMode { get; set; }
        public string AssistanceRequired { get; set; }
        public string ArrivaAt { get; set; }
        public string FullName { get; set; }
        public string CreatedBy { get; set; }

    }
    public class ArrivalIndexVM:ArrivalAllVM
    {
        [Display(Name = "Meals Required")]
        public bool MealRequired { get; set; }

        [Display(Name = "No of Meals")]
        public string NoofMeals { get; set; }

        [Display(Name = "From Date")]
        public DateTime? MealFromDate { get; set; }

        [Display(Name = "To Date")]
        public DateTime? MealToDate { get; set; }

        [Display(Name = "Dietary Preference of the family")]
        public string MealDietPreference { get; set; }

        [Display(Name = "Food Detachment")]
        public bool FoodDetachment { get; set; }

        [Display(Name = "From Date")]
        public DateTime? DetachFromDate { get; set; }

        [Display(Name = "To Date")]
        public DateTime? DetachToDate { get; set; }

        [Display(Name = "Any other specific info pertaining to meal requirement")]
        public string DetachMealInfo { get; set; }

        [Display(Name = "Meals Charges")]
        public string DetachCharges { get; set; }

        [Display(Name = "House No")]
        public string HouseNo { get; set; }

        [Display(Name = "Mlobile No")]
        public string MobileNo { get; set; }

        public virtual ICollection<ArrivalMeal> iArrivalMeals { get; set; }
        public virtual ICollection<ArrivalAccompanied> iArrivalAccompanied { get; set; }
    }
    public class ArrivalCompleteVM: ArrivalIndexVM
    {
        public ArrivalIndexVM ArrivalMasterVM { get; set; }
        public ArrivalMeal ArrivalMealVM { get; set; }
        public ArrivalAccompanied ArrivalAccomapniedVM { get; set; }
    }
}