using Grasews.Infra.CrossCutting.Helpers.RestClient.Enums;
using Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Grasews.Infra.CrossCutting.Helpers.RestClient
{
    public class APIRestRequest : IAPIRestRequest
    {
        public IDictionary<string, string> Headers { get; }

        public string Resource { get; set; }

        public HttpMethodENUM Method { get; set; }

        public object Content { get; set; }

        public int Timeout { get; set; }

        public IList<Parameter> Parameters { get; internal set; }

        public APIRestRequest(string resource, HttpMethodENUM method)
        {
            Headers = new Dictionary<string, string>();
            Parameters = new List<Parameter>();

            Resource = resource;
            Method = method;
        }

        public void AddQueryStringParameter(string name, object value)
        {
            Parameters.Add(new Parameter
            {
                Name = name,
                Value = value,
                Type = ParameterType.QueryString
            });
        }

        public void AddRequestBodyParameter<T>(T value) where T : class
        {
            var bodyBuilder = new List<string>();

            foreach (var property in typeof(T).GetProperties())
            {
                //if (property.PropertyType.IsArray)
                //{
                //    var a = property.GetValue(value) as Array;
                //    var list = new List<string>();
                //    for (int i = 0; i < a.Length; i++)
                //    {
                //        var o = a.GetValue(i).ToString();
                //        list.Add(o);
                //    }
                //    bodyBuilder.Add($"{property.Name}={string.Join(",", list)}");
                //}
                //else
                //{
                    bodyBuilder.Add($"{property.Name}={property.GetValue(value)}");
                //}
            }

            Parameters.Add(new Parameter
            {
                Name = "application/x-www-form-urlencoded",
                Value = string.Join("&", bodyBuilder),
                Type = ParameterType.RequestBody,
                ContentType = "application/x-www-form-urlencoded"
            });
        }
    }
}