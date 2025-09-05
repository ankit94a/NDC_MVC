using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class StaffMaster : BaseEntity
    {
        public StaffMaster()
        {
            iStaffDocument = new List<StaffDocument>();
        }
        [Key]
        public int StaffId { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public string Decoration { get; set; }
        public DateTime DOBirth { get; set; }
        public DateTime? DOMarriage { get; set; }
        public DateTime? DOAppointment { get; set; }
        public bool PostingOut { get; set; }
        public string SelfImage { get; set; }

        //public Guid PhotoId { get; set; }
        public virtual ICollection<StaffDocument> iStaffDocument { get; set; }

        public int FacultyId { get; set; }
        public virtual Faculty Faculties { get; set; }

        public int RankId { get; set; }
        public virtual RankMaster Ranks { get; set; }

        public bool IsLoginUser { get; set; }
        public string LoginUserId { get; set; }
    }
}