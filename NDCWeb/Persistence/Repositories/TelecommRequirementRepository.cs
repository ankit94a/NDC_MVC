using NDCWeb.Core.IRepositories;
using NDCWeb.Models;
using System.Data.Entity;

namespace NDCWeb.Persistence.Repositories
{
    public class TelecommRequirementRepository: Repository<TelecommRequirement>, ITelecommRequirementRepository
    {
        public TelecommRequirementRepository(DbContext context) : base(context)
        {

        }
    }
}