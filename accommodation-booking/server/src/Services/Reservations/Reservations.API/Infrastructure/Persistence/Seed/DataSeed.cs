using Microsoft.EntityFrameworkCore;
using ReservationsLibrary.Enums;
using ReservationsLibrary.Models;

namespace ReservationsLibrary.Data.Seed
{
    public static class DataSeed
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            var accommodations = new[]
            {
                new Accommodation(Guid.NewGuid(), Guid.NewGuid(), true),
                new Accommodation(Guid.NewGuid(), Guid.NewGuid(), false),
                new Accommodation(Guid.NewGuid(), Guid.NewGuid(), false),
                new Accommodation(Guid.NewGuid(), Guid.NewGuid(), true)

            };
            var reservationRequests = new[]
            {
                new ReservationRequest(Guid.NewGuid(),accommodations[0].Id,Guid.NewGuid(),2, 500),
                new ReservationRequest(Guid.NewGuid(),accommodations[1].Id,Guid.NewGuid(),1, 400),
            };
            modelBuilder.Entity<Accommodation>().HasData(accommodations);
            modelBuilder.Entity<ReservationRequest>(rr =>
            {
                rr.HasData(reservationRequests[0]);
                rr.OwnsOne(e => e.Period).HasData(new
                {
                    ReservationRequestId = reservationRequests[0].Id,
                    Start = new DateTime(2023, 6, 10),
                    End = new DateTime(2023, 6, 20)
                    
                });
                rr.HasData(reservationRequests[1]);
                rr.OwnsOne(e => e.Period).HasData(new
                {
                    ReservationRequestId = reservationRequests[1].Id,
                    Start = new DateTime(2023, 6, 15),
                    End = new DateTime(2023, 6, 22)
                });
            });

        }
    }
}
