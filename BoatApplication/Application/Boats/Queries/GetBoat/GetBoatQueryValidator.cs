using BoatApplication.Application.Boat.Queries.GetBoat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Application.Boats.Queries.GetBoat
{
    public class GetBoatQueryValidator : AbstractValidator<GetBoatQuery>
    {
        public GetBoatQueryValidator() {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
        }
    }
}
