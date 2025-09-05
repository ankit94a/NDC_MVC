using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using Microsoft.AspNet.Identity.Owin;
using NDCWeb.Areas.Admin.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Models;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Infrastructure.Extensions;
using System.Runtime.Remoting;

namespace NDCWeb.Areas.Admin.Controllers
{
    public class OtherRequestController : Controller
    {
        #region For Auth Ops
        private ApplicationUserManager _userManager;
        public OtherRequestController()
        { }
        public OtherRequestController(ApplicationUserManager userManager)
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
        #endregion
      
        [Authorize(Roles = CustomRoles.Admin)]
        public ActionResult Index()
        {
            string uId = User.Identity.GetUserId();
            using (NDCWebContext db = new NDCWebContext())
            {
                var otherRequests = db.OtherRequests.Where(x=>x.IsDelete==false).OrderByDescending(x => x.RequestDate);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<OtherRequest, OtherRequestVM>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<OtherRequest>, List<OtherRequestVM>>(otherRequests).ToList();
                return View(indexDto);
                
            }
        }

        [HttpPost]
        [Authorize(Roles = CustomRoles.Admin)]
        public async Task<JsonResult> UpdateApproved(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var SelectedUsers = uow.OthrerRequestRepo.Find(x => x.OtherRequestId == id).FirstOrDefault();
                    int userID = UserManager.FindByEmail(SelectedUsers.UserId).Id;
                    if (SelectedUsers == null)
                        return Json(data: "Could Not Update Status", behavior: JsonRequestBehavior.AllowGet);
                    else if (userID == default(int) || userID == null)
                        return Json(data: "Could Not Update Status", behavior: JsonRequestBehavior.AllowGet);
                    else
                    {
                        var token = await UserManager.GeneratePasswordResetTokenAsync(userID);
                        var pwdResult = await UserManager.ResetPasswordAsync(userID, token, AppSettingsKeyConsts.DefPassKey);
                        SelectedUsers.Status = true;
                        uow.Complete();

                        return Json(data: "Updated", behavior: JsonRequestBehavior.AllowGet);
                    }
                }
            }
            finally { }
        }
        [Authorize(Roles = CustomRoles.Admin)]
        public async Task<ActionResult> Reject(int Id)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                OtherRequest otherRequest = uow.OthrerRequestRepo.Find(x => x.OtherRequestId == Id).SingleOrDefault();
                otherRequest.IsDelete = true;
                uow.OthrerRequestRepo.Update(otherRequest);
                await uow.CommitAsync();
                this.AddNotification("Request Rejected", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }
    }
}