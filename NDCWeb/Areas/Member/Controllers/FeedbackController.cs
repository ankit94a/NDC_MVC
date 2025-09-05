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
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NDCWeb.Areas.Member.Controllers
{
    [Authorize(Roles = CustomRoles.Candidate)]
    [UserMenu(MenuArea = "Member")]
    [CSPLHeaders]
    public class FeedbackController : Controller
    {
        #region Speech
        public ActionResult SpeechEventActiveList()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                
                //var speechEvents = uow.SpeechEventRepo.Find(x => x.Active == true, fk => fk.Speakers, fk2 => fk2.Speakers.Topics, fk4 => fk4.Speakers.Topics.Subjects).OrderByDescending(x => x.SpeechEventId).ToList();
                //var feedbackSpeaker = uow.FeedbackSpeakerRepo.Find(x => x.CreatedBy == uId, fk => fk.SpeechEvents);
                //var rmvSpeechEvent = feedbackSpeaker.Select(x => x.SpeechEvents);
                //foreach (var speechevent in rmvSpeechEvent)
                //{ speechEvents.Remove(speechevent); }

                //var config = new MapperConfiguration(cfg =>
                //{
                //    cfg.CreateMap<IEnumerable<SpeechEvent>, List<SpeechEventIndxVM>>();
                //});
                //IMapper mapper = config.CreateMapper();
                //var indexDto = mapper.Map<IEnumerable<SpeechEvent>, IEnumerable<SpeechEventIndxVM>>(speechEvents).ToList();
                //return View(indexDto);

                var speechEvents = uow.SpeechEventRepo.FindAsQuery(x => x.Active == true, fk => fk.Speakers, fk2 => fk2.Speakers.Topics, fk4 => fk4.Speakers.Topics.Subjects);
                var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                var speechEventsFltr = speechEvents.Where(x => DbFunctions.TruncateTime(now) >= DbFunctions.TruncateTime(x.FeedbackStartDate) && DbFunctions.TruncateTime(now) <= DbFunctions.TruncateTime(x.FeedbackEndDate)).ToList();
                
                var feedbackSpeaker = uow.FeedbackSpeakerRepo.Find(x => x.CreatedBy == uId, fk => fk.SpeechEvents);
                var rmvSpeechEvent = feedbackSpeaker.Select(x => x.SpeechEvents);
                speechEventsFltr.RemoveAll(x => rmvSpeechEvent.Any(y=>y.SpeakerId==x.SpeakerId));
                //check  for peopole not participated
                ViewBag.NoParticipation = "No";
                var feedback = uow.FeedbackSpeakerRepo.Find(x => x.CreatedBy == uId);
                if (feedback.Count() <= 0)
                {
                    ViewBag.NoParticipation = "Yes";
                }
                else
                {
                    ViewBag.NoParticipation = "No";
                }
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<SpeechEvent, SpeechEventIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<SpeechEvent>, List<SpeechEventIndxVM>>(speechEventsFltr).ToList();
                return View(indexDto);
            }
        }
        public ActionResult SpeechFeedbackList()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                //var memberships = uow.FeedbackSpeakerRepo.Find(x=>x.CreatedBy==uId, fk=>fk.Speakers, fk2=>fk2.Speakers.Topics, fk4=>fk4.Speakers.Topics.Subjects);
                var memberships = uow.FeedbackSpeakerRepo.Find(x => x.CreatedBy == uId, fk => fk.SpeechEvents.Speakers, fk2 => fk2.SpeechEvents.Speakers.Topics, fk4 => fk4.SpeechEvents.Speakers.Topics.Subjects).OrderByDescending(x=>x.SpeakerFeedbackId).ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FeedbackSpeaker, FeedbackSpeakerIndexVM>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<FeedbackSpeaker>, List<FeedbackSpeakerIndexVM>>(memberships).ToList();
                return View(indexDto);
            }
        }
        public ActionResult PastActiveSpeechFeedbackList()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {

                var speechEvents = uow.SpeechEventRepo.FindAsQuery(x => x.Active == true, fk => fk.Speakers, fk2 => fk2.Speakers.Topics, fk4 => fk4.Speakers.Topics.Subjects);
                var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddMonths(-2), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                var speechEventsFltr = speechEvents.Where(x => DbFunctions.TruncateTime(now) >= DbFunctions.TruncateTime(x.FeedbackStartDate) && DbFunctions.TruncateTime(now) <= DbFunctions.TruncateTime(x.FeedbackEndDate)).ToList();
               
                var feedbackSpeaker = uow.FeedbackSpeakerRepo.Find(x => x.CreatedBy == uId, fk => fk.SpeechEvents);
                var rmvSpeechEvent = feedbackSpeaker.Select(x => x.SpeechEvents);
                speechEventsFltr.RemoveAll(x => rmvSpeechEvent.Any(y => y.SpeakerId == x.SpeakerId));

                var excSpeechEventId = feedbackSpeaker.Where(x=>x.CreatedBy == uId).Select(x => x.SpeechEventId).ToArray();
                speechEventsFltr.Where(x => !excSpeechEventId.Contains(x.SpeechEventId));
                //check  for peopole not participated
                //var feedback = uow.FeedbackSpeakerRepo.Find(x => x.CreatedBy == uId);
                //if (feedback.Count() > 0)
                //{
                //    this.AddNotification("Feedback on this speech is already given by you ", NotificationType.WARNING);
                //    return RedirectToAction("SpeechFeedbackList");
                //}
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<SpeechEvent, SpeechEventIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<SpeechEvent>, List<SpeechEventIndxVM>>(speechEventsFltr).ToList();
                return View(indexDto);
            }
        }
        [EncryptedActionParameter]
        public ActionResult AddSpeakerFeedback(int speechId)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var speechDetail = uow.SpeechEventRepo.FirstOrDefault(x => x.SpeechEventId == speechId, fk => fk.Speakers, fk2 => fk2.Speakers.Topics, fk4 => fk4.Speakers.Topics.Subjects);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<SpeechEvent, SpeechEventDetailVM>();
                });
                IMapper mapper = config.CreateMapper();
                var speechDetailVM = mapper.Map<SpeechEvent, SpeechEventDetailVM>(speechDetail);
                if (speechDetailVM != null)
                {
                    ViewBag.Subject = uow.SubjectMasterRepository.GetSubjects();
                    FeedbackSpeakerCreateVM objFeedbackCvm = new FeedbackSpeakerCreateVM();
                    objFeedbackCvm.SpeechEventId = speechId;
                    ViewBag.SpeechEventDetail = speechDetailVM;
                    return View(objFeedbackCvm);
                }
                return RedirectToAction("SpeechEventActiveList");
            }
        }
        //POST:
        [HttpPost]
        public async Task<ActionResult> AddSpeakerFeedback(FeedbackSpeakerCreateVM objFeedbackCvm)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var feedback = uow.FeedbackSpeakerRepo.Find(x => x.CreatedBy == uId && x.SpeechEventId == objFeedbackCvm.SpeechEventId);
                if (feedback.Count() > 0)
                {
                    this.AddNotification("Feedback on this speech is already given by you ", NotificationType.WARNING);
                    return RedirectToAction("SpeechFeedbackList");
                }
                else
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<FeedbackSpeakerCreateVM, FeedbackSpeaker>();
                    });
                    IMapper mapper = config.CreateMapper();
                    FeedbackSpeaker CreateDto = mapper.Map<FeedbackSpeakerCreateVM, FeedbackSpeaker>(objFeedbackCvm);
                    uow.FeedbackSpeakerRepo.Add(CreateDto);
                    await uow.CommitAsync();
                    this.AddNotification("Record Saved", NotificationType.SUCCESS);
                    return RedirectToAction("SpeechFeedbackList");
                }
            }
        }
        #endregion

        #region Module
        // GET: Member/Feedback
        public ActionResult ModuleFeedback()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var memberships = uow.FeedbackModuleRepo.Find(x => x.CreatedBy == uId);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<FeedbackModule>, List<FeedbackModuleIndexVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<FeedbackModule>, IEnumerable<FeedbackModuleIndexVM>>(memberships).ToList();
                return View(indexDto);
            }
        }

        // GET: Member/Feedback/Details/5
        public ActionResult AddModuleFeedback()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Course = uow.SubjectMasterRepository.GetSubjects();
            }
            return View();
        }
        //POST:
        [HttpPost]
        public async Task<ActionResult> AddModuleFeedback(FeedbackModuleCreateVM objFeedbackCvm)
        {
            if (ModelState.IsValid)
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<FeedbackModuleCreateVM, FeedbackModule>();
                    });
                    IMapper mapper = config.CreateMapper();
                    FeedbackModule CreateDto = mapper.Map<FeedbackModuleCreateVM, FeedbackModule>(objFeedbackCvm);
                    uow.FeedbackModuleRepo.Add(CreateDto);
                    await uow.CommitAsync();
                    this.AddNotification("Record Saved", NotificationType.SUCCESS);
                    return RedirectToAction("ModuleFeedback");
                }
            }
            else
            {
                return RedirectToAction("ModuleFeedback");
            }

        }
        #endregion
    }
}
