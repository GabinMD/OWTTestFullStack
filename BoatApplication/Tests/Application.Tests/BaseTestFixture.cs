namespace BoatApplication.Application.Tests;

using NUnit.Framework;
using static TestingSetup;

[TestFixture]
public abstract class BaseTestFixture
{
    [SetUp]
    public async Task TestSetUp()
    {
        await ResetState();
    }
}
