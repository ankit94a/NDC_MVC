using AutoMapper;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Models;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Staff.Controllers
{
    [Authorize(Roles = CustomRoles.Staff)]
    [StaffStaticUserMenu]
    [CSPLHeaders]
    //[UserMenu(MenuArea = "Staff")]
    public class LibraryController : Controller
    {
        // GET: Staff/Library
        public ActionResult Index()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var memberships = uow.LibraryMembershipRepo.GetAll();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<LibraryMembership>, List<LibraryMembershipIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<LibraryMembership>, IEnumerable<LibraryMembershipIndxVM>>(memberships).ToList();
                return View(indexDto);
            }
        }
        public ActionResult Membership(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var memberships = uow.LibraryMembershipRepo.FirstOrDefault(x => x.LibraryMembershipId == id);
                if (memberships == null)
                {
                    return View("Create");
                }
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<LibraryMembership>, List<LibraryMembershipIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<LibraryMembership, LibraryMembershipIndxVM>(memberships);  //mapper.Map<IEnumerable<LibraryMembership>, IEnumerable<LibraryMembershipIndxVM>>(memberships).ToList();
                return View(indexDto);
            }
        }

    }
}