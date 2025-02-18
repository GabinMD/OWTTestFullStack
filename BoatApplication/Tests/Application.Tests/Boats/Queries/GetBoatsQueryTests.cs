using BoatApplication.Application.Boats.Commands.CreateBoat;
using BoatApplication.Application.Boats.Queries.GetBoats;
using BoatApplication.Domain.Boats.DTOs;
using BoatApplication.Domain.Common.Exceptions;
using BoatApplication.Domain.Common.Models;
using FluentAssertions;
using NUnit.Framework;


namespace BoatApplication.Application.Tests.Boats.Commands;
using static TestingSetup;

[TestFixture]
public sealed class GetBoatsQueryTests : BaseTestFixture
{

    [Test]
    public async Task ShouldGetBoatsWithPagination()
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

        await SendAsync(command);
        await SendAsync(command);
        await SendAsync(command);
        await SendAsync(command);
        await SendAsync(command);

        var query = new GetBoatsQuery()
        {
            PageNumber = 1,
            PageSize = 4
        };

        var requestResult = await SendAsync(query);

        requestResult.Should().NotBeNull();
        requestResult.Status.Should().Be(BaseAPIResponse.EStatus.Success);
        requestResult.Errors.Should().BeEmpty();
        requestResult.Boats.Should().NotBeNull();
        requestResult.Boats.Items.Count.Should().Be(4);
        requestResult.Boats.TotalCount.Should().Be(5);

        query = new GetBoatsQuery()
        {
            PageNumber = 2,
            PageSize = 4
        };

        requestResult = await SendAsync(query);

        requestResult.Should().NotBeNull();
        requestResult.Status.Should().Be(BaseAPIResponse.EStatus.Success);
        requestResult.Errors.Should().BeEmpty();
        requestResult.Boats.Should().NotBeNull();
        requestResult.Boats.Items.Count.Should().Be(1);
        requestResult.Boats.TotalCount.Should().Be(5);
    }
}
