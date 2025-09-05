using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NDCWeb.Models
{
    public class MessBill:BaseEntity
    {
        [Key]
        public int MessBillId { get; set; }
        public string FullName { get; set; }
        public int MemberStaffId { get; set; }
        public string Arrear { get; set; }
        public string Extra { get; set; }
        public string Messing { get; set; }
        public string Tea { get; set; }
        public string TableMoney { get; set; }
        public string Wine { get; set; }
        public string MessSubs { get; set; }
        public string Rakshika { get; set; }
        public string NDCJournal { get; set; }
        public string RB { get; set; }
        public string BusFund { get; set; }
        public string AlumniDinner { get; set; }
        public string PLD { get; set; }
        public string Corpusfund { get; set; }
        public string BreakupParty { get; set; }
        public string CanteenSmartCard { get; set; }
        public string Total { get; set; }
        public string BillMonth { get; set; }
        public string PayStatus { get; set; }
    }
}