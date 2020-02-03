using Grasews.Infra.CrossCutting.Helpers.RestClient.Enums;
using Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace Grasews.Infra.CrossCutting.Helpers.RestClient
{
    public class APIRestResponse : IAPIRestResponse
    {
        public APIRestResponse()
        {
            Headers = new List<Parameter>();
        }

        public string Content { get; set; }

        public IList<Parameter> Headers { get; protected internal set; }

        public IAPIRestRequest Request { get; set; }

        public ResponseStatusENUM ResponseStatus { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }

    public class APIRestResponse<T> : APIRestResponse, IAPIRestResponse<T>
    {
        public T Data { get; set; }
    }
}