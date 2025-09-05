using AutoMapper;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Staff.View_Models;
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

namespace NDCWeb.Areas.Member.Controllers
{
    [Authorize(Roles = CustomRoles.Candidate)]
    [UserMenu(MenuArea = "Member")]
    [CSPLHeaders]
    public class ModuleActivityController : Controller
    {
        // GET: Member/ModuleActivity
        public ActionResult Index()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var trainingActivities = uow.TrainingActivityRepo.GetAll();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TrainingActivity, TrainingActivityIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<TrainingActivity>, IEnumerable<TrainingActivityIndxVM>>(trainingActivities).ToList().OrderByDescending(x => x.TrainingActivityId);
                return View(indexDto);
            }
        }
        
        public async Task<ActionResult> ShowMediaFiles(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var trainingActivities = await uow.TrainingActivityRepo.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TrainingActivity, TrainingActivityIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var showMediaDto = mapper.Map<TrainingActivity, TrainingActivityIndxVM>(trainingActivities);
                
                return PartialView("_ShowDocuments", showMediaDto);
            }
        }
        //public ActionResult ShowActivity(int id)
        //{
        //    using (var uow = new UnitOfWork(new NDCWebContext()))
        //    {
        //        var trainingActivities = uow.TrainingActivityRepo.GetById(id);
        //        if (trainingActivities == null)
        //            return HttpNotFound();
        //        else
        //        {
        //            var config = new MapperConfiguration(cfg =>
        //            {
        //                cfg.CreateMap<TrainingActivity, TrainingActivityIndxVM>();
        //            });
        //            IMapper mapper = config.CreateMapper();
        //            var indexDto = mapper.Map<TrainingActivity, TrainingActivityIndxVM>(trainingActivities);
        //            //ViewBag.Description = trainingActivity.Description;
        //            //ViewBag.TrainingActivityMedia = uow.TrainingActivityMediaRepo.Find(x=>x.TrainingActivityId==id);

        //            return View();
        //        }
        //    }
        //}
        public ActionResult ModuleCategory(string module)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var trainingActivities = uow.TrainingActivityRepo.Find(x=>x.Module.ToLower() == module.ToLower());
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TrainingActivity, TrainingActivityIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<TrainingActivity>, IEnumerable<TrainingActivityIndxVM>>(trainingActivities).ToList().OrderByDescending(x => x.TrainingActivityId);
                Session["module"] = module;
                return View(indexDto);
            }
        }
    }
}