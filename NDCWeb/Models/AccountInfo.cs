using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class AccountInfo : BaseEntity
    {
        [Key]
        public int AccInfoId { get; set; }
        public string AccountNo { get; set; }
        public string AccountType { get; set; }
        public string IFSC { get; set; }
        public string MICR { get; set; }
        public string PassbookPath { get; set; }
        public string NameAndAddressOfBanker { get; set; }
        public string CDAAcNo { get; set; }
        public string BasicPay { get; set; }
        public string MSP { get; set; }
        public string PayLevel { get; set; }
        public string AddressOfPayAc { get; set; }

        //Added on 29 Nov 2023
        public string NodalOfficeName { get; set; }
        public string NodalOfficeContactNo { get; set; }

        //Added on 29 Nov 2024
        public string NodalOfficeEmail { get; set; }
        public string CivilServiceAcNo { get; set; }
        public string CivilServiceAddressOfPayAc { get; set; }
        public string CivilServiceNodalOfficeName { get; set; }
        public string CivilServiceNodalOfficeContactNo { get; set; }
        public string CivilServiceNodalOfficeEmail { get; set; }

    }
}