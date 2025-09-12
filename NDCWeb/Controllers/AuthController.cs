using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.View_Models;
using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Infrastructure.Helpers.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using NDCWeb.Persistence;
using NDCWeb.Infrastructure.Constants;
using System.Runtime.CompilerServices;
using CaptchaMvc.HtmlHelpers;

namespace NDCWeb.Controllers
{
	[OutputCache(Duration = 0)]
	[Authorize]
	public class AuthController : Controller
	{
		private ApplicationSignInManager _signInManager;
		private ApplicationUserManager _userManager;
		private ApplicationRoleManager _roleManager;
		public AuthController()
		{
		}
		public AuthController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
		{
			UserManager = userManager;
			SignInManager = signInManager;
			RoleManager = roleManager;
		}
		public ApplicationSignInManager SignInManager
		{
			get
			{
				return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
			}
			private set
			{
				_signInManager = value;
			}
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
		public ApplicationRoleManager RoleManager
		{
			get
			{
				return _roleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
			}
			private set
			{
				_roleManager = value;
			}
		}
		public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }
		// GET: Auth
		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
			Session.Clear();
			ViewBag.ReturnUrl = returnUrl;
			LoginViewModel msm = new LoginViewModel();
			secConst.cSalt = AESEncrytDecry.GetSalt();
			msm.hdns = secConst.cSalt.ToString();
			//Session["cSalt"] = AESEncrytDecry.GetSalt();
			//msm.hdns = Session["cSalt"].ToString();
			return View(msm);
		}
		// POST: /Account/Login
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		[ValidateInput(true)]
		public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			//string clientCaptcha = model.VfyCaptcha;
			//string serverCaptcha = Session["CAPTCHA"].ToString();
			//string serverCaptcha = secConst.cCaptext;
			//if (clientCaptcha.Equals(serverCaptcha) && ModelState.IsValid)
			//if (!clientCaptcha.Equals(serverCaptcha))

			if (this.IsCaptchaValid("Validate your captcha"))
			{
				//ViewBag.ShowCAPTCHA = serverCaptcha;
				ViewBag.CaptchaErrorMessage = "Invalid Captcha";
				ModelState.AddModelError("", "Invalid verification code. Please try again.");
				ViewBag.CaptchaError = "Sorry, please type the characters shown.";
				return View(model);
			}
			// check user name in case sensitive
			/*if (model.UserName != null)
            {
                try
                {
                    var user_name = UserManager.FindByName(model.UserName);
                    if (!user_name.UserName.Equals(model.UserName))
                    {
                        ModelState.AddModelError("", "Username must be case-sensitive.");
                        UserActivityHelper.SaveUserActivity("Username must be case-sensitive.(by " + model.UserName + ")", Request.Url.ToString());
                        return View(model);
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Invalid username or password. Note: Username must be case-sensitive.");
                }                
            }*/

			// This doesn't count login failures towards account lockout
			// To enable password failures to trigger account lockout, change to shouldLockout: true

			//For client side password decrypt only
			//string tPassword = AESEncrytDecry.DecryptStringAES(model.Password, secConst.cSalt);
			//var user = await UserManager.FindAsync(model.UserName, tPassword);


			var user1 = await UserManager.FindByNameAsync(model.UserName);
			if (user1 != null)
			{
				// Reset password
				string token = await UserManager.GeneratePasswordResetTokenAsync(user1.Id);
				var result1 = await UserManager.ResetPasswordAsync(user1.Id, token, "@Test#1234");

				if (result1.Succeeded)
				{
					Console.WriteLine("Password reset successfully!");
				}
				else
				{
					foreach (var error in result1.Errors)
					{
						//Console.WriteLine(error.Description);
					}
				}
			}


			var user = await UserManager.FindAsync(model.UserName, model.Password);

			//var result = await SignInManager.PasswordSignInAsync(model.UserName, tPassword, isPersistent: true, shouldLockout: false);
			var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, false, shouldLockout: false);
			if (user != null && result == SignInStatus.Success)
			{
				Session["UserID"] = user.UserName;
				Session["cSalt"] = null;

				if (await UserManager.IsLockedOutAsync(user.Id))
				{
					UserActivityHelper.SaveUserActivity("Account has been locked out for {0} minutes due to multiple failed login attempts", Request.Url.ToString());
					ModelState.AddModelError("", string.Format("Your account has been locked out for {0} minutes due to multiple failed login attempts.", 15)); //ConfigurationManager.AppSettings["DefaultAccountLockoutTimeSpan"].ToString()));
				}

				await UserManager.UpdateSecurityStampAsync(user.Id);
				//if (String.IsNullOrEmpty(returnUrl))
				//{
				if (UserManager.IsInRole(user.Id, "Candidate"))
				{
					Session["UserID"] = user.UserName;
					UserActivityHelper.SaveUserActivity("Login Successfull for for participant user  " + model.UserName, Request.Url.ToString());
					return RedirectToAction("Index", "Home", new { Area = "Member" });
				}
				else if (UserManager.IsInRole(user.Id, "Staff"))
				{
					Session["UserID"] = user.UserName;
					UserActivityHelper.SaveUserActivity("Login Successfull for for Staff user  " + model.UserName, Request.Url.ToString());
					return RedirectToAction("Index", "Home", new { Area = "Staff" });

				}
				else if (UserManager.IsInRole(user.Id, "Alumni"))
				{
					Session["UserID"] = user.UserName;
					UserActivityHelper.SaveUserActivity("Login Successfull for for Alumni user  " + model.UserName, Request.Url.ToString());
					return RedirectToAction("Index", "Home", new { Area = "alumni" });
				}
				else
				{
					ModelState.AddModelError("", "Your are not authorised to use this area. Please contact Web Administrator. !");
				}
			}

			switch (result)
			{
				case SignInStatus.Success:
					return RedirectToLocal(HttpUtility.HtmlEncode(returnUrl));
				case SignInStatus.LockedOut:
					ModelState.AddModelError("", "Your account has been blocked due to three unsuccessful attempts to login. Please contact Web Administrator. !");
					return View(model);
				case SignInStatus.RequiresVerification:
					ModelState.AddModelError("", "Unknown error!");
					return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
				case SignInStatus.Failure:
				default:
					ModelState.AddModelError("", "Invalid Username or Password. Please try again.");
					UserActivityHelper.SaveUserActivity("Login attempted with wrong credentials(by " + model.UserName + ")", Request.Url.ToString());
					return View(model);
			}
		}
		private ActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			return RedirectToAction("Index", "Home");
		}
		[HttpGet]
		[AllowAnonymous]
		public ActionResult Message()
		{
			return View();
		}
		[HttpGet]
		[AllowAnonymous]
		public ActionResult Forgot()
		{
			return View();
		}
		[HttpPost]
		[AllowAnonymous]
		public async Task<ActionResult> Forgot(OtherRequestCrtVM model)
		{
			string clientCaptcha = model.VfyCaptcha;
			string serverCaptcha = Session["CAPTCHA"].ToString();
			bool checkMember = false, checkStaff = false;

			if (!clientCaptcha.Equals(serverCaptcha))
			{
				ViewBag.ShowCAPTCHA = serverCaptcha;
				ViewBag.CaptchaError = "Sorry, please type the characters shown.";
				return View(model);
			}
			NDCWebContext db = new NDCWebContext();

			using (var uow = new UnitOfWork(new NDCWebContext()))
			{
				if (ModelState.IsValid)
				{
					try
					{
						if (model.Usertype == "Member")
						{
							LockerAllotment lockerAllotment = db.LockerAllotments.Where(x => x.LockerNo == model.LockerNo).FirstOrDefault();
							if (lockerAllotment.LockerNo != null)
							{

								OtherRequest otherRequest = new OtherRequest();
								otherRequest.UserId = model.UserId;
								otherRequest.MobileNo = model.MobileNo;
								otherRequest.LockerNo = model.LockerNo;
								otherRequest.RequestDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
								uow.OthrerRequestRepo.Add(otherRequest);
								await uow.CommitAsync();
								//this.AddNotification("Your reset password request has been send to Admin.", NotificationType.SUCCESS);
								return RedirectToAction("Message");
							}
							else
							{
								ModelState.AddModelError("MobileNo", "ERROR:12149, The provided credentials are incorrect; please try again."); //Locker No incorrect
								return View(model);
							}
						}
						else
						{
							StaffMaster ndcStaff = db.StaffMasters.Where(x => x.EmailId == model.UserId).FirstOrDefault();
							if (ndcStaff != null)
							{
								OtherRequest otherRequest = new OtherRequest();
								otherRequest.UserId = model.UserId;
								otherRequest.MobileNo = model.MobileNo;
								otherRequest.RequestDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
								uow.OthrerRequestRepo.Add(otherRequest);
								await uow.CommitAsync();
								//this.AddNotification("Your reset password request has been send to Admin.", NotificationType.SUCCESS);
								return RedirectToAction("Message");
							}
							else
							{
								ModelState.AddModelError("MobileNo", "ERROR:00959, The provided credentials are incorrect; please try again."); //Email id incorrect
								return View(model);
							}
						}
					}
					catch (Exception ex)
					{
						ModelState.AddModelError("MobileNo", "ERROR:13108, The provided credentials are incorrect; please try again."); //Locker or Email Id incorrect
						return View(model);
					}
				}
				else
				{
					return View(model);
				}
			}

		}
		[HttpGet]
		[AllowAnonymous]
		public JsonResult CheckLocker(string LockerNo)
		{
			try
			{
				using (var uow = new UnitOfWork(new NDCWebContext()))
				{
					bool isExist = uow.LockerAllotmentRepo.FirstOrDefault(x => x.LockerNo == LockerNo) != null;
					return Json(isExist, JsonRequestBehavior.AllowGet);
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}