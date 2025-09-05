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
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Staff.Controllers
{
    [CSPLHeaders]
    [Authorize(Roles = CustomRoles.Staff)]
    [StaffStaticUserMenu]
    //[UserMenu(MenuArea = "Staff")]
    public class UniversityDivController : Controller
    {
        // GET: Staff/UniversityDiv
        #region MPhil Forms
        public ActionResult MPhil()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var MphilDegree = uow.MPhilMemberRepo.GetAll();
                var config = new MapperConfiguration(cfg => cfg.CreateMap<MPhilMember, MPhilMemberIndxVM>());
                IMapper mapper = config.CreateMapper();
                var IndexDto = mapper.Map<IEnumerable<MPhilMember>, List<MPhilMemberIndxVM>>(MphilDegree);
                return View(IndexDto);
            }
        }
        public ActionResult PGPartial(int ID)
        {
            
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var MphilDegree = uow.MPhilPostGraduateRepo.Find(x => x.MPhilId == ID);
                var config = new MapperConfiguration(cfg => cfg.CreateMap<MPhilPostGraduate, MPhilPostGraduate>());
                IMapper mapper = config.CreateMapper();
                var IndexDto = mapper.Map<IEnumerable<MPhilPostGraduate>, List<MPhilPostGraduate>>(MphilDegree);
                return PartialView("_MPhilPostGraduate", IndexDto);
            }
        }
        public ActionResult MPhilDetail(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
               // var mphilEnrol = uow.MPhilMemberRepo.FirstOrDefault(x => x.MPhilId == id);
                var mphilEnrol = uow.MPhilMemberRepo.FirstOrDefault(x => x.MPhilId == id, fk => fk.iMPhilPostGraduates);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MPhilMember, MPhilMemberEnrolACKVM>();
                });
                IMapper mapper = config.CreateMapper();
                MPhilMemberEnrolACKVM mphilEnrolAck = mapper.Map<MPhilMember, MPhilMemberEnrolACKVM>(mphilEnrol);
                return View(mphilEnrolAck);
            }
        }
        #endregion MPHil Forms
        #region MPHil Degree
        public ActionResult MUMPhil()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var regMember = uow.MPhilDegreeRepo.GetAll();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MPhilDegree, MphilDegreeIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<MPhilDegree>, List<MphilDegreeIndxVM>>(regMember);
                return View(indexDto);
            }
        }
        public ActionResult MUMPhilDetail(int id)
        {
            string uId = User.Identity.GetUserId();

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var regMember = uow.MPhilDegreeRepo.FirstOrDefault(x => x.MPhilDegreeId == id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MPhilDegree, MphilDegreeIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                MphilDegreeIndxVM mphilEnrolAck = mapper.Map<MPhilDegree, MphilDegreeIndxVM>(regMember);
                return View(mphilEnrolAck);
            }
        }
        #endregion MPhil Degree
        #region Delete
        [HttpPost]
        public async Task<JsonResult> DeleteMPhilProgrammesOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var DeleteItem = await uow.MPhilMemberRepo.GetByIdAsync(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {

                    uow.MPhilMemberRepo.Remove(DeleteItem);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteMPhilDegreeOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var DeleteItem = await uow.MPhilDegreeRepo.GetByIdAsync(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.MPhilDegreeRepo.Remove(DeleteItem);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
        #endregion
    }
}