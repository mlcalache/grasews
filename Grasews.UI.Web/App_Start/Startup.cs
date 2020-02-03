using Grasews.Web.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

[assembly: OwinStartup(typeof(Startup))]

namespace Grasews.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            SimpleInjectorInitializer.Initialize();
            ConfigureAuth(app);

            app.MapSignalR();
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                ExpireTimeSpan = TimeSpan.FromMinutes(60),
                CookieName = "GRASEWS_AUTH_COOKIE",
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Index"),
                ReturnUrlParameter = "url"
            });
        }
    }
}