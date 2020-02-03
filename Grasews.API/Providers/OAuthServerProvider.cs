using Grasews.Domain.Interfaces.Services;
using Grasews.Infra.CrossCutting.Security;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Grasews.API.Providers
{
    /// <summary>
    ///
    /// </summary>
    public class OAuthServerProvider : OAuthAuthorizationServerProvider
    {
        #region Private vars

        private IUserService _userService;

        #endregion Private vars

        #region Public methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            _userService = ServiceLocator.Current.GetInstance<IUserService>();

            var user = _userService.GetByCredentials(context.UserName, context.Password);

            if (user != null)
            {
                var claimUserId = new Claim(ClaimTypes.Sid, user.Id.ToString());
                var claimUserName = new Claim(ClaimTypes.Name, user.Username);
                var claimUserEmail = new Claim(ClaimTypes.Email, user.Email);
                var claimUserFirstName = new Claim(ClaimTypes.GivenName, user.FirstName);
                var claimUserLastName = new Claim(ClaimTypes.Surname, user.LastName);
                var claimRegisterDate = new Claim(SecurityConsts.CLAIMTYPE_REGISTRATIONDATETIME, user.RegistrationDateTime.ToString());
                var claimRole = new Claim(ClaimTypes.Role, user.IsAdmin ? SecurityConsts.ROLE_ADMIN : SecurityConsts.ROLE_USER);

                var claims = new[] { claimUserId, claimUserName, claimUserEmail, claimUserFirstName, claimUserLastName, claimRegisterDate, claimRole };

                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                // TODO : [Login] Change fixed true for IsPersistent (remember user on computer)
                var authProperties = new AuthenticationProperties { IsPersistent = true };

                var ticket = new AuthenticationTicket(identity, authProperties);

                context.Validated(ticket);
            }
            else
            {
                context.SetError("Wrong username or password");
                AddHttpStatusCodeToResponseHeader(context, HttpStatusCode.Unauthorized);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="httpStatusCode"></param>
        private static void AddHttpStatusCodeToResponseHeader(OAuthGrantResourceOwnerCredentialsContext context, HttpStatusCode httpStatusCode)
        {
            context.Response.Headers.Add(ResponseStatusCodeMiddleware.OwinChallengeFlag, new[] { ((int)httpStatusCode).ToString() });
        }

        #endregion Public methods
    }
}