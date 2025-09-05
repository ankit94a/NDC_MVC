using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class ArrivalDetail : BaseEntity
    {
        public ArrivalDetail()
        {
            iArrivalMeals = new List<ArrivalMeal>();
            iArrivalAccompanied = new List<ArrivalAccompanied>();
        }
        [Key]
        public int ArrivalId { get; set; }
        public string ArrivaAt { get; set; }
        public DateTime ArrivalDate { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public string ArrivalMode { get; set; }
        public string TransportationMode { get; set; }
        public string AssistanceRequired { get; set; }


        #region Meals Required
        public bool MealRequired { get; set; }
        public string NoofMeals { get; set; }
        public DateTime? MealFromDate { get; set; }
        public DateTime? MealToDate { get; set; }
        public string MealDietPreference { get; set; }
        #endregion

        #region Food Detachment
        public bool FoodDetachment { get; set; }
        public DateTime? DetachFromDate { get; set; }
        public DateTime? DetachToDate { get; set; }
        public string DetachMealInfo { get; set; }
        public string DetachCharges { get; set; }
        #endregion

        public virtual ICollection<ArrivalMeal> iArrivalMeals { get; set; }
        public virtual ICollection<ArrivalAccompanied> iArrivalAccompanied { get; set; }
    }
}