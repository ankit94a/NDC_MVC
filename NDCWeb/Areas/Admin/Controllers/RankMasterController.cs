using AutoMapper;
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
    [Authorize(Roles = "Admin")]
    [CSPLHeaders]
    public class RankMasterController : Controller
    {
        // GET: Admin/RankMaster
        public async Task<ActionResult> Index()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var ranks = await uow.RankMasterRepo.GetAllAsync();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<RankMaster>, List<RankMasterIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<RankMaster>, IEnumerable<RankMasterIndxVM>>(ranks);
                return View(indexDto);
            }
        }

        // GET: Admin/RankMaster/Create
        public ActionResult Create()
        {
            ViewBag.Service = CustomDropDownList.GetRankService();
            return View();
        }

        // POST: Admin/RankMaster/Create
        [HttpPost]
        public async Task<ActionResult> Create(RankMasterCrtVM objRankMstrCvm)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<RankMasterCrtVM, RankMaster>();
                });
                IMapper mapper = config.CreateMapper();
                RankMaster CreateDto = mapper.Map<RankMasterCrtVM, RankMaster>(objRankMstrCvm);
                uow.RankMasterRepo.Add(CreateDto);
                await uow.CommitAsync();
                return RedirectToAction("Create");
            }
        }

        // GET: Admin/RankMaster/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Service = CustomDropDownList.GetRankService();
                var ranks = await uow.RankMasterRepo.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<RankMaster>, List<RankMasterUpVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map < RankMaster, RankMasterUpVM>(ranks);
                return View(indexDto);
            }
        }

        // POST: Admin/RankMaster/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(RankMasterUpVM objNewsArtclUvm)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<RankMasterUpVM, RankMaster>();
                });
                IMapper mapper = config.CreateMapper();
                RankMaster UpdateDto = mapper.Map<RankMasterUpVM, RankMaster>(objNewsArtclUvm);
                uow.RankMasterRepo.Update(UpdateDto);
                await uow.CommitAsync();
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/RankMaster/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/RankMaster/Delete/5
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
