using AutoMapper;
using NDCWeb.Areas.Admin.Models;
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

namespace NDCWeb.Areas.Staff.Controllers
{
    [CSPLHeaders]
    [Authorize(Roles = CustomRoles.Staff)]
    [StaffStaticUserMenu]
    //[UserMenu(MenuArea = "Staff")]
    public class SpeechEventController : Controller
    {
        // GET: Staff/SpeechEvent
        public ActionResult Index()
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

        // GET: Staff/SpeechEvent/Create
        public ActionResult Create()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Subject = uow.SubjectMasterRepository.GetSubjects();
                return View();
            }
        }

        // POST: Staff/SpeechEvent/Create
        [HttpPost]
        public ActionResult Create(SpeechEventCrtVM objSpeechEventCVm)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<SpeechEventCrtVM, SpeechEvent>();
                });
                IMapper mapper = config.CreateMapper();
                SpeechEvent CreateDto = mapper.Map<SpeechEventCrtVM, SpeechEvent>(objSpeechEventCVm);
                uow.SpeechEventRepo.Add(CreateDto);
                uow.Commit();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }

        // GET: Staff/SpeechEvent/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Staff/SpeechEvent/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var dataModal = await uow.SpeechEventRepo.GetByIdAsync(id);
                if (dataModal == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.SpeechEventRepo.Remove(dataModal);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}
