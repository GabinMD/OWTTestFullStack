using BoatApplication.Application.Boats.Interfaces;
using BoatApplication.Application.Common.Attributes;
using BoatApplication.Domain.Boats.Interfaces;

namespace BoatApplication.Application.Boats.Commands.PurgeBoats
{
    [Authorize(Roles = "Admin")]
    [Authorize(Policy = "CanPurge")]
    public class PurgeBoatsCommand : IRequest<PurgeBoatResponse>, IBoatRequest
    {
    }

    public class PurgeBoatsCommandHandler : IRequestHandler<PurgeBoatsCommand, PurgeBoatResponse>
    {
        private readonly IBoatRepository _boatRepository;
        private readonly IMapper _mapper;

        public PurgeBoatsCommandHandler(IBoatRepository boatRepository, IMapper mapper)
        {
            _boatRepository = boatRepository;
            _mapper = mapper;
        }
        public async Task<PurgeBoatResponse> Handle(PurgeBoatsCommand request, CancellationToken cancellationToken)
        {
            var purgeResults = await _boatRepository.PurgeBoatsAsync(cancellationToken);
            
            if (!purgeResults.result.Succeeded)
                return PurgeBoatResponse.Failure(purgeResults.result.Errors.ToList());

            return PurgeBoatResponse.Success(purgeResults.purgedBoats);
        }
    }
}
