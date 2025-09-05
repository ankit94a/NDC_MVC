using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Infrastructure.Helpers.FileDirectory;
using NDCWeb.Infrastructure.Helpers.FileExt;
using NDCWeb.Models;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace NDCWeb.Areas.Member.Controllers
{
    [Authorize(Roles = CustomRoles.Candidate)]
    [UserMenu(MenuArea = "Member")]
    [CSPLHeaders]
    public class UniversityController : Controller
    {
        // GET: Member/University
        public ActionResult Index()
        {
            string uId = User.Identity.GetUserId();
           
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                MPhilMemberIndxVM objMphil = new MPhilMemberIndxVM();
                var teledetails = uow.MPhilMemberRepo.FirstOrDefault(x => x.CreatedBy == uId);
                var crsMemberPersonal = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == uId);
                if (crsMemberPersonal == null)
                {
                    return Redirect("~/member");
                }
                if (teledetails == null)
                {
                    ViewBag.Gender = CustomDropDownList.GetGender();
                    ViewBag.Community = CustomDropDownList.GetCastCommunity();

                    ViewBag.Country = uow.CountryMasterRepo.GetCountries();
                    ViewBag.Country = uow.CountryMasterRepo.GetCountries();

                    var regMember = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == uId);
                    if (regMember != null)
                    {
                        objMphil.ApplicantNameEnglish = regMember.FirstName + " " + regMember.MiddleName + " " + regMember.Surname;
                        objMphil.CommunicationMob = regMember.MobileNo;
                        objMphil.PermanentMob = regMember.MobileNo;
                        objMphil.EmailId = regMember.EmailId;
                        objMphil.DOBirth = regMember.DOBirth;
                        objMphil.Gender = regMember.Gender;
                        objMphil.Nationality = regMember.CitizenshipCountries.CountryName;
                        objMphil.FatherName = regMember.FatherName + " " + regMember.FatherMiddleName + " " + regMember.FatherSurname;
                        objMphil.MotherName = regMember.MotherName + " " + regMember.MotherMiddleName + " " + regMember.MotherSurname;
                        return View("Mphil");
                    }
                    else
                    {
                        return Redirect("~/member");
                    }
                }
                else
                {
                    var regMember = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == uId);
                    // var mphilEnrol = uow.MPhilMemberRepo.FirstOrDefault(x => x.MPhilId == id);
                    var lockerAllotment = uow.LockerAllotmentRepo.FirstOrDefault(x => x.CourseMemberId == regMember.CourseMemberId);
                    if (lockerAllotment != null)
                    {
                        ViewBag.LockerNo = lockerAllotment.LockerNo;
                    }
                    var mphilEnrol = uow.MPhilMemberRepo.FirstOrDefault(x => x.CreatedBy == uId);
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<MPhilMember, MPhilMemberEnrolACKVM>();
                    });
                    IMapper mapper = config.CreateMapper();
                    MPhilMemberEnrolACKVM mphilEnrolAck = mapper.Map<MPhilMember, MPhilMemberEnrolACKVM>(mphilEnrol);
                    return View("MPhilDetail", mphilEnrolAck);

                }
            }
        }
        public ActionResult PGPartial()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var MphilDegree = uow.MPhilPostGraduateRepo.Find(x => x.CreatedBy == uId);
                var config = new MapperConfiguration(cfg => cfg.CreateMap<MPhilPostGraduate, MPhilPostGraduate>());
                IMapper mapper = config.CreateMapper();
                var IndexDto = mapper.Map<IEnumerable<MPhilPostGraduate>, List<MPhilPostGraduate>>(MphilDegree);
                return PartialView("_MPhilPostGraduate", IndexDto);
            }
        }

        public ActionResult Mphil()
        {
            ViewBag.Gender = CustomDropDownList.GetGender();
            ViewBag.Community = CustomDropDownList.GetCastCommunity();
            string uId = User.Identity.GetUserId();
            MPhilMemberEnrolVM objMphil = new MPhilMemberEnrolVM();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var personalDetail = uow.MPhilMemberRepo.FirstOrDefault(x => x.CreatedBy == uId);
                var crsMemberPersonal = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == uId);
                if (crsMemberPersonal == null)
                {
                    return Redirect("~/member");
                }
                if (personalDetail == null)
                {
                    //Add                   
                    ViewBag.MaritalStatus = CustomDropDownList.GetMaritalStatus();
                    ViewBag.Country = uow.CountryMasterRepo.GetCountries();

                    var regMember = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == uId);
                    if (regMember != null)
                    {
                        objMphil.ApplicantNameEnglish = regMember.FirstName + " " + regMember.MiddleName + " " + regMember.Surname;
                        objMphil.CommunicationMob = regMember.MobileNo;
                        objMphil.PermanentMob = regMember.MobileNo;
                        objMphil.EmailId = regMember.EmailId;
                        objMphil.DOBirth = regMember.DOBirth;
                        objMphil.Gender = regMember.Gender;
                        objMphil.Nationality = regMember.CitizenshipCountries.CountryName;
                        objMphil.FatherName = regMember.FatherName + " " + regMember.FatherMiddleName + " " + regMember.FatherSurname;
                        objMphil.MotherName = regMember.MotherName + " " + regMember.MotherMiddleName + " " + regMember.MotherSurname;

                        return View(objMphil);
                    }
                    else
                    {
                        return Redirect("~/member");
                    }
                }
                else
                {
                    return RedirectToAction("MPhilDetail", new { id = personalDetail.MPhilId });
                }
            }
        }
        [HttpPost]
        [ValidateInput(true)]
        public async Task<ActionResult> Mphil(MPhilMemberCrtVM objMU)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MPhilMemberCrtVM, MPhilMember>();
                });
                IMapper mapper = config.CreateMapper();
                MPhilMember CreateDto = mapper.Map<MPhilMemberCrtVM, MPhilMember>(objMU);
                uow.MPhilMemberRepo.Add(CreateDto);
                await uow.CommitAsync();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("MPhilDetail");
            }
        }

        [HttpPost]
        public ActionResult MphilAck(MPhilMemberEnrolVM modal)
        {
            var mphilEnrol = modal;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MPhilMemberEnrolVM, MPhilMemberEnrolACKVM>();
            });
            IMapper mapper = config.CreateMapper();
            MPhilMemberEnrolACKVM mphilEnrolAck = mapper.Map<MPhilMemberEnrolVM, MPhilMemberEnrolACKVM>(mphilEnrol);
            return PartialView("_MPhilAck", mphilEnrolAck);
        }
        public ActionResult MPhilDetail()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                // var mphilEnrol = uow.MPhilMemberRepo.FirstOrDefault(x => x.MPhilId == id);
                var mphilEnrol = uow.MPhilMemberRepo.FirstOrDefault(x => x.CreatedBy == uId);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MPhilMember, MPhilMemberEnrolACKVM>();
                });
                IMapper mapper = config.CreateMapper();
                MPhilMemberEnrolACKVM mphilEnrolAck = mapper.Map<MPhilMember, MPhilMemberEnrolACKVM>(mphilEnrol);
                return View(mphilEnrolAck);
            }
        }
        public ActionResult Application()
        {
            ViewBag.Gender = CustomDropDownList.GetGender();
            ViewBag.Community = CustomDropDownList.GetCastCommunity();
            return View();
        }
        //For Madras University
        public ActionResult MURegistration()
        {
            ViewBag.Gender = CustomDropDownList.GetGender();
            ViewBag.Community = CustomDropDownList.GetCastCommunity();
            string uId = User.Identity.GetUserId();
            MphilDegreeCrtVM objMphil = new MphilDegreeCrtVM();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var personalDetail = uow.MPhilDegreeRepo.FirstOrDefault(x => x.CreatedBy == uId);
                var crsMemberPersonal = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == uId);
                if (crsMemberPersonal == null)
                {
                    return Redirect("~/member");
                }
                if (personalDetail == null)
                {
                    //Add                   
                    ViewBag.MaritalStatus = CustomDropDownList.GetMaritalStatus();
                    ViewBag.Country = uow.CountryMasterRepo.GetCountries();

                    var regMember = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == uId);
                    if (regMember != null)
                    {
                        objMphil.NameOfApplicant = regMember.FirstName + " " + regMember.MiddleName + " " + regMember.Surname;
                        objMphil.DOB = regMember.DOBirth;
                        objMphil.YearOfAdmission = DateTime.Today.Year;
                        objMphil.StudyMode = "FULL TIME RESEARCH SCHOLAR";
                        objMphil.AffiliateCollege = "NATIONAL DEFENCE COLLEGE UNDER AUTONOMOUS PATTERN";
                        objMphil.IsRecognisedForMphil = "YES";
                        objMphil.ObtainedApproval = "YES";
                        objMphil.NoAndDateOfApproval = "No-A lI/ASO-4/M.Phil/ Defence/2006/1105 dt 23 Feb 2006";
                        objMphil.IsSupervisorRecognisedForCourse = "Yes";
                        objMphil.PlaceOfApplication = "New Delhi, India";
                        objMphil.DateOfApplication = DateTime.Parse(DateTime.Today.Date.ToString("dd MMM yyyy"));
                    }
                    else
                    {
                        return Redirect("~/member");
                    }
                }
                else
                {
                    var regMember = uow.MPhilDegreeRepo.FirstOrDefault(x => x.CreatedBy == uId);
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<MPhilDegree, MphilDegreeIndxVM>();
                    });
                    IMapper mapper = config.CreateMapper();
                    MphilDegreeIndxVM mphilEnrolAck = mapper.Map<MPhilDegree, MphilDegreeIndxVM>(regMember);
                    return View("MUDetail", mphilEnrolAck);
                }
            }
            return View(objMphil);
        }
        [HttpPost]
        public async Task<ActionResult> MURegistration(MphilDegreeCrtVM objMU)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MphilDegreeCrtVM, MPhilDegree>();
                });
                IMapper mapper = config.CreateMapper();
                MPhilDegree CreateDto = mapper.Map<MphilDegreeCrtVM, MPhilDegree>(objMU);
                uow.MPhilDegreeRepo.Add(CreateDto);
                await uow.CommitAsync();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("MUDetail");
            }
        }
       public ActionResult MUIndex()
        {
            string uId = User.Identity.GetUserId();

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
        public ActionResult MUDetail()
        {
            string uId = User.Identity.GetUserId();

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var regMember = uow.MPhilDegreeRepo.FirstOrDefault(x => x.CreatedBy == uId);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MPhilDegree, MphilDegreeIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                MphilDegreeIndxVM mphilEnrolAck = mapper.Map<MPhilDegree, MphilDegreeIndxVM>(regMember);
                return View(mphilEnrolAck);
            }
        }
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
        #endregion
        public async Task<ActionResult> Edit(int id)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var regMember = uow.MPhilDegreeRepo.FirstOrDefault(x => x.CreatedBy == uId);
                var memberships = await uow.MPhilDegreeRepo.GetByIdAsync(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MPhilDegree, MphilDegreeUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<MPhilDegree, MphilDegreeUpVM>(memberships);
                //ViewBag.FullName = regMember.FirstName + " " + regMember.MiddleName + " " + regMember.LastName + ", " + regMember.Honour;
                //ViewBag.Rank = regMember.Ranks.RankName;
                return View(indexDto);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Edit(MphilDegreeUpVM objMphilUp)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MphilDegreeUpVM, MPhilDegree>();
                });
                IMapper mapper = config.CreateMapper();
                MPhilDegree UpdateDto = mapper.Map<MphilDegreeUpVM, MPhilDegree>(objMphilUp);
                uow.MPhilDegreeRepo.Update(UpdateDto);
                await uow.CommitAsync();
                return RedirectToAction("Index");
            }
        }
    }
}
