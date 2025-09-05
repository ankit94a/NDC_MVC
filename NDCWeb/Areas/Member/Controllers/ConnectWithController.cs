using NDCWeb.Areas.Member.View_Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using NDCWeb.Persistence;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using AutoMapper;
using NDCWeb.Infrastructure.Constants;
using System.Collections.Generic;
using System.Linq;
using NDCWeb.Infrastructure.Helpers.FileDirectory;
using System;
using System.Web;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.View_Models;
using NDCWeb.Infrastructure.Helpers.Account;
using System.Security.Claims;

namespace NDCWeb.Areas.Member.Controllers
{
    [Authorize(Roles = CustomRoles.Candidate)]
    [UserMenu(MenuArea = "Member")]
    [CSPLHeaders]
    public class ConnectWithController : Controller
    {
        // GET: Member/ConnectWith
        public ActionResult Participant()
        {
            string UserName = ((ClaimsIdentity)User.Identity).FindFirst("FName").Value;
            UserActivityHelper.SaveUserActivity("Connect with Participant List accessed by  " + UserName, Request.Url.ToString());
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var curcourse = uow.CourseRepo.Find(x => x.IsCurrent == true).FirstOrDefault();
                var personalDetail = uow.CrsMbrPersonalRepo.Find(x => x.CourseId == curcourse.CourseId);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CrsMemberPersonal, CrsMemberPersonalVM>();
                });
                ViewBag.Course = curcourse.CourseName;
                IMapper mapper = config.CreateMapper();
                var participants = mapper.Map<IEnumerable<CrsMemberPersonal>, List<CrsMemberPersonalVM>>(personalDetail);
                return View(participants);
            }
        }
        
        [HttpPost]
        public ActionResult Participant(string searchText)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var curcourse = uow.CourseRepo.Find(x => x.IsCurrent == true).FirstOrDefault();
                var personalDetail = uow.CrsMbrPersonalRepo.FindAsQuery(x => x.CourseId == curcourse.CourseId);
                //var memberqry = uow.CourseRepo.FindAsQuery(x => x.CourseId == curcourse.CourseId);
                
                personalDetail = personalDetail.Where(x => x.FirstName.Contains(searchText) || x.Surname.Contains(searchText) || x.MobileNo.Contains(searchText) || x.EmailId.Contains(searchText));
                var members = personalDetail.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CrsMemberPersonal, CrsMemberPersonalVM>();
                });
                IMapper mapper = config.CreateMapper();
                var participants = mapper.Map<IEnumerable<Models.CrsMemberPersonal>, List<CrsMemberPersonalVM>>(members);
                return View("participant", participants);
            }
        }
        public ActionResult Alumni()
        {
            string UserName = ((ClaimsIdentity)User.Identity).FindFirst("UserName").Value;
            UserActivityHelper.SaveUserActivity("Alumni List accessed by  " + UserName, Request.Url.ToString());
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                //var personalDetail = uow.AlumniRepo.Find(x => x.CreatedBy != null);
                var personalDetail = uow.AlumniRepo.GetAll();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AlumniMaster, AlumniIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var participants = mapper.Map<IEnumerable<Models.AlumniMaster>, List<AlumniIndxVM>>(personalDetail);
                return View(participants);
            }
        }
    }
}