
namespace BoatApplication.Infrastructure.DataBase.Extentions;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<DbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SetupDefaultAsync();
    }
}
