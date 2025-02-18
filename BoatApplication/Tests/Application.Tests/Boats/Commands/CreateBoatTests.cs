using BoatApplication.Application.Boats.Commands.CreateBoat;
using BoatApplication.Domain.Boats.DTOs;
using BoatApplication.Domain.Common.Exceptions;
using BoatApplication.Domain.Common.Models;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;


namespace BoatApplication.Application.Tests.Boats.Commands;
using static TestingSetup;

[TestFixture]
public sealed class CreateBoatTests : BaseTestFixture
{

    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateBoatCommand();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateBoat()
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

        var requestResult = await SendAsync(command);

        requestResult.Should().NotBeNull();
        requestResult.Status.Should().Be(BaseAPIResponse.EStatus.Success);
        requestResult.Errors.Should().BeEmpty();
        requestResult.Boat.Should().NotBeNull();
        requestResult.Boat.Id.Should().Be(1);
        requestResult.Boat.Name.Should().Be(command.Boat.Name);
        requestResult.Boat.Description.Should().Be(command.Boat.Description);
    }
}
