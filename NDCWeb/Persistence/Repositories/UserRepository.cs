using NDCWeb.Areas.Admin.Models;
using NDCWeb.Core.IRepositories;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDCWeb.Persistence.Repositories
{
	public class UserRepository : Repository<AspNetUsers>, IUserRepository
	{
		public UserRepository(DbContext context) : base(context)
		{

		}

		public async Task<List<MasterSearch>> GetUsersByFNameAsync(string search)
		{
			using (var context = new NDCWebContext())
			{
				string query = @" SELECT a.id, a.FName,aur.RoleId,ar.Name as Role FROM AspNetUsers A left join AspNetUserRoles aur on a.Id = aur.UserId left join AspNetRoles ar on aur.RoleId = ar.Id
                 WHERE FName LIKE @Search";

				SqlParameter[] sqlParams = {
			new SqlParameter("@Search", search + "%")
		};

				return await context.Database
					.SqlQuery<MasterSearch>(query, sqlParams)
					.ToListAsync();
			}
		}

		public async Task<List<MasterSearch>> GetUserDetails(MasterSearch user)
		{
			using (var context = new NDCWebContext())
			{
				string query = @" SELECT a.FName,aur.RoleId,ar.Name as Role FROM AspNetUsers A left join AspNetUserRoles aur on a.Id = aur.UserId left join AspNetRoles ar on aur.RoleId = ar.Id
                 WHERE FName LIKE @Search";

			

				return await context.Database
					.SqlQuery<MasterSearch>(query)
					.ToListAsync();
			}
		}
	}
}
