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
using static System.Net.WebRequestMethods;

namespace NDCWeb.Areas.Member.Controllers
{
	[Authorize(Roles = CustomRoles.Candidate)]
	[UserMenu(MenuArea = "Member")]
	[CSPLHeaders]
	[EncryptedActionParameter]
	public class AccomodationController : Controller
	{
		// GET: Member/Accomodation
		public ActionResult Index()
		{
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				string uId = User.Identity.GetUserId();

				var accomodation = uow.AccomodationRepo.Find(x => x.CreatedBy == uId);
				var config = new MapperConfiguration(cfg =>
				{
					cfg.CreateMap<IEnumerable<Accomodation>, List<AccomodationIndexVM>>();
				});
				IMapper mapper = config.CreateMapper();
				var indexDto = mapper.Map<IEnumerable<Accomodation>, IEnumerable<AccomodationIndexVM>>(accomodation).ToList();
				return View(indexDto);
			}
		}
		public ActionResult Edit(int id)
		{
			string uId = User.Identity.GetUserId();

			ViewBag.GetMarriedAccnType = CustomDropDownList.GetMarriedAccnType();
			ViewBag.GetAccnArrangeType = CustomDropDownList.GetAccnArrangeType();
			ViewBag.GetAccnPreference = CustomDropDownList.GetAccnPreference();
			ViewBag.GetAccnFloorRequest = CustomDropDownList.GetAccnFloorRequest();

			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var accomodationdata = uow.AccomodationRepo.GetById(id);
				var config = new MapperConfiguration(cfg =>
				{
					cfg.CreateMap<Accomodation, AccomodationUpVM>();
				});
				var coursereg = uow.CourseRegisterRepo.Find(x => x.UserId == uId).FirstOrDefault();

				ViewBag.FullName = coursereg.FirstName + " " + coursereg.MiddleName + " " + coursereg.LastName;
				ViewBag.Rank = coursereg.Ranks.RankName;
				ViewBag.Decoration = coursereg.Honour;
				ViewBag.DOC = coursereg.DOCommissioning;
				ViewBag.DOS = coursereg.SeniorityYear;
				ViewBag.Address = coursereg.ApptLocation;
				ViewBag.MobileNo = coursereg.MobileNo;
				ViewBag.EmailId = coursereg.EmailId;
				ViewBag.MaritalStatus = CustomDropDownList.GetMaritalStatus();

				IMapper mapper = config.CreateMapper();
				AccomodationUpVM CreateDto = mapper.Map<Accomodation, AccomodationUpVM>(accomodationdata);
				return View(CreateDto);
			}
		}
		[HttpPost]
		public async Task<ActionResult> Edit(AccomodationUpVM objAccomodation, HttpPostedFileBase[] medDocFile, HttpPostedFileBase SignatureDoc)
		{
			ViewBag.GetMarriedAccnType = CustomDropDownList.GetMarriedAccnType();
			ViewBag.GetAccnArrangeType = CustomDropDownList.GetAccnArrangeType();
			ViewBag.GetAccnPreference = CustomDropDownList.GetAccnPreference();
			ViewBag.GetAccnFloorRequest = CustomDropDownList.GetAccnFloorRequest();

			string path = ServerRootConsts.ACCOMODATION_ROOT;
			objAccomodation.iAccomodationMedias = new List<AccomodationMedia>();
			foreach (var file in medDocFile)
			{
				if (file != null && file.ContentLength > 0)
				{
					var fileName = Path.GetFileName(file.FileName);
					Guid guid = Guid.NewGuid();
					//Check File
					CheckBeforeUpload fs = new CheckBeforeUpload();
					fs.filesize = 3000;
					string result = fs.UploadFile(file);
					if (string.IsNullOrEmpty(result))
					{
						AccomodationMedia objMediaFile = new AccomodationMedia()
						{
							GuId = guid,
							FileName = fileName,
							Extension = Path.GetExtension(fileName),
							FilePath = path + guid + Path.GetExtension(fileName),
							AccomodationId = objAccomodation.AccomodationId
						};

						file.SaveAs(Server.MapPath(objMediaFile.FilePath));
						objAccomodation.iAccomodationMedias.Add(objMediaFile);
					}
					else
						this.AddNotification(result, NotificationType.WARNING);
				}
			}

			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var config = new MapperConfiguration(cfg =>
				{
					cfg.CreateMap<Accomodation, AccomodationUpVM>();
				});
				IMapper mapper = config.CreateMapper();
				Accomodation UpdateDto = mapper.Map<AccomodationUpVM, Accomodation>(objAccomodation);
				if (SignatureDoc != null && SignatureDoc.ContentLength > 0)
				{
					var fileName = Path.GetFileName(SignatureDoc.FileName);
					string FilePath = path + Guid.NewGuid() + Path.GetExtension(fileName);
					//Check File
					CheckBeforeUpload fs = new CheckBeforeUpload();
					fs.filesize = 3000;
					string result = fs.UploadFile(SignatureDoc);
					if (string.IsNullOrEmpty(result))
					{
						UpdateDto.SignatureDoc = FilePath;
						SignatureDoc.SaveAs(Server.MapPath(FilePath));
					}
					else
						this.AddNotification(result, NotificationType.WARNING);

				}
				uow.AccomodationRepo.Update(UpdateDto);
				await uow.CommitAsync();
				this.AddNotification("Record Saved", NotificationType.SUCCESS);
				return RedirectToAction("Index");
			}
		}
		public ActionResult Entry()
		{
			ViewBag.GetMarriedAccnType = CustomDropDownList.GetMarriedAccnType();
			ViewBag.GetAccnArrangeType = CustomDropDownList.GetAccnArrangeType();
			ViewBag.GetAccnPreference = CustomDropDownList.GetAccnPreference();
			ViewBag.GetAccnFloorRequest = CustomDropDownList.GetAccnFloorRequest();

			string uId = User.Identity.GetUserId();
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var coursereg = uow.CourseRegisterRepo.Find(x => x.UserId == uId).FirstOrDefault();
				var accomodation = uow.AccomodationRepo.Find(x => x.CreatedBy == uId);
				if (accomodation.Count() == 0)
				{
					ViewBag.FullName = coursereg.FirstName + " " + coursereg.MiddleName + " " + coursereg.LastName;
					ViewBag.Rank = coursereg.Ranks.RankName;
					ViewBag.Decoration = coursereg.Honour;
					ViewBag.DOC = coursereg.DOCommissioning;
					ViewBag.DOS = coursereg.SeniorityYear;
					ViewBag.Address = coursereg.ApptLocation;
					ViewBag.MobileNo = coursereg.MobileNo;
					ViewBag.EmailId = coursereg.EmailId;
					ViewBag.MaritalStatus = CustomDropDownList.GetMaritalStatus();
					return View();
				}
				else
				{
					var config = new MapperConfiguration(cfg =>
					{
						cfg.CreateMap<IEnumerable<Accomodation>, List<AccomodationIndexVM>>();
					});
					IMapper mapper = config.CreateMapper();
					var indexDto = mapper.Map<IEnumerable<Accomodation>, IEnumerable<AccomodationIndexVM>>(accomodation).ToList();
					return View("Index", indexDto);
				}
			}
		}
		[HttpPost]
		public async Task<ActionResult> Entry(AccomodationCrtVM objAccomodationvm, HttpPostedFileBase[] medDocFile, HttpPostedFileBase SignatureDoc)
		{
			string path = ServerRootConsts.ACCOMODATION_ROOT;
			objAccomodationvm.iAccomodationMedias = new List<AccomodationMedia>();
			foreach (var file in medDocFile)
			{
				if (file != null && file.ContentLength > 0)
				{
					var fileName = Path.GetFileName(file.FileName);
					Guid guid = Guid.NewGuid();
					string FilePath = path + guid + Path.GetExtension(fileName);
					//Check File
					CheckBeforeUpload fs = new CheckBeforeUpload();
					fs.filesize = 3000;
					string result = fs.UploadFile(file);
					if (string.IsNullOrEmpty(result))
					{
						AccomodationMedia objMediaFile = new AccomodationMedia()
						{
							GuId = guid,
							FileName = fileName,
							Extension = Path.GetExtension(fileName),
							FilePath = path + guid + Path.GetExtension(fileName)
						};

						file.SaveAs(Server.MapPath(objMediaFile.FilePath));
						objAccomodationvm.iAccomodationMedias.Add(objMediaFile);
					}
					else
					{
						this.AddNotification(result, NotificationType.WARNING);
					}

				}
			}

			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var config = new MapperConfiguration(cfg =>
				{
					cfg.CreateMap<AccomodationCrtVM, Accomodation>();
				});
				IMapper mapper = config.CreateMapper();
				Accomodation CreateDto = mapper.Map<AccomodationCrtVM, Accomodation>(objAccomodationvm);
				if (SignatureDoc != null && SignatureDoc.ContentLength > 0)
				{
					var fileName = Path.GetFileName(SignatureDoc.FileName);
					string FilePath = path + Guid.NewGuid() + Path.GetExtension(fileName);
					//Check File
					CheckBeforeUpload fs = new CheckBeforeUpload();
					fs.filesize = 3000;
					string result = fs.UploadFile(SignatureDoc);
					if (string.IsNullOrEmpty(result))
					{
						SignatureDoc.SaveAs(Server.MapPath(FilePath));
						CreateDto.SignatureDoc = FilePath;
					}
					else
						this.AddNotification(result, NotificationType.WARNING);
				}
				uow.AccomodationRepo.Add(CreateDto);
				await uow.CommitAsync();
				return RedirectToAction("Index");
			}
		}
		[HttpPost]
		public JsonResult DocumentUpload()
		{
			string ROOT_PATH = ServerRootConsts.ACCOMODATION_ROOT;
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
		[HttpPost]
		public async Task<JsonResult> DeleteOnConfirm(int id)
		{
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var DeleteItem = await uow.AccomodationRepo.GetByIdAsync(id);
				if (DeleteItem == null)
				{
					return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
				}
				else
				{
					foreach (var item in DeleteItem.iAccomodationMedias)
					{
						//String path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), item.GuId + item.Extension);
						String path = Server.MapPath(item.FilePath);
						if (System.IO.File.Exists(path))
						{
							System.IO.File.Delete(path);
						}
					}
					uow.AccomodationRepo.Remove(DeleteItem);
					await uow.CommitAsync();
					return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
				}
			}
		}
		[EncryptedActionParameter]
		public ActionResult Print(int id)
		{
			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				var accomodationdata = uow.AccomodationRepo.GetById(id);
				string uId = uow.AccomodationRepo.FirstOrDefault(x => x.AccomodationId == id).CreatedBy;
				var coursereg = uow.CourseRegisterRepo.Find(x => x.UserId == uId).FirstOrDefault();
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
	}
}