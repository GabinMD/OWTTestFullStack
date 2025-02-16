using BoatApplication.Infrastructure.DataBase;
using BoatApplication.WebApp.Server.Services.Identity;
using BoatApplication.Domain.Identity.Interfaces;


namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddWebServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUser, CurrentUser>();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

       // builder.Services.AddExceptionHandler<CustomExceptionHandler>();

        builder.Services.AddEndpointsApiExplorer();
    }

    public static void AddCORS(this IHostApplicationBuilder builder) {
        var frontendUrl = builder.Configuration["AllowedCorsOrigins:Frontend"] ?? "";

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(Constants.WebApp.CorsFrontendPolicy, policy =>
            {
                policy.WithOrigins(frontendUrl)
                      .WithMethods("GET", "POST", "PUT", "DELETE")
                      .WithHeaders("Authorization", "Content-Type")
                      .AllowCredentials();
            });
        });
    }
}
