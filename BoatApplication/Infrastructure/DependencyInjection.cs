using BoatApplication.Domain.Boats.Interfaces;
using BoatApplication.Domain.Common.Interfaces.Services;
using BoatApplication.Domain.Constants;
using BoatApplication.Infrastructure;
using BoatApplication.Infrastructure.DataBase;
using BoatApplication.Infrastructure.DataBase.Interceptors;
using BoatApplication.Infrastructure.DataBase.Interfaces;
using BoatApplication.Infrastructure.DataBase.Repositories;
using BoatApplication.Infrastructure.Identity;
using BoatApplication.Infrastructure.Identity.Services;
using CleanArchitectureBoat.Infrastructure.Data.Interceptors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{

    //Default Identity Configuration
    public static IdentityBuilder AddDefaultIdentity<TUser>(this IServiceCollection services, IConfigurationManager configuration)
         where TUser : class
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? "")), // Clé secrète
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                options.MapInboundClaims = false;
            });

        services.AddOptions().AddLogging();

        services.TryAddScoped<IUserValidator<TUser>, UserValidator<TUser>>();
        services.TryAddScoped<IPasswordValidator<TUser>, PasswordValidator<TUser>>();
        services.TryAddScoped<IPasswordHasher<TUser>, PasswordHasher<TUser>>();
        services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
        services.TryAddScoped<IUserConfirmation<TUser>, DefaultUserConfirmation<TUser>>();

        services.TryAddScoped<IdentityErrorDescriber>();

        services.TryAddScoped<IUserClaimsPrincipalFactory<TUser>, UserClaimsPrincipalFactory<TUser>>();
        services.TryAddScoped<UserManager<TUser>>();


        return new IdentityBuilder(typeof(TUser), services);
    }

    //Configure DbContext And Identity + Authentication
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString(Constants.ApplicationDbContext.ConnectionString);
        Guard.Against.Null(connectionString,
            message: $"Connection string '{Constants.ApplicationDbContext.ConnectionString}' not found.");

        var migrationsAssembly = typeof(ApplicationDbContext).Assembly.GetName().Name;
        builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly(migrationsAssembly));
        });

        builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        builder.Services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        builder.Services.AddScoped<DbContextInitialiser>();

        builder.Services
            .AddDefaultIdentity<User>(builder.Configuration)
            .AddSignInManager<SignInManager<User>>()
            .AddUserManager<UserManager<User>>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddTransient<IIdentityService, IdentityService>();
        builder.Services.AddTransient<ITokenService, TokenService>();
        builder.Services.AddTransient<IBoatRepository, BoatRepository>();

        builder.Services.AddAuthorization(options =>
            options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));

        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}
