using System.Web.Http;

namespace Grasews.API
{
    /// <summary>
    ///
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        ///
        /// </summary>
        protected void Application_Start()
        {
            //var x = CryptHelper.Decrypt(ConfigurationManagerHelper.EmailPassword);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}