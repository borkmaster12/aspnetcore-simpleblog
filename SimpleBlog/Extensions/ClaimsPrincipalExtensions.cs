using System.Security.Claims;

namespace SimpleBlog.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int? GetUserId(this ClaimsPrincipal user)
        {
            var idClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return string.IsNullOrEmpty(idClaim) ? null : int.Parse(idClaim);
        }
    }
}
