using Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces;
using RestSharp;
using System;
using System.Linq;

namespace Grasews.Infra.CrossCutting.Helpers.RestClient.AuthenticationHeader
{
    public abstract class APIAuthenticationHeaderBase
    {
        protected void Create(IAPIRestRequest apiRestRequest, string parameterValue)
        {
            var authParam = apiRestRequest.Parameters.FirstOrDefault(p => p.Name.Equals("Authorization", StringComparison.OrdinalIgnoreCase));

            if (authParam != null)
            {
                authParam.Value = parameterValue;
            }
            else
            {
                apiRestRequest.Parameters.Add(new Parameter
                {
                    Name = "Authorization",
                    Value = parameterValue,
                    Type = ParameterType.HttpHeader
                });
            }
        }
    }
}