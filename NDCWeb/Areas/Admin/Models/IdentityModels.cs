using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace NDCWeb.Areas.Admin.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public const string DisplayNameClaimType = "FName";
        public string FName { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim(DisplayNameClaimType, FName));
            return userIdentity;
        }
    }
    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }
    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int,
      CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
    
    //public class ApplicationRoleManager : RoleManager<CustomRole, int>
    //{
    //    public ApplicationRoleManager(IRoleStore<CustomRole, int> roleStore)
    //        : base(roleStore)
    //    {
    //    }

    //    public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
    //    {
    //        return new ApplicationRoleManager(new RoleStore<CustomRole, int, CustomUserRole>(context.Get<ApplicationDbContext>()));
    //    }
    //}
    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole,
     int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext() : base("NDCWebConString")
        {
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
    public static class CustomRoles
    {
        public const string Admin = "Admin";
        public const string approval = "data-approval";
        public const string Adminapproval = Admin + "," + approval;
        public const string Candidate = "Candidate";
        public const string Staff = "Staff";
        public const string CandidateStaff = Staff + "," + Candidate;
        public const string Alumni = "Alumni";
        public const string AdminStaff = Staff + "," + Admin;
        //public const string AQ = "AQ";
        //public const string Librarian = "Librarian";
        //public const string OfficerOnly = Officer;
        //public const string JCOROnly = JCOR;
        //public const string OfficerOrJco = Officer + "," + JCOR;
        //public const string AdminOrOfficer = Admin + "," + Officer;
        //public const string AdminOrJco = Admin + "," + JCOR;
        //public const string AdminOrOfficerOrJco = Admin + "," + Officer + "," + JCOR;
    }

}