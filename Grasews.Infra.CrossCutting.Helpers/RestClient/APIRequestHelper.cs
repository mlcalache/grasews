using Grasews.Infra.CrossCutting.Helpers.RestClient.Enums;
using Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces;

namespace Grasews.Infra.CrossCutting.Helpers.RestClient
{
    public static class APIRequestHelper
    {
        public static IAPIRestRequest CreateUIAPIRequest(string resource, HttpMethodENUM method, string contentType = "application/json")
        {
            var request = new APIRestRequest(resource, method);
            request.Headers.Add("Content-Type", contentType);

            var timeOut = -1;

            if (int.TryParse(ConfigurationManagerHelper.APITimeout, out timeOut))
            {
                request.Timeout = timeOut;
            }

            return request;
        }

        public static IAPIRestRequest CreateUIAPIRequest(string resource, HttpMethodENUM method, object content)
        {
            var request = CreateUIAPIRequest(resource, method);

            request.Content = content;

            return request;
        }
    }
}