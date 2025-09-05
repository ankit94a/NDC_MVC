using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NDCWeb.Infrastructure.Helpers.Menu
{
    public class StaffStaticMenuHelper
    {
        public string GetStaffType()
        {
            string loginid = HttpContext.Current.User.Identity.GetUserId();

            //string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var staffPersonal = uow.StaffMasterRepo.Find(x => x.LoginUserId == loginid, fk => fk.Faculties).FirstOrDefault();
                if (staffPersonal != null)
                {
                    string staffType = staffPersonal.Faculties.StaffType;
                    //HttpContext.Current.ViewBag.StaffType = staffType;
                    //HttpContext.Current.Session["StaffType"] = staffType;
                    return staffType;
                }
                return null;
            }
        }
    }
}