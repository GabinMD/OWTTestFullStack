using BoatApplication.Domain.Boats.DTOs.Validators;
using FluentValidation;

namespace BoatApplication.Application.Boats.Commands.CreateBoat
{
    public class CreateBoatCommandValidator : AbstractValidator<CreateBoatCommand>
    {
        public CreateBoatCommandValidator()
        {
            RuleFor(x => x.Boat).NotNull();
            RuleFor(x => x.Boat!).SetValidator(new BoatDtoValidator());
        }
    }
}
