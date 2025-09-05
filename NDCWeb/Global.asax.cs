using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Infrastructure.Handlers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.SessionState;

namespace NDCWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            HttpConfiguration config = GlobalConfiguration.Configuration;

            config.Formatters.JsonFormatter
                        .SerializerSettings
                        .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            GlobalFilters.Filters.Add(new CSPLErrorHandler());
            MvcHandler.DisableMvcResponseHeader = true;
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            SameSiteCookieRewriter.FilterSameSiteNoneForIncompatibleUserAgents(sender);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            string coo = "";
            if (Request.Cookies.Count > 0)
            {
                foreach (string s in Request.Cookies.AllKeys)
                {
                    //if (s.ToLower() == ".aspnet.applicationcookie")
                    if (s.ToLower() == "style" || s.ToLower() == ".aspnet.applicationcookie")
                    {
                        HttpCookie c = Request.Cookies[s];
                        coo = c.Value;
                        c.SameSite = System.Web.SameSiteMode.Lax;
                        Response.Cookies.Set(c);

                    }

                    if (s == FormsAuthentication.FormsCookieName || s.ToLower() == ".aspnet.applicationcookie")
                    {
                        Response.Cookies[s].Secure = true;
                    }
                }
            }

            //Check If it is a new session or not , if not then do the further checks
            /*if (Request.Cookies["ASP.NET_SessionId"] != null && Request.Cookies["ASP.NET_SessionId"].Value != null)
            {
                string newSessionID = Request.Cookies["ASP.NET_SessionID"].Value;
                //Check the valid length of your Generated Session ID
                if (newSessionID.Length <= 24)
                {
                    //Log the attack details here
                    Response.Cookies["TriedTohack"].Value = "True";
                    throw new HttpException("Invalid Request");
                }

                //Genrate Hash key for this User,Browser and machine and match with the Entered NewSessionID
                if (GenerateHashKey() != newSessionID.Substring(24))
                {
                    //Log the attack details here
                    Response.Cookies["TriedTohack"].Value = "True";
                    throw new HttpException("Invalid Request");
                }

                //Use the default one so application will work as usual//ASP.NET_SessionId
                Request.Cookies["ASP.NET_SessionId"].Value = Request.Cookies["ASP.NET_SessionId"].Value.Substring(0, 24);
            }*/
        }
        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Remove("X-Powered-By");
            HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
            HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");
            HttpContext.Current.Response.Headers.Remove("Server");
            Response.AddHeader("OMSRM", "DCWW0717");
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Response.Headers.Remove("Server");
            }
            // Missing Cookie Attributes.
            // Commented by CPK since this creates problem when user seesion auto expires, and auto-logout user attempts to re-login. user doesn't authenticated 
            /*if (Request.Cookies.Count > 0)
            {
                foreach (string s in Request.Cookies.AllKeys)
                {
                    HttpCookie c = Request.Cookies[s];
                    c.Value = Request.Cookies[s].Value;
                    c.SameSite = SameSiteMode.Strict; //System.Web.SameSiteMode.Lax;
                    Response.Cookies.Set(c);
                }
            }*/
        }
        void Session_Start(object sender, EventArgs e)
        {

            //get the useragent for the request
            string currentUserAgent = HttpContext.Current.Request.UserAgent;

            //decide if we need to strip off the same site attribute for older browsers
            bool dissallowSameSiteFlag = DisallowsSameSiteNone(currentUserAgent);

            //get the name of the cookie, if not defined default to the "ASP.NET_SessionID" value
            SessionStateSection sessionStateSection = (SessionStateSection)ConfigurationManager.GetSection("system.web/sessionState");
            string sessionCookieName;
            if (sessionStateSection != null)
            {
                //read the name from the configuration
                sessionCookieName = sessionStateSection.CookieName;
            }
            else
            {
                sessionCookieName = "ASP.NET_SessionId";
            }
            //should the flag be positioned to true, then remove the attribute by setting
            //value to SameSiteMode.None
            if (dissallowSameSiteFlag)
                Response.Cookies[sessionCookieName].SameSite = (SameSiteMode)(-1);
            else
                Response.Cookies[sessionCookieName].SameSite = SameSiteMode.None;

            //while we're at it lets also make it secure
            if (Request.IsSecureConnection)
                Response.Cookies[sessionCookieName].Secure = true;
        }
        private bool DisallowsSameSiteNone(string userAgent)
        {
            // check if the user agent is null or empty
            if (String.IsNullOrWhiteSpace(userAgent))
                return false;

            // Cover all iOS based browsers here. This includes:
            // - Safari on iOS 12 for iPhone, iPod Touch, iPad
            // - WkWebview on iOS 12 for iPhone, iPod Touch, iPad
            // - Chrome on iOS 12 for iPhone, iPod Touch, iPad
            // All of which are broken by SameSite=None, because they use the iOS 
            // networking stack.
            if (userAgent.Contains("CPU iPhone OS 12") ||
                userAgent.Contains("iPad; CPU OS 12"))
            {
                return true;
            }

            // Cover Mac OS X based browsers that use the Mac OS networking stack. 
            // This includes:
            // - Safari on Mac OS X.
            // This does not include:
            // - Chrome on Mac OS X
            // Because they do not use the Mac OS networking stack.
            if (userAgent.Contains("Macintosh; Intel Mac OS X 10_14") &&
                userAgent.Contains("Version/") && userAgent.Contains("Safari"))
            {
                return true;
            }

            // Cover Chrome 50-69, because some versions are broken by SameSite=None, 
            // and none in this range require it.
            // Note: this covers some pre-Chromium Edge versions, 
            // but pre-Chromium Edge does not require SameSite=None.
            if (userAgent.Contains("Chrome/5") || userAgent.Contains("Chrome/6"))
            {
                return true;
            }

            return false;
        }
        public static class SameSiteCookieUtils
        {
            /// <summary>
            /// -1 defines the unspecified value, which tells ASPNET to not send the SameSite attribute
            /// </summary>
            public const SameSiteMode Unspecified = (SameSiteMode)(-1);

            public static SameSiteMode GetSameSiteMode(string userAgent, SameSiteMode mode)
            {
                if (string.IsNullOrWhiteSpace(userAgent))
                    return mode;

                if (mode == SameSiteMode.None && DisallowsSameSiteNone(userAgent))
                    return Unspecified;

                return mode;
            }

            public static bool DisallowsSameSiteNone(string userAgent)
            {
                // Cover all iOS based browsers here. This includes:
                // - Safari on iOS 12 for iPhone, iPod Touch, iPad
                // - WkWebview on iOS 12 for iPhone, iPod Touch, iPad
                // - Chrome on iOS 12 for iPhone, iPod Touch, iPad
                // All of which are broken by SameSite=None, because they use the iOS networking
                // stack.
                if (userAgent.Contains("CPU iPhone OS 12") ||
                    userAgent.Contains("iPad; CPU OS 12"))
                {
                    return true;
                }

                // Cover Mac OS X based browsers that use the Mac OS networking stack.
                // This includes:
                // - Safari on Mac OS X.
                // This does not include:
                // - Chrome on Mac OS X
                // Because they do not use the Mac OS networking stack.
                if (userAgent.Contains("Macintosh; Intel Mac OS X 10_14") &&
                    userAgent.Contains("Version/") && userAgent.Contains("Safari"))
                {
                    return true;
                }

                // Cover Chrome 50-69, because some versions are broken by SameSite=None,
                // and none in this range require it.
                // Note: this covers some pre-Chromium Edge versions,
                // but pre-Chromium Edge does not require SameSite=None.
                if (userAgent.Contains("Chrome/5") || userAgent.Contains("Chrome/6"))
                {
                    return true;
                }

                return false;
            }
        }

    }
}
