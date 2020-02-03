using System;
using System.Threading.Tasks;

namespace Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces
{
    public delegate void ResponseEventHandler(APIResponseEventContext eventContext);

    public interface IAPIRestClient
    {
        event ResponseEventHandler OnBadRequest;

        event ResponseEventHandler OnUnauthorized;

        Uri BaseUrl { get; set; }

        IAPIAuthenticationHeaderInitializer AuthenticationHeaderInitializer { get; set; }

        Task<IAPIRestResponse> ExecuteAsync(IAPIRestRequest apiRestRequest);

        Task<IAPIRestResponse<T>> ExecuteAsync<T>(IAPIRestRequest apiRestRequest);
    }
}