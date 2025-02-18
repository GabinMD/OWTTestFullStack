using BoatApplication.Application.Boats.Interfaces;
using BoatApplication.Application.Boats.Models;
using BoatApplication.Domain.Boats.DTOs;
using BoatApplication.Domain.Boats.Interfaces;
using BoatApplication.Domain.Identity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Application.Boats.Commands.CreateBoat
{
    public class CreateBoatCommand : IRequest<GetBoatResponse>, IBoatRequest
    {
        public BoatDto? Boat { get; init; }
    }

    public class CreateBoatCommandHandler : IRequestHandler<CreateBoatCommand, GetBoatResponse>
    {
        private readonly IBoatRepository _boatRepository;
        private readonly IMapper _mapper;

        public CreateBoatCommandHandler(IBoatRepository boatRepository, IMapper mapper)
        {
            _boatRepository = boatRepository;
            _mapper = mapper;
        }
        public async Task<GetBoatResponse> Handle(CreateBoatCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request.Boat, nameof(request.Boat));

            var createResult = await _boatRepository.CreateBoatAsync(request.Boat, cancellationToken);

            if (!createResult.result.Succeeded)
                return GetBoatResponse.Failure(createResult.result.Errors.ToList());

            return GetBoatResponse.Success(_mapper.Map<BoatDto>(createResult.boat));
        }
    }
}
