using AutoMapper;
using NDCWeb.Areas.Admin.View_Models;
using NDCWeb.Data_Contexts;
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
    [Authorize(Roles = "Admin")]
    [CSPLHeaders]
    public class AppointmentController : Controller
    {
        // GET: Admin/Appointment
        public async Task<ActionResult> Index()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var appointments = await uow.AppointmentDetailRepo.GetAllAsync();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<AppointmentDetail>, List<AppointmentDetailIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<AppointmentDetail>, IEnumerable<AppointmentDetailIndxVM>>(appointments).ToList();
                return View(indexDto);
            }
        }

        // GET: Admin/Appointment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Appointment/Create
        [HttpPost]
        public async Task<ActionResult> Create(AppointmentDetailCrtVM objAppointmentCvm)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AppointmentDetailCrtVM, AppointmentDetail>();
                });
                IMapper mapper = config.CreateMapper();
                AppointmentDetail CreateDto = mapper.Map<AppointmentDetailCrtVM, AppointmentDetail>(objAppointmentCvm);
                uow.AppointmentDetailRepo.Add(CreateDto);
                await uow.CommitAsync();
                return RedirectToAction("Create");
            }
        }

        // GET: Admin/Appointment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Appointment/Edit/5
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

        // GET: Admin/Appointment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Appointment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
