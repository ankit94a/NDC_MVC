using AutoMapper;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Infrastructure.Helpers.FileDirectory;
using NDCWeb.Models;
using NDCWeb.Infrastructure.Helpers.FileExt;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public class SpeakersController : Controller
    {
        // GET: Staff/Speakers
        public ActionResult Index()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var speakers = uow.SpeakerRepo.GetAll(fk=>fk.Topics, fk2=>fk2.Topics.Subjects);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Speaker, SpeakerIndxVM>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<Speaker>, List<SpeakerIndxVM>>(speakers).ToList();
                return View(indexDto);
            }
        }        
        public ActionResult Create()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Course = uow.SubjectMasterRepository.GetSubjects();
                ViewBag.Topic = uow.TopicMasterRepository.GetTopics();
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(SpeakerCrtVM objEv)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<SpeakerCrtVM, Speaker>();
                });
                IMapper mapper = config.CreateMapper();
                Speaker CreateDto = mapper.Map<SpeakerCrtVM, Speaker>(objEv);
                uow.SpeakerRepo.Add(CreateDto);
                await uow.CommitAsync();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Course = uow.SubjectMasterRepository.GetSubjects();
                ViewBag.Topic = uow.TopicMasterRepository.GetTopics();

                //var speakers = uow.SpeakerRepo.Find(x => x.SpeakerId == id).SingleOrDefault();
                var speakers = uow.SpeakerRepo.Find(x=>x.SpeakerId == id, fk=>fk.Topics).SingleOrDefault();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Speaker, SpeakerUpVM>()
                    .ForMember(dest => dest.SubjectId, opts => opts.MapFrom(src => src.Topics.SubjectId));
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<Speaker, SpeakerUpVM>(speakers);
                return View(indexDto);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Edit(SpeakerUpVM objSpeakerUvm)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                //var staff = uow.SpeakerRepo.GetById(objSpeakerUvm.SpeakerId);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<SpeakerUpVM, Speaker>();
                });
                IMapper mapper = config.CreateMapper();
                Speaker CreateDto = mapper.Map<SpeakerUpVM, Speaker>(objSpeakerUvm);
                uow.SpeakerRepo.Update(CreateDto);
                await uow.CommitAsync();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }
        //[HttpPost]
        //public ActionResult Edit(StaffMasterUpVM objStaffMasterUvm, HttpPostedFileBase[] docFiles)
        //{
        //    using (var uow = new UnitOfWork(new NDCWebContext()))
        //    {
        //        string path = ServerRootConsts.USER_ROOT;
        //        string DocPath = path + "documents/";
        //        string PhotoPath = path + "photos/";

        //        var staff = uow.StaffMasterRepo.GetById(objStaffMasterUvm.StaffId);
        //        staff.StaffId = objStaffMasterUvm.StaffId;
        //        staff.FullName = objStaffMasterUvm.FullName;
        //        staff.EmailId = objStaffMasterUvm.EmailId;
        //        staff.PhoneNo = objStaffMasterUvm.PhoneNo;
        //        staff.Decoration = objStaffMasterUvm.Decoration;
        //        staff.DOBirth = objStaffMasterUvm.DOBirth;
        //        staff.DOMarriage = objStaffMasterUvm.DOMarriage;
        //        staff.DOAppointment = objStaffMasterUvm.DOAppointment;
        //        staff.PostingOut = objStaffMasterUvm.PostingOut;

        //        staff.FacultyId = objStaffMasterUvm.FacultyId;
        //        staff.RankId = objStaffMasterUvm.RankId;
        //        //staff.IsLoginUser = objStaffMasterUvm.IsLoginUser;
        //        //staff.LoginUserId = objStaffMasterUvm.LoginUserId;

        //        if (!string.IsNullOrEmpty(objStaffMasterUvm.SelfImage))
        //        {
        //            staff.SelfImage = objStaffMasterUvm.SelfImage;
        //        }
        //        foreach (var file in docFiles)
        //        {
        //            if (file != null && file.ContentLength > 0)
        //            {
        //                var fileName = Path.GetFileName(file.FileName);
        //                Guid guid = Guid.NewGuid();
        //                StaffDocument objEntrDocs = new StaffDocument()
        //                {
        //                    GuId = guid,
        //                    FileName = fileName,
        //                    Extension = Path.GetExtension(fileName),
        //                    FilePath = DocPath + guid + Path.GetExtension(fileName)
        //                };
        //                file.SaveAs(Server.MapPath(objEntrDocs.FilePath));
        //                staff.iStaffDocument.Add(objEntrDocs);
        //            }
        //        }
        //        uow.Commit();
        //        return RedirectToAction("Index");
        //    }
        //}
        [HttpPost]
        public async Task<JsonResult> DeleteOnConfirm(int id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var DeleteItem = await uow.SpeakerRepo.GetByIdAsync(id);
                if (DeleteItem == null)
                {
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    uow.SpeakerRepo.Remove(DeleteItem);
                    await uow.CommitAsync();
                    return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
        #region Helper Action
        [HttpPost]
        public JsonResult ImageUpload()
        {
            string ROOT_PATH = ServerRootConsts.SPEAKER_ROOT;
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
                fs.filesize = 5000;
                string result = fs.UploadFile(file);
                if (string.IsNullOrEmpty(result))
                {
                    file.SaveAs(Server.MapPath(location));
                    return Json(new { message = "File Uploaded Successfully!", status = 1, filePath = location });
                }
                else
                    //return Json(new { message = "File Could not be Uploaded!", status = 0, filePath = location });
                    return Json(new { message = result, status = 0, filePath = location });
            }
            else
                return Json(new { message = "No files selected.", status = 0, filePath = "#" });
        }
        [HttpPost]
        public JsonResult DocumentUpload()
        {
            string ROOT_PATH = ServerRootConsts.SPEAKER_ROOT;
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
                fs.filesize = 5000;
                string result = fs.UploadFile(file);
                if (string.IsNullOrEmpty(result))
                {
                    file.SaveAs(Server.MapPath(location));
                    return Json(new { message = "File Uploaded Successfully!", status = 1, filePath = location });
                }
                else
                    //return Json(new { message = "File Could not be Uploaded!", status = 0, filePath = location });
                    return Json(new { message = result, status = 0, filePath = location });
            }
            else
                return Json(new { message = "No files selected.", status = 0, filePath = "#" });
        }
        #endregion
    }
}