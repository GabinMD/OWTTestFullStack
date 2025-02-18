using BoatApplication.Domain.Boats.DTOs;
using BoatApplication.Domain.Common.Models;

namespace BoatApplication.Application.Boats.Models
{
    public class GetBoatResponse : BaseAPIResponse
    {
        public BoatDto? Boat { get; set; }

        public static GetBoatResponse Success(BoatDto boat)
        {
            return new GetBoatResponse() { Boat = boat };
        }

        public static new GetBoatResponse Failure(List<Error> errors)
        {
            return new GetBoatResponse() { Status = EStatus.Error, Errors = errors };
        }
    }
}
