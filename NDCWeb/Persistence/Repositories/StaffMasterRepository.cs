using NDCWeb.Areas.Admin.View_Models;
using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Core.IRepositories;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Persistence.Repositories
{
    public class StaffMasterRepository : Repository<StaffMaster>, IStaffMasterRepository
    {
        public StaffMasterRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<SelectListItem> GetStaff()
        {
            List<SelectListItem> cities = NDCWebContext.StaffMasters
                    .OrderBy(n => n.FacultyId)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.StaffId.ToString(),
                            Text =  n.Faculties.FacultyName
                            //Text = n.FullName + "(" + n.Faculties.FacultyName + ")"

                        }).ToList();
            return new SelectList(cities, "Value", "Text");
        }
        public IEnumerable<SelectListItem> GetSDS()
        {
            List<SelectListItem> cities = NDCWebContext.StaffMasters
                    .OrderBy(n => n.FullName)
                    .Where(n => n.Faculties.StaffType == "SDS")
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.StaffId.ToString(),
                            Text = n.FullName + "(" + n.Faculties.FacultyName + ")"
                        }).ToList();
            return new SelectList(cities, "Value", "Text");
        }
        public IEnumerable<SelectListItem> GetIAG()
        {
            List<SelectListItem> cities = NDCWebContext.StaffMasters
                    .OrderBy(n => n.FullName)
                      .Where(n => n.Faculties.StaffType == "SDS")
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.StaffId.ToString(),
                            Text = n.FullName + "(" + n.Faculties.FacultyName + ")"
                        }).ToList();
            return new SelectList(cities, "Value", "Text");
        }

        public IEnumerable<SelectListItem> GetSDSAppointment()
        {
            List<SelectListItem> cities = NDCWebContext.StaffMasters
                    .OrderBy(n => n.Faculties.FacultyName)
                    .Where(n => n.Faculties.StaffType == "SDS")
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.StaffId.ToString(),
                            Text = n.Faculties.FacultyName
                        }).ToList();
            return new SelectList(cities, "Value", "Text");
        }

        public IEnumerable<SelectListItem> GetIAGAppointment()
        {
            List<SelectListItem> cities = NDCWebContext.StaffMasters
                    .OrderBy(n => n.Faculties.FacultyName)
                      .Where(n => n.Faculties.StaffType == "SDS")
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.StaffId.ToString(),
                            Text = n.Faculties.FacultyName
                        }).ToList();
            return new SelectList(cities, "Value", "Text");
        }
        public async Task<IEnumerable<StaffUserIndxListVM>> GetStaffUserListAsync()
        {
            return await NDCWebContext.Database.SqlQuery<StaffUserIndxListVM>("GetStaffMasterAndAccount").ToListAsync();
        }

        public int DMLUpdateStaffAndAccount(int staffId)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@StaffId", staffId)
            };
            return NDCWebContext.Database.SqlQuery<int>("UpdateStaff_Account @StaffId", sqlParam).Single();
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}