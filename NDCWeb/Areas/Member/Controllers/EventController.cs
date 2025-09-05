using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Infrastructure.Helpers.FileDirectory;
using NDCWeb.Models;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Member.Controllers
{
    [Authorize(Roles = CustomRoles.Candidate)]
    [UserMenu(MenuArea = "Member")]
    [CSPLHeaders]
    public class EventController : Controller
    {
        // GET: Member/Event
        [Authorize(Roles = CustomRoles.Candidate)]
        [UserMenu(MenuArea = "Member")]
        public ActionResult Index()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var memberships = uow.EventParticipantRepository.Find(m=>m.CreatedBy == uId);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<EventParticipant>, List<EventParticipantIndexVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<EventParticipant>, IEnumerable<EventParticipantIndexVM>>(memberships).ToList();
                return View(indexDto);
            }
        }
        [UserMenu(MenuArea = "Member")]
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
        public ActionResult Participate(EventParticipantCreateVM objLeavevm, int id)
        {
           string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.GetCategory = CustomDropDownList.GetParticipationType();
                ViewBag.Course = uow.SubjectMasterRepository.GetSubjects();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<EventParticipant, EventParticipantCreateVM>();
                });
                IMapper mapper = config.CreateMapper();
                //var participants = mapper.Map<IEnumerable<EventParticipant>, List<EventParticipantIndexVM>>(partdetail);
                EventParticipantCreateVM CreateDto = mapper.Map<EventParticipantCreateVM>(objLeavevm);
                //EventParticipantIndexVM IndexDto = mapper.Map<EventParticipant, EventParticipantIndexVM>(partdetail);
                CreateDto.EventId = id;
                return View(CreateDto);
            }
        }

        // POST: Member/Event/Create
        [HttpPost]
        public async Task<ActionResult> Participate(EventParticipantCreateVM objLeavevm)
        {
            //string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<EventParticipantCreateVM, EventParticipant>();
                });
                IMapper mapper = config.CreateMapper();
                EventParticipant CreateDto = mapper.Map<EventParticipantCreateVM, EventParticipant>(objLeavevm);
                uow.EventParticipantRepository.Add(CreateDto);
                await uow.CommitAsync();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }

        // GET: Member/Event/Edit/5
        public ActionResult Edit(int id)
        {
            string uId = User.Identity.GetUserId(); 
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.GetCategory = CustomDropDownList.GetParticipationType();
                ViewBag.Course = uow.SubjectMasterRepository.GetSubjects();

                var eventdata = uow.EventParticipantRepository.GetById(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<EventParticipant, EventParticipantUpdateVM>();
                });
                IMapper mapper = config.CreateMapper();
                EventParticipantUpdateVM CreateDto = mapper.Map<EventParticipant, EventParticipantUpdateVM>(eventdata);

                return View(CreateDto);
            }
        }

        // POST: Member/Event/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(EventParticipantUpdateVM objEvent)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<EventParticipantUpdateVM, EventParticipant>();
                });
                IMapper mapper = config.CreateMapper();
                EventParticipant UpdateDto = mapper.Map<EventParticipantUpdateVM, EventParticipant>(objEvent);
                uow.EventParticipantRepository.Update(UpdateDto);
                await uow.CommitAsync();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }

        // GET: Member/Event/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Member/Event/Delete/5
        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var DeleteItem = await uow.EventParticipantRepository.GetByIdAsync(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Record Could Not be Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.EventParticipantRepository.Remove(DeleteItem);
                    await uow.CommitAsync();
                    return Json(data: "Record Success Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}
