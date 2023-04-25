using Identity.API.Models;
using Identity.API.Services.Register;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Data
{
    public static class AppDbContextSeed
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            await roleManager.CreateAsync(new IdentityRole<Guid>(UserType.HOST.ToString()));
            await roleManager.CreateAsync(new IdentityRole<Guid>(UserType.GUEST.ToString()));

            var host = new ApplicationUser()
            {
                Email = "host@email.com",
                UserName = "host",
                FirstName = "HostFirstName",
                LastName = "HostLastName",
                Address = new Address()
                {
                    Country = "Serbia",
                    City = "Belgrade",
                    Street = "Gagarinova",
                    Number = "1",
                }
            };
            await userManager.CreateAsync(host, "12345");

            var guest = new ApplicationUser()
            {
                Email = "guest@email.com",
                UserName = "guest",
                FirstName = "GuestFirstName",
                LastName = "GuestLastName",
                Address = new Address()
                {
                    Country = "Serbia",
                    City = "Belgrade",
                    Street = "Gagarinova",
                    Number = "1",
                }
            };
            await userManager.CreateAsync(guest, "12345");

            await userManager.AddToRoleAsync(host, UserType.HOST.ToString());
            await userManager.AddToRoleAsync(guest, UserType.GUEST.ToString());
        }
    }
}
