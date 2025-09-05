using NDCWeb.Core.IRepositories;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class StaffDocumentRepository : Repository<StaffDocument>, IStaffDocumentRepository
    {
        public StaffDocumentRepository(DbContext context) : base(context)
        {
        }
    }
}