using Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces;
using System;

namespace Grasews.Infra.CrossCutting.Helpers.RestClient.AuthenticationHeader
{
    public class APIBearerAuthenticationHeaderInitializer : APIAuthenticationHeaderBase, IAPIAuthenticationHeaderInitializer
    {
        // é utilizada uma função ao invés do valor do token, porque assim o método Create chama essa função e sempre pega o token atualizado
        private readonly Func<string> _accessTokenFunc;

        public APIBearerAuthenticationHeaderInitializer(Func<string> accessTokenFunc)
        {
            _accessTokenFunc = accessTokenFunc;
        }

        public void Create(IAPIRestRequest apiRestRequest)
        {
            var accessToken = _accessTokenFunc();

            if (string.IsNullOrEmpty(accessToken))
            {
                return;
            }

            var authParamValue = $"bearer {accessToken}";

            base.Create(apiRestRequest, authParamValue);
        }
    }
}