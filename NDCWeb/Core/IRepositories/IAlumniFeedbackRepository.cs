using NDCWeb.Models;
using NDCWeb.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDCWeb.Core.IRepositories
{
    public interface IAlumniFeedbackRepository : IRepository<AlumniFeedback>
    {
        Task<IEnumerable<AlumniFeedbackListVM>> AlumniFeedbackGetAll();
        //AlumniFeedbackVM AlumniFeedbackGetById(int feedBackId);
    }
}
