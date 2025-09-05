using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Admin.View_Models;
using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Models;
using NDCWeb.Persistence;
using NDCWeb.View_Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Staff.Controllers
{
	[CSPLHeaders]
	[Authorize(Roles = CustomRoles.Staff)]
	[StaffStaticUserMenu]
	//[UserMenu(MenuArea = "Staff")]
	[EncryptedActionParameter]
	[SessionTimeout]
	public class HomeController : Controller
	{
		// GET: Staff/Home
		public async Task<ActionResult> Index()
		{
			ViewBag.Flash = GetFlash();
			string uId = User.Identity.GetUserId();
			//ViewBag.fullname = ((ClaimsIdentity)User.Identity).FindFirst("FName").Value;
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				if (uow.MediaGalleryRepo.FirstOrDefault(x => x.MediaCategoryId == 8 && x.Archive == false) != null)
					ViewBag.TrainingCalendar = uow.MediaFileRepo.FirstOrDefault(x => x.MediaGalleries.MediaCategoryId == 8 && x.MediaGalleries.Archive == false).FilePath;

				if (uow.MediaGalleryRepo.FirstOrDefault(x => x.MediaCategoryId == 9 && x.Archive == false) != null)
					ViewBag.WeeklyTrainingProgramme = uow.MediaFileRepo.FirstOrDefault(x => x.MediaGalleries.MediaCategoryId == 9 && x.MediaGalleries.Archive == false).FilePath;

				var staffMember = uow.StaffMasterRepo.Find(x => x.LoginUserId == uId, fk => fk.Faculties).FirstOrDefault();
				if (staffMember != null)
				{
					ViewBag.fullname = staffMember.FullName;
					ViewBag.ProfilePic = staffMember.SelfImage;
					ViewBag.Appointment = staffMember.Faculties.FacultyName;
					string staffType = staffMember.Faculties.StaffType;
					//ViewBag.StaffType = staffType;
					ViewBag.StaffType = staffMember.Faculties.FacultyId;

					//var onlineupld = uow.ForumBlogRepo.GetAlertOnlineUploadsList();
					//ViewBag.OnlineUpld = onlineupld.Count();

					//var onlineupldPending = uow.ForumBlogRepo.Find(x => x.Status == "Pending" && x.StaffRemark==null && x.StaffId == staffMember.StaffId);
					//var onlineupldPending = uow.ForumBlogRepo.Find(x => x.Status == "Pending"  && x.StaffId == staffMember.StaffId);
					//ViewBag.OnlineUploadPndngCount = onlineupldPending.Count();

					if (staffType.IsExistsInList("AQ", "SDS"))
					{
						var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
						var leaveinfoAdd = await uow.LeaveRepo.GetAddStatusLeaveInfoByAppointmentAsync(staffMember.StaffId, course.CourseId, staffType);
						ViewBag.ActiveLeave = leaveinfoAdd.Count();
					}
					else if (staffType.IsExistsInList("Secretary", "Comdt", "DS (Coord)", "NA"))
					{
						var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
						var lastWeekNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).AddDays(-7);

						var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
						var leaveinfo = await uow.LeaveRepo.GetViewStatusLeaveInfoByAppointmentAsync(staffMember.StaffId, course.CourseId, staffType);
						var leaveinfoActive = leaveinfo.Where(x => lastWeekNow.Date <= (x.ServiceSDSStatusDate != null ? x.ServiceSDSStatusDate.Value.Date : lastWeekNow.AddDays(-30).Date) && x.ServiceSDSStatusDate.Value.Date <= now.Date).ToList();
						ViewBag.SanctionActiveLeave = leaveinfoActive.Count();
					}
					var unvalumni = uow.AlumniRepo.Find(x => x.Verified == false);
					ViewBag.UnverifiedCount = unvalumni.Count();

					var unvalumniinstep = uow.AlumniRepo.Find(x => x.Verified == false && x.InStepCourseId != null);
					ViewBag.UnverifiedInStepCount = unvalumniinstep.Count();

					var aluminifbk = uow.AlumniFeedbackRepo.GetAll();
					ViewBag.FeedbackCount = aluminifbk.Count();

					var unvmember = uow.CourseRegisterRepo.Find(x => x.Approved == false);
					ViewBag.UnverifiedMbrCount = unvmember.Count();

					var alumniarticle = uow.AlumniArticleRepo.GetAll();
					ViewBag.ArticleCount = alumniarticle.Count();

					//DateTime dtnow = DateTime.Now;
					//string dtnows = dtnow.ToString("yyyy-MM-dd");

					var partyevent = uow.EventRepo.Find(x => (DateTime.Compare(x.EventDate, DateTime.Now)) >= 0);
					ViewBag.EventCount = partyevent.Count() + partyevent.Count();

					var iteqpt = uow.InfotechRepo.GetAll();
					ViewBag.ItEqptReqCount = iteqpt.Count();

					var internet = uow.TelecommRequirementRepository.GetAll();
					ViewBag.InternetReqCount = internet.Count();

					var lifemember = uow.LibraryMembershipRepo.GetAll();
					ViewBag.LifeMemCount = lifemember.Count();

					var courseendfb = uow.CourseFeedbackRepo.GetAll();
					ViewBag.CourseEndFBCount = courseendfb.Count();

					var modulefb = uow.FeedbackModuleRepo.GetAll();
					ViewBag.ModuleFBCount = modulefb.Count();

					var speakerfb = uow.FeedbackSpeakerRepo.GetAll();
					ViewBag.SpeakerFBCount = speakerfb.Count();

					var mphilapp = uow.MPhilMemberRepo.GetAll();
					ViewBag.MPhilApp = mphilapp.Count();

					var mphildegree = uow.MPhilDegreeRepo.GetAll();
					ViewBag.MPhilDegree = mphildegree.Count();

					var gencertpending = uow.LeaveRepo.Find(x => x.GenerateCertificate != "Generated").Where(l => l.ServiceSDSStatus == "Sanction");
					ViewBag.GenCertPending = gencertpending.Count();

					ViewBag.GenCirculars = GetCirculars();
				}
			}
			return View();
		}
		public ActionResult IndexNA()
		{
			return View();
		}
		
		public ViewResult Maps()
		{
			return View();
		}
		#region Caledar
		private List<CalendarLeaveInfoListVM> LoadData()
		{
			// Initialization.
			List<CalendarLeaveInfoListVM> lst = new List<CalendarLeaveInfoListVM>();

			try
			{
				// Initialization.
				string line = string.Empty;
				string srcFilePath = "Content/PublicHoliday.txt";
				var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
				var fullPath = Path.Combine(rootPath, srcFilePath);
				string filePath = new Uri(fullPath).LocalPath;
				StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read));

				// Read file.
				while ((line = sr.ReadLine()) != null)
				{
					// Initialization.
					CalendarLeaveInfoListVM infoObj = new CalendarLeaveInfoListVM();
					string[] info = line.Split(',');

					// Setting.
					infoObj.LeaveId = Convert.ToInt32(info[0].ToString());
					infoObj.FullName = info[1].ToString();
					infoObj.LockerNo = info[2].ToString();
					infoObj.Start_Date = info[3].ToString();
					infoObj.End_Date = info[4].ToString();

					// Adding.
					lst.Add(infoObj);
				}

				// Closing.
				sr.Dispose();
				sr.Close();
			}
			catch (Exception ex)
			{
				ViewBag.ErrMsg = ex.Message;
				// info.
				//Console.Write(ex);
			}

			// info.
			return lst;
		}

		#endregion
		#region
		private List<NewsBulletinVM> GetFlash()
		{
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var newsArticle = uow.NewsArticleRepo.GetAll().Where(x => x.NewsCategory == NewsCategory.Upcoming && x.Archive == false && x.DisplayArea == NewsDisplayArea.All).OrderByDescending(x => x.NewsArticleId);
				var config = new MapperConfiguration(cfg =>
				{
					cfg.CreateMap<IEnumerable<NewsArticle>, List<NewsBulletinVM>>();
				});
				IMapper mapper = config.CreateMapper();
				return mapper.Map<IEnumerable<NewsArticle>, IEnumerable<NewsBulletinVM>>(newsArticle).ToList();
			}
		}
		public async Task<ActionResult> Article(int id)
		{
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var newsArticle = await uow.NewsArticleRepo.GetByIdAsync(id);
				if (newsArticle == null)
					return HttpNotFound();
				else
				{
					var config = new MapperConfiguration(cfg =>
					{
						cfg.CreateMap<NewsArticle, NewsArticleIndxVM>();
					});
					IMapper mapper = config.CreateMapper();
					var indexDto = mapper.Map<NewsArticle, NewsArticleIndxVM>(newsArticle);
					await uow.CommitAsync();

					return View(indexDto);
				}
			}
		}
		public ActionResult SocialEvents()
		{
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var eventDetail = uow.EventRepo.GetAll();
				var config = new MapperConfiguration(cfg =>
				{
					cfg.CreateMap<Event, EventIndexVM>();
				});
				IMapper mapper = config.CreateMapper();
				var evenTs = mapper.Map<IEnumerable<Event>, List<EventIndexVM>>(eventDetail);
				return PartialView("_Events", evenTs);
			}
		}
		#endregion

		#region Socials
		public ActionResult BirthdayAlert()
		{
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
				var birthdays = uow.CrsMbrPersonalRepo.GetStaffCourseMemberBirthdayAlert(course.CourseId);
				return PartialView("_BirthdayList", birthdays);
			}
		}
		[HttpPost]
		public JsonResult BirthdayAlert(int month)
		{

			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
				var birthdays = uow.CrsMbrPersonalRepo.GetStaffCourseMemberBirthdayAlert(course.CourseId, month).OrderBy(x => x.DOB);
				return Json(birthdays, JsonRequestBehavior.AllowGet);
				//return PartialView("_BirthdayList", birthdays);
			}
		}
		public ActionResult FamilyBirthdayAlert()
		{
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
				var birthdays = uow.CrsMbrPersonalRepo.GetCrsMbrFamilyBirthdayAlert(course.CourseId);
				return PartialView("_FamilyBirthdayList", birthdays);
			}
		}
		public ActionResult MarriageAnniversaryAlert()
		{
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
				var anniversaries = uow.CrsMbrPersonalRepo.GetMarriageAnniversaryAlert(course.CourseId);
				return PartialView("_MarriageAnniversaryList", anniversaries);
			}
		}
		public ActionResult GetLeaveData()
		{
			string uId = User.Identity.GetUserId();
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
				var leaveinfo = uow.LeaveRepo.GetViewCourseWiseLeaveCount(course.CourseId);
				//result = this.Json(leaveinfo.ToList(), JsonRequestBehavior.AllowGet);
				return PartialView("_MonthlyLeave", leaveinfo);
			}
		}

		#endregion

		#region Alerts
		public ActionResult CircularAlert()
		{
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				//var circular = uow.CircularRepo.GetAlertCircularList("Circular");
				string uId = User.Identity.GetUserId();
				string stafftype = "";
				if (uId == "1025")
					stafftype = "Comdt";
				else
					stafftype = "Staff";

				var circular = uow.CircularDetailRepo.GetOrderAsPerDesignation(stafftype, uId);
				return View(circular);
			}
		}
		public int GetCirculars()
		{
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				//var circular = uow.CircularRepo.GetAlertCircularList("Circular");
				string uId = User.Identity.GetUserId();
				string stafftype = "";
				if (uId == "1025")
					stafftype = "Comdt";
				else
					stafftype = "Staff";

				var circular = uow.CircularDetailRepo.GetOrderAsPerDesignation(stafftype, uId);

				return circular.Count();
			}
		}
		public ActionResult OnlineUploadsAlert()
		{
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var onlineupload = uow.ForumBlogRepo.GetAlertOnlineUploadsList();
				return View(onlineupload);
			}
		}
		public ActionResult OrderAlert()
		{
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var circular = uow.CircularRepo.GetAlertCircularList("Order");
				return View(circular);
			}
		}
		#endregion

	
	}

}