using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class OtherRequest
    {
        [Key]
        public int OtherRequestId { get; set; }
        public string UserId { get; set; }
        public string MobileNo { get; set; }
        public string LockerNo { get; set; }
        public bool Status { get; set; } = false;
        public bool IsDelete { get; set; } = false;
        public string Remark { get; set; }
        public DateTime? RemarkDate { get; set; }
        public DateTime RequestDate { get; set; }
    }
}