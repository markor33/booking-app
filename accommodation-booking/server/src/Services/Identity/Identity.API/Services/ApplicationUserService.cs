using FluentResults;
using Identity.API.Models;
using Identity.API.Services.Register;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Identity.API.Services
{
    public class ApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserService(UserManager<ApplicationUser> userManager) { _userManager = userManager; }

        public async Task<Result> EditProfileAsync(UserProfile user)
        {
            var userToUpdate = await _userManager.FindByEmailAsync(user.Email);
            userToUpdate.Address = user.Address;
            userToUpdate.FirstName = user.Firstname;
            userToUpdate.LastName = user.Lastname;
            var result = await _userManager.UpdateAsync(userToUpdate);

            if(!result.Succeeded)
                return Result.Fail("Failet to update profile");

            return Result.Ok();
        }

        public async Task<Result> ChangeCredentials(Credentials credentials)
        {
            var user = await _userManager.FindByEmailAsync(credentials.Email);
            var result = await _userManager.ChangePasswordAsync(user, credentials.OldPassword, credentials.NewPassword);

            if(!result.Succeeded)
                return Result.Fail("Failed to change password");

            user.UserName = credentials.UserName;
            await _userManager.UpdateAsync(user);

            return Result.Ok();
        }

        public async Task<Result> DeleteUser(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(await CheckCanDelete(userId, role))
            {
                await _userManager.DeleteAsync(user);
                return Result.Ok();
            }
            return Result.Fail("Failed to delete profile");
            
        }

        public async Task<Result<UserProfile>> GetUserProfile(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userProfile = new UserProfile
            {
                Firstname = user.FirstName,
                Lastname = user.LastName,
                Address = user.Address
            };
            return Result.Ok(userProfile);
        }

        public async Task<bool> CheckCanDelete(string userId, string role)
        {
            if (role == "Host")
                return true;
            return true;
        }



    }
}
