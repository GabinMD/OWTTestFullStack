using BoatApplication.Application.Boat.Queries.GetBoat;
using BoatApplication.Application.Boats.Commands.CreateBoat;
using BoatApplication.Domain.Boats.DTOs;
using BoatApplication.Domain.Common.Exceptions;
using BoatApplication.Domain.Common.Models;
using FluentAssertions;
using NUnit.Framework;


namespace BoatApplication.Application.Tests.Boats.Commands;
using static TestingSetup;

[TestFixture]
public sealed class GetBoatQueryTests : BaseTestFixture
{

    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new GetBoatQuery();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldGetBoatById()
    {
        var userId = await RunAsDefaultUserAsync();

        var command = new CreateBoatCommand
        {
            Boat = new BoatDto()
            {
                Name = "Oasis of the Seas",
                Description = "A very big ship",
            }
        };

        var createResult = await SendAsync(command);

        createResult.Should().NotBeNull();
        createResult.Status.Should().Be(BaseAPIResponse.EStatus.Success);
        createResult.Errors.Should().BeEmpty();
        createResult.Boat.Should().NotBeNull();

        var getBoatQuery = new GetBoatQuery() { Id = createResult.Boat.Id };
        var queryResult = await SendAsync(getBoatQuery);

        queryResult.Should().NotBeNull();
        queryResult.Status.Should().Be(BaseAPIResponse.EStatus.Success);
        queryResult.Errors.Should().BeEmpty();
        queryResult.Boat.Should().NotBeNull();
        queryResult.Boat.Id.Should().Be(createResult.Boat.Id);
        queryResult.Boat.Name.Should().Be(command.Boat.Name);
        queryResult.Boat.Description.Should().Be(command.Boat.Description);
    }
}
