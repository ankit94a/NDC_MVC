using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Infrastructure.Helpers.Account;
using NDCWeb.Models;
using NDCWeb.Persistence;
using NDCWeb.View_Models;
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
    [EncryptedActionParameter]
    //[UserMenu(MenuArea = "Staff")]
    public class ParticipantController : Controller
    {
        // GET: Staff/Participant
        public async Task<ActionResult> ParticipantList()
        {
           
            string UserName= ((ClaimsIdentity)User.Identity).FindFirst("FName").Value;
            UserActivityHelper.SaveUserActivity("Participant List accessed by  " + UserName, Request.Url.ToString());
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Courses = uow.CourseRepo.GetCourses();
                //var personalDetail = uow.CrsMbrPersonalRepo.Find(x => x.CreatedBy != null);
                //var config = new MapperConfiguration(cfg =>
                //{
                //    cfg.CreateMap<CrsMemberPersonal, ParticipantIndxVM>();
                //});
                //IMapper mapper = config.CreateMapper();
                //var participants = mapper.Map<IEnumerable<CrsMemberPersonal>, List<ParticipantIndxVM>>(personalDetail);
                var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                ViewBag.CurCourseId = course.CourseId;
                var participants = await uow.CrsMbrPersonalRepo.GetCourseMemberVerifiedListAsync(course.CourseId);
                return View(participants.OrderByDescending(x => x.CourseMemberId).ToList());
            }
        }
        public async Task<ActionResult> CourseParticipantList(int Id)
        {

            string UserName = ((ClaimsIdentity)User.Identity).FindFirst("FName").Value;
            UserActivityHelper.SaveUserActivity("Participant List accessed by  " + UserName, Request.Url.ToString());
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Courses = uow.CourseRepo.GetCourses();
                
                //var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                ViewBag.CurCourseId = Id;
                var participants = await uow.CrsMbrPersonalRepo.GetCourseMemberVerifiedListAsync(Id);
                return View(participants.OrderByDescending(x => x.CourseMemberId).ToList());
            }
        }
        [EncryptedActionParameter]
        public ActionResult ParticipantBio(int id)
        {
            MemberCompletePreviewVM objCompletePreview = new MemberCompletePreviewVM();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var personalDetail = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == id.ToString(), np => np.OfficeStates, np2 => np2.OfficeStates.Countries);
                var appointment = uow.CrsMbrAppointmentRepo.FirstOrDefault(x => x.CreatedBy == id.ToString());
                var qualification = uow.CrsMbrQualificationRepo.Find(x => x.CreatedBy == id.ToString());
                var countryVisits = uow.CountryVisitRepo.Find(x => x.CreatedBy == id.ToString());
                var languages = uow.CrsMbrLanguageRepo.Find(x => x.CreatedBy == id.ToString());
                var asgnmntAppointments = uow.AsgmtAppointmentRepo.Find(x => x.CreatedBy == id.ToString());
                var visa = uow.VisaDetailRepo.FirstOrDefault(x => x.CreatedBy == id.ToString());
                var accountinfo = uow.AccountInfoRepo.FirstOrDefault(x => x.CreatedBy == id.ToString());
                var tally = uow.TallyDetailRepo.FirstOrDefault(x => x.CreatedBy == id.ToString());
                var vehicleSticker = uow.CrsMbrVehicleStickerRepo.Find(x => x.CreatedBy == id.ToString());
                var spouse = uow.CrsMbrSpouseRepo.FirstOrDefault(x => x.CreatedBy == id.ToString(), np2 => np2.iSpouseLanguages, np4 => np4.iSpouseQualifications);
                var passport = uow.PassportDetailRepo.FirstOrDefault(x => x.CreatedBy == id.ToString(), np => np.iChildrenPassports);
                var spouseQualification = uow.SpouseQualificationRepo.Find(x => x.CreatedBy == id.ToString());
                var spouseChildren = uow.SpouseChildrenRepo.Find(x => x.CreatedBy == id.ToString());
                var spouseLanguage = uow.SpouseLanguageRepo.Find(x => x.CreatedBy == id.ToString());


                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CrsMemberPersonal, CrsMemberPersonalIndxVM>();
                    cfg.CreateMap<CrsMbrAppointment, CrsMbrAppointmentIndxVM>();

                    cfg.CreateMap<CrsMbrQualification, CrsMbrQualificationIndxVM>();
                    cfg.CreateMap<CountryVisit, CountryVisitIndxVM>();
                    cfg.CreateMap<CrsMbrLanguage, CrsMbrLanguageIndxVM>();
                    cfg.CreateMap<AsgmtAppointment, ImportantAssignmentIndxVM>();

                    cfg.CreateMap<VisaDetail, VisaDetailIndxVM>();
                    cfg.CreateMap<AccountInfo, AccountInfoIndxVM>();
                    cfg.CreateMap<TallyDetail, TallyDetailIndxVM>();
                    cfg.CreateMap<CrsMbrVehicleSticker, CrsMbrVehicleStickerIndxVM>();

                    cfg.CreateMap<CrsMbrSpouse, SpouseIndxVM>();
                    cfg.CreateMap<PassportDetail, PassportDetailIndxVM>();
                    cfg.CreateMap<SpouseChildren, ChildrenIndxVM>();
                });

                IMapper mapper = config.CreateMapper();
                CrsMemberPersonalIndxVM personalVM = mapper.Map<CrsMemberPersonal, CrsMemberPersonalIndxVM>(personalDetail);
                CrsMbrAppointmentIndxVM appointmentVM = mapper.Map<CrsMbrAppointment, CrsMbrAppointmentIndxVM>(appointment);
                var qualificationsVM = mapper.Map<IEnumerable<CrsMbrQualification>, List<CrsMbrQualificationIndxVM>>(qualification);
                var countryVisitsVM = mapper.Map<IEnumerable<CountryVisit>, List<CountryVisitIndxVM>>(countryVisits);
                var languagesVM = mapper.Map<IEnumerable<CrsMbrLanguage>, List<CrsMbrLanguageIndxVM>>(languages);
                var importantAssignmentsVM = mapper.Map<IEnumerable<AsgmtAppointment>, List<ImportantAssignmentIndxVM>>(asgnmntAppointments);
                VisaDetailIndxVM visaVM = mapper.Map<VisaDetail, VisaDetailIndxVM>(visa);
                AccountInfoIndxVM accountInfoVM = mapper.Map<AccountInfo, AccountInfoIndxVM>(accountinfo);
                TallyDetailIndxVM tallyVM = mapper.Map<TallyDetail, TallyDetailIndxVM>(tally);
                var vehicleStickerVM = mapper.Map<IEnumerable<CrsMbrVehicleSticker>, List<CrsMbrVehicleStickerIndxVM>>(vehicleSticker);
                SpouseIndxVM spouseVM = mapper.Map<CrsMbrSpouse, SpouseIndxVM>(spouse);
                var spouseChildrensVM = mapper.Map<IEnumerable<SpouseChildren>, List<ChildrenIndxVM>>(spouseChildren);


                objCompletePreview.PersonalVM = personalVM;
                objCompletePreview.AppointmentVM = appointmentVM;
                objCompletePreview.QualificationsVM = qualificationsVM;
                objCompletePreview.CountryVisitsVM = countryVisitsVM;
                objCompletePreview.LanguagesVM = languagesVM;
                objCompletePreview.ImportantAssignmentsVM = importantAssignmentsVM;
                objCompletePreview.VisaVM = visaVM;
                objCompletePreview.AccountInfoVM = accountInfoVM;
                objCompletePreview.TallyVM = tallyVM;
                objCompletePreview.VehicleStickerVM = vehicleStickerVM;
                objCompletePreview.SpouseVM = spouseVM;
                objCompletePreview.ChildrensVM = spouseChildrensVM;
            }
            //return PartialView("Preview/_MemberCompletePreview", objCompletePreview);
            return View(objCompletePreview);
        }
        public async Task<ActionResult> LockerAllotmentPrint()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                var lockerallot = await uow.CrsMbrPersonalRepo.GetCourseMemberVerifiedListAsync(course.CourseId);
                return View(lockerallot.OrderByDescending(x => x.CourseMemberId).ToList());
            }
        }
        public async Task<ActionResult> ShowMediaFiles(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var mediaGalry = await uow.AccomodationRepo.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Accomodation, AccomodationIndexVM>();
                });
                IMapper mapper = config.CreateMapper();
                var showMediaDto = mapper.Map<Accomodation, AccomodationIndexVM>(mediaGalry);
                await uow.CommitAsync();
                //return View(indexDto);
                return PartialView("_ShowAccomodationMediaFiles", showMediaDto);
            }
        }
        [EncryptedActionParameter]
        public ActionResult AccomodationPrint(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var accomodationdatacheck = uow.AccomodationRepo.FirstOrDefault(x => x.CreatedBy == id.ToString());
                var accomodationdata = uow.AccomodationRepo.FirstOrDefault(x => x.CreatedBy == id.ToString());
                var coursereg = uow.CourseRegisterRepo.Find(x => x.UserId == id.ToString()).FirstOrDefault();
                if (accomodationdatacheck != null)
                {


                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<Accomodation, AccomodationIndexVM>();
                    });

                    ViewBag.FullName = coursereg.FirstName + " " + coursereg.MiddleName + " " + coursereg.LastName;
                    ViewBag.Rank = coursereg.Ranks.RankName;
                    ViewBag.Decoration = coursereg.Honour;
                    ViewBag.DOC = coursereg.DOCommissioning;
                    ViewBag.DOS = coursereg.SeniorityYear;
                    ViewBag.Address = coursereg.ApptLocation;
                    ViewBag.MobileNo = coursereg.MobileNo;
                    ViewBag.EmailId = coursereg.EmailId;

                    IMapper mapper = config.CreateMapper();
                    AccomodationIndexVM CreateDto = mapper.Map<Accomodation, AccomodationIndexVM>(accomodationdata);
                    return View(CreateDto);
                }
                else
                {
                    //return null;
                    this.AddNotification("Accommodation form not submitted.", NotificationType.WARNING);
                    return RedirectToAction("ParticipantList", "Participant");
                }
                    
                
                
            }
        }
        public ActionResult LockerAllotment()
        {
            using(var uow=new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Courses = uow.CourseRepo.GetCourses();
                return View();
            }
        }
        public async Task<JsonResult> LoadLockerAllotmentAdd(int pageIndex, int courseId)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                #region Old
                //var lockerAllotmentAll = await uow.LockerAllotmentRepo.GetAllAsync(fk1 => fk1.SDSStaffMasters, fk4 => fk4.CrsMemberPersonals);
                //var lockerAllotments = lockerAllotmentAll.Where(x => x.CrsMemberPersonals.CourseId == courseId).ToList();

                //var IAGList = CustomDropDownList.IAGList();
                //var SDSList = uow.StaffMasterRepo.GetSDS();

                //var participantAll = await uow.CrsMbrPersonalRepo.GetAllAsync(fk2 => fk2.Courses);
                //var courseParticipants = participantAll.Where(x => x.CourseId == courseId).ToList();

                //var participants = courseParticipants.Where(s => !lockerAllotments.Any(p => p.CourseMemberId == s.CourseMemberId)).ToList();

                //PagingVM pagingParam = new PagingVM();
                //pagingParam.PageIndex = pageIndex;
                //pagingParam.PageSize = 10;
                //pagingParam.RecordCount = participants.Count();
                //int startIndex = (pageIndex - 1) * pagingParam.PageSize;
                //var participantsFltr = participants.OrderBy(mg => mg.CourseMemberId)
                //                    .Skip(startIndex)
                //                    .Take(pagingParam.PageSize).ToList();

                //var config = new MapperConfiguration(cfg =>
                //{
                //    cfg.CreateMap<CrsMemberPersonal, LockerAllotmentAddVM>();
                //});
                //IMapper mapper = config.CreateMapper();
                //var lockerParticipants = mapper.Map<List<CrsMemberPersonal>, List<LockerAllotmentAddVM>>(participantsFltr).ToList();
                ////var config = new MapperConfiguration(cfg => cfg.CreateMap<CourseRegister, LockerAllotmentIndxVM>());
                ////IMapper mapper = config.CreateMapper();
                ////var lockerParticipants = mapper.Map<List<CourseRegister>, List<LockerAllotmentIndxVM>>(participantsFltr).ToList();
                ///
                #endregion

                //var IAGList = CustomDropDownList.IAGList();
                var SDSList = uow.StaffMasterRepo.GetSDS();
                var IAGList = uow.StaffMasterRepo.GetIAG();
                var participants1 = await uow.LockerAllotmentRepo.GetLockerAllotmentListAsync(courseId, "AddRead");
                var participants = participants1.ToList();
                PagingVM pagingParam = new PagingVM();
                pagingParam.PageIndex = pageIndex;
                pagingParam.PageSize = 10;
                pagingParam.RecordCount = participants.Count();
                int startIndex = (pageIndex - 1) * pagingParam.PageSize;
                var participantsFltr = participants.OrderBy(mg => mg.CourseMemberId)
                                    .Skip(startIndex)
                                    .Take(pagingParam.PageSize).ToList();

                var lockerParticipants = participantsFltr;
                return Json(new { participants = lockerParticipants, iagList = IAGList, sdsList = SDSList, pagingParam = pagingParam }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LockerAllotmentEdit()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Courses = uow.CourseRepo.GetCourses();
                return View();
            }
        }
        public async Task<JsonResult> LoadLockerAllotmentEdit(int pageIndex, int courseId)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                //var IAGList = CustomDropDownList.IAGList();
                var IAGList = uow.StaffMasterRepo.GetIAGAppointment();
                var SDSList = uow.StaffMasterRepo.GetSDSAppointment();
                var lockerAllotments1 = await uow.LockerAllotmentRepo.GetLockerAllotmentListAsync(courseId, "UpRead");
                var lockerAllotments = lockerAllotments1.ToList();
                PagingVM pagingParam = new PagingVM();
                pagingParam.PageIndex = pageIndex;
                pagingParam.PageSize = 120;
                pagingParam.RecordCount = lockerAllotments.Count();
                int startIndex = (pageIndex - 1) * pagingParam.PageSize;
                var lockerAllotmentsFltr = lockerAllotments;
                //.OrderBy(mg => mg.LockerNo.ToInt32())
                                    //.Skip(startIndex)
                                    //.Take(pagingParam.PageSize).ToList();

                var lockerParticipants = lockerAllotmentsFltr;
                //return Json(new { lockerAllotments = lockerParticipants, iagList = IAGList, sdsList = SDSList, pagingParam = pagingParam }, JsonRequestBehavior.AllowGet);
                return Json(new { lockerAllotments = lockerParticipants, iagList = IAGList, sdsList = SDSList }, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> ArrivalAll()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                int CourseId = course.CourseId;
                var arrivaldataall = await uow.ArrivalDetailRepo.GetViewArrivalAllInfoAsync(CourseId);
                return View(arrivaldataall);
            }
        }
        public ActionResult ArrivalIndex(string uId)
        {
            ArrivalCompleteVM objCompletePreview = new ArrivalCompleteVM();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var arrivaldetail = uow.ArrivalDetailRepo.FirstOrDefault(x => x.CreatedBy == uId, fk => fk.iArrivalMeals, fk2 => fk2.iArrivalAccompanied);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ArrivalDetail, ArrivalIndexVM>();
                });
                IMapper mapper = config.CreateMapper();
                ArrivalIndexVM IndexDto = mapper.Map<ArrivalDetail, ArrivalIndexVM>(arrivaldetail);
                var personal = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == uId);
                var appointment = uow.CrsMbrAppointmentRepo.FirstOrDefault(x => x.CreatedBy == uId, fk => fk.Ranks);
                IndexDto.FullName = appointment.Ranks.RankName + " " + personal.FirstName + " " + personal.MiddleName + " " + personal.Surname;
                IndexDto.HouseNo = personal.OfficeHouseNo;
                IndexDto.MobileNo = personal.MobileNo;
                return View(IndexDto);
            }
        }

        #region DS Staff
        public ActionResult CrsMbrRoleAssignment()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Courses = uow.CourseRepo.GetCourses();
                return View();
            }
        }
        #endregion

        #region Accommodation
        public ActionResult Accommodation(string id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var accomodationdata = uow.AccomodationRepo.FirstOrDefault(x => x.CreatedBy == id);
                
                    var coursereg = uow.CourseRegisterRepo.Find(x => x.UserId == id).FirstOrDefault();
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<Accomodation, AccomodationIndexVM>();
                    });

                    ViewBag.FullName = coursereg.FirstName + " " + coursereg.MiddleName + " " + coursereg.LastName;
                    ViewBag.Rank = coursereg.Ranks.RankName;
                    ViewBag.Decoration = coursereg.Honour;
                    ViewBag.DOC = coursereg.DOCommissioning;
                    ViewBag.DOS = coursereg.SeniorityYear;
                    ViewBag.Address = coursereg.ApptLocation;
                    ViewBag.MobileNo = coursereg.MobileNo;
                    ViewBag.EmailId = coursereg.EmailId;

                    IMapper mapper = config.CreateMapper();
                    AccomodationIndexVM CreateDto = mapper.Map<Accomodation, AccomodationIndexVM>(accomodationdata);
                    return View(CreateDto);
                
            }
        }
        [EncryptedActionParameter]

        public ActionResult TrainingSection(int id)
        {
            
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var personalDetail = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == id.ToString());
                var appointment = uow.CrsMbrAppointmentRepo.FirstOrDefault(x => x.CreatedBy == id.ToString());
                var spouse = uow.CrsMbrSpouseRepo.FirstOrDefault(x => x.CreatedBy == id.ToString());

                #region Comment Old code
                //MemberCompletePreviewVM objCompletePreview = new MemberCompletePreviewVM();
                //var config = new MapperConfiguration(cfg =>
                //{
                //    cfg.CreateMap<CrsMemberPersonal, CrsMemberPersonalIndxVM>();
                //    cfg.CreateMap<CrsMbrAppointment, CrsMbrAppointmentIndxVM>();
                //    cfg.CreateMap<CrsMbrSpouse, SpouseIndxVM>();                   
                //});

                //IMapper mapper = config.CreateMapper();
                //CrsMemberPersonalIndxVM personalVM = mapper.Map<CrsMemberPersonal, CrsMemberPersonalIndxVM>(personalDetail);
                //CrsMbrAppointmentIndxVM appointmentVM = mapper.Map<CrsMbrAppointment, CrsMbrAppointmentIndxVM>(appointment);
                //SpouseIndxVM spouseVM = mapper.Map<CrsMbrSpouse, SpouseIndxVM>(spouse);

                //objCompletePreview.PersonalVM = personalVM;
                //objCompletePreview.AppointmentVM = appointmentVM;
                //objCompletePreview.SpouseVM = spouseVM;
                #endregion

                TrainingSectionVM objtrainingSectionVM = new TrainingSectionVM();
                if (personalDetail != null)
                {
                    objtrainingSectionVM.FirstName = personalDetail.FirstName;
                    objtrainingSectionVM.MiddleName = personalDetail.MiddleName;
                    objtrainingSectionVM.Surname = personalDetail.Surname;
                    objtrainingSectionVM.MobileNo = personalDetail.MobileNo;
                    objtrainingSectionVM.AlternateMobileNo = personalDetail.AlternateMobileNo;
                    objtrainingSectionVM.EmailId = personalDetail.EmailId;
                    objtrainingSectionVM.AlternateEmailId = personalDetail.AlternateEmailId;
                    objtrainingSectionVM.Height = personalDetail.Height;
                    objtrainingSectionVM.MemberPassportNo = personalDetail.MemberPassportNo;
                    objtrainingSectionVM.MemberPassportValidUpto = personalDetail.MemberPassportValidUpto;
                    objtrainingSectionVM.VisaNo = personalDetail.VisaNo;
                    objtrainingSectionVM.VisaValidUpto = personalDetail.VisaValidUpto;
                }
                if (appointment != null)
                {
                    objtrainingSectionVM.DOJoining = appointment.DOJoining;
                    objtrainingSectionVM.DOSeniority = appointment.DOSeniority;
                }
                if (spouse != null)
                {
                    objtrainingSectionVM.SpouseName = spouse.SpouseName;
                }
                return View(objtrainingSectionVM);
            }
        }
        #endregion

        #region Library
        public ActionResult Library(string id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var personalDetail = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == id);
                var appointment = uow.CrsMbrAppointmentRepo.FirstOrDefault(x => x.CreatedBy == id);
                var lockerAllotment = uow.LockerAllotmentRepo.FirstOrDefault(x => x.CourseMemberId == personalDetail.CourseMemberId);

                
                LibraryMembershipIndxVM objLibmembership = new LibraryMembershipIndxVM();
                objLibmembership.MemberName = appointment.Ranks.RankName + " " + personalDetail.FirstName + " " + personalDetail.MiddleName + " " + personalDetail.Surname;
                //objLibmembership.r = coursereg.Ranks.RankName;
                objLibmembership.Designation = appointment.Designation;
                objLibmembership.LockerNo = lockerAllotment.LockerNo;
                objLibmembership.Address = personalDetail.OfficeHouseNo + " " + personalDetail.OfficeArea + " " + personalDetail.OfficeCity + " " + personalDetail.OfficeStates.StateName;
                objLibmembership.MobileNo = personalDetail.MobileNo;
                objLibmembership.EmailId = personalDetail.EmailId;

                
                return View(objLibmembership);
            }
        }
        #endregion

        #region Alumni
        public ActionResult AlumniList()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                //var personalDetail = uow.AlumniRepo.Find(x => x.CreatedBy != null);
                var personalDetail = uow.AlumniRepo.GetAll();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<Models.AlumniMaster>, List<AlumniIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var participants = mapper.Map<IEnumerable<Models.AlumniMaster>, List<AlumniIndxVM>>(personalDetail);
                return View(participants);
            }
        }
        #endregion

        #region SDS
        public async Task<ActionResult> IAGCourseMemberList()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var staff = uow.StaffMasterRepo.Find(x => x.LoginUserId == uId, fk => fk.Faculties).FirstOrDefault();
                var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                var participants = await uow.CrsMbrPersonalRepo.GetIAGSDSCourseMemberListAsync(staff.StaffId, course.CourseId);
                return View(participants);
            }
        }
        #endregion
        
    }
}
