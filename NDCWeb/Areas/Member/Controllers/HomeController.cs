using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Models;
using NDCWeb.Persistence;
using NDCWeb.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Member.Controllers
{
    [Authorize(Roles = CustomRoles.Candidate)]
    [UserMenu(MenuArea = "Member")]
    [CSPLHeaders]
    [SessionTimeout]
    public class HomeController : Controller
    {
        // GET: Member/Home
        public ActionResult Index()
        {
            ViewBag.Flash = GetFlash();
            ViewBag.Upcoming = GetUpcoming();
            string uId = User.Identity.GetUserId();
            //ViewBag.fullname = User.Identity.get();
            ViewBag.fullname= ((ClaimsIdentity)User.Identity).FindFirst("FName").Value;
            using(var uow=new UnitOfWork(new NDCWebContext()))
            {
                if (uow.MediaGalleryRepo.FirstOrDefault(x => x.MediaCategoryId == 8 && x.Archive == false) != null)
                    ViewBag.TrainingCalendar = uow.MediaFileRepo.FirstOrDefault(x => x.MediaGalleries.MediaCategoryId == 8 && x.MediaGalleries.Archive == false).FilePath;

                if (uow.MediaGalleryRepo.FirstOrDefault(x => x.MediaCategoryId == 9 && x.Archive == false) != null)
                    ViewBag.WeeklyTrainingProgramme = uow.MediaFileRepo.FirstOrDefault(x => x.MediaGalleries.MediaCategoryId == 9 && x.MediaGalleries.Archive == false).FilePath;

                //var memberPersonal = uow.CrsMbrPersonalRepo.Find(x => x.CreatedBy == uId).OrderByDescending(x=>x.CourseMemberId).FirstOrDefault();
                var memberPersonal = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == uId);
                var personalDetail = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == uId, np => np.OfficeStates, np2 => np2.OfficeStates.Countries);
                var coursereg = uow.CourseRegisterRepo.Find(x => x.UserId == uId).FirstOrDefault();
                var membercourse = uow.CourseRepo.Find(x => x.CourseId == coursereg.CourseId).FirstOrDefault();
                
                if (memberPersonal != null)
                {
                    ViewBag.ProfilePic = memberPersonal.MemberImgPath;
                    ViewBag.Exists = memberPersonal.CourseId;
                    ViewBag.Course = membercourse.CourseName;

                    var lockerAllotment = uow.LockerAllotmentRepo.FirstOrDefault(x => x.CourseMemberId == memberPersonal.CourseMemberId);
                    if (lockerAllotment != null)
                    {
                        ViewBag.LockerNo = lockerAllotment.LockerNo;
                        ViewBag.RolesAssign = lockerAllotment.RolesAssign;

                        string leaveAlert = uow.LeaveRepo.GetAlertLeaveForCourseMember(uId);
                        if (!string.IsNullOrEmpty(leaveAlert))
                            ViewBag.LeaveAlert = leaveAlert;
                    }
                }
                else
                {
                    ViewBag.Exists = null;
                }
            }
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ViewResult Maps()
        {
            return View();
        }
        public ViewResult UploadProcedure()
        {
            return View();
        }
        private List<NewsBulletinVM> GetFlash()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var newsArticle = uow.NewsArticleRepo.GetAll().Where(x => x.NewsCategory == NewsCategory.Flash && x.Archive == false && x.DisplayArea == NewsDisplayArea.Member).OrderByDescending(x => x.NewsArticleId);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<NewsArticle>, List<NewsBulletinVM>>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<IEnumerable<NewsArticle>, IEnumerable<NewsBulletinVM>>(newsArticle).ToList();
            }
        }
        private List<NewsBulletinVM> GetUpcoming()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var newsArticle = uow.NewsArticleRepo.GetAll().Where(x => x.NewsCategory == NewsCategory.Upcoming && x.Archive == false && x.DisplayArea == NewsDisplayArea.Member || x.DisplayArea == NewsDisplayArea.All).OrderByDescending(x => x.NewsArticleId);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<NewsArticle>, List<NewsBulletinVM>>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<IEnumerable<NewsArticle>, IEnumerable<NewsBulletinVM>>(newsArticle).ToList();
            }
        }

        #region Socials
        public ActionResult BirthdayAlert()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                var birthdays = uow.CrsMbrPersonalRepo.GetStaffCourseMemberBirthdayAlert(course.CourseId).OrderBy(x=>x.DOB);
                return PartialView("_BirthdayList", birthdays);
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
		//public ActionResult PreviousCourseAlert()
		//{
		//    using (var uow = new UnitOfWork(new NDCWebContext()))
		//    {
		//        var course = uow.CourseRepo.Find(x => x.IsCurrent == true && x.UnderRegistration == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
		//        if (course != null)               
		//        return View(course);
		//    }
		//}
		public ActionResult CircularAlert()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                //var circular = uow.CircularRepo.GetAlertCircularList("Circular");
                string uId = User.Identity.GetUserId();
                var circular = uow.CircularDetailRepo.GetOrderAsPerDesignation("Member", uId);
                return View(circular);
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
        public ActionResult BillAlert()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                //var personal = uow.MessBillRepo.FirstOrDefault(x => x.MemberStaffId = Uid);
                 var BillDetail = uow.MessBillRepo.GetBillDetail(uId);
                return View(BillDetail);
            }
        }
        #endregion

        #region Document download
        public ActionResult DocAlert()
        {
            string uId = User.Identity.GetUserId();
            return View();
        }
        #endregion
    }
}