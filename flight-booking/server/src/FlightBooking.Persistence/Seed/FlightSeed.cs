using FlightBooking.Business.Entities;
using FlightBooking.Business.Repositories;
using Microsoft.Extensions.DependencyInjection;


namespace FlightBooking.Persistence.Seed
{
    public static class FlightSeed
    {
        public static async Task SeedFlights(IServiceProvider services)
        {
            var flightRepository = services.GetRequiredService<IFlightRepository>();

             await flightRepository.CreateAsync(new Flight()
            {
                DepartureTime = new DateTime(2023, 6, 25, 20, 30, 0),
                LandingTime = new DateTime(2023, 6, 25, 23, 30, 0),
                Origin = "Belgrade",
                Destination = "Frankfurt",
                TicketPrice = 500,
                NumOfAvailableTickets = 120,
                ImgUrl = "https://res.cloudinary.com/disvuvajt/image/upload/v1680096546/frankfurt_ogmxqz.jpg"
             });

            await flightRepository.CreateAsync(new Flight()
            {
                DepartureTime = new DateTime(2023, 5, 20, 17, 30, 0),
                LandingTime = new DateTime(2023, 5, 20, 22, 30, 0),
                Origin = "Belgrade",
                Destination = "London",
                TicketPrice = 750,
                NumOfAvailableTickets = 100,
                ImgUrl = "https://res.cloudinary.com/disvuvajt/image/upload/v1680096546/london_dx1sgb.jpg"
            });

            await flightRepository.CreateAsync(new Flight()
            {
                DepartureTime = new DateTime(2023, 6, 20, 7, 30, 0),
                LandingTime = new DateTime(2023, 6, 20, 22, 0, 0),
                Origin = "Belgrade",
                Destination = "Miami",
                TicketPrice = 1750,
                NumOfAvailableTickets = 80,
                ImgUrl = "https://res.cloudinary.com/disvuvajt/image/upload/v1680096546/majami_uqv9yz.jpg"
            });
        }
    }
}
