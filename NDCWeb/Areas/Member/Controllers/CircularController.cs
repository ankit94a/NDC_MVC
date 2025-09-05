using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Infrastructure.Helpers.FileDirectory;
using NDCWeb.Models;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Member.Controllers
{
    [Authorize(Roles = CustomRoles.Candidate)]
    [UserMenu(MenuArea = "Member")]
    [CSPLHeaders]
    public class CircularController : Controller
    {
        // GET: Member/Circular
        public ActionResult Index()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var circulars = uow.CircularRepo.Find(x => x.Category == "Circular");
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<Circular>, List<CircularIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<Circular>, IEnumerable<CircularIndxVM>>(circulars).ToList();
                return View(indexDto);
            }
        }
        public ActionResult Orders()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                string uId = User.Identity.GetUserId();
                var circular = uow.CircularDetailRepo.GetOrderAsPerDesignation("Member", uId);

                //var circulars = uow.CircularRepo.Find(x=> x.Category == "Order");
                /*var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<Circular>, List<CircularAlertVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<Circular>, IEnumerable<CircularAlertVM>>(circular).ToList();*/
                return View(circular);
            }
        }
        [EncryptedActionParameter]
        public async Task<ActionResult> ShowMediaFiles(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var mediaGalry = await uow.CircularRepo.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Circular, CircularIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var showMediaDto = mapper.Map<Circular, CircularIndxVM>(mediaGalry);
                await uow.CommitAsync();
                //return View(indexDto);
                return PartialView("_ShowDocuments", showMediaDto);
            }
        }
        [EncryptedActionParameter]
        public async Task<ActionResult> OCFiles(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var mediaGalry = await uow.CircularRepo.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Circular, CircularIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var showMediaDto = mapper.Map<Circular, CircularIndxVM>(mediaGalry);
                await uow.CommitAsync();
                return View(showMediaDto);
                //return PartialView("_ShowDocuments", showMediaDto);
            }
        }

    }
}