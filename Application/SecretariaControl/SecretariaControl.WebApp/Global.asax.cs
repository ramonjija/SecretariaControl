using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace SecretariaControl.WebApp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest()
        {
            SecretariaControl.WebApp.Core.Context.ApplicationContext.DataBaseContext = new SecretariaControl.DataAccess.SecretariaControlEntities();
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            // Get the authentication cookie
            string cookieName = System.Web.Security.FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = Context.Request.Cookies[cookieName];

            // If the cookie can't be found, don't issue the ticket
            if (authCookie == null) return;

            // Get the authentication ticket and rebuild the principal 
            // & identity
            System.Web.Security.FormsAuthenticationTicket authTicket = System.Web.Security.FormsAuthentication.Decrypt(authCookie.Value);
            var userPrincipal = new SecretariaControl.WebApp.Core.Authentication.CustomPrincipal(authTicket);
            Context.User = userPrincipal;

        }

        protected void Application_EndRequest()
        {
            if (SecretariaControl.WebApp.Core.Context.ApplicationContext.DataBaseContext != null)
            {
                SecretariaControl.WebApp.Core.Context.ApplicationContext.DataBaseContext.Dispose(); 
            }
        }
    }
}