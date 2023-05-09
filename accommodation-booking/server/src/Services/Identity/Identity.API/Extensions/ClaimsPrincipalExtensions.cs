using Identity.API.Services.Register;
using System.Security.Claims;

namespace Identity.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string UserEmail(this ClaimsPrincipal User)
        {
            return User.Claims.First(c => c.Type == "Email").Value;
        }
        public static string UserRole(this ClaimsPrincipal User)
        {
            return User.Claims.First(c => c.Type == "Role").Value;
        }
        public static string UserId(this ClaimsPrincipal User)
        {
            return User.Claims.First(c => c.Type == "Sub").Value;
        }
    }
}
