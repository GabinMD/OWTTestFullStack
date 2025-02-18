using BoatApplication.Application.Boats.Commands.CreateBoat;
using BoatApplication.Application.Boats.Commands.UpdateBoat;
using BoatApplication.Domain.Boats.DTOs;
using BoatApplication.Domain.Boats.Entitites;
using BoatApplication.Domain.Common.Exceptions;
using BoatApplication.Domain.Common.Models;
using FluentAssertions;
using NUnit.Framework;


namespace BoatApplication.Application.Tests.Boats.Commands;
using static TestingSetup;

[TestFixture]
public sealed class UpdateBoatTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var userId = await RunAsDefaultUserAsync();
        var command = new UpdateBoatCommand();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldUpdateBoat()
    {
        var userId = await RunAsDefaultUserAsync();

        var createResult = await SendAsync(new CreateBoatCommand()
        {
            Boat = new BoatDto()
            {
                Name = "Oasis of the Seas",
                Description = "A very big ship",
            }
        });

        createResult.Should().NotBeNull();
        createResult.Status.Should().Be(BaseAPIResponse.EStatus.Success);
        createResult.Errors.Should().BeEmpty();
        createResult.Boat.Should().NotBeNull();

        var command = new UpdateBoatCommand
        {
            Boat = new BoatDto()
            {
                Id = createResult.Boat.Id,
                Name = "Titanic",
                Description = "Titanic is better",
            },
            Id = createResult.Boat.Id
        };

        var updateResult = await SendAsync(command);

        updateResult.Should().NotBeNull();
        updateResult.Status.Should().Be(BaseAPIResponse.EStatus.Success);
        updateResult.Errors.Should().BeEmpty();
        updateResult.Boat.Should().NotBeNull();
        updateResult.Boat.Id.Should().Be(command.Id);
        updateResult.Boat.Name.Should().Be(command.Boat.Name);
        updateResult.Boat.Description.Should().Be(command.Boat.Description);
    }

    [Test]
    public async Task ShouldNotUpdateBoatWhenNotOwner()
    {
        var userId = await RunAsDefaultUserAsync();

        var createResult = await SendAsync(new CreateBoatCommand()
        {
            Boat = new BoatDto()
            {
                Name = "Oasis of the Seas",
                Description = "A very big ship",
            }
        });

        createResult.Should().NotBeNull();
        createResult.Status.Should().Be(BaseAPIResponse.EStatus.Success);
        createResult.Errors.Should().BeEmpty();
        createResult.Boat.Should().NotBeNull();


        userId = await RunAsCustomUserAsync("test2", "Testing1234!");


        var command = new UpdateBoatCommand
        {
            Boat = new BoatDto()
            {
                Id = createResult.Boat.Id,
                Name = "Titanic",
                Description = "Titanic is better",
            },
            Id = createResult.Boat.Id
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ForbiddenAccessException>();
    }
}
