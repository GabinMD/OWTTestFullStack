using BoatApplication.Application.Boat.Queries.GetBoat;
using BoatApplication.Application.Boats.Commands.DeleteBoat;
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
    public class DeleteBoatCommandValidator : AbstractValidator<DeleteBoatCommand>
    {
        public DeleteBoatCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
        }
    }
}
