using AutoMapper;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Infrastructure.Helpers.Account;
using NDCWeb.Models;
using NDCWeb.Persistence;
using NDCWeb.View_Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace NDCWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [CSPLHeaders]
    [SessionTimeout]
    public class UserActivityController : Controller
    {
        // GET: Admin/UserActivity
        public async Task<ActionResult> Index()
        {
            UserActivityHelper.SaveUserActivity("Audit log accessed", Request.Url.ToString());
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var userActivity = await uow.UserActivityRepo.GetAllAsync();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<UserActivity>, List<UserActivityVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<UserActivity>, IEnumerable<UserActivityVM>>(userActivity).ToList();
                await uow.CommitAsync();
                return View(indexDto);
            }
        }
        public async Task<ActionResult> Feedback()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var feedBacks = await uow.SiteFeedbackRepo.GetAllAsync();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<SiteFeedback>, List<AlumniFeedbackIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<SiteFeedback>, IEnumerable<AlumniFeedbackIndxVM>>(feedBacks).ToList();
                return View(indexDto);
            }
        }
        [HttpPost]
        public async Task<JsonResult> DeleteFeedbackOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var dataModal = await uow.SiteFeedbackRepo.GetByIdAsync(id);
                if (dataModal == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.SiteFeedbackRepo.Remove(dataModal);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }

        // GET: Admin/UserActivity/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/UserActivity/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/UserActivity/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/UserActivity/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/UserActivity/Edit/5
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

        // GET: Admin/UserActivity/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/UserActivity/Delete/5
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
