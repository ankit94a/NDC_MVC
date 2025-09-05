using System;
using System.Configuration;
using System.Web.Configuration;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace NDCWeb
{
    public class CustomSessionIDManager : SessionIDManager, ISessionIDManager
    {
        void ISessionIDManager.SaveSessionID(HttpContext context, string id, out bool redirected, out bool cookieAdded)
        {
            base.SaveSessionID(context, id, out redirected, out cookieAdded);

            if (cookieAdded)
            {
                SessionStateSection sessionStateSection = (System.Web.Configuration.SessionStateSection)ConfigurationManager.GetSection("system.web/sessionState");
                var cookie = context.Response.Cookies[sessionStateSection.CookieName];
                cookie.Path = context.Request.ApplicationPath; // "/content";
                cookie.SameSite = System.Web.SameSiteMode.Strict;
                cookie.HttpOnly = true;
                //cookie.Domain= "ndc.nic.in";
            }
        }
        
    }
}