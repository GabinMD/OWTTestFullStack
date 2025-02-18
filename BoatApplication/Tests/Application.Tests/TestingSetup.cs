using Azure.Core;
using BoatApplication.Application.Boats.Services;
using BoatApplication.Application.Tests.Setup;
using BoatApplication.Domain.Boats.Interfaces;
using BoatApplication.Domain.Common.Interfaces.Services;
using BoatApplication.Domain.Constants;
using BoatApplication.Infrastructure.DataBase;
using BoatApplication.Infrastructure.DataBase.Repositories;
using BoatApplication.Infrastructure.Identity;
using BoatApplication.Infrastructure.Identity.Extentions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BoatApplication.Application.Tests;

[SetUpFixture]
public partial class TestingSetup
{
    private static ITestDatabase _database = null!;
    private static CustomWebApplicationFactory _factory = null!;
    public static IServiceScopeFactory _scopeFactory = null!;
    private static string? _userId;

    [OneTimeSetUp]
    public async Task RunBeforeAnyTests()
    {
        _database = await TestDatabaseFactory.CreateAsync();

        _factory = new CustomWebApplicationFactory(_database.GetConnection(), _database.GetConnectionString());

        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    public static IBoatRepository BoatRepository()
    {
        using var scope = _scopeFactory.CreateScope();

        var boatRepository = _factory.Services.GetRequiredService<IBoatRepository>();

        return boatRepository;
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task SendAsync(IBaseRequest request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        await mediator.Send(request);
    }

    public static string? GetUserId()
    {
        return _userId;
    }

    public static async Task<string> RunAsDefaultUserAsync()
    {
        return await RunAsUserAsync("test", "Testing1234!", Array.Empty<string>());
    }

    public static async Task<string> RunAsCustomUserAsync(string name, string passwd)
    {
        return await RunAsUserAsync(name, passwd, Array.Empty<string>());
    }

    public static async Task<string> RunAsAdministratorAsync()
    {
        return await RunAsUserAsync("admin", "AdminRoot1234!", new[] { Roles.Administrator });
    }

    public static async Task<string> RunAsUserAsync(string userName, string password, string[] roles)
    {
        using var scope = _scopeFactory.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        var user = new User { UserName = userName, Email = userName };

        var result = await userManager.CreateAsync(user, password);

        if (roles.Any())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            await userManager.AddToRolesAsync(user, roles);
        }

        if (result.Succeeded)
        {
            _userId = user.Id;

            return _userId;
        }

        var errors = string.Join(Environment.NewLine, result.ToApplicationResult().Errors.Select(e => $"{e.Code}:${e.Description}"));

        throw new Exception($"Unable to create {userName}.{Environment.NewLine}{errors}");
    }

    public static async Task ResetState()
    {
        try
        {
            await _database.ResetAsync();
        }
        catch (Exception)
        {
        }

        _userId = null;
    }

    public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public static async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.Set<TEntity>().CountAsync();
    }

    [OneTimeTearDown]
    public async Task RunAfterAnyTests()
    {
        await _database.DisposeAsync();
        await _factory.DisposeAsync();
    }
}