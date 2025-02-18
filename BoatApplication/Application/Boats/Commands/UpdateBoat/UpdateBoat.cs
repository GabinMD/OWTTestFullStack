using BoatApplication.Application.Boats.Attributes;
using BoatApplication.Application.Boats.Interfaces;
using BoatApplication.Application.Boats.Models;
using BoatApplication.Application.Common.Attributes;
using BoatApplication.Domain.Boats.DTOs;
using BoatApplication.Domain.Boats.Interfaces;
using BoatApplication.Domain.Common.Models;
using BoatApplication.Domain.Constants;
using BoatApplication.Domain.Identity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Application.Boats.Commands.UpdateBoat
{
    [BoatAuthorize(Policy = "IsOwner")]
    public class UpdateBoatCommand : IRequest<GetBoatResponse>, ITargetBoatRequest
    {
        public int Id { get; init; } = -1;
        public BoatDto? Boat { get; init; }
    }

    public class UpdateBoatCommandHandler : IRequestHandler<UpdateBoatCommand, GetBoatResponse>
    {
        private readonly IBoatRepository _boatRepository;
        private readonly IMapper _mapper;

        public UpdateBoatCommandHandler(IBoatRepository boatRepository, IMapper mapper)
        {
            _boatRepository = boatRepository;
            _mapper = mapper;
        }
        public async Task<GetBoatResponse> Handle(UpdateBoatCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request.Boat, nameof(request.Boat));

            var updateResult = await _boatRepository.UpdateBoatAsync(request.Boat, cancellationToken);

            if (!updateResult.result.Succeeded)
                return GetBoatResponse.Failure(updateResult.result.Errors.ToList());

            return GetBoatResponse.Success(_mapper.Map<BoatDto>(updateResult.boat));
        }
    }
}
