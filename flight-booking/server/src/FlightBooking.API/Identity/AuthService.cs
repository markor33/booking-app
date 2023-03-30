using FlightBooking.API.Identity.HttpModels;
using FlightBooking.API.Identity.Models;
using FlightBooking.Business.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlightBooking.API.Identity
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;

        public AuthService(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            IUserRepository userRepository)
        {
            _configuration = configuration;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByNameAsync(loginRequest.Username);
            if (user == null)
                return null;
            var result = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!result)
                return null;
            var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            return new LoginResponse()
            {
                Jwt = BuildToken(user, userRole)
            };
        }

        public async Task<IdentityResult> Register(RegisterRequest registerRequest)
        {
            var user = new ApplicationUser()
            {
                UserName = registerRequest.Username,
                Email = registerRequest.Email,
            };
            var result = await _userManager.CreateAsync(user, registerRequest.Password);
            if (!result.Succeeded)
                return result;
            result = await _userManager.AddToRoleAsync(user, "USER");
            if (!result.Succeeded)
                return result;
            registerRequest.User.AppUserId = user.Id;
            await _userRepository.CreateAsync(registerRequest.User);
            return result;
        }

        private string BuildToken(ApplicationUser user, string role)
        {
            var issuer = _configuration["Jwt:ValidIssuer"];
            var secret = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(ClaimTypes.Role, role)
                }),
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
