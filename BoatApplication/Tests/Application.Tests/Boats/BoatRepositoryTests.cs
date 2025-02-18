using BoatApplication.Application.Boats.Commands.CreateBoat;
using BoatApplication.Domain.Boats.DTOs;
using BoatApplication.Domain.Boats.Interfaces;
using BoatApplication.Domain.Common.Exceptions;
using BoatApplication.Domain.Common.Interfaces.Services;
using BoatApplication.Domain.Common.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;


namespace BoatApplication.Application.Tests.Boats.Commands;
using static TestingSetup;

[TestFixture]
public sealed class BoatRepositoryTestes : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateBoat()
    {
        using var scope = _scopeFactory.CreateScope();
        var boatRepository = scope.ServiceProvider.GetRequiredService<IBoatRepository>();
        var userId = await RunAsDefaultUserAsync();

        var boat = new BoatDto()
        {
            Name = "Oasis of the Seas",
            Description = "A very big ship",
        };

        var response = await boatRepository.CreateBoatAsync(boat, CancellationToken.None);

        response.result.Succeeded.Should().BeTrue();
        response.boat.Should().NotBeNull();
        response.boat.Name.Should().Be(boat.Name);
        response.boat.Description.Should().Be(boat.Description);
    }
}
