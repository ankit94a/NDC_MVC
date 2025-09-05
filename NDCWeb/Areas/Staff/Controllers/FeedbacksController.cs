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
using NDCWeb.View_Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NDCWeb.Areas.Staff.Controllers
{
    [CSPLHeaders]
    [Authorize(Roles = CustomRoles.Staff)]
    [StaffStaticUserMenu]
    //[UserMenu(MenuArea = "Staff")]
    [EncryptedActionParameter]
    public class FeedbacksController : Controller
    {
        // GET: Staff/Feedbacks
        public async Task<ActionResult> Index(string category)
        {
            category = category ?? "All";
            ViewBag.Category = category;
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {

                var speechEvents = await uow.SpeechEventRepo.GetSpeakerFeedbackForStaff(category);
                return View(speechEvents);

            }
          
        }
        public ActionResult SpeechEventList()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var speechEvents = uow.SpeechEventRepo.GetAll(fk => fk.Speakers, fk2 => fk2.Speakers.Topics, fk4 => fk4.Speakers.Topics.Subjects);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<SpeechEvent>, List<SpeechEventIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<SpeechEvent>, IEnumerable<SpeechEventIndxVM>>(speechEvents).ToList();
                return View(indexDto);
            }
        }
        public async Task<ActionResult> SpeechFeedbackList()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                var speechFeedbacksAll = await uow.FeedbackSpeakerRepo.GetSpeechFeedback(course.CourseId);
                return View(speechFeedbacksAll);
            }
        }
        public async Task<ActionResult> SpeechFeedbackNoting()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                var speechFeedbacksAll = await uow.FeedbackSpeakerRepo.GetSpeechFeedback(course.CourseId);
                return View(speechFeedbacksAll);
            }
        }
        public async Task<ActionResult> SpeechFeedbackSummary()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                var speechFeedbacksAll = await uow.FeedbackSpeakerRepo.GetSpeechFeedback(course.CourseId);
                return View(speechFeedbacksAll);
            }
        }
        public async Task<ActionResult> SpeechFeedbackSDS()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                var speechFeedbacksAll = await uow.FeedbackSpeakerRepo.GetSpeechFeedback(course.CourseId);
                return View(speechFeedbacksAll);
            }
        }
        public ActionResult Module(string category)
        {
            category = category ?? "All";
            ViewBag.Category = category;
            string uId = User.Identity.GetUserId();
            using (NDCWebContext db = new NDCWebContext())
            {
                List<FeedbackModule> fbm = db.FeedbackModules.ToList();
                List<CrsMemberPersonal> cmp = db.CrsMemberPersonals.ToList();
                List<Course> cm = db.Courses.ToList();
                List<LockerAllotment> la = db.LockerAllotments.ToList();
                List<SubjectMaster> sm = db.SubjectMasters.ToList();
                List<StaffMaster> smasts = db.StaffMasters.ToList();
                List<Faculty> fac = db.Faculties.ToList();
                bool isSDS = false;
                ViewBag.Choice = category;


                var chkSDS = (from sbStaff in smasts
                              join fbFac in fac on sbStaff.FacultyId equals fbFac.FacultyId
                              where fbFac.StaffType == "SDS" && sbStaff.LoginUserId == uId
                              select(sbStaff)).ToList();
                if (chkSDS.Count > 0)
                {
                    isSDS = true;
                }
                var modulefeedback = (from fbModule in fbm
                                      join cmPersonal in cmp on fbModule.CreatedBy equals cmPersonal.CreatedBy
                                      join cmCourse in cm on cmPersonal.CourseId equals cmCourse.CourseId
                                      join sMaster in sm on fbModule.SubjectId equals sMaster.SubjectId
                                      join lAllotment in la on cmPersonal.CourseMemberId equals lAllotment.CourseMemberId into table1
                                      from lAllotment in table1.ToList()
                                      where sMaster.Code == category.ToUpper() && cmCourse.IsCurrent == true
                                      orderby sMaster.SubjectName ascending
                                      select new StaffFeedbackModuleVM()
                                      {
                                          SubjectId = fbModule.SubjectId,
                                          CoordChairperson = fbModule.CoordChairperson,
                                          TopicForDelete = fbModule.TopicForDelete,
                                          TopicForAdition = fbModule.TopicForAdition,
                                          Suggestions = fbModule.Suggestions,
                                          SuggestChanges = fbModule.SuggestChanges,
                                          SuggestionOther = fbModule.SuggestionOther,
                                          CommentsAndRecomedation = fbModule.CommentsAndRecomedation,
                                          LockerNo = lAllotment.LockerNo,
                                          Subjects = sMaster,
                                          ModuleFeedbackId = fbModule.ModuleFeedbackId,
                                          IsSDS = isSDS,
                                      }).ToList();
                return View(modulefeedback);
            }
        }
        // This code added on dated 16.02.2023 by yogendra
        public ActionResult ModuleComment(int id,string cat)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var feedback = uow.FeedbackModuleRepo.GetById(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FeedbackModule, FeedbackModuleUpdateVM>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<FeedbackModule, FeedbackModuleUpdateVM>(feedback);
                indexDto.Choice = cat;
                return View(indexDto);
            }
        }
        // This code added on dated 16.02.2023 by yogendra
        [HttpPost]
        public async Task<ActionResult> ModuleComment(FeedbackModuleUpdateVM objFeedbackUvm)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                FeedbackModule feedback = uow.FeedbackModuleRepo.GetById(objFeedbackUvm.ModuleFeedbackId);
                feedback.CommentsAndRecomedation = objFeedbackUvm.CommentsAndRecomedation;
                uow.FeedbackModuleRepo.Update(feedback);
                await uow.CommitAsync();
                this.AddNotification("Record Edit", NotificationType.SUCCESS);
                return RedirectToAction("Module", new { category = objFeedbackUvm.Choice});
            }
        }
        public async Task<ActionResult> Course()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                var memberships = await uow.CourseFeedbackRepo.GetCourseEndFeedbackMemberList(course.CourseId);
                ViewBag.CourseNo = course.CourseName;
                return View(memberships);
            }
        }
        public ActionResult CourseDetails(int id)
        {
            CourseFeedbackIndxVM objCompletePreview = new CourseFeedbackIndxVM();

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var feedbackdetail = uow.CourseFeedbackRepo.FirstOrDefault(x => x.FeedbackId == id && x.IsSubmit == true);
                var membercourse = uow.CrsMbrPersonalRepo.Find(x => x.CreatedBy == feedbackdetail.CreatedBy).FirstOrDefault();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CourseFeedback, CourseFeedbackIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var courseFeedback = mapper.Map<CourseFeedback, CourseFeedbackIndxVM>(feedbackdetail);
                courseFeedback.FullName = membercourse.FirstName + " " + membercourse.MiddleName + "" + membercourse.Surname;
                courseFeedback.MobileNo = membercourse.MobileNo;
                courseFeedback.Email = membercourse.EmailId;

                return View(courseFeedback);
            }
        }
        public ActionResult AlumniFeedBackList()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var siteFeedback = uow.SiteFeedbackRepo.GetAll();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<SiteFeedback>, List<AlumniFeedbackIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<SiteFeedback>, IEnumerable<AlumniFeedbackIndxVM>>(siteFeedback).ToList();
                return View(indexDto);
            }
        }
        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var DeleteItem = await uow.SiteFeedbackRepo.GetByIdAsync(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.SiteFeedbackRepo.Remove(DeleteItem);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}