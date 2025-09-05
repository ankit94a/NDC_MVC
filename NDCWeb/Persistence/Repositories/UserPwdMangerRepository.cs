using NDCWeb.Core.IRepositories;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class UserPwdMangerRepository : Repository<UserPwdManger>, IUserPwdMangerRepository
    {
        public UserPwdMangerRepository(DbContext context) : base(context)
        {
        }
        public async Task<bool> Validatepwdhistory(string Username, string password)
        {
            bool response = false;
            var _pwd = await this.NDCWebContext.UserPwdMangers.Where(item => item.Username == Username).OrderByDescending(p => p.ModifyDate).Take(3).ToListAsync();
            if (_pwd.Count > 0)
            {
                var validate = _pwd.Where(o => o.Password == password);
                if (validate.Any())
                {
                    response = true;
                }
            }
            return response;

        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}