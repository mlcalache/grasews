using Grasews.API;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Http;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Grasews.API
{
    /// <summary>
    ///
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        ///
        /// </summary>
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Grasews.API");

                    c.ApiKey("Token")
                        .Description("Filling bearer token here")
                        .Name("Authorization")
                        .In("header");

                    c.IncludeXmlComments(GetXmlCommentsPath());

                    c.GroupActionsBy(apiDesc => {
                        var attr = apiDesc
                            .GetControllerAndActionAttributes<DisplayNameAttribute>()
                            .FirstOrDefault();

                        // use controller name if the attribute isn't specified
                        return attr?.DisplayName ?? apiDesc.ActionDescriptor.ControllerDescriptor.ControllerName;
                    });
                })
                .EnableSwaggerUi(c =>
                {
                    c.EnableApiKeySupport("Authorization", "header");

                    c.InjectJavaScript(thisAssembly, "Grasews.API.Scripts.SwaggerAuthorization.js");
                });
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected static string GetXmlCommentsPath() => $@"{AppDomain.CurrentDomain.BaseDirectory}\Grasews.API.xml";
    }
}