using NDCWeb.Data_Contexts;
using NDCWeb.Persistence;
using NDCWeb.Areas.Admin.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using NDCWeb.Models;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Filters;

namespace NDCWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [CSPLHeaders]
    public class MenuUrlMasterController : Controller
    {
        // GET: MenuUrlMaster
        public async Task<ActionResult> Index()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var menuUrlMstr = await uow.MenuUrlMstrRepo.GetAllAsync();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<MenuUrlMaster>, List<MenuUrlMasterIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<MenuUrlMaster>, IEnumerable<MenuUrlMasterIndxVM>>(menuUrlMstr).ToList();
                return View(indexDto);
            }
        }

        // GET: MenuUrlMaster/Create
        public async Task<ActionResult> Create()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.UrlArea = CustomDropDownList.GetArea();
                await uow.CommitAsync();
                return View();
            }
        }

        // POST: MenuUrlMaster/Create
        [HttpPost]
        public async Task<ActionResult> Create(MenuUrlMasterCrtVM objMenuUrlMstrCvm)
        {
            using(var uow=new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MenuUrlMasterCrtVM, MenuUrlMaster>();
                });
                IMapper mapper = config.CreateMapper();
                MenuUrlMaster CreateDto = mapper.Map<MenuUrlMasterCrtVM, MenuUrlMaster>(objMenuUrlMstrCvm);
                uow.MenuUrlMstrRepo.Add(CreateDto);
                await uow.CommitAsync();
                return RedirectToAction("Create");
            }
        }

        // GET: MenuUrlMaster/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MenuUrlMaster/Edit/5
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

        // GET: MenuUrlMaster/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MenuUrlMaster/Delete/5
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
