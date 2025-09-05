using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Services.Description;

namespace NDCWeb.api
{
    [RoutePrefix("api/courseMembers")]
    [Authorize]
    public class CourseMemberApiController : ApiController
    {
        #region Get Methods
        // GET: api/CourseMember
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CourseMember/5
        public string Get(int id)
        {
            return "value";
        }

        //[HttpGet]
        //[Route("GetMyCountryVisits")]
        //public IEnumerable<CountryVisit> GetMyCountryVisits()
        //{
        //    string uId = User.Identity.GetUserId();
        //    using (var uow = new UnitOfWork(new NDCWebContext()))
        //    {
        //        return uow.CountryVisitRepo.Find(x => x.CreatedBy == uId);
        //    }
        //}
        #endregion

        #region Post Methods
        // POST: api/CourseMember
        [HttpPost]
        [Route("Register")]
        public async Task<HttpResponseMessage> Register([FromBody] CourseMember courseMember)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (courseMember == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.CourseMemberRepo.Add(courseMember);
                    await uow.CommitAsync();

                    var message = Request.CreateResponse(HttpStatusCode.Created, courseMember);
                    //message.Headers.Location = new Uri(Request.RequestUri +"/"+ courseMember.CourseMemberId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("Personal")]
        public async Task<HttpResponseMessage> Personal([FromBody] CrsMemberPersonalCrtVM personal)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    //to store user with current course
                    var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                    if (personal == null || course == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<CrsMemberPersonalCrtVM, CrsMemberPersonal>();
                       
                    });
                    IMapper mapper = config.CreateMapper();
                    CrsMemberPersonal UpdateDto = mapper.Map< CrsMemberPersonalCrtVM, CrsMemberPersonal>(personal);

                    personal.CourseId = course.CourseId;
                    uow.CrsMbrPersonalRepo.Add(UpdateDto);
                    await uow.CommitAsync();

                    var message = Request.CreateResponse(HttpStatusCode.Created);
                    //message.Headers.Location = new Uri(Request.RequestUri +"/"+ courseMember.CourseMemberId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPost]
        [Route("ServiceMultiple")]
        public async Task<HttpResponseMessage> ServiceMultiple([FromBody] ServiceMultipleAddVM services)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (services == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.CrsMbrAppointmentRepo.Add(services.Appointment);
                    uow.CrsMbrQualificationRepo.AddRange(services.Qualifications);
                    uow.CountryVisitRepo.AddRange(services.CountryVisits);
                    uow.CrsMbrLanguageRepo.AddRange(services.Languages);
                    uow.AsgmtAppointmentRepo.AddRange(services.ImportantAssignments);
                    await uow.CommitAsync();

                    var message = Request.CreateResponse(HttpStatusCode.Created);
                    //message.Headers.Location = new Uri(Request.RequestUri +"/"+ courseMember.CourseMemberId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("Appointment")]
        public async Task<HttpResponseMessage> Appointment([FromBody] CrsMbrAppointment appointment)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (appointment == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.CrsMbrAppointmentRepo.Add(appointment);

                    var crsRegister = uow.CourseRegisterRepo.FirstOrDefault(x => x.UserId == appointment.CreatedBy);
                    crsRegister.RankId = appointment.RankId;
                    await uow.CommitAsync();

                    var message = Request.CreateResponse(HttpStatusCode.Created);
                    //message.Headers.Location = new Uri(Request.RequestUri +"/"+ courseMember.CourseMemberId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("Address")]
        public HttpResponseMessage Address([FromBody] CrsMbrAddressCrtVM  address)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (address == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");
                    //CrsMbrAddress
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<CrsMbrAddressCrtVM, CrsMbrAddress>();

                    });
                    IMapper mapper = config.CreateMapper();
                    CrsMbrAddress UpdateDto = mapper.Map<CrsMbrAddressCrtVM, CrsMbrAddress>(address);
                    uow.CrsMbrAddressRepo.Add(UpdateDto);
                    uow.Commit();

                    var message = Request.CreateResponse(HttpStatusCode.Created, address);
                    //message.Headers.Location = new Uri(Request.RequestUri +"/"+ courseMember.CourseMemberId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("Qualification")]
        public HttpResponseMessage Qualification([FromBody] List<CrsMbrQualification> qualifications)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (qualifications == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.CrsMbrQualificationRepo.AddRange(qualifications);
                    uow.Commit();

                    var message = Request.CreateResponse(HttpStatusCode.Created);
                    //message.Headers.Location = new Uri(Request.RequestUri +"/"+ courseMember.CourseMemberId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("CountryVisit")]
        public HttpResponseMessage CountryVisit([FromBody] List<CountryVisit> countryVisits)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (countryVisits == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.CountryVisitRepo.AddRange(countryVisits);
                    uow.Commit();

                    var message = Request.CreateResponse(HttpStatusCode.Created);
                    //message.Headers.Location = new Uri(Request.RequestUri +"/"+ courseMember.CourseMemberId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("LanguageKnown")]
        public HttpResponseMessage LanguageKnown([FromBody] List<CrsMbrLanguage> languages)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (languages == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.CrsMbrLanguageRepo.AddRange(languages);
                    uow.Commit();

                    var message = Request.CreateResponse(HttpStatusCode.Created);
                    //message.Headers.Location = new Uri(Request.RequestUri +"/"+ courseMember.CourseMemberId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("ImportantAssignment")]
        public HttpResponseMessage ImportantAssignment([FromBody] List<AsgmtAppointment> assignments)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (assignments == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.AsgmtAppointmentRepo.AddRange(assignments);
                    uow.Commit();

                    var message = Request.CreateResponse(HttpStatusCode.Created);
                    //message.Headers.Location = new Uri(Request.RequestUri +"/"+ courseMember.CourseMemberId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("Biography")]
        public HttpResponseMessage Biography([FromBody] CrsMbrBiography biography)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (biography == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.CrsMbrBiographyRepo.Add(biography);
                    uow.Commit();

                    var message = Request.CreateResponse(HttpStatusCode.Created, biography);
                    //message.Headers.Location = new Uri(Request.RequestUri +"/"+ biography.CourseMemberId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("Spouse")]
        public async Task<HttpResponseMessage> Spouse([FromBody] CrsMbrSpouse spouse)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (spouse == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.CrsMbrSpouseRepo.Add(spouse);
                    await uow.CommitAsync();

                    var message = Request.CreateResponse(HttpStatusCode.Created, spouse);
                    //message.Headers.Location = new Uri(Request.RequestUri +"/"+ courseMember.CourseMemberId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("SpouseChildren")]
        public HttpResponseMessage SpouseChildren([FromBody] List<SpouseChildren> spouseChildrens)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (spouseChildrens == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.SpouseChildrenRepo.AddRange(spouseChildrens);
                    uow.Commit();
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("SpouseLanguage")]
        public HttpResponseMessage SpouseLanguage([FromBody] List<SpouseLanguage> spouseLanguages)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (spouseLanguages == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.SpouseLanguageRepo.AddRange(spouseLanguages);
                    uow.Commit();
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPost]
        [Route("SpouseQualification")]
        public HttpResponseMessage SpouseQualification([FromBody] List<SpouseQualification> spouseQualifications)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (spouseQualifications == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.SpouseQualificationRepo.AddRange(spouseQualifications);
                    uow.Commit();
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("Children")]
        public HttpResponseMessage Children([FromBody] SpouseChildren spouseChildrens)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (spouseChildrens == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.SpouseChildrenRepo.Add(spouseChildrens);
                    uow.Commit();
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("Passport")]
        public async Task<HttpResponseMessage> Passport([FromBody] PassportDetail passport)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (passport == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.PassportDetailRepo.Add(passport);
                    await uow.CommitAsync();

                    var message = Request.CreateResponse(HttpStatusCode.Created, passport);
                    //message.Headers.Location = new Uri(Request.RequestUri +"/"+ courseMember.CourseMemberId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("ChildrenPassport")]
        public HttpResponseMessage ChildrenPassport([FromBody] List<ChildrenPassport> childrenPassport)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (childrenPassport == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.ChildrenPassportRepo.AddRange(childrenPassport);
                    uow.Commit();
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("Visa")]
        public async Task<HttpResponseMessage> Visa([FromBody] VisaDetail visa)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (visa == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.VisaDetailRepo.Add(visa);
                    await uow.CommitAsync();

                    var message = Request.CreateResponse(HttpStatusCode.Created, visa);
                    //message.Headers.Location = new Uri(Request.RequestUri +"/"+ courseMember.CourseMemberId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("AccountInfo")]
        public HttpResponseMessage AccountInfo([FromBody] AccountInfoCrtVM accountInfo)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (accountInfo == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<AccountInfoCrtVM, AccountInfo>();

                    });
                    IMapper mapper = config.CreateMapper();
                    AccountInfo UpdateDto = mapper.Map<AccountInfoCrtVM, AccountInfo>(accountInfo);
                    uow.AccountInfoRepo.Add(UpdateDto);

                    uow.Commit();

                    var message = Request.CreateResponse(HttpStatusCode.Created, accountInfo);
                    //message.Headers.Location = new Uri(Request.RequestUri +"/"+ courseMember.CourseMemberId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("Tally")]
        public async Task<HttpResponseMessage> Tally([FromBody] TallyVehicleAddVM tallyVehicles)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (tallyVehicles == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.TallyDetailRepo.Add(tallyVehicles.Tally);
                    uow.CrsMbrVehicleStickerRepo.AddRange(tallyVehicles.VehicleStickers);
                    await uow.CommitAsync();

                    var message = Request.CreateResponse(HttpStatusCode.Created);
                    //message.Headers.Location = new Uri(Request.RequestUri +"/"+ courseMember.CourseMemberId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPost]
        [Route("VehicleSticker")]
        public HttpResponseMessage VehicleSticker([FromBody] List<CrsMbrVehicleSticker> vehicleStickers)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (vehicleStickers == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.CrsMbrVehicleStickerRepo.AddRange(vehicleStickers);
                    uow.Commit();

                    return Request.CreateResponse(HttpStatusCode.Created);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPost]
        [Route("Arrival")]
        public async Task<HttpResponseMessage> Arrival([FromBody] ArrivalDetail arrival)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (arrival == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.ArrivalDetailRepo.Add(arrival);
                    await uow.CommitAsync();

                    var message = Request.CreateResponse(HttpStatusCode.Created, arrival);
                    //message.Headers.Location = new Uri(Request.RequestUri +"/"+ courseMember.CourseMemberId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("Ration")]
        public async Task<HttpResponseMessage> Ration([FromBody] ServiceRation ration)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (ration == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.ServiceRationRepo.Add(ration);
                    await uow.CommitAsync();

                    var message = Request.CreateResponse(HttpStatusCode.Created, ration);
                    //message.Headers.Location = new Uri(Request.RequestUri +"/"+ courseMember.CourseMemberId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("Rakshika")]
        public async Task<HttpResponseMessage> Rakshika([FromBody] Rakshika rakshika)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (rakshika == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.RakshikaRepo.Add(rakshika);
                    await uow.CommitAsync();

                    var message = Request.CreateResponse(HttpStatusCode.Created, rakshika);
                    //message.Headers.Location = new Uri(Request.RequestUri +"/"+ courseMember.CourseMemberId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        #endregion

        #region Put Methods
        // PUT: api/CourseMember/5
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpPost]
        [Route("PersonalEdit/{id}")]
        public HttpResponseMessage PersonalEdit(int id, [FromBody] CrsMemberPersonalUpVM personal)
        {
            try
            {
                
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var entity = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CourseMemberId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    else
                    {
                        entity.FirstName = personal.FirstName;
                        entity.MiddleName = personal.MiddleName;
                        entity.Surname = personal.Surname;
                        entity.FatherName = personal.FatherName;
                        entity.FatherMiddleName = personal.FatherMiddleName;
                        entity.FatherSurname = personal.FatherSurname;
                        entity.MotherName = personal.MotherName;
                        entity.MotherMiddleName = personal.MotherMiddleName;
                        entity.MotherSurname = personal.MotherSurname;
                        entity.NickName = personal.NickName;

                        entity.Gender = personal.Gender;
                        entity.MaritalStatus = personal.MaritalStatus;
                        //entity.CitizenshipCountry = personal.CitizenshipCountry;
                        entity.DOBirth = personal.DOBirth;
                        entity.DOMarriage = personal.DOMarriage;
                        entity.EmailId = personal.EmailId;
                        entity.AlternateEmailId = personal.AlternateEmailId;
                        entity.MobileNo = personal.MobileNo;
                        entity.AlternateMobileNo = personal.AlternateMobileNo;
                        entity.IndentificationMark = personal.IndentificationMark;
                        entity.BloodGroup = personal.BloodGroup;
                        entity.PANCardNo = personal.PANCardNo;
                        entity.VoterIdNo = personal.VoterIdNo;
                        entity.StayBySpouse = personal.StayBySpouse;
                        entity.Undertaking = personal.Undertaking;
                        entity.Height = personal.Height;
                        entity.CommunicationAddress = personal.CommunicationAddress;
                        entity.OfficeHouseNo = personal.OfficeHouseNo;
                        entity.OfficePremisesName = personal.OfficePremisesName;
                        entity.OfficeStreet = personal.OfficeStreet;
                        entity.OfficeArea = personal.OfficeArea;
                        entity.OfficeCity = personal.OfficeCity;
                        entity.OfficeZipCode = personal.OfficeZipCode;
                        //entity.ResidentHouseNo = personal.ResidentHouseNo;
                        //entity.ResidentPremisesName = personal.ResidentPremisesName;
                        //entity.ResidentStreet = personal.ResidentStreet;
                        //entity.ResidentArea = personal.ResidentArea;
                        //entity.ResidentCity = personal.ResidentCity;
                        //entity.ResidentZipCode = personal.ResidentZipCode;
                        entity.BioSketch = personal.BioSketch;

                        entity.MemberImgPath = personal.MemberImgPath;
                        entity.JointImgPath = personal.JointImgPath;
                        entity.AadhaarPath = personal.AadhaarPath;
                        entity.OfficeStateId = personal.OfficeStateId;
                        //entity.ResidentStateId = personal.ResidentStateId;
                        entity.CitizenshipCountryId = personal.CitizenshipCountryId;
                        entity.DietaryPref = personal.DietaryPref;
                        entity.MedicalCategory = personal.MedicalCategory;

                        entity.SpouseName = personal.SpouseName;
                        entity.NOK = personal.NOK;

                        entity.HoldingPassport = personal.HoldingPassport;
                        entity.MemberPassportNo = personal.MemberPassportNo;
                        entity.MemberPassportName = personal.MemberPassportName;
                        entity.MemberPassportIssueDate = personal.MemberPassportIssueDate; 
                        entity.MemberPassportValidUpto = personal.MemberPassportValidUpto;
                        entity.MemberPassportType = personal.MemberPassportType;
                        entity.CountryIssued = personal.CountryIssued;
                        entity.MemberPassportImgPath = personal.MemberPassportImgPath;
                        entity.MemberPassportBackImgPath = personal.MemberPassportBackImgPath;
                        entity.VisaNo = personal.VisaNo;
                        entity.VisaIssueDate = personal.VisaIssueDate;
                        entity.VisaValidUpto = personal.VisaValidUpto; ;
                        entity.VisaPath = personal.VisaPath;
                        entity.SelfFRRONo = personal.SelfFRRONo;
                        entity.SelfIssueDate = personal.SelfIssueDate;
                        entity.SelfValidUpto = personal.SelfValidUpto;
                        entity.SelfFRROPath = personal.SelfFRROPath;
                        entity.HoldingPersonalPassportSelf = personal.HoldingPersonalPassportSelf;
                        if (personal.HoldingPersonalPassportSelf == true)
                        {
                            entity.MemberPersonalPassportNo = personal.MemberPersonalPassportNo;
                            entity.MemberPersonalPassportName = personal.MemberPersonalPassportName;
                            entity.MemberPersonalPassportIssueDate = personal.MemberPersonalPassportIssueDate;
                            entity.MemberPersonalPassportValidUpto = personal.MemberPersonalPassportValidUpto;
                            entity.CountryIssuedPersonalPassport = personal.CountryIssuedPersonalPassport;
                            entity.MemberPersonalPassportImgPath = personal.MemberPersonalPassportImgPath;
                            entity.MemberPersonalPassportBackImgPath = personal.MemberPersonalPassportBackImgPath;
                        }
                        //if (ModelState.IsValid)
                        //{
                            uow.Commit();
                            var message = Request.CreateResponse(HttpStatusCode.Created);
                            return message;
                        //}
                        //else
                        //{
                        //    var message = Request.CreateResponse(HttpStatusCode.BadRequest);
                        //    return message;
                        //}

                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("AppointmentEdit/{id}")]
        public HttpResponseMessage AppointmentEdit(int id, [FromBody] CrsMbrAppointment appointment)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var entity = uow.CrsMbrAppointmentRepo.FirstOrDefault(x => x.AppointmentId == id, np => np.Ranks);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    else
                    {
                        entity.Designation = appointment.Designation;
                        entity.Organisation = appointment.Organisation;
                        entity.Location = appointment.Location;
                        entity.DOJoining = appointment.DOJoining;
                        entity.DOSeniority = appointment.DOSeniority;
                        entity.ServiceNo = appointment.ServiceNo;
                        entity.Service = appointment.Service;
                        entity.Branch = appointment.Branch;
                        entity.RankId = appointment.RankId;

                        entity.WorkingAsDAMA = appointment.WorkingAsDAMA;
                        entity.WorkingAsDAMADetails = appointment.WorkingAsDAMADetails;

                        var crsRegister = uow.CourseRegisterRepo.FirstOrDefault(x => x.UserId == entity.CreatedBy);
                        crsRegister.RankId = appointment.RankId;
                        uow.Commit();

                        var message = Request.CreateResponse(HttpStatusCode.Created);
                        return message;
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("AddressEdit/{id}")]
        public HttpResponseMessage AddressEdit(int id, [FromBody] CrsMbrAddress address)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var entity = uow.CrsMbrAddressRepo.FirstOrDefault(x => x.MemberAddressId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    else
                    {
                        entity.CurrentAddress = address.CurrentAddress;
                        entity.CurrentTelephone = address.CurrentTelephone;
                        entity.CurrentFax = address.CurrentFax;
                        entity.PermanentAddress = address.PermanentAddress;
                        entity.PermanentTelephone = address.PermanentTelephone;
                        entity.PermanentFax = address.PermanentFax;
                        entity.OffcTelephone = address.OffcTelephone;
                        entity.City = address.City;
                        entity.ZipCode = address.ZipCode;
                        entity.StateId = address.StateId;

                        uow.Commit();
                        var message = Request.CreateResponse(HttpStatusCode.Created);
                        return message;
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("BiographyEdit/{id}")]
        public HttpResponseMessage BiographyEdit(int id, [FromBody] CrsMbrBiography biography)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var entity = uow.CrsMbrBiographyRepo.FirstOrDefault(x => x.BiographyId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    else
                    {
                        entity.PenPicture = biography.PenPicture;
                        entity.FamilyBackground = biography.FamilyBackground;
                        entity.EarlySchooling = biography.EarlySchooling;
                        entity.AcademicAchievement = biography.AcademicAchievement;
                        entity.PersonalValueSystem = biography.PersonalValueSystem;

                        uow.Commit();
                        return Request.CreateResponse(HttpStatusCode.Created);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("SpouseEdit/{id}")]
        public HttpResponseMessage SpouseEdit(int id, [FromBody] CrsMbrSpouse spouse)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var entity = uow.CrsMbrSpouseRepo.FirstOrDefault(x => x.SpouseId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    else
                    {
                        entity.SpouseName = spouse.SpouseName;
                        entity.SpouseBloodGroup = spouse.SpouseBloodGroup;
                        entity.ContactNo = spouse.ContactNo;
                        entity.SpouseDOBirth = spouse.SpouseDOBirth;
                        entity.Occupation = spouse.Occupation;
                        entity.SpouseStay = spouse.SpouseStay;

                        //entity.FoodPreference = spouse.FoodPreference;
                        //entity.EmailId = spouse.EmailId;
                        //entity.Biography = spouse.Biography;
                        //entity.Qualification = spouse.Qualification;
                        entity.EduHigher = spouse.EduHigher;
                        entity.EduSubject = spouse.EduSubject;
                        entity.EduDivision = spouse.EduDivision;
                        entity.EduUniversity = spouse.EduUniversity;
                        //entity.iSpouseChildrens = spouse.iSpouseChildrens;
                        //entity.iSpouseLanguages = spouse.iSpouseLanguages;
                        entity.HoldingPassport = spouse.HoldingPassport;
                        entity.SpousePassportNo = spouse.SpousePassportNo;
                        entity.SpousePassportName = spouse.SpousePassportName;
                        entity.SpousePassportIssueDate = spouse.SpousePassportIssueDate;
                        entity.SpousePassportValidUpto = spouse.SpousePassportValidUpto;
                        entity.SpousePassportType = spouse.SpousePassportType;
                        entity.SpousePassportCountryIssued = spouse.SpousePassportCountryIssued;
                        entity.SpousePassportImgPath = spouse.SpousePassportImgPath;
                        entity.SpousePassportBackImgPath = spouse.SpousePassportBackImgPath;
                        entity.SpouseVisaNo = spouse.SpouseVisaNo;
                        entity.SpouseVisaIssueDate = spouse.SpouseVisaIssueDate;
                        entity.SpouseVisaValidUpto = spouse.SpouseVisaValidUpto;
                        entity.SpouseVisaPath = spouse.SpouseVisaPath;
                        entity.SpouseFRRONo = spouse.SpouseFRRONo;
                        entity.SpouseFRROIssueDate = spouse.SpouseFRROIssueDate;
                        entity.SpouseFRROValidUpto = spouse.SpouseFRROValidUpto;
                        entity.SpouseFRROPath = spouse.SpouseFRROPath;

                        uow.Commit();
                        return Request.CreateResponse(HttpStatusCode.Created);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("ChildrenEdit/{id}")]
        public HttpResponseMessage ChildrenEdit(int id, [FromBody] SpouseChildren child)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var entity = uow.SpouseChildrenRepo.FirstOrDefault(x => x.ChildId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    else
                    {
                        entity.ChildName = child.ChildName;
                        entity.ChildGender = child.ChildGender;
                        entity.ChildDOBirth = child.ChildDOBirth;
                        entity.ChildOccupation = child.ChildOccupation;
                        entity.ChildContactNo = child.ChildContactNo;
                        entity.ChildStayWithMember = child.ChildStayWithMember;
                        entity.ChildPassportNo = child.ChildPassportNo;
                        entity.ChildPassportName = child.ChildPassportName;
                        entity.ChildPassportIssueDate = child.ChildPassportIssueDate;
                        entity.ChildPassportValidUpto = child.ChildPassportValidUpto;
                        entity.ChildPassportType = child.ChildPassportType;
                        entity.ChildPassportCountryIssued = child.ChildPassportCountryIssued;
                        entity.ChildPassportImgPath = child.ChildPassportImgPath;
                        entity.ChildPassportBackImgPath = child.ChildPassportBackImgPath;
                        entity.ChildVisaNo = child.ChildVisaNo;
                        entity.ChildVisaIssueDate = child.ChildVisaIssueDate;
                        entity.ChildVisaValidUpto = child.ChildVisaValidUpto;
                        entity.ChildVisaPath = child.ChildVisaPath;

                        uow.Commit();
                        return Request.CreateResponse(HttpStatusCode.Created);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("PassportEdit/{id}")]
        public HttpResponseMessage PassportEdit(int id, [FromBody] PassportDetail passport)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var entity = uow.PassportDetailRepo.FirstOrDefault(x => x.PassportId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    else
                    {
                        entity.HoldingPassport = passport.HoldingPassport;
                        entity.MemberPassportNo = passport.MemberPassportNo;
                        entity.MemberPassportIssueDate = passport.MemberPassportIssueDate;
                        entity.MemberPassportValidUpto = passport.MemberPassportValidUpto;
                        entity.MemberPassportType = passport.MemberPassportType;
                        entity.SpousePassportNo = passport.SpousePassportNo;
                        entity.SpousePassportIssueDate = passport.SpousePassportIssueDate;
                        entity.SpousePassportValidUpto = passport.SpousePassportValidUpto;
                        entity.SpousePassportType = passport.SpousePassportType;
                        entity.MemberPassportImgPath = passport.MemberPassportImgPath;
                        entity.SpousePassportImgPath = passport.SpousePassportImgPath;
                        //entity.iChildrenPassports = passport.iChildrenPassports;

                        uow.Commit();
                        return Request.CreateResponse(HttpStatusCode.Created);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("VisaEdit/{id}")]
        public HttpResponseMessage VisaEdit(int id, [FromBody] VisaDetail visa)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var entity = uow.VisaDetailRepo.FirstOrDefault(x => x.VisaId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    else
                    {
                        entity.VisaEntryType = visa.VisaEntryType;
                        entity.VisaNo = visa.VisaNo;
                        entity.VisaIssueDate = visa.VisaIssueDate;
                        entity.VisaValidUpto = visa.VisaValidUpto;
                        entity.SelfFRRONo = visa.SelfFRRONo;
                        entity.SelfIssueDate = visa.SelfIssueDate;
                        entity.SelfValidUpto = visa.SelfValidUpto;
                        entity.SpouseFRRONo = visa.SpouseFRRONo;
                        entity.SpouseIssueDate = visa.SpouseIssueDate;
                        entity.SpouseValidUpto = visa.SpouseValidUpto;
                        entity.VisaPath = visa.VisaPath;
                        entity.SelfFRROPath = visa.SelfFRROPath;
                        entity.SpouseFRROPath = visa.SpouseFRROPath;

                        uow.Commit();
                        return Request.CreateResponse(HttpStatusCode.Created);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("AccountInfoEdit/{id}")]
        public HttpResponseMessage AccountInfoEdit(int id, [FromBody] AccountInfoUpVM accountInfo)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var entity = uow.AccountInfoRepo.FirstOrDefault(x => x.AccInfoId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    else
                    {
                        entity.AccountNo = accountInfo.AccountNo;
                        entity.AccountType = accountInfo.AccountType;
                        entity.IFSC = accountInfo.IFSC;
                        entity.MICR = accountInfo.MICR;
                        entity.CDAAcNo = accountInfo.CDAAcNo;
                        entity.PassbookPath = accountInfo.PassbookPath;
                        entity.NameAndAddressOfBanker = accountInfo.NameAndAddressOfBanker;
                        entity.BasicPay = accountInfo.BasicPay;
                        entity.MSP = accountInfo.MSP;
                        entity.PayLevel = accountInfo.PayLevel;
                        entity.AddressOfPayAc = accountInfo.AddressOfPayAc;
                        entity.NodalOfficeName= accountInfo.NodalOfficeName;
                        entity.NodalOfficeContactNo= accountInfo.NodalOfficeContactNo;

                        entity.NodalOfficeEmail = accountInfo.NodalOfficeEmail;
                        entity.CivilServiceAcNo = accountInfo.CivilServiceAcNo;
                        entity.CivilServiceAddressOfPayAc = accountInfo.CivilServiceAddressOfPayAc;
                        entity.CivilServiceNodalOfficeName = accountInfo.CivilServiceAddressOfPayAc;
                        entity.CivilServiceNodalOfficeContactNo = accountInfo.CivilServiceNodalOfficeContactNo;
                        entity.CivilServiceNodalOfficeEmail = accountInfo.CivilServiceNodalOfficeEmail;

                        uow.Commit();
                        return Request.CreateResponse(HttpStatusCode.Created);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("TallyEdit/{id}")]
        public HttpResponseMessage TallyEdit(int id, [FromBody] TallyDetail tally)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var entity = uow.TallyDetailRepo.FirstOrDefault(x => x.TallyId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    else
                    {
                        entity.RankAbbr = tally.RankAbbr;
                        entity.PassportName = tally.PassportName;
                        entity.TabName = tally.TabName;
                        entity.NickName = tally.NickName;
                        entity.CountryService = tally.CountryService;
                        entity.NameORRank = tally.NameORRank;
                        entity.ResidentialAddress = tally.ResidentialAddress;
                        entity.MobileNo = tally.MobileNo;
                        entity.TelephoneNo = tally.TelephoneNo;
                        entity.BrandModelNo = tally.BrandModelNo;
                        entity.Colour = tally.Colour;
                        entity.RegistrationNo = tally.RegistrationNo;
                        entity.DrivingLicenseNo = tally.DrivingLicenseNo;
                        entity.NoOfVehicle = tally.NoOfVehicle;
                        entity.RegistrationCertificatePath = tally.RegistrationCertificatePath;
                        entity.DrivingLicensePath = tally.DrivingLicensePath;

                        uow.Commit();
                        return Request.CreateResponse(HttpStatusCode.Created);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("Arrival/Edit")]
        public async Task<HttpResponseMessage> ArrivalEdit([FromBody] ArrivalDetail arrival)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (arrival == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.ArrivalDetailRepo.Add(arrival);
                    await uow.CommitAsync();

                    var message = Request.CreateResponse(HttpStatusCode.Created, arrival);
                    //message.Headers.Location = new Uri(Request.RequestUri +"/"+ courseMember.CourseMemberId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        #endregion

        #region Remove Methods
        // DELETE: api/CourseMember/5
        public void Delete(int id)
        {
        }

        //[HttpDelete]
        [HttpPost]
        [Route("QualificationDelete/{id}")]
        public HttpResponseMessage QualificationDelete(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var entity = uow.CrsMbrQualificationRepo.GetById(id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    else
                    {
                        uow.CrsMbrQualificationRepo.Remove(entity);
                        uow.Commit();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //[HttpDelete]
        [HttpPost]
        [Route("CountryVisitDelete/{id}")]
        public HttpResponseMessage CountryVisitDelete(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var entity = uow.CountryVisitRepo.GetById(id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    else
                    {
                        uow.CountryVisitRepo.Remove(entity);
                        uow.Commit();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //[HttpDelete]
        [HttpPost]
        [Route("LanguageKnownDelete/{id}")]
        public HttpResponseMessage LanguageKnownDelete(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var entity = uow.CrsMbrLanguageRepo.GetById(id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    else
                    {
                        uow.CrsMbrLanguageRepo.Remove(entity);
                        uow.Commit();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //[HttpDelete]
        [HttpPost]
        [Route("ImportantAssignmentDelete/{id}")]
        public HttpResponseMessage ImportantAssignmentDelete(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var entity = uow.AsgmtAppointmentRepo.GetById(id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    else
                    {
                        uow.AsgmtAppointmentRepo.Remove(entity);
                        uow.Commit();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //[HttpDelete]
        [HttpPost]
        [Route("ChildrenDelete/{id}")]
        public HttpResponseMessage ChildrenDelete(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var entity = uow.SpouseChildrenRepo.GetById(id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    else
                    {
                        uow.SpouseChildrenRepo.Remove(entity);
                        uow.Commit();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //[HttpDelete]
        [HttpPost]
        [Route("SpouseLanguageDelete/{id}")]
        public HttpResponseMessage SpouseLanguageDelete(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var entity = uow.SpouseLanguageRepo.GetById(id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    else
                    {
                        uow.SpouseLanguageRepo.Remove(entity);
                        uow.Commit();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        //[HttpDelete]
        [HttpPost]
        [Route("SpouseQualificationDelete/{id}")]
        public HttpResponseMessage SpouseQualificationDelete(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var entity = uow.SpouseQualificationRepo.GetById(id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    else
                    {
                        uow.SpouseQualificationRepo.Remove(entity);
                        uow.Commit();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //[HttpDelete]
        [HttpPost]
        [Route("ChildrenPassportDelete/{id}")]
        public HttpResponseMessage ChildrenPassportDelete(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var entity = uow.ChildrenPassportRepo.GetById(id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    else
                    {
                        uow.ChildrenPassportRepo.Remove(entity);
                        uow.Commit();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        //[HttpDelete]
        [HttpPost]
        [Route("VehicleStickerDelete/{id}")]
        public HttpResponseMessage VehicleStickerDelete(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var entity = uow.CrsMbrVehicleStickerRepo.GetById(id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    else
                    {
                        uow.CrsMbrVehicleStickerRepo.Remove(entity);
                        uow.Commit();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        #endregion
    }
}
