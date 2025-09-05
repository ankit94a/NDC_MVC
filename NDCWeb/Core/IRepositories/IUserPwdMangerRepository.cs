using NDCWeb.Models;
using NDCWeb.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NDCWeb.Core.IRepositories
{
    public interface IUserPwdMangerRepository: IRepository<UserPwdManger>
    {
        Task<bool> Validatepwdhistory(string Username, string password);
    }
}