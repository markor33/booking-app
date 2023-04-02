using FlightBooking.Business.Entities;
using FlightBooking.Business.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.ConstrainedExecution;


namespace FlightBooking.Persistence.Seed
{
    public static class UserSeed
    {
        public static async Task SeedUsers(IServiceProvider services)
        {
            var userRepository = services.GetRequiredService<IUserRepository>();

            await userRepository.CreateAsync(new User()
            {
                AppUserId = Guid.Parse("564495c9-7ce4-4f9c-a200-5439d8afe821"),
                FirstName = "User",
                LastName = "User",
                Address = new Address() {
                    Country = "Serbia",
                    City = "Novi Sad",
                    Street = "Zeleznicka",
                    Number = "13"
                }
            });

            await userRepository.CreateAsync(new User()
            {
                AppUserId = Guid.Parse("37c7eff7-3509-490e-854b-c8639291763e"),
                FirstName = "Admin",
                LastName = "Admin",
                Address = new Address()
                {
                    Country = "Serbia",
                    City = "Novi Sad",
                    Street = "Masinska",
                    Number = "49"
                }
            });

        }
    }
}
