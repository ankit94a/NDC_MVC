using NDCWeb.Data_Contexts;
using NDCWeb.Persistence;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using NDCWeb.Areas.Staff.View_Models;
using AutoMapper;
using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.View_Models;
using NDCWeb.Infrastructure.Constants;
using Microsoft.AspNet.Identity;
using System;

namespace NDCWeb.Areas.Staff.Controllers
{
    [CSPLHeaders]
    [StaffStaticUserMenu]
    [EncryptedActionParameter]
    public class MessBillController : Controller
    {
        // GET: Staff/MessBill
        [Authorize(Roles = CustomRoles.Staff)]
        public async Task<ActionResult> BillIndex()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                // var participants = await uow.CrsMbrPersonalRepo.GetCourseMemberVerifiedListAsync(course.CourseId);
                var participants = await uow.MessBillRepo.GetMessBillListAsync(course.CourseId);
                return View(participants.OrderByDescending(x => x.MemberStaffId).ToList());
            }
        }
        
        public async Task<ActionResult> StaffIndex()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                 var indexDto1 = await uow.StaffMasterRepo.GetStaffUserListAsync();
                var indexDto = await uow.MessBillRepo.GetStaffUserandbillListAsync();
                return View(indexDto);
            }
        }
       
        public ActionResult UpdateBillStatus()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                var participants = uow.MessBillRepo.GetMessBillList(course.CourseId);
                return View();
            }
        }
        public ActionResult StaffBillDetail()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                //var personal = uow.MessBillRepo.FirstOrDefault(x => x.MemberStaffId = Uid);
                var BillDetail = uow.MessBillRepo.GetBillDetail(uId);
                return View(BillDetail);
            }
        }
        public async Task<JsonResult> LoadBillDetailEdit(int pageIndex, int courseId)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                
               // var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                var participants = await uow.MessBillRepo.GetMessBillList(courseId);
                PagingVM pagingParam = new PagingVM();
                pagingParam.PageIndex = pageIndex;
                pagingParam.PageSize = 120;
                int startIndex = (pageIndex - 1) * pagingParam.PageSize;
                return Json(new { BillDetail = participants }, JsonRequestBehavior.AllowGet);
            }
        }
       
        public async Task<ActionResult> Mesbillsave(MessBillAllVM objmessbill)
        {

            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MessBillAllVM, MessBillAllVM>();
                });
                IMapper mapper = config.CreateMapper();
                MessBillAllVM CreateDto = mapper.Map<MessBillAllVM, MessBillAllVM>(objmessbill);
              //  uow.SiteFeedbackRepo.Add(CreateDto);
                await uow.CommitAsync();
                //Mail.FeedbackEmail(CreateDto.FullName, "Website Feedback", CreateDto.EmailId, CreateDto.Comment, CreateDto.DepartmentSubject);
                this.AddNotification("Your bill igenerate successfully ", NotificationType.SUCCESS);
                return RedirectToAction("Feedback");
            }
        }
        
    }
}