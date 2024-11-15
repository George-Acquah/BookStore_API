using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BookStore.API.Helpers
{
    public static class ClaimsHelper
    {
        public static Guid? GetUserIdFromClaims(ClaimsPrincipal user) 
        { 
            var userIdClaim = user.FindFirst("UserId")?.Value; 
            if (userIdClaim != null && Guid.TryParse(userIdClaim, out Guid userId)) 
            { 
                return userId; 
            } 
            return null; 
        }
        public static string? GetUserEmailFromClaims(ClaimsPrincipal user) 
        { 
            return user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value; 
        }
        public static string? GetUserRoleFromClaims(ClaimsPrincipal user) 
        { 
            return user.FindFirst(ClaimTypes.Role)?.Value; 
        }
    }
}
