using System;
using System.Configuration;

namespace Grasews.Infra.CrossCutting.Helpers
{
    public static class ConfigurationManagerHelper
    {
        private const int DEFAULT_TOKEN_EXPIRES_IN_MINUTES = 30;

        public static string CorsAllowedOrigin => ConfigurationManager.AppSettings["grasews:CorsAllowedOrigin"];
        public static string APITimeout => ConfigurationManager.AppSettings["grasews:APITimeout"];
        public static string ApiUrl => ConfigurationManager.AppSettings["grasews:ApiUrl"];
        public static string BaseUrlForAcceptInvitation => ConfigurationManager.AppSettings["grasews:BaseUrlForAcceptInvitation"];
        public static string EmailPassword => ConfigurationManager.AppSettings["grasews:USDYFT"];

        public static bool Html_TreeViewMenu_ShowTooltip => Convert.ToBoolean(ConfigurationManager.AppSettings["grasews:Tree-View-Menu:Show-Tooltip"]);
        public static bool Html_ChangeSkins_Enabled => Convert.ToBoolean(ConfigurationManager.AppSettings["grasews:Change-Skins:Enabled"]);

        public static int GrasewsTokenExpiresInMinutes
        {
            get
            {
                var webConfigValue = ConfigurationManager.AppSettings["grasews:TokenExpiresInMinutes"];

                if (!string.IsNullOrEmpty(webConfigValue))
                {
                    if (int.TryParse(webConfigValue, out int intValue))
                    {
                        return intValue;
                    }
                }

                return DEFAULT_TOKEN_EXPIRES_IN_MINUTES;
            }
        }

        public static string DatabaseDefaultSchema => ConfigurationManager.AppSettings["grasews:DatabaseDefaultSchema"];
        public static string SentryUrl => ConfigurationManager.AppSettings["sentry:Url"];
    }
}