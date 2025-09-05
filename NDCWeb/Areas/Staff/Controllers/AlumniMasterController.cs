using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Alumni.View_Models;
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

namespace NDCWeb.Areas.Staff.Controllers
{
    [CSPLHeaders]
    [Authorize(Roles = CustomRoles.Staff)]
    [StaffStaticUserMenu]
    public class AlumniMasterController : Controller
    {
        // GET: Staff/AlumniMaster
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> AlumniFeedback()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var alumniFeedbacks = await uow.AlumniFeedbackRepo.AlumniFeedbackGetAll();
                return View(alumniFeedbacks);
            }
        }
        public ActionResult AlumniArticle()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var alumniArticles = uow.AlumniArticleRepo.GetAll();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<AlumniArticle>, List<AlumniArticleIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<AlumniArticle>, IEnumerable<AlumniArticleIndxVM>>(alumniArticles).ToList();
                return View(indexDto);
            }
        }
    }
}