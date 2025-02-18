using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Domain.Boats.DTOs.Validators
{
    public class BoatDtoValidator : AbstractValidator<BoatDto>
    {
        public BoatDtoValidator() {
            RuleFor(b => b.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(b => b.Name).MaximumLength(500).WithMessage("Name must not exceed 500 characters");
        }
    }
}
