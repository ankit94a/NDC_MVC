using NDCWeb.Core.IRepositories;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class AlumniArticleMediaRepository : Repository<AlumniArticleMedia>, IAlumniArticleMediaRepository
    {
        public AlumniArticleMediaRepository(DbContext context) : base(context)
        {
        }
    }
}