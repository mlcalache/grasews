using Grasews.Domain.Interfaces.Services;
using Grasews.Infra.CrossCutting.Helpers.Extensions;
using System;
using System.Security.Claims;
using System.Threading;

namespace Grasews.Infra.CrossCutting.Security
{
    public class UserIdentityService : IUserIdentityService
    {
        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose

        #region IUserIdentityService public props

        public string Email
        {
            get
            {
                if (Thread.CurrentPrincipal is ClaimsPrincipal principal)
                {
                    return principal.GetEmail();
                }

                return string.Empty;
            }
        }

        public string GivenName
        {
            get
            {
                if (Thread.CurrentPrincipal is ClaimsPrincipal principal)
                {
                    return principal.GetGivenName();
                }

                return string.Empty;
            }
        }

        public int Id
        {
            get
            {
                if (Thread.CurrentPrincipal is ClaimsPrincipal principal)
                {
                    return principal.GetId();
                }

                return 0;
            }
        }

        public bool IsAdmin
        {
            get
            {
                if (Thread.CurrentPrincipal is ClaimsPrincipal principal)
                {
                    return principal.GetClaimValue(ClaimTypes.Role) == SecurityConsts.ROLE_ADMIN;
                }

                return false;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                if (Thread.CurrentPrincipal is ClaimsPrincipal principal)
                {
                    return principal.Identity.IsAuthenticated;
                }

                return false;
            }
        }

        public string Name
        {
            get
            {
                if (Thread.CurrentPrincipal is ClaimsPrincipal principal)
                {
                    return principal.Identity.Name;
                }

                return string.Empty;
            }
        }

        public DateTime? RegistrationDateTime
        {
            get
            {
                if (Thread.CurrentPrincipal is ClaimsPrincipal principal)
                {
                    return Convert.ToDateTime(principal.GetClaimValue(SecurityConsts.CLAIMTYPE_REGISTRATIONDATETIME));
                }

                return null;
            }
        }

        public string Surname
        {
            get
            {
                if (Thread.CurrentPrincipal is ClaimsPrincipal principal)
                {
                    return principal.GetSurname();
                }

                return string.Empty;
            }
        }

        public string Username
        {
            get
            {
                if (Thread.CurrentPrincipal is ClaimsPrincipal principal)
                {
                    return principal.GetClaimValue(ClaimTypes.Name);
                }

                return string.Empty;
            }
        }

        #endregion IUserIdentityService public props
    }
}