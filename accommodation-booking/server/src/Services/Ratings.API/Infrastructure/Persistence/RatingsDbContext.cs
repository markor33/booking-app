using Microsoft.EntityFrameworkCore;
using Ratings.API.Infrastructure.Persistence.Seed;
using RatingsLibrary.Models;

namespace Ratings.API.Infrastructure.Persistence
{
    public class RatingsDbContext : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<AccommodationRating> AccommodationRatings { get; set; }
        public DbSet<HostRating> HostRatings { get; set; }
        public DbSet<ProminentHost> ProminentHosts { get; set; }

        public RatingsDbContext(DbContextOptions<RatingsDbContext> options) : base(options)
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

            builder.SeedData();

            base.OnModelCreating(builder);
        }
    }
}
