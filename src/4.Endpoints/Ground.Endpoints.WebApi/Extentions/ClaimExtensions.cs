using System.Security.Claims;

namespace Ground.Endpoints.WebApi.Extentions
{
    /// <summary>
    /// An extension class for ClaimsPrincipal to easily retrieve claim values by claim type.
    /// </summary>
    public static class ClaimExtensions
    {
        public static string GetClaim(this ClaimsPrincipal userClaimsPrincipal, string claimType)
        {
            return userClaimsPrincipal.Claims?.FirstOrDefault((Claim x) => x.Type == claimType)?.Value;
        }
    }
}
