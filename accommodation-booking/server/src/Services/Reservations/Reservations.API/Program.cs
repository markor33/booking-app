using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Reservations.API.Infrasructure;
using ReservationsLibrary.Data;
using ReservationsLibrary.Services;
using Reservations.API.DTO;
using Microsoft.AspNetCore.Authentication;
using Reservations.API.Security;
using Reservations.API.Infrasructure.Persistence.Repositories;
using Reservations.API.GrpcServices;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ReservationAPI", Version = "v1" });
});

builder.Services.AddAuthentication("Default")
                .AddScheme<AuthenticationSchemeOptions, AuthHandler>("Default", null);
builder.Services.AddAuthorization();

builder.Services.AddDbContext<ReservationsDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Reservations")));
builder.Services.AddScoped(typeof(IAccommodationRepository), typeof(AccommodationRepository));
builder.Services.AddScoped(typeof(IPriceRepository), typeof(PriceRepository));
builder.Services.AddScoped(typeof(IReservationRepository), typeof(ReservationRepository));
builder.Services.AddScoped(typeof(IReservationRequestRepository), typeof(ReservationRequestRepository));

builder.Services.AddScoped(typeof(IAccommodationService), typeof(AccommodationService));
builder.Services.AddScoped(typeof(IReservationRequestService), typeof(ReservationRequestService));
builder.Services.AddScoped(typeof(IReservationService), typeof(ReservationService));

builder.Services.AddScoped<IHospitalAPIClient, HospitalAPIClient>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
});

builder.WebHost.UseKestrel(options => {
    options.Listen(IPAddress.Any, 80, listenOptions => {
        listenOptions.Protocols = HttpProtocols.Http1;
    });

    options.Listen(IPAddress.Any, 5000, listenOptions => {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<ReservationsGrpcService>();

app.Run();
