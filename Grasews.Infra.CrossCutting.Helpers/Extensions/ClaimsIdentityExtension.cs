using System.Linq;
using System.Security.Claims;

namespace Grasews.Infra.CrossCutting.Helpers.Extensions
{
    public static class ClaimsIdentityExtension
    {
        public static void RemoveClaim(this ClaimsIdentity claimsIdentity, string type, string value)
        {
            claimsIdentity.FindAll(c => c.Type == type && c.Value == value).ToList().ForEach(claimsIdentity.RemoveClaim);
        }

        public static void RemoveClaims(this ClaimsIdentity claimsIdentity, string type)
        {
            claimsIdentity.FindAll(c => c.Type == type).ToList().ForEach(claimsIdentity.RemoveClaim);
        }

        public static string GetClaimValue(this ClaimsIdentity claimsIdentity, string type)
        {
            var claim = claimsIdentity.Claims.FirstOrDefault(p => p.Type == type);

            return claim != null ? claim.Value : string.Empty;
        }

        public static bool AnyClaimValue(this ClaimsIdentity claimsIdentity, string type)
        {
            var value = claimsIdentity.GetClaimValue(type);

            return !string.IsNullOrEmpty(value);
        }

        public static bool AnyClaim(this ClaimsIdentity claimsIdentity, string type, string value)
        {
            return claimsIdentity.Claims.Any(p => p.Type == type && p.Value == value);
        }
    }
}