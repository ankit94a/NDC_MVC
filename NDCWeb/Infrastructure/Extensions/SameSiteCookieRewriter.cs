using System;
using System.Web;

namespace NDCWeb.Infrastructure.Extensions
{
    public class SameSiteCookieRewriter
    {
        public static void FilterSameSiteNoneForIncompatibleUserAgents(object sender)
        {
            HttpApplication application = sender as HttpApplication;

            if (application != null)
            {
                var userAgent = application.Context.Request.UserAgent;
                if (SameSite.BrowserDetection.DisallowsSameSiteNone(userAgent))
                {
                    application.Response.AddOnSendingHeaders(context =>
                    {
                        var cookies = context.Response.Cookies;
                        for (var i = 0; i < cookies.Count; i++)
                        {
                            var cookie = cookies[i];
                            if (cookie.SameSite == SameSiteMode.None)
                            {
                                cookie.SameSite = (SameSiteMode)(-1); // Unspecified
                            }
                        }
                    });
                }
                else
                {
                    application.Response.AddOnSendingHeaders(context =>
                    {
                        var cookies = context.Response.Cookies;
                        for (var i = 0; i < cookies.Count; i++)
                        {
                            var cookie = cookies[i];
                            if (cookie.SameSite == SameSiteMode.None)
                            {
                                cookie.SameSite = SameSiteMode.Lax; //(SameSiteMode)(-1); // Unspecified
                            }
                        }
                    });
                }
            }
        }
        //public static void AdjustSpecificCookieSettings()
        //{
        //    HttpContext.Current.Response.AddOnSendingHeaders(context =>
        //    {
        //        var cookies = context.Response.Cookies;
        //        for (var i = 0; i < cookies.Count; i++)
        //        {
        //            var cookie = cookies[i];
        //            // Forms auth: ".ASPXAUTH"
        //            // Session: "ASP.NET_SessionId"
        //            if (string.Equals(".ASPXAUTH", cookie.Name, StringComparison.Ordinal))
        //            {
        //                if (SameSite.BrowserDetection.DisallowsSameSiteNone(userAgent))
        //                {
        //                    cookie.SameSite = -1;
        //                }
        //                else
        //                {
        //                    cookie.SameSite = SameSiteMode.None;
        //                }
        //                cookie.Secure = true;
        //            }
        //        }
        //    });
        //}
    }
}