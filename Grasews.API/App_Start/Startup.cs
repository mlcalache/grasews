using Grasews.API.Providers;
using Grasews.Infra.CrossCutting.Helpers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;

[assembly: OwinStartup(typeof(Grasews.API.App_Start.Startup))]

namespace Grasews.API.App_Start
{
    /// <summary>
    ///
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///
        /// </summary>
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        public static void Configuration(IAppBuilder app)
        {
            SimpleInjectorWebApiInitializer.Initialize(app);

            ConfigureOAuth(app);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureOAuth(IAppBuilder app)
        {
            //use a cookie to temporarily store information about a user logging in with a third party login provider
            //app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            var tokenExpiresInMinutes = ConfigurationManagerHelper.GrasewsTokenExpiresInMinutes;

            var OAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(tokenExpiresInMinutes),
                Provider = new OAuthServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        }
    }
}