using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class TrainingActivity
    {
        public TrainingActivity()
        {
            iTrainingActivityMedias = new List<TrainingActivityMedia>();
        }
        public int TrainingActivityId { get; set; }
        public string Module { get; set; }
        public string Activity { get; set; }
        public string Description { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool Active { get; set; }

        //public int MenuId { get; set; }
        //public MenuItemMaster MenuItemMasters { get; set; }
        
        public virtual ICollection<TrainingActivityMedia> iTrainingActivityMedias { get; set; }
    }
}