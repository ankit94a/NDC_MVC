using NDCWeb.Areas.Member.View_Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Admin.View_Models;
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
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Infrastructure.Extensions;
using System.Text;
using NDCWeb.Infrastructure.Helpers.FileExt;
using NDCWeb.Infrastructure.Helpers.Account;
using System.Net;
using Microsoft.Owin.Security;

namespace NDCWeb.Areas.Member.Controllers
{
    [Authorize(Roles = CustomRoles.Candidate)]
    [UserMenu(MenuArea = "Member")]
    [CSPLHeaders]
    public class CourseMemberController : Controller
    {
        // GET: Member/CourseMember
        public ActionResult CourseEnrol()
        {
            ViewBag.Service = CustomDropDownList.GetRankService();
            ViewBag.Gender = CustomDropDownList.GetGender();
            ViewBag.MaritalStatus = CustomDropDownList.GetMaritalStatus();
            return View();
        }

        public ActionResult Register()
        {
            ViewBag.Service = CustomDropDownList.GetRankService();
            ViewBag.Gender = CustomDropDownList.GetGender();
            ViewBag.MaritalStatus = CustomDropDownList.GetMaritalStatus();
            return View();
        }
        

        #region Partial Views
        public ActionResult PersonalDetailPartial(string mode)
        {
            string uId = User.Identity.GetUserId();
            if (uId == null)
            {
                RedirectToAction("Login", "Account", new { area = "Admin" });
            }
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var personalDetail = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == uId, np => np.OfficeStates, np2 => np2.OfficeStates.Countries);
                if (personalDetail == null)
                {
                    //Add
                    ViewBag.Service = CustomDropDownList.GetRankService();
                    ViewBag.Gender = CustomDropDownList.GetGender();
                    ViewBag.BloodGroup = CustomDropDownList.GetBloodGroups();
                    ViewBag.MaritalStatus = CustomDropDownList.GetMaritalStatus();
                    ViewBag.Country = uow.CountryMasterRepo.GetCountries();
                    ViewBag.ddlCountry = uow.CountryMasterRepo.GetCountriesOnlyName();
                    ViewBag.HoldingPassport = CustomDropDownList.GetHoldingPassport();
                    ViewBag.PassportType = CustomDropDownList.GetPassportType();

                    var regMember = uow.CourseRegisterRepo.FirstOrDefault(x => x.UserId == uId);
                    CrsMemberPersonalCrtVM objPersonal = new CrsMemberPersonalCrtVM();
                    //objPersonal.FullName = regMember.FirstName + " " + regMember.MiddleName + " " + regMember.LastName;
                    objPersonal.FirstName = regMember.FirstName;
                    objPersonal.MiddleName = regMember.MiddleName;
                    objPersonal.Surname = regMember.LastName;
                    objPersonal.Gender = regMember.Gender;
                    objPersonal.DOBirth = regMember.DOBirth;
                    objPersonal.EmailId = regMember.EmailId;
                    objPersonal.MobileNo = regMember.MobileNo;
                    //objPersonal.Branch = regMember.Branch;
                    ViewBag.ServiceCategory = regMember.Ranks.Service;
                    //objPersonal.RankId = regMember.RankId;
                    //ViewData["SelectedRank"] = regMember.RankId;
                    return PartialView("Add/_CourseMemberPersonal", objPersonal);
                }
                else
                {
                    if (mode == "E")
                    {
                        //Update
                        ViewBag.Service = CustomDropDownList.GetRankService();
                        ViewBag.Gender = CustomDropDownList.GetGender();
                        ViewBag.BloodGroup = CustomDropDownList.GetBloodGroups();
                        ViewBag.MaritalStatus = CustomDropDownList.GetMaritalStatus();
                        ViewBag.Country = uow.CountryMasterRepo.GetCountries();
                        ViewBag.ddlCountry = uow.CountryMasterRepo.GetCountriesOnlyName();
                        ViewBag.HoldingPassport = CustomDropDownList.GetHoldingPassport();
                        ViewBag.PassportType = CustomDropDownList.GetPassportType();

                        var config = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<CrsMemberPersonal, CrsMemberPersonalUpVM>()
                            .ForMember(dest => dest.OfficeCountryId, opts => opts.MapFrom(src => src.OfficeStates.CountryId));
                        });
                        IMapper mapper = config.CreateMapper();
                        CrsMemberPersonalUpVM UpdateDto = mapper.Map<CrsMemberPersonal, CrsMemberPersonalUpVM>(personalDetail);
                        //ViewData["SelectedRank"] = UpdateDto.RankId;
                        ViewData["SelectedOfficeState"] = UpdateDto.OfficeStateId;
                        return PartialView("Edit/_CourseMemberPersonalEdit", UpdateDto);
                    }
                    else
                    {
                        //Read
                        var config = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<CrsMemberPersonal, CrsMemberPersonalIndxVM>();
                        });
                        IMapper mapper = config.CreateMapper();
                        CrsMemberPersonalIndxVM DetailDto = mapper.Map<CrsMemberPersonal, CrsMemberPersonalIndxVM>(personalDetail);
                        return PartialView("Display/_CourseMemberPersonal", DetailDto);
                    }
                }
            }
        }
        public ActionResult ServicePartial(string mode)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var appointment = uow.CrsMbrAppointmentRepo.FirstOrDefault(x => x.CreatedBy == uId);
                if (appointment == null)
                {
                    ViewBag.Service = CustomDropDownList.GetRankService();

                    var regMember = uow.CourseRegisterRepo.FirstOrDefault(x => x.UserId == uId);
                    CrsMbrAppointmentCrtVM objAppointment = new CrsMbrAppointmentCrtVM();
                    objAppointment.Designation = regMember.ApptDesignation;
                    objAppointment.Organisation = regMember.ApptOrganisation;
                    objAppointment.Location = regMember.ApptLocation;
                    objAppointment.Branch = regMember.Branch;
                    objAppointment.Service = regMember.Ranks.Service;
                    objAppointment.RankId = regMember.RankId;
                    ViewData["SelectedRank"] = regMember.RankId;
                    return PartialView("Add/_ServiceMultiple", objAppointment);
                }
                else
                    return PartialView("Display/_ServiceMultiple");
            }
        }
        public ActionResult AppointmentPartial(string mode)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var appointment = uow.CrsMbrAppointmentRepo.FirstOrDefault(x => x.CreatedBy == uId);
                if (appointment == null)
                {
                    //Add
                    ViewBag.Service = CustomDropDownList.GetRankService();

                    var regMember = uow.CourseRegisterRepo.FirstOrDefault(x => x.UserId == uId);
                    CrsMbrAppointmentCrtVM objAppointment = new CrsMbrAppointmentCrtVM();
                    objAppointment.Designation = regMember.ApptDesignation;
                    objAppointment.Organisation = regMember.ApptOrganisation;
                    objAppointment.Location = regMember.ApptLocation;
                    objAppointment.Branch = regMember.Branch;
                    objAppointment.Service = regMember.Ranks.Service;
                    objAppointment.RankId = regMember.RankId;
                    objAppointment.DOSeniority = DateTime.Now;
                    objAppointment.DOJoining = regMember.DOCommissioning;
                    ViewData["SelectedRank"] = regMember.RankId;
                    return PartialView("Add/_ServiceDetail", objAppointment);
                }
                else
                {
                    if (mode == "E")
                    {
                        ViewBag.Service = CustomDropDownList.GetRankService();

                        var config = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<CrsMbrAppointment, CrsMbrAppointmentUpVM>();
                        });
                        IMapper mapper = config.CreateMapper();
                        CrsMbrAppointmentUpVM UpdateDto = mapper.Map<CrsMbrAppointment, CrsMbrAppointmentUpVM>(appointment);
                        ViewData["SelectedRank"] = UpdateDto.RankId;
                        return PartialView("Edit/_ServiceDetailEdit", UpdateDto);
                    }
                    else
                    {
                        var config = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<CrsMbrAppointment, CrsMbrAppointmentIndxVM>();
                        });
                        IMapper mapper = config.CreateMapper();
                        CrsMbrAppointmentIndxVM DetailDto = mapper.Map<CrsMbrAppointment, CrsMbrAppointmentIndxVM>(appointment);
                        return PartialView("Display/_AppointmentDetails", DetailDto);
                    }
                }
            }
        }
        

        public ActionResult CountryVisitPartial(string mode)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                if (mode == "A")
                {
                    return PartialView("Add/_CountryVisit");
                    //Update
                    //var config = new MapperConfiguration(cfg => cfg.CreateMap<CountryVisit, CountryVisitUpVM>());
                    //IMapper mapper = config.CreateMapper();
                    //var UpdateDto = mapper.Map<IEnumerable<CountryVisit>, List<CountryVisitUpVM>>(countryVisits);
                    //return PartialView("Edit/_CountryVisit", UpdateDto);
                }
                else
                {
                    var countryVisits = uow.CountryVisitRepo.Find(x => x.CreatedBy == uId);
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<CountryVisit, CountryVisitIndxVM>());
                    IMapper mapper = config.CreateMapper();
                    var IndexDto = mapper.Map<IEnumerable<CountryVisit>, List<CountryVisitIndxVM>>(countryVisits);
                    return PartialView("Display/_CountryVisit", IndexDto);
                    //if (countryVisits.Count() > 0)
                    //{
                    //    var config = new MapperConfiguration(cfg => cfg.CreateMap<CountryVisit, CountryVisitIndxVM>());
                    //    IMapper mapper = config.CreateMapper();
                    //    var IndexDto = mapper.Map<IEnumerable<CountryVisit>, List<CountryVisitIndxVM>>(countryVisits);
                    //    return PartialView("Display/_CountryVisit", IndexDto);
                    //}
                    //else
                    //{
                    //    return PartialView("Add/_CountryVisit");
                    //}
                }
            }
        }
        public ActionResult QualificationPartial(string mode)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                if (mode == "A")
                {
                    return PartialView("Add/_Qualification");
                }
                else
                {
                    var qualification = uow.CrsMbrQualificationRepo.Find(x => x.CreatedBy == uId);
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<CrsMbrQualification, CrsMbrQualificationIndxVM>());
                    IMapper mapper = config.CreateMapper();
                    var IndexDto = mapper.Map<IEnumerable<CrsMbrQualification>, List<CrsMbrQualificationIndxVM>>(qualification);
                    return PartialView("Display/_Qualification", IndexDto);
                }
            }
        }
        public ActionResult LanguagesKnownPartial(string mode)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                if (mode == "A")
                {
                    return PartialView("Add/_LanguagesKnown");
                }
                else
                {
                    var languages = uow.CrsMbrLanguageRepo.Find(x => x.CreatedBy == uId);
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<CrsMbrLanguage, CrsMbrLanguageIndxVM>());
                    IMapper mapper = config.CreateMapper();
                    var IndexDto = mapper.Map<IEnumerable<CrsMbrLanguage>, List<CrsMbrLanguageIndxVM>>(languages);
                    return PartialView("Display/_LanguagesKnown", IndexDto);
                }
            }
        }
        public ActionResult ImportantAssignmentPartial(string mode)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                if (mode == "A")
                {
                    return PartialView("Add/_ImportantAssignment");
                }
                else
                {
                    var asgnmntAppointments = uow.AsgmtAppointmentRepo.Find(x => x.CreatedBy == uId);
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<AsgmtAppointment, ImportantAssignmentIndxVM>());
                    IMapper mapper = config.CreateMapper();
                    var IndexDto = mapper.Map<IEnumerable<AsgmtAppointment>, List<ImportantAssignmentIndxVM>>(asgnmntAppointments);
                    return PartialView("Display/_AssignmentAppointment", IndexDto);
                }
            }
        }

        public ActionResult SpousePartial(string mode)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var spouse = uow.CrsMbrSpouseRepo.FirstOrDefault(x => x.CreatedBy == uId, np2 => np2.iSpouseLanguages);
                if (spouse == null)
                {
                    //Add
                    ViewBag.ddlCountry = uow.CountryMasterRepo.GetCountriesOnlyName();
                    ViewBag.Gender = CustomDropDownList.GetGender();
                    ViewBag.BloodGroup = CustomDropDownList.GetBloodGroups();
                    ViewBag.PassportType = CustomDropDownList.GetPassportType();
                    return PartialView("Add/_Spouse");
                }
                else
                {
                    if (mode == "E")
                    {
                        //Update
                        ViewBag.ddlCountry = uow.CountryMasterRepo.GetCountriesOnlyName();
                        ViewBag.Gender = CustomDropDownList.GetGender();
                        ViewBag.BloodGroup = CustomDropDownList.GetBloodGroups();
                        ViewBag.PassportType = CustomDropDownList.GetPassportType();
                        var config = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<CrsMbrSpouse, SpouseUpVM>();
                        });
                        IMapper mapper = config.CreateMapper();
                        SpouseUpVM UpdateDTO = mapper.Map<CrsMbrSpouse, SpouseUpVM>(spouse);
                        return PartialView("Edit/_Spouse", UpdateDTO);
                    }
                    else
                    {
                        //Read
                        var config = new MapperConfiguration(cfg => cfg.CreateMap<CrsMbrSpouse, SpouseIndxVM>());
                        IMapper mapper = config.CreateMapper();
                        SpouseIndxVM DetailDTO = mapper.Map<CrsMbrSpouse, SpouseIndxVM>(spouse);
                        return PartialView("Display/_Spouse", DetailDTO);
                    }
                }
            }
        }
        public ActionResult SpouseLanguagePartial(string mode)
        {

            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                if (mode == "A")
                {
                    return PartialView("Edit/_SpouseLanguage");
                }
                else
                {
                    var spouseLanguage = uow.SpouseLanguageRepo.Find(x => x.CreatedBy == uId);
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<SpouseLanguage, SpouseLanguageIndxVM>());
                    IMapper mapper = config.CreateMapper();
                    var IndexDto = mapper.Map<IEnumerable<SpouseLanguage>, List<SpouseLanguageIndxVM>>(spouseLanguage);
                    return PartialView("Display/_SpouseLanguage", IndexDto);
                }
            }
        }
        public ActionResult ChildrenPartial(string mode, int childId = 0)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var children = uow.SpouseChildrenRepo.Find(x => x.CreatedBy == uId);
                if (children.Count() < 1 || mode == "A")
                {
                    //Add
                    ViewBag.ddlCountry = uow.CountryMasterRepo.GetCountriesOnlyName();
                    ViewBag.Gender = CustomDropDownList.GetGender();
                    ViewBag.YesNoOpt = CustomDropDownList.GetYesNoOpt();
                    ViewBag.PassportType = CustomDropDownList.GetPassportType();
                    return PartialView("Add/_Children");
                }
                else
                {
                    if (childId > 0)
                    {
                        //Edit
                        ViewBag.ddlCountry = uow.CountryMasterRepo.GetCountriesOnlyName();
                        ViewBag.Gender = CustomDropDownList.GetGender();
                        ViewBag.YesNoOpt = CustomDropDownList.GetYesNoOpt();
                        ViewBag.PassportType = CustomDropDownList.GetPassportType();
                        var child = children.FirstOrDefault(x => x.ChildId == childId);
                        var config = new MapperConfiguration(cfg => cfg.CreateMap<SpouseChildren, ChildrenUpVM>());
                        IMapper mapper = config.CreateMapper();
                        var UpdateDto = mapper.Map<SpouseChildren, ChildrenUpVM>(child);
                        return PartialView("Edit/_Children", UpdateDto);
                    }
                    else
                    {
                        //Read
                        var config = new MapperConfiguration(cfg => cfg.CreateMap<SpouseChildren, ChildrenIndxVM>());
                        IMapper mapper = config.CreateMapper();
                        var IndexDto = mapper.Map<IEnumerable<SpouseChildren>, List<ChildrenIndxVM>>(children);
                        return PartialView("Display/_Children", IndexDto);
                    }
                }
            }
        }
        public ActionResult SpouseQualificationPartial(string mode)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                if (mode == "A")
                {
                    return PartialView("Edit/_SpouseQualification");
                }
                else
                {
                    var spouseQualification = uow.SpouseQualificationRepo.Find(x => x.CreatedBy == uId);
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<SpouseQualification, SpouseQualificationIndxVM>());
                    IMapper mapper = config.CreateMapper();
                    var IndexDto = mapper.Map<IEnumerable<SpouseQualification>, List<SpouseQualificationIndxVM>>(spouseQualification);
                    return PartialView("Display/_SpouseQualification", IndexDto);
                }
            }
        }
        public ActionResult TallyPartial(string mode)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var tally = uow.TallyDetailRepo.FirstOrDefault(x => x.CreatedBy == uId);
                if (tally == null)
                {
                    //Add
                    return PartialView("Add/_Tally");
                }
                else
                {
                    if (mode == "E")
                    {
                        //Update
                        var config = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<TallyDetail, TallyDetailUpVM>();
                        });
                        IMapper mapper = config.CreateMapper();
                        TallyDetailUpVM UpdateDTO = mapper.Map<TallyDetail, TallyDetailUpVM>(tally);
                        return PartialView("Edit/_Tally", UpdateDTO);
                    }
                    else
                    {
                        //Read
                        var config = new MapperConfiguration(cfg => cfg.CreateMap<TallyDetail, TallyDetailIndxVM>());
                        IMapper mapper = config.CreateMapper();
                        TallyDetailIndxVM DetailDTO = mapper.Map<TallyDetail, TallyDetailIndxVM>(tally);
                        return PartialView("Display/_Tally", DetailDTO);
                    }
                }
            }
        }
        public ActionResult VehicleStickerPartial(string mode)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                if (mode == "A")
                {
                    return PartialView("Add/_VehicleSticker");
                }
                else
                {
                    var vehicleStickers = uow.CrsMbrVehicleStickerRepo.Find(x => x.CreatedBy == uId);
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<CrsMbrVehicleSticker, CrsMbrVehicleStickerIndxVM>());
                    IMapper mapper = config.CreateMapper();
                    var IndexDto = mapper.Map<IEnumerable<CrsMbrVehicleSticker>, List<CrsMbrVehicleStickerIndxVM>>(vehicleStickers);
                    return PartialView("Display/_VehicleSticker", IndexDto);
                }
            }
        }
        public ActionResult AccountInfoPartial(string mode)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var accountinfo = uow.AccountInfoRepo.FirstOrDefault(x => x.CreatedBy == uId);
                if (accountinfo == null)
                {
                    //Add
                    return PartialView("Add/_AccountInfo");
                }
                else
                {
                    if (mode == "E")
                    {
                        //Update
                        var config = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<AccountInfo, AccountInfoUpVM>();
                        });
                        IMapper mapper = config.CreateMapper();
                        AccountInfoUpVM UpdateDTO = mapper.Map<AccountInfo, AccountInfoUpVM>(accountinfo);
                        return PartialView("Edit/_AccountInfo", UpdateDTO);
                    }
                    else
                    {
                        //Read
                        var config = new MapperConfiguration(cfg => cfg.CreateMap<AccountInfo, AccountInfoIndxVM>());
                        IMapper mapper = config.CreateMapper();
                        AccountInfoIndxVM DetailDTO = mapper.Map<AccountInfo, AccountInfoIndxVM>(accountinfo);
                        return PartialView("Display/_AccountInfo", DetailDTO);
                    }
                }
            }
        }

        #region Remove tabs
        //public ActionResult AddressPartial(string mode)
        //{
        //    string uId = User.Identity.GetUserId();
        //    using (var uow = new UnitOfWork(new NDCWebContext()))
        //    {
        //        var address = uow.CrsMbrAddressRepo.FirstOrDefault(x => x.CreatedBy == uId, np => np.States, np2 => np2.States.Countries);
        //        if (address == null)
        //        {
        //            //Add
        //            ViewBag.Country = uow.CountryMasterRepo.GetCountries();
        //            return PartialView("Add/_Address");
        //        }
        //        else
        //        {
        //            if (mode == "E")
        //            {
        //                //Update
        //                ViewBag.Country = uow.CountryMasterRepo.GetCountries();
        //                var config = new MapperConfiguration(cfg =>
        //                {
        //                    cfg.CreateMap<CrsMbrAddress, CrsMbrAddressUpVM>()
        //                    .ForMember(dest => dest.CountryId, opts => opts.MapFrom(src => src.States.CountryId));
        //                });
        //                IMapper mapper = config.CreateMapper();
        //                CrsMbrAddressUpVM UpdateDto = mapper.Map<CrsMbrAddress, CrsMbrAddressUpVM>(address);
        //                ViewData["SelectedState"] = UpdateDto.StateId;
        //                return PartialView("Edit/_AddressEdit", UpdateDto);
        //            }
        //            else
        //            {
        //                var config = new MapperConfiguration(cfg =>
        //                {
        //                    cfg.CreateMap<CrsMbrAddress, CrsMbrAddressIndxVM>();
        //                });
        //                IMapper mapper = config.CreateMapper();
        //                CrsMbrAddressIndxVM DetailDto = mapper.Map<CrsMbrAddress, CrsMbrAddressIndxVM>(address);
        //                return PartialView("Display/_Address", DetailDto);
        //            }
        //        }
        //    }
        //}
        //public ActionResult BiographyPartial(string mode)
        //{
        //    string uId = User.Identity.GetUserId();
        //    using (var uow = new UnitOfWork(new NDCWebContext()))
        //    {
        //        var biography = uow.CrsMbrBiographyRepo.FirstOrDefault(x => x.CreatedBy == uId);
        //        if (biography == null)
        //        {
        //            //Add
        //            ViewBag.Gender = CustomDropDownList.GetGender();
        //            return PartialView("Add/_Biography");
        //        }
        //        else
        //        {
        //            if (mode == "E")
        //            {
        //                //Update
        //                ViewBag.Gender = CustomDropDownList.GetGender();
        //                var config = new MapperConfiguration(cfg =>
        //                {
        //                    cfg.CreateMap<CrsMbrBiography, BiographyUpVM>();
        //                });
        //                IMapper mapper = config.CreateMapper();
        //                BiographyUpVM UpdateDTO = mapper.Map<CrsMbrBiography, BiographyUpVM>(biography);
        //                return PartialView("Edit/_Biography", UpdateDTO);
        //            }
        //            else
        //            {
        //                //Read
        //                var config = new MapperConfiguration(cfg => cfg.CreateMap<CrsMbrBiography, BiographyIndxVM>());
        //                IMapper mapper = config.CreateMapper();
        //                BiographyIndxVM DetailDTO = mapper.Map<CrsMbrBiography, BiographyIndxVM>(biography);
        //                return PartialView("Display/_Biography", DetailDTO);
        //            }
        //        }
        //    }
        //}
        //public ActionResult VisaPartial(string mode)
        //{
        //    string uId = User.Identity.GetUserId();
        //    using (var uow = new UnitOfWork(new NDCWebContext()))
        //    {
        //        var visa = uow.VisaDetailRepo.FirstOrDefault(x => x.CreatedBy == uId);
        //        if (visa == null)
        //        {
        //            //Add
        //            ViewBag.VisaEntryType = CustomDropDownList.GetVisaEntryType();
        //            return PartialView("Add/_VisaDetail");
        //        }
        //        else
        //        {
        //            if (mode == "E")
        //            {
        //                //Update
        //                ViewBag.VisaEntryType = CustomDropDownList.GetVisaEntryType();
        //                var config = new MapperConfiguration(cfg =>
        //                {
        //                    cfg.CreateMap<VisaDetail, VisaDetailUpVM>();
        //                });
        //                IMapper mapper = config.CreateMapper();
        //                VisaDetailUpVM UpdateDto = mapper.Map<VisaDetail, VisaDetailUpVM>(visa);
        //                return PartialView("Edit/_VisaDetail", UpdateDto);
        //            }
        //            else
        //            {
        //                //Read
        //                var config = new MapperConfiguration(cfg =>
        //                {
        //                    cfg.CreateMap<VisaDetail, VisaDetailIndxVM>();
        //                });
        //                IMapper mapper = config.CreateMapper();
        //                VisaDetailIndxVM DetailDTO = mapper.Map<VisaDetail, VisaDetailIndxVM>(visa);
        //                return PartialView("Display/_VisaDetail", DetailDTO);
        //            }
        //        }
        //    }
        //}
        //public ActionResult SpouseChildrenPartial(string mode)
        //{
        //    string uId = User.Identity.GetUserId();
        //    using (var uow = new UnitOfWork(new NDCWebContext()))
        //    {
        //        if (mode == "A")
        //        {
        //            ViewBag.Gender = CustomDropDownList.GetGender();
        //            ViewBag.YesNoOpt = CustomDropDownList.GetYesNoOpt();
        //            return PartialView("Edit/_SpouseChildren");
        //        }
        //        else
        //        {
        //            var spouseChildren = uow.SpouseChildrenRepo.Find(x => x.CreatedBy == uId);
        //            var config = new MapperConfiguration(cfg => cfg.CreateMap<SpouseChildren, SpouseChildrenIndxVM>());
        //            IMapper mapper = config.CreateMapper();
        //            var IndexDto = mapper.Map<IEnumerable<SpouseChildren>, List<SpouseChildrenIndxVM>>(spouseChildren);
        //            return PartialView("Display/_SpouseChildren", IndexDto);
        //        }
        //    }
        //}
        //public ActionResult PassportPartial(string mode)
        //{
        //    string uId = User.Identity.GetUserId();
        //    using (var uow = new UnitOfWork(new NDCWebContext()))
        //    {
        //        var passport = uow.PassportDetailRepo.FirstOrDefault(x => x.CreatedBy == uId);
        //        if (passport == null)
        //        {
        //            //Add
        //            ViewBag.HoldingPassport = CustomDropDownList.GetHoldingPassport();
        //            ViewBag.PassportType = CustomDropDownList.GetPassportType();
        //            return PartialView("Add/_PassportDetail");
        //        }
        //        else
        //        {
        //            if (mode == "E")
        //            {
        //                //Update
        //                ViewBag.HoldingPassport = CustomDropDownList.GetHoldingPassport();
        //                ViewBag.PassportType = CustomDropDownList.GetPassportType();
        //                var config = new MapperConfiguration(cfg =>
        //                {
        //                    cfg.CreateMap<PassportDetail, PassportDetailUpVM>();
        //                });
        //                IMapper mapper = config.CreateMapper();
        //                PassportDetailUpVM UpdateDTO = mapper.Map<PassportDetail, PassportDetailUpVM>(passport);
        //                return PartialView("Edit/_PassportDetail", UpdateDTO);
        //            }
        //            else
        //            {
        //                //Read
        //                var config = new MapperConfiguration(cfg => cfg.CreateMap<PassportDetail, PassportDetailIndxVM>());
        //                IMapper mapper = config.CreateMapper();
        //                PassportDetailIndxVM DetailDTO = mapper.Map<PassportDetail, PassportDetailIndxVM>(passport);
        //                return PartialView("Display/_PassportDetail", DetailDTO);
        //            }
        //        }
        //    }
        //}
        //public ActionResult PassportChildrenPartial(string mode)
        //{
        //    string uId = User.Identity.GetUserId();
        //    using (var uow = new UnitOfWork(new NDCWebContext()))
        //    {
        //        if (mode == "A")
        //        {
        //            return PartialView("Edit/_PassportChildren");
        //        }
        //        else
        //        {
        //            var spouseChildren = uow.ChildrenPassportRepo.Find(x => x.CreatedBy == uId);
        //            var config = new MapperConfiguration(cfg => cfg.CreateMap<ChildrenPassport, PassportChildrenIndxVM>());
        //            IMapper mapper = config.CreateMapper();
        //            var IndexDto = mapper.Map<IEnumerable<ChildrenPassport>, List<PassportChildrenIndxVM>>(spouseChildren);
        //            return PartialView("Display/_PassportChildren", IndexDto);
        //        }
        //    }
        //}
        #endregion

        #endregion

        public ActionResult MemberCompletePreviewPartial(string mode)
        {
            MemberCompletePreviewVM objCompletePreview = new MemberCompletePreviewVM();
            string uId = User.Identity.GetUserId();
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
                var spouse = uow.CrsMbrSpouseRepo.FirstOrDefault(x => x.CreatedBy == uId, np2 => np2.iSpouseLanguages, np4=>np4.iSpouseQualifications);
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
            return PartialView("Preview/_MemberCompletePreview", objCompletePreview);
        }

        [HttpPost]
        public ActionResult RegisterACK(CourseMemberCrtVM modal)
        {
            var courseMember = modal;
            return PartialView("_RegisterACK", courseMember);
        }

        #region Views
        public ActionResult Biography()
        {
            return View();
        }
        public ActionResult Arrival()
        {
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
            return PartialView("_ArrivalAck", ArriveAck);
        }
        public ActionResult Tally()
        {
            return View();
        }
        public ActionResult Ration()
        {
            return View();
        }
        public ActionResult AccountInfo()
        {
            return View();
        }
        public ActionResult Spouse()
        {
            return View();
        }
        public ActionResult Rakshika()
        {
            return View();
        }
        #endregion

        #region CourseRegister
        public ActionResult CourseRegister()
        {
            ViewBag.Service = CustomDropDownList.GetRankService();
            ViewBag.Gender = CustomDropDownList.GetGender();
            ViewBag.MaritalStatus = CustomDropDownList.GetMaritalStatus();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CourseRegister(CourseRegisterCrtVM objCourseRegisterCvm)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CourseRegisterCrtVM, CourseRegister>();
                });
                IMapper mapper = config.CreateMapper();
                CourseRegister CreateDto = mapper.Map<CourseRegisterCrtVM, CourseRegister>(objCourseRegisterCvm);
                uow.CourseRegisterRepo.Add(CreateDto);
                await uow.CommitAsync();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("CourseRegister");
            }
        }

        public async Task<ActionResult> RegisterParticipant()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var courseRegisters = await uow.CourseRegisterRepo.GetAllAsync(np => np.Ranks);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<CourseRegister>, List<CourseRegisterIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<CourseRegister>, IEnumerable<CourseRegisterIndxVM>>(courseRegisters).ToList();
                return View(indexDto);
            }
        }
        #endregion

        #region Helper Action
        [HttpPost]
        public JsonResult ImageUpload()
        {
            string ROOT_PATH = ServerRootConsts.USER_ROOT;
            string PHOTO_PATH = ROOT_PATH + "photos/";
            string CURRENT_YEAR = DateTime.Now.Year.ToString();
            string CURRENT_MONTH = DateTime.Now.ToString("MMM");

            string file_PATH = PHOTO_PATH + CURRENT_YEAR + "/" + CURRENT_MONTH + "/";
            if (Request.Files.Count > 0)
            {
                DirectoryHelper.CreateFolder(Server.MapPath(file_PATH));
                HttpPostedFileBase file = Request.Files[0];
                Guid guid = Guid.NewGuid();
                string newFileName = guid + Path.GetExtension(file.FileName);
                string location = file_PATH + newFileName;
                //Check File
                CheckBeforeUpload fs = new CheckBeforeUpload();
                fs.filesize = 3000;
                string result = fs.UploadFile(file);
                if (string.IsNullOrEmpty(result))
                {
                    file.SaveAs(Server.MapPath(location));
                    return Json(new { message = "File Uploaded Successfully!", status = 1, filePath = location });
                }
                else
                    //return Json(new { message = "File Could not be Uploaded!", status = 0 });
                    return Json(new { message = result, status = 0 });
            }
            else
                return Json(new { message = "No files selected.", status = 0, filePath = "#" });
        }
        [HttpPost]
        public JsonResult DocumentUpload()
        {
            string ROOT_PATH = ServerRootConsts.USER_ROOT;
            string DOC_PATH = ROOT_PATH + "documents/";
            string CURRENT_YEAR = DateTime.Now.Year.ToString();
            string CURRENT_MONTH = DateTime.Now.ToString("MMM");

            string file_PATH = DOC_PATH + CURRENT_YEAR + "/" + CURRENT_MONTH + "/";
            if (Request.Files.Count > 0)
            {
                DirectoryHelper.CreateFolder(Server.MapPath(file_PATH));
                HttpPostedFileBase file = Request.Files[0];
                Guid guid = Guid.NewGuid();
                string newFileName = guid + Path.GetExtension(file.FileName);
                string location = file_PATH + newFileName;
                //Check File
                CheckBeforeUpload fs = new CheckBeforeUpload();
                fs.filesize = 3000;
                string result = fs.UploadFile(file);
                if (string.IsNullOrEmpty(result))
                {
                    file.SaveAs(Server.MapPath(location));
                    return Json(new { message = "File Uploaded Successfully!", status = 1, filePath = location });
                }
                else
                    return Json(new { message = result, status = 0 });
            }
            else
                return Json(new { message = "No files selected.", status = 0, filePath = "#" });
        }
        #endregion
        #region Password
        private ApplicationUserManager _userManager;
        public CourseMemberController()
        { }
        public CourseMemberController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public string GetSalt()
        {
            var builder = new StringBuilder();
            while (builder.Length < 16)
            {
                builder.Append(RNG.Next(10).ToString());
            }
            return builder.ToString();
        }
        private static Random RNG = new Random();

        public ActionResult ChangeUserPassword()
        {
            secConst.cSalt = GetSalt();
            ChangeUserPasswordViewModel msm = new ChangeUserPasswordViewModel();
            msm.hdns = secConst.cSalt.ToString();
            return View(msm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeUserPassword(ChangeUserPasswordViewModel model)
        {
            string userName = User.Identity.GetUserName();
            bool pwdOK = false;
            string Msg = "";
            //string userId = User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(userName);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("Login", "Auth", new { area = "" });
            }
            var token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var _pwdhistory = await uow.UserPwdMangerRepo.Validatepwdhistory(userName, AESEncrytDecry.DecryptStringAES(model.NewPassword).Replace(secConst.cSalt, ""));
                if (_pwdhistory)
                {
                    pwdOK = false; //"fail";
                    Msg = "You have already used this password in last 3 transaction. Please use different password.";
                }
                else
                {
                    pwdOK = true;
                    var userPwdMgr = new UserPwdManger
                    {
                        Username = userName,
                        Password = AESEncrytDecry.DecryptStringAES(model.NewPassword).Replace(secConst.cSalt, ""),
                        ModifyDate = DateTime.Now,
                    };
                    uow.UserPwdMangerRepo.Add(userPwdMgr);
                    uow.Commit();
                }
            }
            if (pwdOK)
            {
                var result = await UserManager.ChangePasswordAsync(user.Id, AESEncrytDecry.DecryptStringAES(model.CurrentPassword).Replace(secConst.cSalt, ""), AESEncrytDecry.DecryptStringAES(model.NewPassword).Replace(secConst.cSalt, ""));
                if (result.Succeeded)
                {
                    string[] myCookies = Request.Cookies.AllKeys;
                    //var user = await UserManager.FindByNameAsync(User.Identity.Name);
                    try
                    {
                        if (user != null)
                        {
                            UserActivityHelper.SaveUserActivity("Change Password by user  " + User.Identity.Name, Request.Url.ToString());
                            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                            await UserManager.UpdateSecurityStampAsync(user.Id);
                            Session.Abandon();
                        }

                        foreach (string cookie in myCookies)
                        {
                            Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
                        }
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                    foreach (string cookie in myCookies)
                    {
                        Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
                    }
                    Session.Abandon();
                    return RedirectToAction("ChangeUserPasswordConfirmation", "Home", new { Area = "" });
                    //return RedirectToAction("ChangeUserPasswordConfirmation");
                }
                AddErrors(result);
                return View();
            }
            else
            {
                this.AddNotification(Msg, NotificationType.WARNING);
                return View();
            }
            //string userName = User.Identity.GetUserName();
            ////string userId = User.Identity.GetUserId();
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}
            //var user = await UserManager.FindByNameAsync(userName);
            //if (user == null)
            //{
            //    // Don't reveal that the user does not exist
            //    return RedirectToAction("Login", "Account", new { area = "Admin" });
            //}
            //var token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            ////var result = await UserManager.ResetPasswordAsync(user.Id, token, model.NewPassword);

            ////var result = await UserManager.ChangePasswordAsync(user.Id, model.CurrentPassword, model.NewPassword);
            //var result = await UserManager.ChangePasswordAsync(user.Id, AESEncrytDecry.DecryptStringAES(model.CurrentPassword).Replace(secConst.cSalt, ""), AESEncrytDecry.DecryptStringAES(model.NewPassword).Replace(secConst.cSalt, ""));
            //if (result.Succeeded)
            //{
            //    string[] myCookies = Request.Cookies.AllKeys;
            //    //var user = await UserManager.FindByNameAsync(User.Identity.Name);
            //    try
            //    {
            //        if (user != null)
            //        {
            //            UserActivityHelper.SaveUserActivity("Change Password by user  " + User.Identity.Name, Request.Url.ToString());
            //            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //            await UserManager.UpdateSecurityStampAsync(user.Id);
            //            Session.Abandon();
            //        }

            //        foreach (string cookie in myCookies)
            //        {
            //            Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //        throw;
            //    }
            //    foreach (string cookie in myCookies)
            //    {
            //        Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            //    }
            //    Session.Abandon();
            //    return RedirectToAction("ChangeUserPasswordConfirmation", "Home", new { Area = "" });
            //    //return RedirectToAction("ChangeUserPasswordConfirmation");
            //}
            //AddErrors(result);
            //return View();
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        public ActionResult ChangeUserPasswordConfirmation()
        {
            return View();
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        #endregion
    }
}