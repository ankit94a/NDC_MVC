using NDCWeb.Areas.Admin.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NDCWeb.Infrastructure.Helpers.Account
{
    public static class UserAccountHelper
    {
        public static int getCurrentUserRoleId()
        {
            using (var context = new ApplicationDbContext())
            {
                int loginid = int.Parse(HttpContext.Current.User.Identity.GetUserId());
                var user = context.Users.ToList().Find(x => x.Id == loginid);
                var roleIds = user.Roles.Select(x => x.RoleId);
                int roleId = roleIds.First();
                //int roleId = context.Roles.ToList().First(x => x.Id == rl).Id;
                
                return roleId;
            }
        }
        public static string getCurrentUserRole()
        {
            using (var context = new ApplicationDbContext())
            {
                int loginid = int.Parse(HttpContext.Current.User.Identity.GetUserId());
                var user = context.Users.ToList().Find(x => x.Id == loginid);
                var roleIds = user.Roles.Select(x => x.RoleId);
                int roleId = roleIds.First();
                string role = context.Roles.ToList().Find(x => x.Id == roleId).Name;

                return role;
            }
        }

        //public static int GetUserByRoleId()
        //{
        //    using (var context = new ApplicationDbContext())
        //    {
        //        int loginid = int.Parse(HttpContext.Current.User.Identity.GetUserId());
        //        var user = context.Users.Where(x=>x.Roles.Where(y=>y.RoleId==1))
        //        var roleIds = user.Roles.Select(x => x.Role);
        //        int roleId = roleIds.First();
        //        //int roleId = context.Roles.ToList().First(x => x.Id == rl).Id;

        //        return roleId;
        //    }
        //}
    }
}