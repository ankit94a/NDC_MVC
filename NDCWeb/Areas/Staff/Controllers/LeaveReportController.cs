using NDCWeb.Areas.Admin.Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Staff.Controllers
{
    [CSPLHeaders]
    [Authorize(Roles = CustomRoles.Staff)]
    //[UserMenu(MenuArea = "Staff")]
    [StaffStaticUserMenu]
    public class LeaveReportController : Controller
    {
        // GET: Staff/LeaveReport
        public ActionResult LeaveIndianCourse(int leaveid, string type)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                if (type == "1")
                {
                    return RedirectToAction("IndianCourseParticipantLeaveRpt", new { leaveid = leaveid });
                }
                else if (type == "2")
                {
                    return RedirectToAction("ForeignCourseParticipantLeaveRpt", new { leaveid = leaveid });
                }
                else
                {
                    return RedirectToAction("IndianCourseParticipantLeaveRpt", new { leaveid = leaveid });
                }
            }
            
            
        }

        #region Redirecting Rpts
        public ActionResult IndianCourseParticipantLeaveRpt(int leaveid)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var leaveinfo = uow.LeaveRepo.GetLeaveReportDetail(leaveid);
                return View(leaveinfo);
            }
        }
        public ActionResult ForeignCourseParticipantLeaveRpt(int leaveid)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var leaveinfo = uow.LeaveRepo.GetLeaveReportDetail(leaveid);
                return View(leaveinfo);
            }
        }
        public ActionResult ForeignParticipantStudyTourLeaveRpt(int leaveid)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var leaveinfo = uow.LeaveRepo.GetLeaveReportDetail(leaveid);
                return View(leaveinfo);
            }
        }
        public ActionResult LeaveCertificateRpt(int leaveid)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var staffs = uow.StaffMasterRepo.GetAll(x => x.Faculties);
                string aqName = staffs.FirstOrDefault(x => x.Faculties.StaffType == "AQ").FullName;

                var leaveinfo = uow.LeaveRepo.GetLeaveCertificate(leaveid);
                leaveinfo.AQName = aqName;
                return View(leaveinfo);
            }
        }
        #endregion

        public ActionResult LeaveNotingRpt(int leaveid)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var staffs = uow.StaffMasterRepo.GetAll(x => x.Faculties);
                string aqName = staffs.FirstOrDefault(x => x.Faculties.StaffType == "AQ").FullName;

                var leaveinfo = uow.LeaveRepo.GetLeaveReportDetail(leaveid);
                leaveinfo.AQName = aqName;
                //leaveinfo.JDSAdmName = staffs.FirstOrDefault(x => x.Faculties.StaffType == "JDS (ADM)").FullName; ;
                return View(leaveinfo);
            }
        }
        public ActionResult GetLeaveData()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                var leaveinfo = uow.LeaveRepo.GetViewCourseWiseLeaveCount(course.CourseId);
                //result = this.Json(leaveinfo.ToList(), JsonRequestBehavior.AllowGet);
                return PartialView("_AlreadyOnLeave", leaveinfo);
            }
        }

        public ActionResult GenerateCertificate(int leaveid)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var leaveinfo = uow.LeaveRepo.GenerateCertificate(leaveid);
                this.AddNotification("Leave Certificate Generated!", NotificationType.SUCCESS);
               // return RedirectToAction("LeaveCertificateRpt", leaveid);
                return RedirectToAction("LeaveCertificateRpt", leaveinfo);
            }
        }
    }
}