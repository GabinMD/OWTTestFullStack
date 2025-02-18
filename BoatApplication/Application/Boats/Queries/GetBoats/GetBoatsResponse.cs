using BoatApplication.Domain.Boats.DTOs;
using BoatApplication.Domain.Common.Models;

namespace BoatApplication.Application.Boats.Queries.GetBoats
{
    public class GetBoatsResponse : BaseAPIResponse
    {
        public PaginatedList<BoatDto>? Boats { get; set; }

        public static GetBoatsResponse Success(PaginatedList<BoatDto> boats)
        {
            return new GetBoatsResponse() { Boats = boats };
        }

        public static new GetBoatsResponse Failure(List<Error> errors)
        {
            return new GetBoatsResponse() { Status = EStatus.Error, Errors = errors };
        }
    }
}
