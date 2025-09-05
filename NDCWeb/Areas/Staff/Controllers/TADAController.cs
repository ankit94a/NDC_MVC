using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Filters;
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
    [Authorize(Roles = CustomRoles.CandidateStaff)]
    [CSPLHeaders]
    [StaffStaticUserMenu]
    public class TADAController : Controller
    {
        // GET: Staff/TADA
        public ActionResult Index()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var tadaClaims = uow.TADAClaimsRepo.GetAll();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<TADAClaims>, List<TADAClaimsIndexVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<TADAClaims>, IEnumerable<TADAClaimsIndexVM>>(tadaClaims).ToList();
                return View(indexDto);
            }
        }
    }
}