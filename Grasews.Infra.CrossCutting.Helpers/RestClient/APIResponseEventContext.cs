using Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces;

namespace Grasews.Infra.CrossCutting.Helpers.RestClient
{
    public class APIResponseEventContext
    {
        public IAPIRestResponse RestResponse { get; set; }

        public bool EventHandled { get; set; }

        public bool RetryLastRequest { get; set; }

        public APIResponseEventContext(IAPIRestResponse restResponse)
        {
            RestResponse = restResponse;
        }
    }
}