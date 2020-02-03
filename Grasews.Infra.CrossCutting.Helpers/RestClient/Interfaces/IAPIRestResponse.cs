using Grasews.Infra.CrossCutting.Helpers.RestClient.Enums;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces
{
    public interface IAPIRestResponse
    {
        string Content { get; set; }

        IList<Parameter> Headers { get; }

        IAPIRestRequest Request { get; set; }

        ResponseStatusENUM ResponseStatus { get; set; }

        HttpStatusCode StatusCode { get; set; }
    }

    public interface IAPIRestResponse<T> : IAPIRestResponse
    {
        T Data { get; }
    }
}