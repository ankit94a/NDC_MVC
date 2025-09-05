using NDCWeb.Core.IRepositories;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class EventMemberRepository : Repository<EventMember>, IEventMemberRepository
    {
        public EventMemberRepository(DbContext context) : base(context)
        {
        }
    }
}