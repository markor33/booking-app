using EventBus.NET.Integration.EventBus;
using FluentResults;
using Identity.API.IntegrationEvents;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Services.Register
{
    public class RegisterService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEventBus _eventBus;

        public RegisterService(UserManager<ApplicationUser> userManager, IEventBus eventBus)
        {
            _userManager = userManager;
            _eventBus = eventBus;
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
            _eventBus.Publish(new HostRegisteredIntegrationEvent(user.Id));
            if (!result.Succeeded)
                return Result.Fail("Registration failed");

            result = await _userManager.AddToRoleAsync(user, register.UserType.ToString());
            if (!result.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return Result.Fail("Registration failed");
            }

            if (register.UserType == UserType.GUEST)
                _eventBus.Publish(new GuestCreatedIntegrationEvent(user.Id, user.FirstName + " " + user.LastName));

            return Result.Ok();
        }
    }
}
