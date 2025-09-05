using AutoMapper;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Admin.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Models;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = CustomRoles.Admin)]
    [CSPLHeaders]
    public class FacultyController : Controller
    {
        // GET: Admin/Faculty
        public async Task<ActionResult> Index()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var faculties = await uow.FacultyRepo.GetAllAsync();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Faculty, FacultyIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<Faculty>, List<FacultyIndxVM>>(faculties);
                return View(indexDto);
            }
        }

        // GET: Admin/Faculty/Create
        public ActionResult Create()
        {
            ViewBag.FacultyType = CustomDropDownList.GetFacultyType();
            ViewBag.StaffType = CustomDropDownList.GetStaffType();
            return View();
        }

        // POST: Admin/Faculty/Create
        [HttpPost]
        public async Task<ActionResult> Create(FacultyCrtVM objFacultyCvm)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FacultyCrtVM, Faculty>();
                });
                IMapper mapper = config.CreateMapper();
                Faculty CreateDto = mapper.Map<FacultyCrtVM, Faculty>(objFacultyCvm);
                uow.FacultyRepo.Add(CreateDto);
                await uow.CommitAsync();
                return RedirectToAction("Create");
            }
        }

        // GET: Admin/Faculty/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.FacultyType = CustomDropDownList.GetFacultyType();
                ViewBag.StaffType = CustomDropDownList.GetStaffType();

                var faculties = await uow.FacultyRepo.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Faculty, FacultyUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                FacultyUpVM UpdateDto = mapper.Map<Faculty, FacultyUpVM>(faculties);
                return View(UpdateDto);
            }
        }

        // POST: Admin/Faculty/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(FacultyUpVM objFacultyUpVM)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FacultyUpVM, Faculty>();
                });
                IMapper mapper = config.CreateMapper();
                Faculty UpdateDto = mapper.Map<FacultyUpVM, Faculty>(objFacultyUpVM);
                uow.FacultyRepo.Update(UpdateDto);
                await uow.CommitAsync();
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var DeleteItem = await uow.FacultyRepo.GetByIdAsync(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.FacultyRepo.Remove(DeleteItem);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}
