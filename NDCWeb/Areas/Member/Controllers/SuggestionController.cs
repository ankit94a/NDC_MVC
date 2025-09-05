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

namespace NDCWeb.Areas.Member.Controllers
{
    [Authorize(Roles = CustomRoles.Candidate)]
    [UserMenu(MenuArea = "Member")]
    [CSPLHeaders]
    public class SuggestionController : Controller
    {
        // GET: Member/Leave
        public ActionResult Index()
        {
            int sno = 1;
            string uId = User.Identity.GetUserId();
            using (NDCWebContext db = new NDCWebContext())
            {
                List<Suggestion> segestions = db.Suggestions.Where(x => x.CreatedBy == uId).OrderByDescending(x=>x.CreatedAt).ToList();

                var segestionList = (from hc in segestions
                                      select new SuggestionIndxVM()
                                      {
                                          Sno = sno++,
                                          CreatedAt=hc.CreatedAt,
                                          SuggestionType = hc.SuggestionType,
                                          Description = hc.Description,
                                          Reply=hc.Reply,
                                          Status=hc.Status,
                                          LastUpdatedAt = hc.LastUpdatedAt,
            }).ToList();
                return View(segestionList);
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            string uId = User.Identity.GetUserId();

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.SuggestionTypesOption = uow.SuggestionRepository.GetSuggestionType();
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(SuggestionCrtVM objEv)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.SegestionTypesOption = uow.SuggestionRepository.GetSuggestionType();

                if (ModelState.IsValid)
                {
                    Suggestion segestion = new Suggestion();
                    segestion.Description = objEv.Description;
                    segestion.SuggestionType = objEv.SuggestionType;

                    if (objEv.SuggestionType == "Library")
                        segestion.StaffId = 30;
                    else if (objEv.SuggestionType == "GSO (System)")
                        segestion.StaffId = 45;
                    else if (objEv.SuggestionType == "Training")
                        segestion.StaffId = 28;
                    else if (objEv.SuggestionType == "Admin")
                        segestion.StaffId = 22;
                    else if (objEv.SuggestionType == "Security")
                        segestion.StaffId = 42;
                    else if (objEv.SuggestionType == "Officer Mess")
                        segestion.StaffId = 40;
                    else if (objEv.SuggestionType == "University Division")
                        segestion.StaffId = 40;

                    uow.SuggestionRepository.Add(segestion);
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