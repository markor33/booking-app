using EventBus.NET.Integration.EventBus;
using FluentResults;
using GrpcAccommodationSearch;
using GrpcReservations;
using Identity.API.IntegrationEvents;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Services
{
    public class ApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEventBus _eventBus;

        public ApplicationUserService(
            UserManager<ApplicationUser> userManager,
            IEventBus eventBus)
        {
            _userManager = userManager;
            _eventBus = eventBus;
        }

        public async Task<Result> EditProfileAsync(UserProfile user)
        {
            var userToUpdate = await _userManager.FindByEmailAsync(user.Email);
            userToUpdate.Address = user.Address;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
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

            user.Status = UserStatus.PENDING_DELETE;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return Result.Fail("Delete account failed");

            if (role == "HOST")
                _eventBus.Publish(new DeleteHostRequestIntegrationEvent(user.Id));
            else if (role == "GUEST")
                _eventBus.Publish(new DeleteGuestRequestIntegrationEvent(user.Id));

            return Result.Ok();
        }

        public async Task<Result<UserProfile>> GetUserProfile(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userProfile = new UserProfile
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address
            };
            return Result.Ok(userProfile);
        }
        public async Task<Result<string>> GetUserFullName(string hostId)
        {
            var user = await _userManager.FindByIdAsync(hostId);
            return Result.Ok(user.FirstName + " " + user.LastName);
        }

        public async Task<Result<GuestUser>> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var guestUser = new GuestUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return guestUser;
        }
    }
}
