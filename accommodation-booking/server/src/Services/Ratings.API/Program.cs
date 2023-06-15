using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Ratings.API.Infrastructure.Persistence;
using Ratings.API.Infrastructure.Persistence.Repositories;
using Ratings.API.Security;
using RatingsLibrary.Repository;
using RatingsLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("Default")
                .AddScheme<AuthenticationSchemeOptions, AuthHandler>("Default", null);
builder.Services.AddAuthorization();

builder.Services.AddDbContext<RatingsDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Ratings")));

builder.Services.AddScoped(typeof(IAccommodationRatingRepository), typeof(AccommodationRatingRepository));
builder.Services.AddScoped(typeof(IReservationRepository), typeof(ReservationRepository));
builder.Services.AddScoped(typeof(IHostRatingRepository), typeof(HostRatingRepository));
builder.Services.AddScoped(typeof(IProminentHostRepository), typeof(ProminentHostRepository));

builder.Services.AddScoped(typeof(IAccommodationRatingService), typeof(AccommodationRatingService));
builder.Services.AddScoped(typeof(IReservationService), typeof(ReservationService));
builder.Services.AddScoped(typeof(IHostRatingService), typeof(HostRatingService));
builder.Services.AddScoped(typeof(IProminentHostService), typeof(ProminentHostService));

builder.Services.AddScoped<IBookingAPIClient, BookingAPIClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
