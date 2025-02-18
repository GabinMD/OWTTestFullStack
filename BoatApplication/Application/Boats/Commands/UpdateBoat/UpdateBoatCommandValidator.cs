using BoatApplication.Application.Boats.Commands.UpdateBoat;
using BoatApplication.Domain.Boats.DTOs.Validators;
using FluentValidation;

namespace BoatApplication.Application.Boats.Commands.CreateBoat
{
    public class UpdateBoatCommandValidator : AbstractValidator<UpdateBoatCommand>
    {
        public UpdateBoatCommandValidator()
        {
            RuleFor(x => x.Boat).NotNull();
            RuleFor(x => x.Boat!).SetValidator(new BoatDtoValidator());
        }
    }
}
