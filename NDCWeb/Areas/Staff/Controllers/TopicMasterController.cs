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
    //[UserMenu(MenuArea = "Staff")]
    [StaffStaticUserMenu]
    public class TopicMasterController : Controller
    {
        // GET: Staff/TopicMaster
        public async Task<ActionResult> Index()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var topics = await uow.TopicMasterRepository.GetAllAsync();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TopicMaster, TopicMasterIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<TopicMaster>, IEnumerable<TopicMasterIndxVM>>(topics);
                return View(indexDto);
            }
        }

        // GET: Admin/RankMaster/Create
        public ActionResult Create()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Course = uow.SubjectMasterRepository.GetSubjects();
                return View();
            }
        }

        // POST: Admin/RankMaster/Create
        [HttpPost]
        public async Task<ActionResult> Create(TopicMasterCrtVM objTopicCvm)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var topics = uow.TopicMasterRepository.FirstOrDefault(x => x.SubjectId == objTopicCvm.SubjectId && x.TopicName == objTopicCvm.TopicName);
                if (topics != null)
                {
                    this.AddNotification("Record Already Created..! Please change Subject and Topic Name", NotificationType.WARNING);
                    return RedirectToAction("Index");
                }
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TopicMasterCrtVM, TopicMaster>();
                });
                IMapper mapper = config.CreateMapper();
                TopicMaster CreateDto = mapper.Map<TopicMasterCrtVM, TopicMaster>(objTopicCvm);
                uow.TopicMasterRepository.Add(CreateDto);
                await uow.CommitAsync();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/RankMaster/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Course = uow.SubjectMasterRepository.GetSubjects();

                var topics = await uow.TopicMasterRepository.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TopicMaster, TopicMasterUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<TopicMaster, TopicMasterUpVM>(topics);
                return View(indexDto);
            }
        }

        // POST: Admin/RankMaster/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(TopicMasterUpVM objTopicMstrUvm)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var topics = uow.TopicMasterRepository.FirstOrDefault(x => x.SubjectId == objTopicMstrUvm.SubjectId && x.TopicName == objTopicMstrUvm.TopicName && x.TopicId != objTopicMstrUvm.TopicId);
                if (topics != null)
                {
                    this.AddNotification("Record Already Exists..! Please change Module and Activity", NotificationType.WARNING);
                    return RedirectToAction("Index");
                }
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TopicMasterUpVM, TopicMaster>();
                });
                IMapper mapper = config.CreateMapper();
                TopicMaster UpdateDto = mapper.Map<TopicMasterUpVM, TopicMaster>(objTopicMstrUvm);
                uow.TopicMasterRepository.Update(UpdateDto);
                await uow.CommitAsync();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var dataModal = await uow.TopicMasterRepository.GetByIdAsync(id);
                if (dataModal == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.TopicMasterRepository.Remove(dataModal);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}