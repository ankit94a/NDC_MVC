using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Staff.View_Models
{
	public class StaffLeaveInfoCommonVM
	{
		[Required(ErrorMessage = "Leave Id Not Supplied")]
		[Display(Name = "Leave Id")]
		public int LeaveId { get; set; }

		[Required(ErrorMessage = "Leave Category Not Supplied")]
		[Display(Name = "Leave For")]
		public string LeaveCategory { get; set; }

		[Required(ErrorMessage = "Leave Type Not Supplied")]
		[Display(Name = "Leave Type")]
		public string LeaveType { get; set; }

		[Required(ErrorMessage = "From Date Not Supplied")]
		[Display(Name = "From Date")]
		[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
		public DateTime FromDate { get; set; }

		[Required(ErrorMessage = "To Date Not Supplied")]
		[Display(Name = "To Date")]
		[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
		public DateTime ToDate { get; set; }

		[Required(ErrorMessage = "Total Days Not Supplied")]
		[Display(Name = "Total Days")]
		public int TotalDays { get; set; }

		[Display(Name = "Prefix Date")]
		[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? PrefixDate { get; set; }

		[Display(Name = "Prefix To Date")]
		[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? PrefixToDate { get; set; }

		[Display(Name = "Suffix Date")]
		[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? SuffixDate { get; set; }

		[Display(Name = "Suffix To Date")]
		[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? SuffixToDate { get; set; }

		[Required(ErrorMessage = "Reason For Leave Not Supplied")]
		[Display(Name = "Reason For Leave")]
		public string ReasonForLeave { get; set; }

		[Required(ErrorMessage = "Address On Leave Not Supplied")]
		[Display(Name = "Address On Leave")]
		public string AddressOnLeave { get; set; }

		[Required(ErrorMessage = "Telephone No Not Supplied")]
		[Display(Name = "Telephone No")]
		public string TeleNo { get; set; }

		[Display(Name = "Recommend By Embassy")]
		public string RecommendByEmbassy { get; set; }
		public string DocPath { get; set; }

		[Display(Name = "Country Id")]
		public int? CountryId { get; set; }
		public virtual CountryMaster Country { get; set; }
	}

	public class StaffLeaveInfoVM : StaffLeaveInfoCommonVM
	{
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
	}

	public class ShowCompleteLeaveStatusListVM : StaffLeaveInfoVM
	{
		public string FullName { get; set; }
		public string LockerNo { get; set; }
		public string ServiceNo { get; set; }
		public bool BtnLeaveCertificate { get; set; }
	}
	public class CalendarLeaveInfoListVM : StaffLeaveInfoCommonVM
	{
		public string FullName { get; set; }
		public string LockerNo { get; set; }
		public string Start_Date { get; set; }
		public string End_Date { get; set; }

	}

	public class AddStatusLeaveInfoListVM : StaffLeaveInfoCommonVM
	{
		public string FullName { get; set; }
		public string LockerNo { get; set; }
		public string StaffType { get; set; }
	}

	#region Report
	public class ShowLeaveRptVM : StaffLeaveInfoVM
	{
		public string FullName { get; set; }
		public string LockerNo { get; set; }
		public string ServiceNo { get; set; }
		public string CountryName { get; set; }
		public string AQName { get; set; }
		public string JDSAdmName { get; set; }
	}
	public class ShowLeaveCertificateRptVM : StaffLeaveInfoVM
	{
		public string FullName { get; set; }
		public string LockerNo { get; set; }
		public string ServiceNo { get; set; }
		public string CountryName { get; set; }
		public string AQName { get; set; }
	}
	#endregion





	#region Staff Leaveinfo Not Use
	public class AQLeaveInfoListVM : StaffLeaveInfoVM
	{

	}
	public class ADSLeaveInfoListVM : StaffLeaveInfoVM
	{

	}
	public class SDSLeaveInfoListVM : StaffLeaveInfoVM
	{

	}
	public class SecretaryLeaveInfoListVM : StaffLeaveInfoVM
	{

	}
	#endregion

	public class NotificationModel
	{
		public string Title { get; set; }
		public int Count { get; set; }
	}
}