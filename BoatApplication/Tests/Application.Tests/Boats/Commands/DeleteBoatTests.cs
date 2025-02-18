using BoatApplication.Application.Boats.Commands.CreateBoat;
using BoatApplication.Application.Boats.Commands.DeleteBoat;
using BoatApplication.Domain.Boats.DTOs;
using BoatApplication.Domain.Boats.Entitites;
using BoatApplication.Domain.Common.Exceptions;
using BoatApplication.Domain.Common.Models;
using FluentAssertions;
using NUnit.Framework;


namespace BoatApplication.Application.Tests.Boats.Commands;
using static TestingSetup;

[TestFixture]
public sealed class DeleteBoatTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var userId = await RunAsDefaultUserAsync();
        var command = new DeleteBoatCommand();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldDeleteBoat()
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

        var command = new DeleteBoatCommand
        {
            Id = createResult.Boat.Id
        };

        var updateResult = await SendAsync(command);

        updateResult.Should().NotBeNull();
        updateResult.Status.Should().Be(BaseAPIResponse.EStatus.Success);
        updateResult.Errors.Should().BeEmpty();
    }

    [Test]
    public async Task ShouldNotDeleteBoatWhenNotOwner()
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


        var command = new DeleteBoatCommand
        {
            Id = createResult.Boat.Id
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ForbiddenAccessException>();
    }
}
