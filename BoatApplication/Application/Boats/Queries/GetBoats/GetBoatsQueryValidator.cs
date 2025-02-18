using BoatApplication.Application.Boats.Queries.GetBoats;
using FluentValidation;

namespace BoatApplication.Application.Boats.Queries.GetBoat
{
    public class GetBoatsQueryValidator : AbstractValidator<GetBoatsQuery>
    {
        public GetBoatsQueryValidator() {
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(0);
        }
    }
}
