﻿using Microsoft.EntityFrameworkCore;
using ReservationsLibrary.Data.Seed;
using ReservationsLibrary.Models;
using ReservationsLibrary.Repository.Base;

namespace ReservationsLibrary.Data
{
    public class ReservationsDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<ReservationRequest> ReservationRequests { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public ReservationsDbContext(DbContextOptions<ReservationsDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Reservation>()
                .OwnsOne(e => e.Period)
                .Property(p => p.Start)
                .HasColumnName("Start");

            builder.Entity<Reservation>()
                .OwnsOne(e => e.Period)
                .Property(p => p.End)
                .HasColumnName("End");

            builder.Entity<ReservationRequest>()
                .OwnsOne(e => e.Period)
                .Property(p => p.Start)
                .HasColumnName("Start");

            builder.Entity<ReservationRequest>()
                .OwnsOne(e => e.Period)
                .Property(p => p.End)
                .HasColumnName("End");

            builder.SeedData();
            base.OnModelCreating(builder);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await base.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
