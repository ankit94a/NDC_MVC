using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Infrastructure.Filters
{
    public class CSPLNoCache : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            context.HttpContext.Response.Cache.SetValidUntilExpires(false);
            context.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            context.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.HttpContext.Response.Cache.SetNoStore();

            base.OnResultExecuting(context);
        }
    }
}