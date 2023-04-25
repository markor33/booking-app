using FluentResults;
using Identity.API.Models;
using Identity.API.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.API.Services.Login
{
    public class LoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        public LoginService(
            UserManager<ApplicationUser> userManager,
            IOptions<JwtOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<Result<LoginResponse>> LoginAsync(LoginViewModel login)
        {
            var user = await _userManager.FindByNameAsync(login.Username);
            if (user is null)
                return Result.Fail("Login failed");

            var result = await _userManager.CheckPasswordAsync(user, login.Password);
            if (!result)
                return Result.Fail("Login failed");

            var jwt = BuildToken(user);
            return Result.Ok(new LoginResponse() { Jwt = jwt });
        }

        private string BuildToken(ApplicationUser user)
        {
            Claim[] userClaims = GetUserClaims(user);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(userClaims),
                Expires = DateTime.UtcNow.AddMonths(1),
                Issuer = _jwtOptions.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret)), SecurityAlgorithms.HmacSha512Signature)
            };

            JwtSecurityTokenHandler tokenHandler = new();
            JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(jwtToken);

            return token;
        }

        private static Claim[] GetUserClaims(ApplicationUser user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };
            return claims;
        }

    }
}
