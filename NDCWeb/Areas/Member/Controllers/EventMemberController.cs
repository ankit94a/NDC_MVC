using AutoMapper;
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

namespace NDCWeb.Areas.Member.Controllers
{
    [Authorize(Roles = CustomRoles.Candidate)]
    [UserMenu(MenuArea = "Member")]
    [CSPLHeaders]
    public class EventMemberController : Controller
    {
        // GET: Member/Event
        public ActionResult Eventboard()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var personalDetail = uow.EventRepo.GetAll();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Event, EventIndexVM>();
                });
                IMapper mapper = config.CreateMapper();
                var participants = mapper.Map<IEnumerable<Event>, List<EventIndexVM>>(personalDetail);
                return View(participants);
            }
        }
        public ActionResult Enrol(string attendType, int eventId)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.AttendTypeOpt = CustomDropDownList.GetEventAttendTypeOpt();
                ViewBag.AttendOpt = CustomDropDownList.GetEventAttendOpt();
                ViewBag.DietPrefOpt = CustomDropDownList.GetEventDietPreferenceOpt();
                ViewBag.LiquorOpt = CustomDropDownList.GetEventLiquorOpt();
                EventMemberEnrolVM objEventMemberEnrolCvm = new EventMemberEnrolVM();
                objEventMemberEnrolCvm.EventId = eventId;
                objEventMemberEnrolCvm.AttendType = attendType;
                return View(objEventMemberEnrolCvm);
            }
        }

        // POST: MenuItemMaster/Create
        [HttpPost]
        public async Task<ActionResult> Enrol(EventMemberEnrolVM objEventMemberEnrolCvm)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<EventMemberEnrolVM, EventMember>();
                });
                IMapper mapper = config.CreateMapper();
                EventMember CreateDto = mapper.Map<EventMemberEnrolVM, EventMember>(objEventMemberEnrolCvm);
                uow.EventMemberRepo.Add(CreateDto);
                await uow.CommitAsync();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("Create");
            }
        }
    }
}
