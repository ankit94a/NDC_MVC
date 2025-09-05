using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
	public class Event : BaseEntity
	{
		[Key]
		public int EventId { get; set; }
		public string EventName { get; set; }
		public string EventVenu { get; set; }
		public DateTime EventDate { get; set; }
		public DateTime EventTime { get; set; }
		public string EventDress { get; set; }
		public string Remarks { get; set; }
	}

	public class AspNetUsers
	{
		[Key]
		public int Id { get; set; }

		public string FName { get; set; }

		public string Email { get; set; }

		public bool EmailConfirmed { get; set; }

		public string PasswordHash { get; set; }

		public string SecurityStamp { get; set; }

		public string PhoneNumber { get; set; }

		public bool PhoneNumberConfirmed { get; set; }

		public bool TwoFactorEnabled { get; set; }

		public DateTime? LockoutEndDateUtc { get; set; }

		public bool LockoutEnabled { get; set; }

		public int AccessFailedCount { get; set; }

		public string UserName { get; set; }
	}

	public class MasterSearch
	{
		public int Id { get; set; }

		public string FName { get; set; }
		public int RoleId { get; set; }
		public string Role { get; set; }
	}

}