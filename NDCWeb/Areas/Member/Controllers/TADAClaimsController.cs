using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
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
    public class TADAClaimsController : Controller
    {
        // GET: Member/TADAClaims
        public ActionResult Index()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var tadaClaims = uow.TADAClaimsRepo.Find(x => x.CreatedBy == uId);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<TADAClaims>, List<TADAClaimsIndexVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<TADAClaims>, IEnumerable<TADAClaimsIndexVM>>(tadaClaims).ToList();
                return View(indexDto);
            }
        }
        public ActionResult TADAClaimsEdit(int id)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var regMember = uow.CourseRegisterRepo.FirstOrDefault(x => x.UserId == uId);

                TADAClaimsUpVM objPersonal = new TADAClaimsUpVM();
                
                var tadaclaimsdata = uow.TADAClaimsRepo.GetById(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TADAClaims, TADAClaimsUpVM>();
                });
                ViewBag.FullName = regMember.Ranks.RankName + " " + regMember.FirstName + " " + regMember.MiddleName + " " + regMember.LastName;
                IMapper mapper = config.CreateMapper();
                TADAClaimsUpVM CreateDto = mapper.Map<TADAClaims, TADAClaimsUpVM>(tadaclaimsdata);
                return View(CreateDto);
            }
        }
        [HttpPost]
        public async Task<ActionResult> TADAClaimsEdit(TADAClaimsUpVM objTADAClaims)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TADAClaims, TADAClaimsUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                TADAClaims UpdateDto = mapper.Map<TADAClaimsUpVM, TADAClaims>(objTADAClaims);
                uow.TADAClaimsRepo.Update(UpdateDto);
                await uow.CommitAsync();
                return RedirectToAction("Index");
            }
        }
        public ActionResult TADAClaimsEntry()
        {
            TADAClaimsCrtVM objtada = new TADAClaimsCrtVM();
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var regMember = uow.CourseRegisterRepo.FirstOrDefault(x => x.UserId == uId);
                
                ViewBag.FullName = regMember.Ranks.RankName + " " + regMember.FirstName + " " + regMember.MiddleName + " " + regMember.LastName;
                ViewBag.MobileNo = regMember.MobileNo;
                //objtada.MobileNo = regMember.MobileNo;
                return View();
            }
        }
        [HttpPost]
        public async Task<ActionResult> TADAClaimsEntry(TADAClaimsCrtVM objTADaClaimsvm)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TADAClaimsCrtVM, TADAClaims>();
                });
                IMapper mapper = config.CreateMapper();
                TADAClaims CreateDto = mapper.Map<TADAClaimsCrtVM, TADAClaims>(objTADaClaimsvm);
                uow.TADAClaimsRepo.Add(CreateDto);
                await uow.CommitAsync();
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var DeleteItem = await uow.TADAClaimsRepo.GetByIdAsync(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.TADAClaimsRepo.Remove(DeleteItem);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}