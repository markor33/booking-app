using FluentResults;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Services.Register
{
    public class RegisterService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> RegisterAsync(RegisterViewModel register)
        {
            var user = new ApplicationUser()
            {
                UserName = register.Username,
                Email = register.Email,
                FirstName = register.Firstname, 
                LastName = register.Lastname,
                Address = register.Address
            };

            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
                return Result.Fail("Registration failed");

            result = await _userManager.AddToRoleAsync(user, register.UserType.ToString());
            if (!result.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return Result.Fail("Registration failed");
            }

            return Result.Ok();
        }
    }
}
