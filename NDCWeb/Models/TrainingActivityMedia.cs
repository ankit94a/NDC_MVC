using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class TrainingActivityMedia
    {
        public TrainingActivityMedia()
        {
            GuId = Guid.NewGuid();
        }

        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid GuId { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string FilePath { get; set; }

        [Column(Order = 2)]
        public int TrainingActivityId { get; set; }
        public TrainingActivity TrainingActivities { get; set; }
    }
}