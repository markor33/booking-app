using Microsoft.EntityFrameworkCore;
using Ratings.Utils;
using RatingsLibrary.Models;

namespace Ratings.API.Infrastructure.Persistence.Seed
{
    public static class DataSeed
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            var reservations = new[]
            {
                new Reservation{ Id = Guid.NewGuid(), GuestId = Guid.NewGuid(), AccommodationId = Guid.NewGuid(), HostId = Guid.NewGuid()},
                new Reservation{ Id = Guid.NewGuid(), GuestId = Guid.NewGuid(), AccommodationId = Guid.NewGuid(), HostId = Guid.NewGuid()}

            };

             modelBuilder.Entity<Reservation>(rr =>
             {
                 rr.HasData(reservations[0]);
                 rr.OwnsOne(e => e.Period).HasData(new
                 {
                     ReservationId = reservations[0].Id,
                     Start = new DateTime(2023, 4, 10),
                     End = new DateTime(2023, 4, 25)

                 });
                 rr.HasData(reservations[1]);
                 rr.OwnsOne(e => e.Period).HasData(new
                 {
                     ReservationId = reservations[1].Id,
                     Start = new DateTime(2023, 5, 20),
                     End = new DateTime(2023, 5, 25)
                 });
             });
        }
    }
}
