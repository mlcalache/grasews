using Grasews.Infra.CrossCutting.Helpers.RestClient.Enums;
using Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Grasews.Infra.CrossCutting.Helpers.RestClient
{
    public class APIRestClient : IAPIRestClient
    {
        private readonly RestSharp.RestClient _restClient = new RestSharp.RestClient();

        public event ResponseEventHandler OnBadRequest;

        public event ResponseEventHandler OnUnauthorized;

        public IAPIAuthenticationHeaderInitializer AuthenticationHeaderInitializer { get; set; }

        public Uri BaseUrl
        {
            get
            {
                return _restClient.BaseUrl;
            }

            set
            {
                _restClient.BaseUrl = value;
            }
        }

        public APIRestClient()
        {
            IDeserializer deserializer = new Serializer();
            _restClient.AddHandler("application/json", deserializer);
            _restClient.AddHandler("text/json", deserializer);
            _restClient.AddHandler("text/x-json", deserializer);

            _restClient.AddDefaultHeader("Accept", "application/json");
        }

        public async Task<IAPIRestResponse> ExecuteAsync(IAPIRestRequest apiRestRequest)
        {
            AuthenticationHeaderInitializer?.Create(apiRestRequest);

            var restRequest = Convert(apiRestRequest);

            var asyncTask = _restClient.ExecuteTaskAsync(restRequest);

            var restResponse = await asyncTask.ConfigureAwait(false);

            var apiRestResponse = Convert(restResponse);

            if (apiRestResponse.StatusCode != HttpStatusCode.OK && apiRestResponse.StatusCode != HttpStatusCode.NoContent)
            {
                apiRestResponse = ExecuteHandlers(apiRestResponse);
            }

            return apiRestResponse;
        }

        public async Task<IAPIRestResponse<T>> ExecuteAsync<T>(IAPIRestRequest apiRestRequest)
        {
            AuthenticationHeaderInitializer?.Create(apiRestRequest);

            var restRequest = Convert(apiRestRequest);

            var asyncTask = _restClient.ExecuteTaskAsync<T>(restRequest);

            var restResponse = await asyncTask.ConfigureAwait(false);

            var apiRestResponse = Convert(restResponse);

            if (apiRestResponse.StatusCode != HttpStatusCode.OK
                && apiRestResponse.StatusCode != HttpStatusCode.NotFound
                && apiRestResponse.StatusCode != HttpStatusCode.NoContent
                && string.IsNullOrEmpty(apiRestResponse.Content))
            {
                apiRestResponse = ExecuteHandlers(apiRestResponse);
            }

            return apiRestResponse;
        }

        private R ExecuteHandlers<R>(R restResponse) where R : IAPIRestResponse
        {
            var eventContext = new APIResponseEventContext(restResponse);

            switch (restResponse.StatusCode)
            {
                case HttpStatusCode.NotFound:
                case HttpStatusCode.BadRequest:
                    {
                        OnBadRequest?.Invoke(eventContext);
                        break;
                    }
                case HttpStatusCode.Unauthorized:
                    {
                        OnUnauthorized?.Invoke(eventContext);
                        break;
                    }

                default:
                    {
                        throw new Exception("Unexpected Case");
                    }
            }

            if (!eventContext.EventHandled)
            {
                ThrowHttpException(restResponse);
            }

            if (eventContext.RetryLastRequest)
            {
                var methods = GetType().GetMethods().Where(s => s.Name == "ExecuteAsync");
                var method = methods.FirstOrDefault();

                if (eventContext.RestResponse.GetType().IsGenericType)
                {
                    method = methods.LastOrDefault().MakeGenericMethod(eventContext.RestResponse.GetType().GenericTypeArguments);
                }

                eventContext.RestResponse = ((Task<R>)method.Invoke(this, new object[] { eventContext.RestResponse.Request })).Result;
            }

            return (R)eventContext.RestResponse;
        }

        private void ThrowHttpException(IAPIRestResponse restResponse)
        {
            var messageBuilder = new StringBuilder();

            var parameters = string.Join(", ", restResponse.Request.Parameters.Select(x => x.Name + "=" + (x.Value ?? "NULL")));

            messageBuilder.AppendLine($"Request to {_restClient.BaseUrl.AbsoluteUri}/{restResponse.Request.Resource}");
            messageBuilder.AppendLine($"failed with status code {(int)restResponse.StatusCode} - {restResponse.StatusCode}");
            messageBuilder.AppendLine($"parameters: {parameters}");
            messageBuilder.AppendLine($"and content: {restResponse.Content}");

            throw new HttpException((int)restResponse.StatusCode, messageBuilder.ToString());
        }

        private IRestRequest Convert(IAPIRestRequest apiRestRequest)
        {
            var restRequest = new RestRequest(apiRestRequest.Resource, (Method)apiRestRequest.Method);

            if (apiRestRequest.Content != null)
            {
                restRequest.JsonSerializer = new Serializer();
                restRequest.AddJsonBody(apiRestRequest.Content);
            }

            foreach (var header in apiRestRequest.Headers)
            {
                restRequest.AddHeader(header.Key, header.Value);
            }

            restRequest.Timeout = apiRestRequest.Timeout;

            foreach (var parameter in apiRestRequest.Parameters)
            {
                ConvertParameter(parameter, restRequest);
            }

            return restRequest;
        }

        private void ConvertParameter(Parameter parameter, RestRequest restRequest)
        {
            if (parameter.Value == null)
            {
                return;
            }

            if (parameter.Value.GetType().IsClass && !(parameter.Value is string))
            {
                if (parameter.Value.GetType().IsArray)
                {
                    foreach (var item in (Array)parameter.Value)
                    {
                        var parametro = new Parameter
                        {
                            Name = $"{parameter.Name}",
                            Value = item,
                            Type = ParameterType.QueryString
                        };

                        ConvertParameter(parametro, restRequest);
                    }
                }
                else
                {
                    var properties = parameter.Value.GetType().GetProperties();

                    foreach (var property in properties)
                    {
                        var propertyValue = property.GetValue(parameter.Value);

                        if (propertyValue == null)
                        {
                            continue;
                        }

                        var parametro = new Parameter
                        {
                            Name = $"{parameter.Name}.{property.Name}",
                            Value = property.PropertyType.IsEnum ? (int)propertyValue : propertyValue,
                            Type = ParameterType.QueryString
                        };

                        ConvertParameter(parametro, restRequest);
                    }
                }
            }
            else
            {
                if (parameter.Value.GetType() == typeof(DateTime))
                {
                    parameter.Value = $"{parameter.Value:s}";
                }

                restRequest.AddParameter(parameter);
            }
        }

        private IAPIRestRequest Convert(IRestRequest restRequest)
        {
            return new APIRestRequest(restRequest.Resource, (HttpMethodENUM)restRequest.Method)
            {
                Parameters = restRequest.Parameters.Select(s => s).ToList()
            };
        }

        private IAPIRestResponse Convert(IRestResponse restResponse)
        {
            return new APIRestResponse
            {
                Content = restResponse.Content,
                Headers = restResponse.Headers.Select(s => s).ToList(),
                Request = Convert(restResponse.Request),
                ResponseStatus = (ResponseStatusENUM)restResponse.ResponseStatus,
                StatusCode = restResponse.StatusCode
            };
        }

        private IAPIRestResponse<T> Convert<T>(IRestResponse<T> restResponse)
        {
            return new APIRestResponse<T>
            {
                Content = restResponse.Content,
                Headers = restResponse.Headers.Select(s => s).ToList(),
                Request = Convert(restResponse.Request),
                ResponseStatus = (ResponseStatusENUM)restResponse.ResponseStatus,
                StatusCode = restResponse.StatusCode,
                Data = restResponse.Data
            };
        }

        // Serializador utilizado pelo RestClient do RestSharp (os métodos e propriedades não podem ser apagados mesmo não tendo referências no código)
        private class Serializer : ISerializer, IDeserializer
        {
            private readonly Newtonsoft.Json.JsonSerializer _serializer;

            public string DateFormat { get; set; }
            public string RootElement { get; set; }
            public string Namespace { get; set; }
            public string ContentType { get; set; }

            public Serializer()
            {
                ContentType = "application/json";
                _serializer = new Newtonsoft.Json.JsonSerializer
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Include,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc
                };
            }

            public T Deserialize<T>(IRestResponse response)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return default(T);
                }

                var content = response.Content;

                using (var stringReader = new StringReader(content))
                {
                    using (var jsonTextReader = new JsonTextReader(stringReader))
                    {
                        return _serializer.Deserialize<T>(jsonTextReader);
                    }
                }
            }

            public string Serialize(object obj)
            {
                using (var stringWriter = new StringWriter())
                {
                    using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                    {
                        jsonTextWriter.Formatting = Formatting.Indented;
                        jsonTextWriter.QuoteChar = '"';

                        _serializer.Serialize(jsonTextWriter, obj);

                        var result = stringWriter.ToString();
                        return result;
                    }
                }
            }
        }
    }
}