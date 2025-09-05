using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Models;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Staff.Controllers
{
    [CSPLHeaders]
    [Authorize(Roles = CustomRoles.Staff)]
    [StaffStaticUserMenu]
    public class HolidayCalendarController : Controller
    {
        public ActionResult Index()
        {
            string uId = User.Identity.GetUserId();
            using (NDCWebContext db = new NDCWebContext())
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var staffMember = uow.StaffMasterRepo.Find(x => x.LoginUserId == uId, fk => fk.Faculties).FirstOrDefault();
                    if (staffMember != null)
                    {
                        if (staffMember.Faculties.FacultyId == 11)
                        {
                            List<CountryMaster> countryMasters = db.CountryMasters.ToList();
                            List<HolidayCalendar> holidayCalendars = db.HolidayCalendars.ToList();

                            var modulefeedback = (from hc in holidayCalendars
                                                  join cm in countryMasters on hc.CountryId equals cm.CountryId
                                                  orderby hc.HolidayCalendarId descending
                                                  select new HolidayCalendarIndxVM()
                                                  {
                                                      HolidayCalendarId = hc.HolidayCalendarId,
                                                      Description = hc.Description,
                                                      Month = hc.HolidayDate.Month,
                                                      Day = hc.HolidayDate.Day,
                                                      HolidayType = hc.HolidayType,
                                                      CountryName = cm.CountryName
                                                  }).ToList();
                            return View(modulefeedback);
                        }
                        else
                        {
                            this.AddNotification("Not Authorize.", NotificationType.ERROR);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        this.AddNotification("Not Staff Member.", NotificationType.ERROR);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
        }
        [HttpGet]
        public ActionResult Add()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var staffMember = uow.StaffMasterRepo.Find(x => x.LoginUserId == uId, fk => fk.Faculties).FirstOrDefault();
                if (staffMember != null)
                {
                    if(staffMember.Faculties.FacultyId==11)
                    {
                        ViewBag.Country = uow.CountryMasterRepo.GetCountries();
                        return View();
                    }
                    else
                    {
                        this.AddNotification("Not Authorize.", NotificationType.ERROR);
                        return RedirectToAction("Index","Home");
                    }
                }
                else
                {
                    this.AddNotification("Not Staff Member.", NotificationType.ERROR);
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        [HttpPost]
        public ActionResult Add(HolidayCalendarAddVM obj)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction("Create", new {id= obj.CountryId});
            }
            else
            {
                return View(obj);
            }
        }
        [HttpGet]
        public ActionResult Create(int id)
        {
            string uId = User.Identity.GetUserId();

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Country = uow.CountryMasterRepo.GetSelectedCountries(id);
                var staffMember = uow.StaffMasterRepo.Find(x => x.LoginUserId == uId, fk => fk.Faculties).FirstOrDefault();
                if (staffMember != null)
                {
                    if (staffMember.Faculties.FacultyId == 11)
                    {
                        if (id == 101)
                        {
                            ViewBag.HolidayTypes = uow.HolidayCalendarRepo.GetHolidayTypeIndia();
                        }
                        else
                        {
                            ViewBag.HolidayTypes = uow.HolidayCalendarRepo.GetHolidayTypeOther();
                        }
                        HolidayCalendarCrtVM holidayCalendarCrtVM = new HolidayCalendarCrtVM();
                        holidayCalendarCrtVM.CountryId = id;
                        holidayCalendarCrtVM.HolidayDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                        return View(holidayCalendarCrtVM);
                    }
                    else
                    {
                        this.AddNotification("Not Authorize.", NotificationType.ERROR);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    this.AddNotification("Not Staff Member.", NotificationType.ERROR);
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create(HolidayCalendarCrtVM objEv)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Country = uow.CountryMasterRepo.GetSelectedCountries(objEv.CountryId);
                
                if (objEv.CountryId == 101)
                {
                    ViewBag.HolidayTypes = uow.HolidayCalendarRepo.GetHolidayTypeIndia();
                }
                else
                {
                    ViewBag.HolidayTypes = uow.HolidayCalendarRepo.GetHolidayTypeOther();
                }


                if (ModelState.IsValid)
                {
                    HolidayCalendar holidayCalendar = new HolidayCalendar();
                    holidayCalendar.Description = objEv.Description;
                    holidayCalendar.HolidayDate = objEv.HolidayDate;
                    holidayCalendar.Day = objEv.HolidayDate.Day;
                    holidayCalendar.Month = objEv.HolidayDate.Month;
                    holidayCalendar.CountryId = objEv.CountryId;
                    holidayCalendar.HolidayType = objEv.HolidayType;

                    if (objEv.HolidayType == "Closed Holidays of India")
                        holidayCalendar.ColorCode = "#FFCCCB";
                    else if (objEv.HolidayType == "Restricted  Holidays of India")
                        holidayCalendar.ColorCode = "#ADD8E6";
                    else if (objEv.HolidayType == "Independence Day")
                        holidayCalendar.ColorCode = "#FFA500";
                    else if (objEv.HolidayType == "National Day")
                        holidayCalendar.ColorCode = "#90EE90";


                    uow.HolidayCalendarRepo.Add(holidayCalendar);
                    await uow.CommitAsync();
                    this.AddNotification("Record Saved", NotificationType.SUCCESS);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(objEv);
                }
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            string uId = User.Identity.GetUserId();

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                HolidayCalendar holiday = uow.HolidayCalendarRepo.Find(x=>x.HolidayCalendarId==id).SingleOrDefault();

                ViewBag.Country = uow.CountryMasterRepo.GetSelectedCountries(holiday.CountryId);
                var staffMember = uow.StaffMasterRepo.Find(x => x.LoginUserId == uId, fk => fk.Faculties).FirstOrDefault();
                
                if (staffMember != null)
                {
                    if (staffMember.Faculties.FacultyId == 11)
                    {
                        if (holiday != null)
                        {
                            if (holiday.CountryId == 101)
                            {
                                ViewBag.HolidayTypes = uow.HolidayCalendarRepo.GetHolidayTypeIndia();
                            }
                            else
                            {
                                ViewBag.HolidayTypes = uow.HolidayCalendarRepo.GetHolidayTypeOther();
                            }

                            HolidayCalendarUpVM holidayCalendarUpVM = new HolidayCalendarUpVM();
                            holidayCalendarUpVM.HolidayCalendarId = holiday.HolidayCalendarId;
                            holidayCalendarUpVM.Description = holiday.Description;
                            holidayCalendarUpVM.HolidayDate = holiday.HolidayDate;
                            holidayCalendarUpVM.HolidayType = holiday.HolidayType;
                            holidayCalendarUpVM.CountryId = holiday.CountryId;
                            return View(holidayCalendarUpVM);
                        }
                        else
                        {
                            this.AddNotification("Error!", NotificationType.ERROR);
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        this.AddNotification("Not Authorize.", NotificationType.ERROR);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    this.AddNotification("Not Staff Member.", NotificationType.ERROR);
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        [HttpPost]
        public async Task<ActionResult> Edit(HolidayCalendarUpVM objHolidayCalendarUvm)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Country = uow.CountryMasterRepo.GetSelectedCountries(objHolidayCalendarUvm.CountryId);

                if (objHolidayCalendarUvm.CountryId == 101)
                {
                    ViewBag.HolidayTypes = uow.HolidayCalendarRepo.GetHolidayTypeIndia();
                }
                else
                {
                    ViewBag.HolidayTypes = uow.HolidayCalendarRepo.GetHolidayTypeOther();
                }

                if (ModelState.IsValid)
                {
                    HolidayCalendar holidayCalendar = uow.HolidayCalendarRepo.Find(x => x.HolidayCalendarId == objHolidayCalendarUvm.HolidayCalendarId).SingleOrDefault();

                    if(holidayCalendar!=null)
                    {
                        holidayCalendar.Description = objHolidayCalendarUvm.Description;
                        holidayCalendar.HolidayDate = objHolidayCalendarUvm.HolidayDate;
                        holidayCalendar.Day = objHolidayCalendarUvm.HolidayDate.Day;
                        holidayCalendar.Month = objHolidayCalendarUvm.HolidayDate.Month;
                        holidayCalendar.HolidayType = objHolidayCalendarUvm.HolidayType;
                        holidayCalendar.CountryId = objHolidayCalendarUvm.CountryId;

                        if (objHolidayCalendarUvm.HolidayType == "Closed Holidays of India")
                            holidayCalendar.ColorCode = "#FFCCCB";
                        else if (objHolidayCalendarUvm.HolidayType == "Restricted  Holidays of India")
                            holidayCalendar.ColorCode = "#ADD8E6";
                        else if (objHolidayCalendarUvm.HolidayType == "Independence Day")
                            holidayCalendar.ColorCode = "#FFA500";
                        else if (objHolidayCalendarUvm.HolidayType == "National Day")
                            holidayCalendar.ColorCode = "#90EE90";

                        uow.HolidayCalendarRepo.Update(holidayCalendar);
                        await uow.CommitAsync();
                        this.AddNotification("Record Saved", NotificationType.SUCCESS);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        this.AddNotification("Error!", NotificationType.ERROR);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return View(objHolidayCalendarUvm);
                }

            }
        }
        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var DeleteItem = await uow.HolidayCalendarRepo.GetByIdAsync(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.HolidayCalendarRepo.Remove(DeleteItem);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult GetHolidayCalendra()
        {
            DateTime localVersion = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

            //List<GetHolidayCalendarVM> getHolidayCalendarList = new List<GetHolidayCalendarVM>();
            //using (NDCWebContext db = new NDCWebContext())
            //{
            //    List<CountryMaster> countryMasters = db.CountryMasters.ToList();
            //    List<HolidayCalendar> holidayCalendars = db.HolidayCalendars.ToList();

            //    var modulefeedback = (from hc in holidayCalendars
            //                          join cm in countryMasters on hc.CountryId equals cm.CountryId
            //                          orderby hc.HolidayCalendarId descending
            //                          select new GetHolidayCalendarVM()
            //                          {
            //                              HolidayCalendarId = hc.HolidayCalendarId,
            //                              Description = hc.Description,
            //                              StartDate= new DateTime(localVersion.Year, hc.Month, hc.Day,0,0,1),
            //                              EndDate = new DateTime(localVersion.Year, hc.Month, hc.Day, 23, 59, 59),
            //                              HolidayType = hc.HolidayType,
            //                              ColorCode = hc.ColorCode,
            //                              CountryName = cm.CountryName
            //                          }).ToList();
            //    return new JsonResult { Data = modulefeedback, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //}
            using (NDCWebContext db = new NDCWebContext())
            {
                List<HolidayCalendar> holidayCalendars = db.HolidayCalendars.ToList();
                List<GetHolidayCalendarVM> getHolidayCalendarVMs = new List<GetHolidayCalendarVM>();

                List<HolidayCalendar> holidayCalendarsWithCurrentYear = new List<HolidayCalendar>();
                foreach (var item in holidayCalendars)
                {
                    HolidayCalendar holidayCalendar = new HolidayCalendar()
                    {
                        HolidayCalendarId = item.HolidayCalendarId,
                        Description = item.Description,
                        HolidayType = item.HolidayType,
                        HolidayDate = new DateTime(localVersion.Year, item.Month, item.Day, 0, 0, 0),
                        ColorCode = item.ColorCode,
                        CountryId=item.CountryId,
                    };
                    holidayCalendarsWithCurrentYear.Add(holidayCalendar);
                }

                var HolidayGroup = holidayCalendarsWithCurrentYear.GroupBy(x => new { x.HolidayDate, x.HolidayType })
                                                                    .Select(x => new HolidayCalendarGroupVM
                                                                    { 
                                                                        HolidayDate = x.Key.HolidayDate,
                                                                        HolidayType =x.Key.HolidayType,
                                                                    });


                foreach (var item in HolidayGroup)
                {
                    GetHolidayCalendarVM getHolidayCalendarVM = new GetHolidayCalendarVM();
                    List<HolidayCalendar> holidayCalendars1 = holidayCalendarsWithCurrentYear.Where(x => x.HolidayDate == item.HolidayDate && x.HolidayType == item.HolidayType).ToList();
                    string des=string.Empty;
                    string country_name = string.Empty;
                    var sb = new StringBuilder();
                    sb.Append(@"<ul style='list-style-type: square;'>");

                    foreach (var item2 in holidayCalendars1)
                    {
                        if(item2.HolidayType== "Closed Holidays of India")
                        {
                            sb.Append(@"<li>" + (item2.Description) + ".</li>");
                        }
                        else if (item2.HolidayType == "Restricted  Holidays of India")
                        {
                            sb.Append(@"<li>" + (item2.Description) + ".</li>");
                        }
                        else if (item2.HolidayType == "Independence Day")
                        {
                            country_name = (from c in db.CountryMasters.ToList()
                                            where c.CountryId == item2.CountryId
                                            select c.CountryName).First().ToString();

                            sb.Append(@"<li>" + (item2.Description) + " of " + country_name + ".</li>");
                        }
                        else if (item2.HolidayType == "National Day")
                        {
                            country_name = (from c in db.CountryMasters.ToList()
                                            where c.CountryId == item2.CountryId
                                            select c.CountryName).First().ToString();

                            sb.Append(@"<li>" + (item2.Description) + " of " + country_name + ".</li>");
                        }

                        getHolidayCalendarVM.ColorCode = item2.ColorCode;
                    }
                    sb.Append(@"</ul>");
                    getHolidayCalendarVM.StartDate = item.HolidayDate;
                    getHolidayCalendarVM.EndDate = new DateTime(item.HolidayDate.Year, item.HolidayDate.Month, item.HolidayDate.Day, 23, 59, 59);
                    getHolidayCalendarVM.HolidayType = item.HolidayType;
                    getHolidayCalendarVM.Description = sb.ToString();
                    getHolidayCalendarVMs.Add(getHolidayCalendarVM);
                }
                return new JsonResult { Data = getHolidayCalendarVMs, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public ViewResult IndexCalendar()
        {
            return View();
        }
    }
}