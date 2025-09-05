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
using System.Web.Mvc;

namespace NDCWeb.Areas.Member.Controllers
{
    [Authorize(Roles = CustomRoles.Candidate)]
    [UserMenu(MenuArea = "Member")]
    [CSPLHeaders]
    public class LeaveController : Controller
    {
        // GET: Member/Leave
        public ActionResult IndividualLeaveList()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var leaves = uow.LeaveRepo.Find(x=>x.CreatedBy == uId, fk=>fk.Country);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Leave, LeaveIndexVM>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<Leave>, List<LeaveIndexVM>>(leaves).ToList();
                
                if (indexDto != null)
                {
                    foreach (var leave in indexDto)
                    {
                        if (leave.ServiceSDSStatus == "Sanction") //Sanction
                        {
                            leave.BtnLeaveCertificate = true;
                        }
                    }
                }

                return View(indexDto);
            }
        }
        public ActionResult IndividualLeaveEntry()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                //LeaveCrtVM objLeaveCrt = new LeaveCrtVM();
                ViewBag.Country = uow.CountryMasterRepo.GetCountries();
                CrsMemberPersonal crsMemberPersonal = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == uId, np=>np.CitizenshipCountries);
                CrsMbrAppointment crsMbrAppointment = uow.CrsMbrAppointmentRepo.FirstOrDefault(x => x.CreatedBy == uId, np => np.Ranks);
                if (crsMemberPersonal != null && crsMbrAppointment!=null)
                {
                    ViewBag.Citizenship = crsMemberPersonal.CitizenshipCountries.CountryName;
                    ViewBag.FullName = crsMbrAppointment.Ranks.RankName + " " + crsMemberPersonal.FirstName + " " + crsMemberPersonal.MiddleName + " " + crsMemberPersonal.Surname;

                    ViewBag.GetLeaveCategory = CustomDropDownList.GetLeaveCategory();
                    ViewBag.LeaveIn = CustomDropDownList.GetLeaveIn();
                    ViewBag.LeaveType = CustomDropDownList.GetLeaveType();
                    ViewBag.EmbassyRecmdType = CustomDropDownList.GetEmbassyRecmdType();
					ViewBag.LeaveDuration = CustomDropDownList.GetLeaveDuration();

					var lockerDetail = uow.LockerAllotmentRepo.FirstOrDefault(x => x.CourseMemberId == crsMemberPersonal.CourseMemberId);
                    if(lockerDetail != null)
                    {
                        ViewBag.LockerNo = lockerDetail.LockerNo;
                    }
                    else
                    {
                        this.AddNotification("Locker No. has not been alloted", NotificationType.WARNING);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    this.AddNotification("First Enter Personal And Service Detail", NotificationType.WARNING);
                    return RedirectToAction("CourseEnrol", "CourseMember");
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult IndividualLeaveEntry(LeaveVM objLeavevm)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                int qryResult = uow.LeaveRepo.CheckMemberLeaveInsVal(uId);
                if (qryResult == 1)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<LeaveVM, Leave>();
                    });
                    IMapper mapper = config.CreateMapper();
                    Leave CreateDto = mapper.Map<LeaveVM, Leave>(objLeavevm);
                    uow.LeaveRepo.Add(CreateDto);
                    uow.Commit();
                    this.AddNotification("Record Saved", NotificationType.SUCCESS);
                    
                }
                else if (qryResult == 2)
                {
                    this.AddNotification("Last Leave is pending", NotificationType.WARNING);
                    
                }
                return RedirectToAction("IndividualLeaveList");
            }
        }
        public ActionResult IndividualLeaveEntryEdit(int id)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var crsMemberPersonal = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == uId);

                ViewBag.Citizenship = crsMemberPersonal.CitizenshipCountries.CountryName;
                ViewBag.Country = uow.CountryMasterRepo.GetCountries();
            }
            ViewBag.GetLeaveCategory = CustomDropDownList.GetLeaveCategory();
            ViewBag.LeaveType = CustomDropDownList.GetLeaveType();
            ViewBag.EmbassyRecmdType = CustomDropDownList.GetEmbassyRecmdType();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var leavedata = uow.LeaveRepo.GetById(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Leave, LeaveUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                LeaveUpVM CreateDto = mapper.Map<Leave, LeaveUpVM>(leavedata);
               
                return View(CreateDto);
            }
        }
        [HttpPost]
        public async Task<ActionResult> IndividualLeaveEntryEdit(LeaveUpVM objLeavevm)
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var crsMemberPersonal = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == uId);
                ViewBag.Citizenship = crsMemberPersonal.CitizenshipCountries.CountryName;

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<LeaveUpVM, Leave>();
                });
                IMapper mapper = config.CreateMapper();
                Leave UpdateDto = mapper.Map<LeaveUpVM, Leave>(objLeavevm);
                uow.LeaveRepo.Update(UpdateDto);
                await uow.CommitAsync();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("IndividualLeaveList");
            }
        }
        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var DeleteItem = await uow.LeaveRepo.GetByIdAsync(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.LeaveRepo.Remove(DeleteItem);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
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
        #region Report
        public ActionResult LeaveCertificateRpt(int leaveid)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var staffs = uow.StaffMasterRepo.GetAll(x => x.Faculties);
                string aqName = staffs.FirstOrDefault(x => x.Faculties.StaffType == "AQ").FullName;

                var leaveinfo = uow.LeaveRepo.GetLeaveCertificate(leaveid);
                leaveinfo.AQName = aqName;
                return View(leaveinfo);
            }
        }
        #endregion
    }
}