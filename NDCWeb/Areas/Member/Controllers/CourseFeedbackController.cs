using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Member.View_Models;
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
    public class CourseFeedbackController : Controller
    {
        // GET: Member/CourseFeedback
        public ActionResult Index()
        {
            string uId = User.Identity.GetUserId();
            CourseFeedbackIndxVM objCompletePreview = new CourseFeedbackIndxVM();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var feedbackdetail = uow.CourseFeedbackRepo.GetAll();
                if (feedbackdetail == null)
                {
                    return RedirectToAction("Create");
                }
                else
                {
                    return RedirectToAction("Detail",new { id=uId });
                    //var memberdetail = uow.CrsMbrPersonalRepo.Find(x => x.CreatedBy == uId);
                    //var config = new MapperConfiguration(cfg =>
                    //{
                    //    cfg.CreateMap<CourseFeedback, CourseFeedbackIndxVM>();
                    //});
                    //IMapper mapper = config.CreateMapper();
                    //var courseFeedback = mapper.Map<IEnumerable<CourseFeedback>, List<CourseFeedbackIndxVM>>(feedbackdetail);
                    //return View(courseFeedback);
                }
            }
        }
        public ActionResult Detail(string id)
        {
            CourseFeedbackIndxVM objCompletePreview = new CourseFeedbackIndxVM();

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var membercourse = uow.CrsMbrPersonalRepo.Find(x => x.CreatedBy == id).FirstOrDefault();
                var feedbackdetail = uow.CourseFeedbackRepo.FirstOrDefault(x => x.CreatedBy == id);

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
        public ActionResult Create()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var feedbackdetail = uow.CourseFeedbackRepo.FirstOrDefault(x => x.CreatedBy == uId);
                if (feedbackdetail != null)
                {
                    this.AddNotification("Course end Feedback is already given", NotificationType.WARNING);
                    return RedirectToAction("Index");
                }
                return View();
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create(CourseFeedbackCrtVM objCourseFeedbackCvm)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CourseFeedbackCrtVM, CourseFeedback>();
                });
                IMapper mapper = config.CreateMapper();
                CourseFeedback CreateDto = mapper.Map<CourseFeedbackCrtVM, CourseFeedback>(objCourseFeedbackCvm);
                uow.CourseFeedbackRepo.Add(CreateDto);
                await uow.CommitAsync();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("Create");
            }
        }
        public ActionResult Edit(int id)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var courseFeedbk = uow.CourseFeedbackRepo.GetById(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CourseFeedback, CourseFeedbackUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                CourseFeedbackUpVM CreateDto = mapper.Map<CourseFeedbackUpVM>(courseFeedbk);
                CreateDto.FeedbackId = id;
                return View(CreateDto);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Edit(CourseFeedbackUpVM objEvent, string submitType)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CourseFeedbackUpVM, CourseFeedback>();
                });
                IMapper mapper = config.CreateMapper();

                if (submitType == "Save")
                    objEvent.IsSubmit = false;
                else if (submitType == "Submit")
                    objEvent.IsSubmit = true;

                CourseFeedback UpdateDto = mapper.Map<CourseFeedbackUpVM, CourseFeedback>(objEvent);            
                uow.CourseFeedbackRepo.Update(UpdateDto);
                await uow.CommitAsync();
                if (submitType == "Save")
                    this.AddNotification("Feedback Saved Sucessfully", NotificationType.SUCCESS);
                else if (submitType == "Submit")
                    this.AddNotification("Feedback Submitted Sucessfully", NotificationType.SUCCESS);

                return RedirectToAction("Index");
            }
        }

    }
}