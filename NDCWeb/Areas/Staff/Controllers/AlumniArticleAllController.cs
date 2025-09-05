using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Alumni.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
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
    public class AlumniArticleAllController : Controller
    {
        // GET: Staff/AlumniArticleAll
        public async Task<ActionResult> Index(string category)
        {
            category = category ?? "Article";
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var alumniarticleAll = await uow.AlumniArticleRepo.GetAlumniUploadsForStaff(uId, category);
                return View(alumniarticleAll);
            }
        }
        public async Task<ActionResult> ShowMediaFiles(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var mediaGalry = await uow.AlumniArticleRepo.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AlumniArticle, AlumniArticleIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var showMediaDto = mapper.Map<AlumniArticle, AlumniArticleIndxVM>(mediaGalry);
                await uow.CommitAsync();
                //return View(indexDto);
                return PartialView("_Showmedia", showMediaDto);
            }
        }
    }
}