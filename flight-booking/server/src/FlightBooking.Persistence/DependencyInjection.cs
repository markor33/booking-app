using FlightBooking.Business.Repositories;
using FlightBooking.Business.Services;
using FlightBooking.Persistence.Repositories;
using FlightBooking.Persistence.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace FlightBooking.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddScoped(typeof(IMongoDbFactory), typeof(MongoDbFactory));

            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IFlightRepository), typeof(FlightRepository));

            services.AddScoped(typeof(IFlightService), typeof(FlightService));
            services.AddScoped(typeof(IUserService), typeof(UserService));

            services.AddScoped(typeof(IBookedFlightRepository), typeof(BookedFlightRepository));
            services.AddScoped(typeof(IBookedFlightService), typeof(BookedFlightService));

            return services;
        }
    }
}
