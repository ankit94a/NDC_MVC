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
    [EncryptedActionParameter]
    public class TelecommRequirementController : Controller
    {
        // GET: Member/TelecommRequirement
        public ActionResult Index()
        {
            string uId = User.Identity.GetUserId();
            TelecommRequirementVM objCompletePreview = new TelecommRequirementVM();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var teledetails = uow.TelecommRequirementRepository.FirstOrDefault(x => x.CreatedBy == uId);
                var crsMemberPersonal = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == uId);
                if (crsMemberPersonal == null)
                {
                    return Redirect("~/member");
                }
                if (teledetails == null)
                {
                    //ViewBag.GetAccnPreference = CustomDropDownList.GetAccnPreference();
                    //ViewBag.InternetPref = CustomDropDownList.GetInterNetPref();
                    //var personal = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == uId);
                    //var appointment = uow.CrsMbrAppointmentRepo.FirstOrDefault(x => x.CreatedBy == uId, fk => fk.Ranks);
                    //ViewBag.FullName = appointment.Ranks.RankName + " " + personal.FirstName + " " + personal.MiddleName + " " + personal.Surname;
                    return RedirectToAction("Create");
                }
                else
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<TelecommRequirement, TelecommRequirementIndexVM>();
                    });
                    IMapper mapper = config.CreateMapper();
                    TelecommRequirementIndexVM IndexDto = mapper.Map<TelecommRequirement, TelecommRequirementIndexVM>(teledetails);
                    var personal = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == uId);
                    var appointment = uow.CrsMbrAppointmentRepo.FirstOrDefault(x => x.CreatedBy == uId, fk => fk.Ranks);
                    IndexDto.FullName = appointment.Ranks.RankName + " " + personal.FirstName + " " + personal.MiddleName + " " + personal.Surname;
                    return View(IndexDto);
                }
            }
        }

        // GET: Member/TelecommRequirement/Create
        public ActionResult Create()
        {
            string uId = User.Identity.GetUserId();
            ViewBag.GetAccnPreference = CustomDropDownList.GetAccnPreference();
            ViewBag.InternetPref = CustomDropDownList.GetInterNetPref();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var telecomdata = uow.TelecommRequirementRepository.FirstOrDefault(x => x.CreatedBy == uId);
                if(telecomdata != null)
                {
                    return RedirectToAction("Index");
                }
                var personal = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == uId);
                var register = uow.CourseRegisterRepo.FirstOrDefault(x => x.UserId == uId, fk => fk.Ranks);
                ViewBag.FullName = register.Ranks.RankName + " " + personal.FirstName + " " + personal.MiddleName + " " + personal.Surname;
            }
            return View();
        }

        // POST: Member/TelecommRequirement/Create
        [HttpPost]
        public ActionResult Create(TelecommRequirementCreateVM objTelelComm)
        {
            try
            {
                string uId = User.Identity.GetUserId();
                ViewBag.GetAccnPreference = CustomDropDownList.GetAccnPreference();
                ViewBag.InternetPref = CustomDropDownList.GetInterNetPref();
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {

                    if (ModelState.IsValid)
                    {
                        var config = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<TelecommRequirementCreateVM, TelecommRequirement>();
                        });
                        IMapper mapper = config.CreateMapper();
                        TelecommRequirement CreateDto = mapper.Map<TelecommRequirementCreateVM, TelecommRequirement>(objTelelComm);
                        uow.TelecommRequirementRepository.Add(CreateDto);
                        uow.Commit();
                    }
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Member/TelecommRequirement/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.GetAccnPreference = CustomDropDownList.GetAccnPreference();
            ViewBag.InternetPref = CustomDropDownList.GetInterNetPref();
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                
                var teledata = uow.TelecommRequirementRepository.GetById(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TelecommRequirement, TelecommRequirementUpdateVM>();
                });
                IMapper mapper = config.CreateMapper();
                TelecommRequirementUpdateVM CreateDto = mapper.Map<TelecommRequirement, TelecommRequirementUpdateVM>(teledata);
                return View(CreateDto);
            }
        }

        // POST: Member/TelecommRequirement/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(TelecommRequirementUpdateVM objTeleReqUpvm)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var crsMemberPersonal = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == uId);
                ViewBag.Citizenship = crsMemberPersonal.CitizenshipCountries.CountryName;
                if (ModelState.IsValid)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<TelecommRequirementUpdateVM, TelecommRequirement>();
                    });
                    IMapper mapper = config.CreateMapper();
                    TelecommRequirement UpdateDto = mapper.Map<TelecommRequirementUpdateVM, TelecommRequirement>(objTeleReqUpvm);
                    uow.TelecommRequirementRepository.Update(UpdateDto);
                    await uow.CommitAsync();
                    this.AddNotification("Record Update", NotificationType.SUCCESS);
                }
                return RedirectToAction("Index");
            }
        }

        // GET: Member/TelecommRequirement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Member/TelecommRequirement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
