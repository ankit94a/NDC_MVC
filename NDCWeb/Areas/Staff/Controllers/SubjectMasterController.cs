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
    public class SubjectMasterController : Controller
    {
        // GET: Staff/Subject
        public ActionResult Index()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var subject = uow.SubjectMasterRepository.GetAll();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<SubjectMaster, SubjectMasterIndexVM>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<SubjectMaster>, List<SubjectMasterIndexVM>>(subject).ToList();
                return View(indexDto);
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(SubjectMasterCrtVM objSub)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var subject = uow.SubjectMasterRepository.Find(x => x.SubjectName == objSub.SubjectName);
                if (subject.Count() > 0)
                {
                    this.AddNotification("Record Already Created..! Please change Subject Name", NotificationType.WARNING);
                    return RedirectToAction("Index");
                }
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<SubjectMasterCrtVM, SubjectMaster>();
                });
                IMapper mapper = config.CreateMapper();
                SubjectMaster CreateDto = mapper.Map<SubjectMasterCrtVM, SubjectMaster>(objSub);
                uow.SubjectMasterRepository.Add(CreateDto);
                await uow.CommitAsync();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var subject = uow.SubjectMasterRepository.GetById(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<SubjectMaster, SubjectMasterUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<SubjectMaster, SubjectMasterUpVM>(subject);
                return View(indexDto);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Edit(SubjectMasterUpVM objSubjectUvm)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var subject = uow.SubjectMasterRepository.Find(x => x.SubjectName == objSubjectUvm.SubjectName && x.SubjectId != objSubjectUvm.SubjectId);
                if (subject.Count() > 0)
                {
                    this.AddNotification("Record Already Exists..! Please change Subject Name", NotificationType.WARNING);
                    return RedirectToAction("Index");
                }
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<SubjectMasterUpVM, SubjectMaster>();
                });
                IMapper mapper = config.CreateMapper();
                SubjectMaster UpdateDto = mapper.Map<SubjectMasterUpVM, SubjectMaster>(objSubjectUvm);
                uow.SubjectMasterRepository.Update(UpdateDto);
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
                var DeleteItem = await uow.SubjectMasterRepository.GetByIdAsync(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.SubjectMasterRepository.Remove(DeleteItem);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}