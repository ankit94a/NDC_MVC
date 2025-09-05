using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Models;
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
    [StaffStaticUserMenu]
    //[UserMenu(MenuArea = "Staff")]
    public class StaffLeaveInfoController : Controller
    {
        // GET: Staff/StaffLeaveInfo
        public async Task<ActionResult> ShowCompleteLeaveStatusList()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var staff = await uow.StaffMasterRepo.SingleOrDefaultAsync(x => x.LoginUserId == uId, np => np.Faculties);
                if (staff == null)
                    return RedirectToAction("");
                else
                {
                    string staffType = staff.Faculties.StaffType;
                    if (staffType.IsExistsInList("AQ", "SDS", "Secretary", "Comdt", "DS (Coord)", "JDS (ADM)","NA"))
                    {
                        var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                        var leaveinfo = await uow.LeaveRepo.GetViewStatusLeaveInfoByAppointmentAsync(staff.StaffId, course.CourseId, staffType);
                        if (TempData["leaveinfo"] != null)
                        {
                            var data = TempData["leaveinfo"];
                            leaveinfo = (IEnumerable<ShowCompleteLeaveStatusListVM>) TempData["leaveinfo"];
                        }                      

                        // Show/Hide New-Leave Button
                        if (staffType == "AQ" || staffType == "SDS")
                            ViewBag.BtnNewLeave = "Show";

                        // Show/Hide link of Leave-Certificate Report
                        if (leaveinfo != null)
                        {
                            if (staffType == "AQ" || staffType == "DS (Coord)" || staffType == "JDS (ADM)")
                            {
                                foreach (var leave in leaveinfo)
                                {
                                    if (leave.AQStatus == "Sanction" || leave.IAGStatus == "Sanction" || leave.ServiceSDSStatus == "Sanction")
                                    {
                                        leave.BtnLeaveCertificate = true;
                                    }
                                    if(leave.GenerateCertificate=="Generated")
                                    {
                                        leave.GenerateCertificate = "Generated";
                                    }
                                    else
                                    {
                                        leave.GenerateCertificate = "Not Generated";
                                    }
                                }
                            }
                        }
                        return View(leaveinfo);
                    }
                    else
                        return RedirectToAction("");
                }
            }
        }
        public async Task<ActionResult> Monthly()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var staff = await uow.StaffMasterRepo.SingleOrDefaultAsync(x => x.LoginUserId == uId, np => np.Faculties);
                if (staff == null)
                    return RedirectToAction("");
                else
                {
                    string staffType = staff.Faculties.StaffType;
                    if (staffType.IsExistsInList("AQ", "SDS", "Secretary", "Comdt", "DS (Coord)", "NA"))
                    {
                        var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                        var leaveinfo = await uow.LeaveRepo.GetViewStatusLeaveInfoByAppointmentAsync(staff.StaffId, course.CourseId, staffType);

                        // Show/Hide New-Leave Button
                        if (staffType == "AQ" || staffType == "SDS")
                            ViewBag.BtnNewLeave = "Show";

                        // Show/Hide link of Leave-Certificate Report
                        if (leaveinfo != null && staffType == "AQ")
                        {
                            foreach (var leave in leaveinfo)
                            {
                                if (leave.AQStatus == "Sanction" || leave.IAGStatus == "Sanction" || leave.ServiceSDSStatus == "Sanction")
                                {
                                    leave.BtnLeaveCertificate = true;
                                }
                            }
                        }
                        return View(leaveinfo);
                    }
                    else
                        return RedirectToAction("");
                }
            }
        }
        public async Task<ActionResult> AddStatusLeaveInfoList()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var staff = await uow.StaffMasterRepo.SingleOrDefaultAsync(x => x.LoginUserId == uId, np=>np.Faculties);
                if (staff == null)
                    return RedirectToAction("");
                else
                {
                    string staffType = staff.Faculties.StaffType;
                    //if(staffType.IsExistsInList("AQ", "SDS", "Secretary", "Comdt"))
                    if (staffType.IsExistsInList("AQ", "SDS"))
                    {
                        //if(staffType == "AQ")
                        //    ViewBag.Status = CustomDropDownList.GetLeaveOptRecNot();
                        //if (staffType == "SDS")
                        //    ViewBag.Status = CustomDropDownList.GetLeaveStatus();
                        
                        //ViewBag.AQStatus = CustomDropDownList.GetLeaveOptRecNot();
                        ViewBag.AQStatus = CustomDropDownList.GetLeaveOptInitNot();
                        ViewBag.IAGStatus = CustomDropDownList.GetLeaveOptRecNot();
                        ViewBag.SSDSStatus = CustomDropDownList.GetLeaveStatus();
                        var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                        var leaveinfoAdd = await uow.LeaveRepo.GetAddStatusLeaveInfoByAppointmentAsync(staff.StaffId, course.CourseId, staffType);
                        
                        return View(leaveinfoAdd);
                    }
                    else
                        return RedirectToAction("");
                }
            }
        }
        [HttpPost]
        public async Task<JsonResult> StaffLeaveStatusAdd(int leaveId, string status, string staffType)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                DateTime today = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                var leave = await uow.LeaveRepo.GetByIdAsync(leaveId);
                if (leave == null)
                    return Json("Record Not Found", JsonRequestBehavior.AllowGet);

                if (staffType == "AQ")
                {
                    leave.AQStatus = status;
                    leave.AQStatusDate = today;
                }
                else if (staffType == "IAG")
                {
                    leave.IAGStatus = status;
                    leave.IAGStatusDate = today;
                }
                else if (staffType == "SSDS")
                {
                    leave.ServiceSDSStatus = status;
                    leave.ServiceSDSStatusDate = today;
                }
                //else if (staffType == "Service")
                //{
                //    leave.ServiceSDSStatus = status;
                //    leave.ServiceSDSStatusDate = today;
                //}
                //else if (staffType == "IAGService")
                //{
                //    if (status == "Sanction")
                //    {
                //        leave.IAGStatus = "Recommend";
                //        leave.IAGStatusDate = today;

                //        leave.ServiceSDSStatus = status;
                //        leave.ServiceSDSStatusDate = today;
                //    }
                //    else if (status == "Not Recommend")
                //    {
                //        leave.IAGStatus = status;
                //        leave.IAGStatusDate = today;
                //    }
                //}
                else
                {
                    return Json("UnKnown Appointment", JsonRequestBehavior.AllowGet);
                }
                uow.Commit();
                return Json("Record Updated", JsonRequestBehavior.AllowGet);
            }
        }

        #region Specific Appointment Leave
        //public ActionResult AQLeaveInfoList()
        //{
        //    string uId = User.Identity.GetUserId();
        //    using (var uow = new UnitOfWork(new NDCWebContext()))
        //    {
        //        ViewBag.Status = CustomDropDownList.GetLeaveStatus();
        //        var feedBacks = uow.LeaveRepo.GetAll();
        //        var config = new MapperConfiguration(cfg =>
        //        {
        //            cfg.CreateMap<IEnumerable<Leave>, List<AQLeaveInfoListVM>>();
        //        });
        //        IMapper mapper = config.CreateMapper();
        //        var indexDto = mapper.Map<IEnumerable<Leave>, IEnumerable<AQLeaveInfoListVM>>(feedBacks).ToList();
        //        return View(indexDto);
        //    }
        //}
        //public async Task<JsonResult> AqLeaveStatusUpdate(int leaveId, string status)
        //{
        //    using (var uow = new UnitOfWork(new NDCWebContext()))
        //    {
        //        var leave = await uow.LeaveRepo.GetByIdAsync(leaveId);
        //        if(leave == null)
        //            return Json("Record Not Found", JsonRequestBehavior.AllowGet);

        //        leave.AQStatus = status;
        //        leave.AQStatusDate = System.DateTime.Now;
        //        uow.Commit();
        //        return Json("Record Updated", JsonRequestBehavior.AllowGet);
        //    }
        //}
        //public ActionResult SecretarySDSLeaveInfoList()
        //{
        //    string uId = User.Identity.GetUserId();
        //    using (var uow = new UnitOfWork(new NDCWebContext()))
        //    {
        //        ViewBag.Status = CustomDropDownList.GetLeaveStatus();
        //        var feedBacks = uow.LeaveRepo.GetAll();
        //        var config = new MapperConfiguration(cfg =>
        //        {
        //            cfg.CreateMap<IEnumerable<Leave>, List<AQLeaveInfoListVM>>();
        //        });
        //        IMapper mapper = config.CreateMapper();
        //        var indexDto = mapper.Map<IEnumerable<Leave>, IEnumerable<AQLeaveInfoListVM>>(feedBacks).ToList();
        //        return View(indexDto);
        //    }
        //}
        //public async Task<JsonResult> SecretarySDSLeaveStatusUpdate(int leaveId, string status)
        //{
        //    using (var uow = new UnitOfWork(new NDCWebContext()))
        //    {
        //        var leave = await uow.LeaveRepo.GetByIdAsync(leaveId);
        //        if (leave == null)
        //            return Json("Record Not Found", JsonRequestBehavior.AllowGet);

        //        leave.AQStatus = status;
        //        leave.AQStatusDate = System.DateTime.Now;
        //        uow.Commit();
        //        return Json("Record Updated", JsonRequestBehavior.AllowGet);
        //    }
        //}
        //public ActionResult ComdtLeaveInfoList()
        //{
        //    string uId = User.Identity.GetUserId();
        //    using (var uow = new UnitOfWork(new NDCWebContext()))
        //    {
        //        ViewBag.Status = CustomDropDownList.GetLeaveStatus();
        //        var feedBacks = uow.LeaveRepo.GetAll();
        //        var config = new MapperConfiguration(cfg =>
        //        {
        //            cfg.CreateMap<IEnumerable<Leave>, List<AQLeaveInfoListVM>>();
        //        });
        //        IMapper mapper = config.CreateMapper();
        //        var indexDto = mapper.Map<IEnumerable<Leave>, IEnumerable<AQLeaveInfoListVM>>(feedBacks).ToList();
        //        return View(indexDto);
        //    }
        //}
        //public async Task<JsonResult> ComdtLeaveStatusUpdate(int leaveId, string status)
        //{
        //    using (var uow = new UnitOfWork(new NDCWebContext()))
        //    {
        //        var leave = await uow.LeaveRepo.GetByIdAsync(leaveId);
        //        if (leave == null)
        //            return Json("Record Not Found", JsonRequestBehavior.AllowGet);

        //        leave.AQStatus = status;
        //        leave.AQStatusDate = System.DateTime.Now;
        //        uow.Commit();
        //        return Json("Record Updated", JsonRequestBehavior.AllowGet);
        //    }
        //}
        #endregion

        public async Task<ActionResult> ShowGenerateLeaveCertList()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var staff = await uow.StaffMasterRepo.SingleOrDefaultAsync(x => x.LoginUserId == uId, np => np.Faculties);
                if (staff == null)
                    return RedirectToAction("");
                else
                {
                    string staffType = staff.Faculties.StaffType;
                    if (staffType.IsExistsInList("AQ"))
                    {
                        var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                        var leaveinfo = await uow.LeaveRepo.GetViewStatusLeaveInfoByAppointmentForGenCertAsync(staff.StaffId, course.CourseId, staffType);
                        TempData["leaveinfo"] = leaveinfo;
                        return RedirectToAction("ShowCompleteLeaveStatusList");
                    }
                    else
                        return RedirectToAction("");
                }
            }
        }
    }

}