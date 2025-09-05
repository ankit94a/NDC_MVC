using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Core.IRepositories;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Models;
using NDCWeb.Persistence;
using NDCWeb.Persistence.Repositories;
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
	//[UserMenu(MenuArea = "Staff")]
	public class EventController : Controller
	{
		// GET: Staff/Event
		public ActionResult Index()
		{
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var personalDetail = uow.EventRepo.GetAll();
				var config = new MapperConfiguration(cfg =>
				{
					cfg.CreateMap<Event, EventIndexVM>();
				});
				IMapper mapper = config.CreateMapper();
				var participants = mapper.Map<IEnumerable<Event>, List<EventIndexVM>>(personalDetail);
				return View(participants);
			}
		}
		[HttpGet]
		public async Task<ActionResult> MasterSearch(string query)
		{
			if (!string.IsNullOrEmpty(query))
			{
				using (var context = new NDCWebContext())
				{
					var repo = new UserRepository(context);
					var users = await repo.GetUsersByFNameAsync(query);
					TempData["SearchResults"] = users;
					TempData["Query"] = query;
				}
			}
			// Redirect back to the previous page (referrer)
			if (Request.UrlReferrer != null)
			{
				return Redirect(Request.UrlReferrer.ToString());
			}

			// Redirect to any page (e.g., Index) that uses _Layout.cshtml
			return RedirectToAction("Index", "Home");
		}


		[HttpGet]
		public ActionResult MemberCompletePreviewPartial(string userId)
		{
			MemberCompletePreviewVM objCompletePreview = new MemberCompletePreviewVM();
			string uId = userId;
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var personalDetail = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == uId, np => np.OfficeStates, np2 => np2.OfficeStates.Countries);
				var appointment = uow.CrsMbrAppointmentRepo.FirstOrDefault(x => x.CreatedBy == uId);
				var qualification = uow.CrsMbrQualificationRepo.Find(x => x.CreatedBy == uId);
				var countryVisits = uow.CountryVisitRepo.Find(x => x.CreatedBy == uId);
				var languages = uow.CrsMbrLanguageRepo.Find(x => x.CreatedBy == uId);
				var asgnmntAppointments = uow.AsgmtAppointmentRepo.Find(x => x.CreatedBy == uId);
				//var biography = uow.CrsMbrBiographyRepo.FirstOrDefault(x => x.CreatedBy == uId);
				var visa = uow.VisaDetailRepo.FirstOrDefault(x => x.CreatedBy == uId);
				var accountinfo = uow.AccountInfoRepo.FirstOrDefault(x => x.CreatedBy == uId);
				var tally = uow.TallyDetailRepo.FirstOrDefault(x => x.CreatedBy == uId);
				var vehicleSticker = uow.CrsMbrVehicleStickerRepo.Find(x => x.CreatedBy == uId);
				var spouse = uow.CrsMbrSpouseRepo.FirstOrDefault(x => x.CreatedBy == uId, np2 => np2.iSpouseLanguages, np4 => np4.iSpouseQualifications);
				var passport = uow.PassportDetailRepo.FirstOrDefault(x => x.CreatedBy == uId, np => np.iChildrenPassports);
				var spouseQualification = uow.SpouseQualificationRepo.Find(x => x.CreatedBy == uId);
				var spouseChildren = uow.SpouseChildrenRepo.Find(x => x.CreatedBy == uId);
				var spouseLanguage = uow.SpouseLanguageRepo.Find(x => x.CreatedBy == uId);
				//var passportChildren = uow.ChildrenPassportRepo.Find(x => x.CreatedBy == uId);


				var config = new MapperConfiguration(cfg =>
				{
					cfg.CreateMap<CrsMemberPersonal, CrsMemberPersonalIndxVM>();
					cfg.CreateMap<CrsMbrAppointment, CrsMbrAppointmentIndxVM>();

					cfg.CreateMap<CrsMbrQualification, CrsMbrQualificationIndxVM>();
					cfg.CreateMap<CountryVisit, CountryVisitIndxVM>();
					cfg.CreateMap<CrsMbrLanguage, CrsMbrLanguageIndxVM>();
					cfg.CreateMap<AsgmtAppointment, ImportantAssignmentIndxVM>();

					//cfg.CreateMap<CrsMbrBiography, BiographyIndxVM>();
					cfg.CreateMap<VisaDetail, VisaDetailIndxVM>();
					cfg.CreateMap<AccountInfo, AccountInfoIndxVM>();
					cfg.CreateMap<TallyDetail, TallyDetailIndxVM>();
					cfg.CreateMap<CrsMbrVehicleSticker, CrsMbrVehicleStickerIndxVM>();

					cfg.CreateMap<CrsMbrSpouse, SpouseIndxVM>();
					cfg.CreateMap<PassportDetail, PassportDetailIndxVM>();
					cfg.CreateMap<SpouseChildren, ChildrenIndxVM>();
					//cfg.CreateMap<SpouseQualification, SpouseQualificationIndxVM>();
					//cfg.CreateMap<SpouseLanguage, SpouseLanguageIndxVM>();
					//cfg.CreateMap<ChildrenPassport, PassportChildrenIndxVM>();
				});

				IMapper mapper = config.CreateMapper();
				CrsMemberPersonalIndxVM personalVM = mapper.Map<CrsMemberPersonal, CrsMemberPersonalIndxVM>(personalDetail);
				CrsMbrAppointmentIndxVM appointmentVM = mapper.Map<CrsMbrAppointment, CrsMbrAppointmentIndxVM>(appointment);
				var qualificationsVM = mapper.Map<IEnumerable<CrsMbrQualification>, List<CrsMbrQualificationIndxVM>>(qualification);
				var countryVisitsVM = mapper.Map<IEnumerable<CountryVisit>, List<CountryVisitIndxVM>>(countryVisits);
				var languagesVM = mapper.Map<IEnumerable<CrsMbrLanguage>, List<CrsMbrLanguageIndxVM>>(languages);
				var importantAssignmentsVM = mapper.Map<IEnumerable<AsgmtAppointment>, List<ImportantAssignmentIndxVM>>(asgnmntAppointments);
				//BiographyIndxVM biographyVM = mapper.Map<CrsMbrBiography, BiographyIndxVM>(biography);
				VisaDetailIndxVM visaVM = mapper.Map<VisaDetail, VisaDetailIndxVM>(visa);
				AccountInfoIndxVM accountInfoVM = mapper.Map<AccountInfo, AccountInfoIndxVM>(accountinfo);
				TallyDetailIndxVM tallyVM = mapper.Map<TallyDetail, TallyDetailIndxVM>(tally);
				var vehicleStickerVM = mapper.Map<IEnumerable<CrsMbrVehicleSticker>, List<CrsMbrVehicleStickerIndxVM>>(vehicleSticker);
				SpouseIndxVM spouseVM = mapper.Map<CrsMbrSpouse, SpouseIndxVM>(spouse);
				PassportDetailIndxVM passportIndxVM = mapper.Map<PassportDetail, PassportDetailIndxVM>(passport);
				var spouseChildrensVM = mapper.Map<IEnumerable<SpouseChildren>, List<ChildrenIndxVM>>(spouseChildren);
				//var spouseLanguagesVM = mapper.Map<IEnumerable<SpouseLanguage>, List<SpouseLanguageIndxVM>>(spouseLanguage);
				//var spouseQualificationVM = mapper.Map<IEnumerable<SpouseQualification>, List<SpouseQualificationIndxVM>>(spouseQualification);
				//var passportChildrensVM = mapper.Map<IEnumerable<ChildrenPassport>, List<PassportChildrenIndxVM>>(passportChildren);


				objCompletePreview.PersonalVM = personalVM;
				objCompletePreview.AppointmentVM = appointmentVM;
				objCompletePreview.QualificationsVM = qualificationsVM;
				objCompletePreview.CountryVisitsVM = countryVisitsVM;
				objCompletePreview.LanguagesVM = languagesVM;
				objCompletePreview.ImportantAssignmentsVM = importantAssignmentsVM;
				//objCompletePreview.BiographyVM = biographyVM;
				//objCompletePreview.VisaVM = visaVM;
				objCompletePreview.AccountInfoVM = accountInfoVM;
				objCompletePreview.TallyVM = tallyVM;
				objCompletePreview.VehicleStickerVM = vehicleStickerVM;
				objCompletePreview.SpouseVM = spouseVM;
				//objCompletePreview.PassportVM = passportIndxVM;
				objCompletePreview.ChildrensVM = spouseChildrensVM;
				//objCompletePreview.SpouseLanguagesVM = spouseLanguagesVM;
				//objCompletePreview.SpouseQualificationsVM = spouseQualificationVM;
				//objCompletePreview.PassportChildrensVM = passportChildrensVM;
			}
			//return PartialView("Preview/_MemberCompletePreview", objCompletePreview);
			return Json(new
			{
				success = true,
				data= objCompletePreview,
			}, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Eventboard()
		{
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var personalDetail = uow.EventRepo.GetAll();
				var config = new MapperConfiguration(cfg =>
				{
					cfg.CreateMap<Event, EventIndexVM>();
				});
				IMapper mapper = config.CreateMapper();
				var participants = mapper.Map<IEnumerable<Event>, List<EventIndexVM>>(personalDetail);
				return View(participants);
			}
		}
		// GET: Staff/Event/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Staff/Event/Create
		[HttpPost]
		public async Task<ActionResult> Create(EventCreateVM objEv)
		{
			try
			{
				using (var uow = new UnitOfWork(new NDCWebContext()))
				{
					var config = new MapperConfiguration(cfg =>
					{
						cfg.CreateMap<EventCreateVM, Event>();
					});
					IMapper mapper = config.CreateMapper();
					Event CreateDto = mapper.Map<EventCreateVM, Event>(objEv);
					uow.EventRepo.Add(CreateDto);
					await uow.CommitAsync();
					this.AddNotification("Record Saved", NotificationType.SUCCESS);
					return RedirectToAction("Index");
				}
			}
			catch
			{
				return View();
			}
		}

		// GET: Staff/Event/Edit/5
		public async Task<ActionResult> Edit(int id)
		{
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var evnt = await uow.EventRepo.GetByIdAsync(id);
				var config = new MapperConfiguration(cfg =>
				{
					cfg.CreateMap<IEnumerable<Event>, List<EventUpdVM>>();
				});
				IMapper mapper = config.CreateMapper();
				EventUpdVM indexDto = mapper.Map<Event, EventUpdVM>(evnt);
				return View(indexDto);
			}
		}

		// POST: Staff/Event/Edit/5
		[HttpPost]
		public async Task<ActionResult> Edit(EventUpdVM objInfotechUp)
		{
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var config = new MapperConfiguration(cfg =>
				{
					cfg.CreateMap<EventUpdVM, Event>();
				});
				IMapper mapper = config.CreateMapper();
				Event UpdateDto = mapper.Map<EventUpdVM, Event>(objInfotechUp);
				uow.EventRepo.Update(UpdateDto);
				await uow.CommitAsync();
				this.AddNotification("Record Saved", NotificationType.SUCCESS);
				return RedirectToAction("Index");
			}
		}

		// POST: Staff/Event/Delete/5
		[HttpPost]
		public async Task<JsonResult> DeleteOnConfirm(int id)
		{
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var DeleteItem = await uow.EventRepo.GetByIdAsync(id);
				if (DeleteItem == null)
				{
					return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
				}
				else
				{
					uow.EventRepo.Remove(DeleteItem);
					await uow.CommitAsync();
					return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
				}
			}
		}
		public async Task<ActionResult> LatestEvent()
		{
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var eventname = await uow.EventRepo.GetViewLatestEventAllInfoAsync();
				return View(eventname);
			}
		}

		public async Task<ActionResult> LatestEventMembers(string uId)
		{
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var eventmember = await uow.EventRepo.GetViewLatestEventMembersAllInfoAsync(uId);
				return View(eventmember);
			}
		}
	}
}
