using Grasews.Infra.CrossCutting.Helpers.RestClient.Enums;
using RestSharp;
using System.Collections.Generic;

namespace Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces
{
    public interface IAPIRestRequest
    {
        IDictionary<string, string> Headers { get; }

        string Resource { get; }

        HttpMethodENUM Method { get; }

        object Content { get; set; }

        int Timeout { get; }

        IList<Parameter> Parameters { get; }

        void AddRequestBodyParameter<T>(T value) where T : class;

        void AddQueryStringParameter(string name, object value);
    }
}