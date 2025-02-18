using System.Data.Common;

namespace BoatApplication.Application.Tests.Setup;

public interface ITestDatabase
{
    Task InitialiseAsync();

    DbConnection GetConnection();

    string GetConnectionString();

    Task ResetAsync();

    Task DisposeAsync();
}
