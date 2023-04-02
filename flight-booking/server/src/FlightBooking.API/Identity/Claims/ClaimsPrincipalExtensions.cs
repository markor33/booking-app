using System.Security.Claims;

namespace HospitalAPI.Security
{
    public static class ClaimsPrincipalExtensions
    {
        public static string UserId(this ClaimsPrincipal User)
        {
            return User.Claims.First(c => c.Type == "UserId").Value;
        }
    }
}