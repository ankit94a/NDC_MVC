using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class CrsMbrAddress : BaseEntity
    {
        [Key]
        public int MemberAddressId { get; set; }
        public string CurrentAddress { get; set; }
        public string CurrentTelephone { get; set; }
        public string CurrentFax { get; set; }
        public string PermanentAddress { get; set; }
        public string PermanentTelephone { get; set; }
        public string PermanentFax { get; set; }
        public string OffcTelephone { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        public int StateId { get; set; }
        public virtual StateMaster States { get; set; }
    }
}