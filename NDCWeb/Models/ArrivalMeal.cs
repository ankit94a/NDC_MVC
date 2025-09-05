using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class ArrivalMeal : BaseEntity
    {
        [Key]
        public int MealId { get; set; }
        public DateTime Date { get; set; }
        public bool Breakfast { get; set; }
        public bool Lunch { get; set; }
        public bool Dinner { get; set; }

        public int ArrivalId { get; set; }
        public virtual ArrivalDetail ArrivalDetails { get; set; }
    }
}