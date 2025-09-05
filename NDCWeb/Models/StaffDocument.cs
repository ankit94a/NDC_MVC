using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class StaffDocument
    {
        public StaffDocument()
        {
            GuId = Guid.NewGuid();
        }
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid GuId { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string FilePath { get; set; }
        //public DocumentType DocumentType { get; set; }
        public bool Verify { get; set; }

        [Column(Order = 1)]
        public int StaffId { get; set; }
        public StaffMaster StaffMasters { get; set; }
    }
}