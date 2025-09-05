using AutoMapper;
using CaptchaMvc.HtmlHelpers;
using NDCWeb.Areas.Admin.View_Models;
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
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Controllers
{
    [CSPLHeaders]
    [UserMenu(MenuArea = "NA")]
    public class InStepController : Controller
    {
        // GET: InStep
        public ActionResult Index()
        {
            return View();
        }

        // GET: InStep/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Register()
        {
            InStepRegistrationCrtVM msm = new InStepRegistrationCrtVM();
            secConst.cSalt = "8080808080808080";//AESEncrytDecry.GetSalt();
            msm.hdns = secConst.cSalt.ToString();

            ViewBag.Service = CustomDropDownList.GetInstepService();
            ViewBag.Gender = CustomDropDownList.GetGender();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var course = uow.InStepCourseRepo.Find(x => x.UnderRegistration == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                if (course != null)
                {
                    ViewBag.Course = course.CourseName;
                    return View(msm);
                }
                else
                    return RedirectToAction("Closed");


            }
        }
        [HttpPost]
        public async Task<ActionResult> Register(InStepRegistrationCrtVM objCourseRegisterCvm)
        {
            if (!this.IsCaptchaValid("Validate your captcha"))
            {
                ViewBag.CaptchaErrorMessage = "Invalid Captcha";
                ModelState.AddModelError("", "Invalid verification code. Please try again.");

                #region redirect View With get Data
                ViewBag.Service = CustomDropDownList.GetInstepService();
                ViewBag.Gender = CustomDropDownList.GetGender();
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var course = uow.InStepCourseRepo.Find(x => x.UnderRegistration == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                    if (course != null)
                        ViewBag.Course = course.CourseName;
                    return View();
                }
                #endregion
            }
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var course = uow.InStepCourseRepo.Find(x => x.UnderRegistration == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<InStepRegistrationCrtVM, InStepRegistration>();
                });
                IMapper mapper = config.CreateMapper();
                InStepRegistration CreateDto = mapper.Map<InStepRegistrationCrtVM, InStepRegistration>(objCourseRegisterCvm);
                uow.InStepRegistrationRepo.Add(CreateDto);
                CreateDto.CourseId = course.CourseId;
                CreateDto.AadhaarNo = objCourseRegisterCvm.BaseId;
                await uow.CommitAsync();
                TempData["RefrenceNo"] = CreateDto.InStepRegId;
                return RedirectToAction("Thankyou");
            }
        }
        public ActionResult Thankyou()
        {

            TempData["alertMessage"] = "Your details are successfuly submitted. However, your details are yet to be verified by NDC.";
            TempData["refNo"] = GenerateNewRandom();
            TempData["refNo"] = TempData["RefrenceNo"];
            return View();
        }
        private static string GenerateNewRandom()
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateNewRandom();
            }
            return r;
        }
        // GET: InStep/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InStep/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: InStep/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InStep/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: InStep/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InStep/Delete/5
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
        [HttpGet]
        public ActionResult closed()
        {
            return View();
        }
        #region Helper Action
        [HttpPost]
        //[ValidateAntiForgeryToken]
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
                string result = fs.UploadFile(file, guid);
                if (string.IsNullOrEmpty(result))
                {
                    file.SaveAs(Server.MapPath(location));
                    return Json(new { message = "Photo Uploaded Successfully!", status = 1, filePath = location });
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
                    //return new JsonErrorResult(new { foo = "bar" }, HttpStatusCode.NotFound);
                    return Json(new { message = result, status = 0 });
            }
            else
                return Json(new { message = "No files selected.", status = 0, filePath = "#" });
        }
        #endregion
        #region Check Existing
        //[HttpGet]
        //public JsonResult CheckExistingMobile(string ConfirmMobileNo)
        //{
        //    try
        //    {
        //        using (var uow = new UnitOfWork(new NDCWebContext()))
        //        {
        //            bool isExist = uow.InStepRegistrationRepo.FirstOrDefault(x => x.MobileNo == ConfirmMobileNo) != null;
        //            return Json(!isExist, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //[HttpGet]
        //public JsonResult CheckExistingEmail(string ConfirmEmail)
        //{
        //    try
        //    {
        //        using (var uow = new UnitOfWork(new NDCWebContext()))
        //        {
        //            bool isExist = uow.InStepRegistrationRepo.FirstOrDefault(x => x.EmailId == ConfirmEmail) != null;
        //            return Json(!isExist, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        #endregion
    }
}
