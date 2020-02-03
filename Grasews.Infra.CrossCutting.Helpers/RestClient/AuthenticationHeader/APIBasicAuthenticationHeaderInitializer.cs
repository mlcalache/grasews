using Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces;
using System;
using System.Text;

namespace Grasews.Infra.CrossCutting.Helpers.RestClient.AuthenticationHeader
{
    public class APIBasicAuthenticationHeaderInitializer : APIAuthenticationHeaderBase, IAPIAuthenticationHeaderInitializer
    {
        private readonly string _authHeader;

        public APIBasicAuthenticationHeaderInitializer(string username, string password)
        {
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));

            _authHeader = $"Basic {token}";
        }

        public void Create(IAPIRestRequest apiRestRequest)
        {
            base.Create(apiRestRequest, _authHeader);
        }
    }
}