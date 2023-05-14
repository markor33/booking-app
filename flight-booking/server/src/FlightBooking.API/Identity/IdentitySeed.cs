using FlightBooking.API.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace FlightBooking.API.Identity
{
    public class IdentitySeed
    {
        public static async Task SeedIdentityDatabase(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await roleManager.CreateAsync(new ApplicationRole("USER"));
            await roleManager.CreateAsync(new ApplicationRole("ADMIN"));

            var user = new ApplicationUser()
            {
                UserName = "user",
                Email = "user@email.com"
            };
            await userManager.CreateAsync(user, "12345");

            var admin = new ApplicationUser()
            {
                UserName = "admin",
                Email = "admin@email.com"
            };
            await userManager.CreateAsync(admin, "12345");

            await userManager.AddToRoleAsync(user, "USER");
            await userManager.AddToRoleAsync(admin, "ADMIN");
        }
    }
}
