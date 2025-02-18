using BoatApplication.Application.Boats.Commands.CreateBoat;
using BoatApplication.Application.Boats.Commands.PurgeBoats;
using BoatApplication.Application.Common.Attributes;
using BoatApplication.Domain.Boats.DTOs;
using BoatApplication.Domain.Boats.Entitites;
using BoatApplication.Domain.Common.Exceptions;
using BoatApplication.Domain.Common.Models;
using FluentAssertions;
using NUnit.Framework;

namespace BoatApplication.Application.Tests.Boats.Commands;

using static TestingSetup;

[TestFixture]
public sealed class PurgeBoatsTests : BaseTestFixture
{
    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        var command = new PurgeBoatsCommand();

        command.GetType().Should().BeDecoratedWith<AuthorizeAttribute>();

        var action = () => SendAsync(command);

        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task ShouldDenyNonAdministrator()
    {
        await RunAsDefaultUserAsync();

        var command = new PurgeBoatsCommand();

        var action = () => SendAsync(command);

        await action.Should().ThrowAsync<ForbiddenAccessException>();
    }

    [Test]
    public async Task ShouldAllowAdministrator()
    {
        await RunAsAdministratorAsync();

        var command = new PurgeBoatsCommand();

        var action = () => SendAsync(command);

        await action.Should().NotThrowAsync<ForbiddenAccessException>();
    }
    [Test]
    public async Task ShouldDeleteAllLists()
    {
        await RunAsAdministratorAsync();

        await SendAsync(new CreateBoatCommand
        {
            Boat = new BoatDto
            {
                Name = "Boat #1",
                Description = "Description",
            }
        });

        await SendAsync(new CreateBoatCommand
        {
            Boat = new BoatDto
            {
                Name = "Boat #2",
                Description = "Description",
            }
        });

        await SendAsync(new CreateBoatCommand
        {
            Boat = new BoatDto
            {
                Name = "Boat #3",
                Description = "Description",
            }
        });

        await SendAsync(new PurgeBoatsCommand());

        var count = await CountAsync<Domain.Boats.Entitites.Boat>();

        count.Should().Be(0);
    }
}
