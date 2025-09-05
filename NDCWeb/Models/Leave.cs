using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class Leave : BaseEntity
    {
        [Key]
        public int LeaveId { get; set; }
        public string LeaveCategory { get; set; }
        public string LeaveType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime? PrefixDate { get; set; }
        public DateTime? PrefixToDate { get; set; }
        public DateTime? SuffixDate { get; set; }
        public DateTime? SuffixToDate { get; set; }
        public int TotalDays { get; set; }
        public string ReasonForLeave { get; set; }
        public string AddressOnLeave { get; set; }
        public string TeleNo { get; set; }
        public string RecommendByEmbassy { get; set; }
        public string DocPath { get; set; }

        #region Staff Status
        public string AQStatus { get; set; }
        public DateTime? AQStatusDate { get; set; }
        public string IAGStatus { get; set; }
        public DateTime? IAGStatusDate { get; set; }
        public string ServiceSDSStatus { get; set; }
        public DateTime? ServiceSDSStatusDate { get; set; }
        public string SecretaryStatus { get; set; }
        public DateTime? SecretaryStatusDate { get; set; }
        public string ComdtStatus { get; set; }
        public DateTime? ComdtStatusDate { get; set; }

        public string GenerateCertificate { get; set; }
        #endregion

        public int? CountryId { get; set; }
        public virtual CountryMaster Country { get; set; }
		public string LeaveDuration { get; set; }
		public string LeaveIn { get; set; }
	}
}