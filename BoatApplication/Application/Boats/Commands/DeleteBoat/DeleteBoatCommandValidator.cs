using BoatApplication.Application.Boats.Commands.DeleteBoat;
using FluentValidation;

namespace BoatApplication.Application.Boats.Commands.CreateBoat
{
    public class DeleteBoatCommandValidator : AbstractValidator<DeleteBoatCommand>
    {
        public DeleteBoatCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
