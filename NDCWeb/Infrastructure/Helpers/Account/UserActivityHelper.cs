using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NDCWeb.Infrastructure.Helpers.Account
{
    public static class UserActivityHelper
    {
        
        public static void SaveUserActivity(string data, string url)
        {

            string userName = HttpContext.Current.User.Identity.Name;
            string ipAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var userActivity = new UserActivity
                {
                    Data = data,
                    Url = url,
                    UserName = userName,
                    IpAddress = ipAddress,
                    ActivityDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"))
                };
                uow.UserActivityRepo.Add(userActivity);
                uow.Commit();
            }
        }
        public static void SaveVisitor(int MenuId, string slug)
        {
            string userName = HttpContext.Current.User.Identity.Name;
            string ipAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var userActivity = new Models.Visitor
                {
                    MenuId = MenuId,
                    Slug = slug,
                    IpAddress = ipAddress,
                    VisitDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"))
                };
                uow.VisitorRepo.Add(userActivity);
                uow.Commit();
            }
        }

    }
}