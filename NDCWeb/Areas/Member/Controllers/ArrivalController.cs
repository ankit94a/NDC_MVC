using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Member.View_Models;
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

namespace NDCWeb.Areas.Member.Controllers
{
    [Authorize(Roles = CustomRoles.Candidate)]
    [UserMenu(MenuArea = "Member")]
    [CSPLHeaders]
    public class ArrivalController : Controller
    {
        // GET: Member/Arrival
        public ActionResult Index()
        {
            string uId = User.Identity.GetUserId();
            ArrivalDetailCompletePreviewVM objCompletePreview = new ArrivalDetailCompletePreviewVM();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var arrivaldetail = uow.ArrivalDetailRepo.FirstOrDefault(x => x.CreatedBy == uId, fk => fk.iArrivalMeals, fk2 => fk2.iArrivalAccompanied);
                if (arrivaldetail==null)
                {
                    ViewBag.ArrivalLoc = CustomDropDownList.GetArrivalAtLocation();
                    ViewBag.ArrivalMode = CustomDropDownList.GetArrivalMode();
                    ViewBag.DietPreference = CustomDropDownList.GetArrivalDietPreference();
                    ViewBag.DetachmentFoodCharges = CustomDropDownList.GetDetachmentFoodCharges();

                    var personal = uow.CourseRegisterRepo.FirstOrDefault(x => x.UserId == uId, fk => fk.Ranks);
                    //var appointment = uow.CrsMbrAppointmentRepo.FirstOrDefault(x => x.CreatedBy == uId, fk => fk.Ranks);
                    ViewBag.FullName = personal.Ranks.RankName + " " + personal.FirstName + " " + personal.MiddleName + " " + personal.LastName;
                    return View("Create");
                }
                else
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<ArrivalDetail, ArrivalDetailIndxVM>();
                    });
                    IMapper mapper = config.CreateMapper();
                    ArrivalDetailIndxVM IndexDto = mapper.Map<ArrivalDetail, ArrivalDetailIndxVM>(arrivaldetail);
                    var personal = uow.CourseRegisterRepo.FirstOrDefault(x => x.UserId == uId, fk => fk.Ranks);
                    IndexDto.FullName = personal.Ranks.RankName +" "+ personal.FirstName + " " + personal.MiddleName + " " + personal.LastName;
                    return View(IndexDto);
                }
            }
        }
        public ActionResult Create()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.ArrivalLoc = CustomDropDownList.GetArrivalAtLocation();
                ViewBag.ArrivalMode = CustomDropDownList.GetArrivalMode();
                ViewBag.DietPreference = CustomDropDownList.GetArrivalDietPreference();
                ViewBag.DetachmentFoodCharges = CustomDropDownList.GetDetachmentFoodCharges();
                
                var personal = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == uId);
                var appointment = uow.CrsMbrAppointmentRepo.FirstOrDefault(x => x.CreatedBy == uId, fk => fk.Ranks);
                ViewBag.FullName = appointment.Ranks.RankName + " " + personal.FirstName + " " + personal.MiddleName + " " + personal.Surname;
            }
            return View();
        }
        [HttpPost]
        public ActionResult ArrivalAck(ArrivalDetailVM modal)
        {
            var ArrivalM = modal;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ArrivalDetailVM, ArrivalDetailAck>();
            });
            IMapper mapper = config.CreateMapper();
            ArrivalDetailAck ArriveAck = mapper.Map<ArrivalDetailVM, ArrivalDetailAck>(ArrivalM);
            return PartialView("Index", ArriveAck);
        }
        public ActionResult Edit(int id)
        {
            string uId = User.Identity.GetUserId();
            ViewBag.GetAccnPreference = CustomDropDownList.GetAccnPreference();
            ViewBag.ArrivalMode = CustomDropDownList.ArrivalModes();

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var personal = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == uId);
                var appointment = uow.CrsMbrAppointmentRepo.FirstOrDefault(x => x.CreatedBy == uId, fk => fk.Ranks);
                ViewBag.FullName = appointment.Ranks.RankName + " " + personal.FirstName + " " + personal.MiddleName + " " + personal.Surname;

                var arrivaldata = uow.ArrivalDetailRepo.GetById(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ArrivalDetail, ArrivalDetailUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                ArrivalDetailUpVM CreateDto = mapper.Map<ArrivalDetail, ArrivalDetailUpVM>(arrivaldata);
                return View(CreateDto);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Edit(ArrivalDetailUpVM objArrivaldetail)
        {
            ViewBag.GetAccnPreference = CustomDropDownList.GetAccnPreference();
            ViewBag.ArrivalModes = CustomDropDownList.ArrivalModes();

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ArrivalDetail, ArrivalDetailUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                ArrivalDetail UpdateDto = mapper.Map<ArrivalDetailUpVM, ArrivalDetail>(objArrivaldetail);
                uow.ArrivalDetailRepo.Update(UpdateDto);
                await uow.CommitAsync();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }
    }
}