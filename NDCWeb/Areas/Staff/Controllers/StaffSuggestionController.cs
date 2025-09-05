using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Data_Contexts;
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
using System.Web.Security;

namespace NDCWeb.Areas.Staff.Controllers
{
    [Authorize(Roles = CustomRoles.Staff)]
    [CSPLHeaders]
    [StaffStaticUserMenu]
    public class StaffSuggestionController : Controller
    {
        public ActionResult Index()
        {
            int sno = 1;
            string uId = User.Identity.GetUserId();
            using (NDCWebContext db = new NDCWebContext())
            {
                StaffMaster staffMaster = db.StaffMasters.FirstOrDefault(x => x.LoginUserId == uId);
                if (staffMaster != null)
                {
                    int[] StaffId = { 30, 45, 28, 22, 42,40,46 };
                    if (StaffId.Contains(staffMaster.StaffId))
                    {
                        List<Suggestion> suggestion = db.Suggestions.Where(x => x.StaffId == staffMaster.StaffId).OrderByDescending(x => x.CreatedAt).ToList();
                        List<LockerAllotment> lockerAllotments = db.LockerAllotments.ToList();
                        List<CrsMemberPersonal> crsMemberPersonals = db.CrsMemberPersonals.ToList();

                        var suggestionList = (from hc in suggestion
                                             select new StaffSuggestionIndxVM()
                                             {
                                                 Sno = sno++,
                                                 SuggestionId=hc.SuggestionId,
                                                 CreatedAt = hc.CreatedAt,
                                                 LastUpdatedAt = hc.LastUpdatedAt,
                                                 SuggestionType = hc.SuggestionType,
                                                 Description = hc.Description,
                                                 Reply = hc.Reply,
                                                 Status = hc.Status,
                                                 LockerNo = (from s in suggestion
                                                             join c in crsMemberPersonals on s.CreatedBy equals c.CreatedBy
                                                             join l in lockerAllotments on c.CourseMemberId equals l.CourseMemberId
                                                             select l.LockerNo).First().ToString()
                                             }).ToList();
                        return View(suggestionList);
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
        public ActionResult IndexAllType()
        {
            int sno = 1;
            string uId = User.Identity.GetUserId();
            using (NDCWebContext db = new NDCWebContext())
            {
                StaffMaster staffMaster = db.StaffMasters.FirstOrDefault(x => x.LoginUserId == uId);
                if (staffMaster != null)
                {
                    int[] StaffId = { 26, 27, 32 };
                    if (StaffId.Contains(staffMaster.StaffId))
                    {
                        List<Suggestion> suggestion = db.Suggestions.OrderBy(x => x.SuggestionType).ToList();

                        List<LockerAllotment> lockerAllotments = db.LockerAllotments.ToList();
                        List<CrsMemberPersonal> crsMemberPersonals = db.CrsMemberPersonals.ToList();

                        var suggestionList = (from hc in suggestion
                                              select new StaffSuggestionIndxVM()
                                              {
                                                  Sno = sno++,
                                                  SuggestionId = hc.SuggestionId,
                                                  CreatedAt = hc.CreatedAt,
                                                  LastUpdatedAt = hc.LastUpdatedAt,
                                                  SuggestionType = hc.SuggestionType,
                                                  Description = hc.Description,
                                                  Reply = hc.Reply,
                                                  Status = hc.Status,
                                                  LockerNo = (from s in suggestion
                                                              join c in crsMemberPersonals on s.CreatedBy equals c.CreatedBy
                                                              join l in lockerAllotments on c.CourseMemberId equals l.CourseMemberId
                                                              select l.LockerNo).First().ToString()
                                              }).ToList();
                        return View(suggestionList);
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
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            string uId = User.Identity.GetUserId();
            using (NDCWebContext db = new NDCWebContext())
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    ViewBag.StatusTypesOption = uow.SuggestionRepository.GetStatusType();

                    Suggestion suggestion = uow.SuggestionRepository.Find(x => x.SuggestionId == Id).SingleOrDefault();
                    if (suggestion != null)
                    {
                        StaffMaster staffMaster = db.StaffMasters.FirstOrDefault(x => x.LoginUserId == uId);
                        if(staffMaster!=null)
                        {
                            if(staffMaster.StaffId == suggestion.StaffId)
                            {
                                StaffSuggestionUpdVM staffSuggestionUpdVM = new StaffSuggestionUpdVM();
                                staffSuggestionUpdVM.SuggestionId = suggestion.SuggestionId;
                                staffSuggestionUpdVM.Reply = suggestion.Reply;
                                staffSuggestionUpdVM.Status = suggestion.Status;
                                return View(staffSuggestionUpdVM);
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
                    else
                    {
                        this.AddNotification("Not Valid Id.", NotificationType.ERROR);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
        }
        [HttpPost]
        public async Task<ActionResult> Edit(StaffSuggestionUpdVM objEv)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.StatusTypesOption = uow.SuggestionRepository.GetStatusType();

                if (ModelState.IsValid)
                {
                    Suggestion suggestion = uow.SuggestionRepository.Find(x=>x.SuggestionId == objEv.SuggestionId).SingleOrDefault();
                    suggestion.Reply = objEv.Reply;
                    suggestion.Status = objEv.Status;

                    uow.SuggestionRepository.Update(suggestion);
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
    }
}