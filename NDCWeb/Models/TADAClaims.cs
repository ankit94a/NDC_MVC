using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class TADAClaims :BaseEntity
    {
        [Key]
        public int TADAId { get; set; }
        public string CDAACNo { get; set; }
        public string BasicPay { get; set; }
        public string MSP { get; set; }
        public string PayLevel { get; set; }
        public string MobileNo { get; set; }
        public string BankNameAddress { get; set; }
        public string PayACOffcAddress { get; set; }
        public string FullName { get; set; }
    }
}