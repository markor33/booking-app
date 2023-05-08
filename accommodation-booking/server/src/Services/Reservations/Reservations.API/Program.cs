using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Reservations.API.Infrasructure;
using ReservationsLibrary.Data;
using ReservationsLibrary.Services;
using Microsoft.Extensions.DependencyInjection;
using Reservations.API.DTO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ReservationAPI", Version = "v1" });
});

builder.Services.AddDbContext<ReservationsDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Reservations")));
builder.Services.AddScoped(typeof(IAccommodationRepository), typeof(AccommodationRepository));
builder.Services.AddScoped(typeof(IPriceRepository), typeof(PriceRepository));
builder.Services.AddScoped(typeof(IReservationRepository), typeof(ReservationRepository));
builder.Services.AddScoped(typeof(IReservationRequestRepository), typeof(ReservationRequestRepository));

builder.Services.AddScoped(typeof(IAccommodationService), typeof(AccommodationService));
builder.Services.AddScoped(typeof(IReservationRequestService), typeof(ReservationRequestService));
builder.Services.AddScoped(typeof(IReservationService), typeof(ReservationService));

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
