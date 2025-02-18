using BoatApplication.Application.Boat.Queries.GetBoat;
using FluentValidation;

namespace BoatApplication.Application.Boats.Queries.GetBoat
{
    public class GetBoatQueryValidator : AbstractValidator<GetBoatQuery>
    {
        public GetBoatQueryValidator() {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
        }
    }
}
