using BoatApplication.Application.Boat.Queries.GetBoat;
using BoatApplication.Application.Boats.Models;
using BoatApplication.Domain.Boats.DTOs;
using BoatApplication.Domain.Boats.DTOs.Validators;
using BoatApplication.Domain.Boats.Interfaces;
using BoatApplication.Domain.Identity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
