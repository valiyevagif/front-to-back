using System.Security.Claims;

namespace Bigon.Infrastructure.Extensions
{
    public static partial class Extension
    {
        public static string GetClaimValue(this ClaimsPrincipal principal, string type)
        {
            return principal.Claims.FirstOrDefault(m => m.Type.Equals(type)).Value;
        }

        public static bool HasClaim(this ClaimsPrincipal principal, string type)
        {
            return principal.Claims.Any(m => m.Type.Equals(type) && m.Value.Equals("1")) || principal.IsInRole("superadmin");
        }
    }
}
