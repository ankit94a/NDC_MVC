using NDCWeb.Areas.Member.View_Models;
using System.Web.Mvc;
using NDCWeb.Persistence;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.View_Models;


namespace NDCWeb.Areas.Alumni.Controllers
{
    [Authorize(Roles = CustomRoles.Alumni)]
    [UserMenu(MenuArea = "Alumni")]
    [CSPLHeaders]
    public class AlumniConnectWithController : Controller
    {
        // GET: Alumni/AlumniConnectWith
        public ActionResult CurrentCourse()
        {
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
        public ActionResult MyCourseMates()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var alumni = uow.AlumniRepo.FirstOrDefault(x=>x.UserId == uId);
                if (alumni.InStepCourseId ==null)
                {
                    var alumnis = uow.AlumniRepo.Find(x => x.CourseSerNo == alumni.CourseSerNo);
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<AlumniMaster, AlumniIndxVM>();
                    });
                    IMapper mapper = config.CreateMapper();
                    var participants = mapper.Map<IEnumerable<Models.AlumniMaster>, List<AlumniIndxVM>>(alumnis);
                    return View(participants);
                }
                else
                {
                    var alumnis = uow.AlumniRepo.Find(x => x.InStepCourseId == alumni.InStepCourseId && x.Email != alumni.Email);
                    ViewBag.instepcourse = uow.InStepCourseRepo.FirstOrDefault(x => x.CourseId == alumni.InStepCourseId).CourseName.ToString();
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<AlumniMaster, AlumniIndxVM>();
                    });
                    IMapper mapper = config.CreateMapper();
                    var participants = mapper.Map<IEnumerable<Models.AlumniMaster>, List<AlumniIndxVM>>(alumnis);
                    return View(participants);
                }
            }
        }
        [HttpPost]
        public ActionResult MyCourseMates(string searchText)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var alumni = uow.AlumniRepo.FirstOrDefault(x => x.UserId == uId);
                string serviceNo = alumni.CourseSerNo;
                if (alumni.InStepCourseId == null)
                {
                    var alumnisQry = uow.AlumniRepo.FindAsQuery(x => x.CourseSerNo == serviceNo);
                    alumnisQry = alumnisQry.Where(x => x.FirstName.Contains(searchText) || x.Surname.Contains(searchText) || x.MobileNo.Contains(searchText) || x.Email.Contains(searchText));

                    var alumnis = alumnisQry.ToList();
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<AlumniMaster, AlumniIndxVM>();
                    });
                    IMapper mapper = config.CreateMapper();
                    var participants = mapper.Map<IEnumerable<Models.AlumniMaster>, List<AlumniIndxVM>>(alumnis);
                    return View("MyCourseMates", participants);
                }
                else
                {
                    var alumnisQry = uow.AlumniRepo.FindAsQuery(x => x.InStepCourseId == alumni.InStepCourseId);
                    ViewBag.instepcourse = uow.InStepCourseRepo.FirstOrDefault(x => x.CourseId == alumni.InStepCourseId).CourseName.ToString();
                    alumnisQry = alumnisQry.Where(x => x.FirstName.Contains(searchText) || x.Surname.Contains(searchText) || x.MobileNo.Contains(searchText) || x.Email.Contains(searchText));

                    var alumnis = alumnisQry.ToList();
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<AlumniMaster, AlumniIndxVM>();
                    });
                    IMapper mapper = config.CreateMapper();
                    var participants = mapper.Map<IEnumerable<Models.AlumniMaster>, List<AlumniIndxVM>>(alumnis);
                    return View("MyCourseMates", participants);
                }
            }
        }
        public ActionResult OtherAlumni()
        {

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