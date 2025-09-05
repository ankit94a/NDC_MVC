using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Infrastructure.Filters
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["UserID"] == null)
            {
                filterContext.Result = new RedirectResult("~/auth/login");
                return;
            }
            else if (true)
            {

            }
            base.OnActionExecuting(filterContext);
        }
    }
    public class SessionCheck : ActionFilterAttribute
    {
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    string ut = DataSecurity.EncryptString(csConst.AuthSalt, HttpContext.Current.Session["UserID"].ToString() + csConst.AuthSalt); // HttpContext.Current.Session["UserID"].ToString();
        //    HttpSessionStateBase session = filterContext.HttpContext.Session;
        //    string CurId = DataSecurity.EncryptString(csConst.AuthSalt, HttpContext.Current.Request.Headers["User-Agent"].ToString() + HttpContext.Current.Session["UserID"].ToString() + csConst.AuthSalt);  //System.Web.HttpContext.Current.Request.Cookies[ut].Value;
        //    if (HttpContext.Current.Request.Cookies[ut] != null)
        //    {
        //        if (CurId != HttpContext.Current.Request.Cookies[ut].Value)
        //        {
        //            filterContext.Result = new RedirectResult("~/cpanel/account/login");
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        filterContext.Result = new RedirectResult("~/cpanel/account/login");
        //        return;
        //    }
        //    base.OnActionExecuting(filterContext);
        //}
    }
}