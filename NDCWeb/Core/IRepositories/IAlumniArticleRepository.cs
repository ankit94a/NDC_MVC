using NDCWeb.Areas.Alumni.View_Models;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDCWeb.Core.IRepositories
{
    public interface IAlumniArticleRepository : IRepository<AlumniArticle>
    {
        Task<IEnumerable<AlumniArticleAllVM>> GetAlumniUploadsForStaff(string Uid, string Category);
    }
}
