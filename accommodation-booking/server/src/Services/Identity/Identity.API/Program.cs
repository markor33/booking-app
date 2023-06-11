using GrpcAccommodationSearch;
using GrpcReservations;
using Identity.API.Data;
using Identity.API.Integration;
using Identity.API.Integration.EventBus;
using Identity.API.Integration.Events;
using Identity.API.Integration.Extensions;
using Identity.API.Integration.SubscriptionManager;
using Identity.API.Models;
using Identity.API.Options;
using Identity.API.Services;
using Identity.API.Services.Login;
using Identity.API.Services.Register;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NATS.Client;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Identity")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
{
    builder
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .SetIsOriginAllowed((host) => true)
    .WithOrigins("http://localhost:4200/");

}));

builder.Services.AddTransient<LoginService>();
builder.Services.AddTransient<RegisterService>();
builder.Services.AddTransient<ApplicationUserService>();

builder.Services.AddGrpcClient<Reservations.ReservationsClient>((services, options) =>
{
    options.Address = new Uri("http://host.docker.internal:12001");
});
builder.Services.AddGrpcClient<AccommodationSearch.AccommodationSearchClient>((services, options) =>
{
    options.Address = new Uri("http://host.docker.internal:13001");
});

builder.Services.AddSingleton<ISubscriptionManager, SubscriptionManager>();
builder.Services.AddSingleton<IConnection>(provider =>
{
    var factory = new ConnectionFactory();
    var url = builder.Configuration.GetSection("NATS").GetValue<string>("Url");
    return factory.CreateConnection(url);
});
builder.Services.AddSingleton<IEventBus, NatsEventBus>();
builder.Services.AddIntegrationEventsHandlers(typeof(TestIntegrationEventHandler).Assembly);

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.AddHandlers(typeof(TestIntegrationEvent).Assembly);

// await AppDbContextSeed.Seed(app.Services.CreateScope().ServiceProvider);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
