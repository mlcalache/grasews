using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;

namespace Grasews.Infra.CrossCutting.Helpers.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static void AddClaim(this ClaimsPrincipal claimsPrincipal, string type, string value)
        {
            if (claimsPrincipal.Identity is ClaimsIdentity identity)
            {
                identity.AddClaim(new Claim(type, value));

                claimsPrincipal = new GenericPrincipal(identity, null);

                Thread.CurrentPrincipal = claimsPrincipal;
            }
        }

        public static bool AnyClaim(this ClaimsPrincipal claimsPrincipal, string type, string value)
        {
            return claimsPrincipal.Identity is ClaimsIdentity identity && identity.AnyClaim(type, value);
        }

        public static string GetClaimValue(this ClaimsPrincipal claimsPrincipal, string type)
        {
            return claimsPrincipal.Identity is ClaimsIdentity identity ? identity.GetClaimValue(type) : string.Empty;
        }

        public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email);

            return claim != null ? claim.Value : string.Empty;
        }

        public static string GetGivenName(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal.Claims.FirstOrDefault(p => p.Type == ClaimTypes.GivenName);

            return claim != null ? claim.Value : string.Empty;
        }

        public static int GetId(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Sid);

            var id = 0;

            if (claim != null)
            {
                if (int.TryParse(claim.Value, out id))
                    return id;
            }

            return id;
        }

        public static string GetSurname(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Surname);

            return claim != null ? claim.Value : string.Empty;
        }

        public static void RemoveClaim(this ClaimsPrincipal claimsPrincipal, Claim claim)
        {
            var identity = claimsPrincipal.Identity as ClaimsIdentity;

            if (identity != null)
            {
                identity.TryRemoveClaim(claim);

                claimsPrincipal = new GenericPrincipal(identity, null);

                Thread.CurrentPrincipal = claimsPrincipal;
            }
        }

        public static void RemoveClaim(this ClaimsPrincipal claimsPrincipal, string type, string value)
        {
            if (claimsPrincipal.Identity is ClaimsIdentity identity)
            {
                identity.RemoveClaim(type, value);

                claimsPrincipal = new GenericPrincipal(identity, null);

                Thread.CurrentPrincipal = claimsPrincipal;
            }
        }

        public static void RemoveClaims(this ClaimsPrincipal claimsPrincipal, string type)
        {
            if (claimsPrincipal.Identity is ClaimsIdentity identity)
            {
                identity.RemoveClaims(type);

                claimsPrincipal = new GenericPrincipal(identity, null);

                Thread.CurrentPrincipal = claimsPrincipal;
            }
        }
    }
}