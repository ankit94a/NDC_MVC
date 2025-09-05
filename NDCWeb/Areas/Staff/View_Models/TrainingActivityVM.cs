using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Staff.View_Models
{
    public class TrainingActivityVM
    {
        public TrainingActivityVM()
        {
            iTrainingActivityMedias = new List<TrainingActivityMedia>();
        }
        public int TrainingActivityId { get; set; }
        public string Module { get; set; }
        public string Activity { get; set; }
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FromDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ToDate { get; set; }
        public bool Active { get; set; }

        //public int MenuId { get; set; }
        //public MenuItemMaster MenuItemMasters { get; set; }

        public virtual ICollection<TrainingActivityMedia> iTrainingActivityMedias { get; set; }
    }
    public class TrainingActivityIndxVM : TrainingActivityVM
    {
    }
    public class TrainingActivityCrtVM : TrainingActivityVM
    {
        //public int ParentId { get; set; }
    }
    public class TrainingActivityUpVM : TrainingActivityVM
    {
        //public int ParentId { get; set; }
    }
    public class TrainingActivityCompleteIndxVM
    {
        public int TrainingActivityId { get; set; }
        public string Description { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool Active { get; set; }
        //public int MenuId { get; set; }

        public string Module { get; set; }
        public string Activity { get; set; }
    }

    public class TrainingActivityMediaIndxVM
    {
        public Guid GuId { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string FilePath { get; set; }

        public int TrainingActivityId { get; set; }
        public TrainingActivity TrainingActivities { get; set; }
    }
}