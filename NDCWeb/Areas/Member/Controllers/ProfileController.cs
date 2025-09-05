using NDCWeb.Areas.Admin.Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Member.Controllers
{
    [Authorize(Roles = CustomRoles.Candidate)]
    [UserMenu(MenuArea = "Member")]
    [CSPLHeaders]
    public class ProfileController : Controller
    {
        // GET: Member/Profile
        public ActionResult Index()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Service = CustomDropDownList.GetRankService();
                ViewBag.Gender = CustomDropDownList.GetGender();
                ViewBag.MaritalStatus = CustomDropDownList.GetMaritalStatus();
                ViewBag.Country = uow.CountryMasterRepo.GetCountries();
            }
                return View();
        }

        // GET: Member/Profile/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Member/Profile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Member/Profile/Create
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

        // GET: Member/Profile/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Member/Profile/Edit/5
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

        // GET: Member/Profile/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Member/Profile/Delete/5
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
    }
}
