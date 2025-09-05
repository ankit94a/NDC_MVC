using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Staff.View_Models
{
    public class MessBillAllVM
    {
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
        public string PayStatus  { get; set; }

        
    }
    public class MessBillUpdate
    {
        public int MessBillId { get; set; }
        public string FullName { get; set; }
        public string Arrear { get; set; }
        public string Total { get; set; }
        public string BillMonth { get; set; }

    }
    public class MessBillReadVM
    {
        public int MessBillId { get; set; }
        [Display(Name = "FullName")]
        public string FullName { get; set; }
        [Display(Name = "MemberStaffId")]
        public int MemberStaffId { get; set; }
        [Display(Name = "Arrear")]
        public string Arrear { get; set; }
        [Display(Name = "Extra")]
        public string Extra { get; set; }
        [Display(Name = "Messing")]
        public string Messing { get; set; }
        [Display(Name = "Tea")]
        public string Tea { get; set; }
        [Display(Name = "TableMoney")]
        public string TableMoney { get; set; }
        [Display(Name = "Wine")]
        public string Wine { get; set; }
        [Display(Name = "MessSubs")]
        public string MessSubs { get; set; }
        [Display(Name = "Rakshika")]
        public string Rakshika { get; set; }
        [Display(Name = "NDCJournal")]
        public string NDCJournal { get; set; }
        [Display(Name = "RB")]
        public string RB { get; set; }
        [Display(Name = "BusFund")]
        public string BusFund { get; set; }
        [Display(Name = "AlumniDinner")]
        public string AlumniDinner { get; set; }
        [Display(Name = "PLD")]
        public string PLD { get; set; }
        [Display(Name = "Corpusfund")]
        public string Corpusfund { get; set; }
        [Display(Name = "BreakupParty")]
        public string BreakupParty { get; set; }
        [Display(Name = "CanteenSmartCard")]
        public string CanteenSmartCard { get; set; }
        [Display(Name = "Total")]
        public string Total { get; set; }
        [Display(Name = "BillMonth")]
        public string BillMonth { get; set; }
        [Display(Name = "PayStatus")]
        public string PayStatus { get; set; }


    }
    public class MessAlertVM
    {
      

        [Key]
        [Required(ErrorMessage = "MessBill Id Not Supplied")]
        [Display(Name = "MessBill Id")]
        public int MessBillId { get; set; }
        [Display(Name = "FullName")]
        public string FullName { get; set; }
        [Display(Name = "MemberStaffId")]
        public int MemberStaffId { get; set; }
        [Display(Name = "Arrear")]
        public string Arrear { get; set; }
        [Display(Name = "Extra")]
        public string Extra { get; set; }
        [Display(Name = "Messing")]
        public string Messing { get; set; }
        [Display(Name = "Tea")]
        public string Tea { get; set; }
        [Display(Name = "TableMoney")]
        public string TableMoney { get; set; }
        [Display(Name = "Wine")]
        public string Wine { get; set; }
        [Display(Name = "MessSubs")]
        public string MessSubs { get; set; }
        [Display(Name = "Rakshika")]
        public string Rakshika { get; set; }
        [Display(Name = "NDCJournal")]
        public string NDCJournal { get; set; }
        [Display(Name = "RB")]
        public string RB { get; set; }
        [Display(Name = "BusFund")]
        public string BusFund { get; set; }
        [Display(Name = "AlumniDinner")]
        public string AlumniDinner { get; set; }
        [Display(Name = "PLD")]
        public string PLD { get; set; }
        [Display(Name = "Corpusfund")]
        public string Corpusfund { get; set; }
        [Display(Name = "BreakupParty")]
        public string BreakupParty { get; set; }
        [Display(Name = "CanteenSmartCard")]
        public string CanteenSmartCard { get; set; }
        [Display(Name = "Total")]
        public string Total { get; set; }
        [Display(Name = "BillMonth")]
        public string BillMonth { get; set; }
        [Display(Name = "PayStatus")]
        public string PayStatus { get; set; }


    }

}