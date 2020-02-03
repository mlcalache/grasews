using Grasews.Web.App_Start;
using System.Net;
using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Grasews.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //ModelMetadataProviders.Current = new EmptyStringDataAnnotationsModelMetadataProvider();

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }

    public class EmptyStringDataAnnotationsModelMetadataProvider : System.Web.Mvc.DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(System.Collections.Generic.IEnumerable<System.Attribute> attributes, System.Type containerType, System.Func<object> modelAccessor, System.Type modelType, string propertyName)
        {
            var modelMetadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            modelMetadata.ConvertEmptyStringToNull = false;
            return modelMetadata;
        }
    }
}