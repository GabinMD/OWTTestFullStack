using BoatApplication.Application.Boats.Commands.CreateBoat;
using BoatApplication.Domain.Boats.DTOs;
using BoatApplication.Domain.Common.Exceptions;
using BoatApplication.Domain.Common.Interfaces.Services;
using BoatApplication.Domain.Common.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;


namespace BoatApplication.Application.Tests.Boats.Commands;
using static TestingSetup;

[TestFixture]
public sealed class BoatServiceTestes : BaseTestFixture
{
    [Test]
    public async Task ShouldBeBoatOwner()
    {
        using var scope = _scopeFactory.CreateScope();
        var boatService = scope.ServiceProvider.GetRequiredService<IBoatService>();

        var userId = await RunAsDefaultUserAsync();

        var command = new CreateBoatCommand
        {
            Boat = new BoatDto()
            {
                Name = "Oasis of the Seas",
                Description = "A very big ship",
            }
        };

        var requestResult = await SendAsync(command);

        requestResult.Should().NotBeNull();
        requestResult.Boat.Should().NotBeNull();

        var isOwner = await boatService.IsBoatOwner(userId, requestResult.Boat.Id);
        isOwner.Should().BeTrue();
    }
}
