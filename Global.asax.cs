using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace lab_1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie auth = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (auth == null || auth.Value == "")
                return;
            FormsAuthenticationTicket authTicket;
            try
            {
                authTicket = FormsAuthentication.Decrypt(auth.Value);

            }
            catch { return; }

            string[] roles = authTicket.UserData.Split(';');

            if (Context.User != null)
            {
                Context.User = new GenericPrincipal(Context.User.Identity,roles);
            }
        }
    }
}
