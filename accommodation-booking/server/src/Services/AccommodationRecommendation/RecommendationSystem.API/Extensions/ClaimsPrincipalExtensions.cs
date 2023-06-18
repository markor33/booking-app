using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RecommendationSystem.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string UserEmail(this ClaimsPrincipal User)
        {
            return User.Claims.First(c => c.Type == ClaimTypes.Email).Value;
        }
        public static string UserRole(this ClaimsPrincipal User)
        {
            return User.Claims.First(c => c.Type == ClaimTypes.Role).Value;
        }
        public static string UserId(this ClaimsPrincipal User)
        {
            return User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
