using BoatApplication.Application.Boats.Interfaces;
using BoatApplication.Application.Boats.Models;
using BoatApplication.Domain.Boats.DTOs;
using BoatApplication.Domain.Boats.Interfaces;

namespace BoatApplication.Application.Boat.Queries.GetBoat
{
    public record class GetBoatQuery : IRequest<GetBoatResponse>, IBoatRequest
    {
        public int Id { get; init; } = -1;
    }

    public class GetBoatQueryHandler : IRequestHandler<GetBoatQuery, GetBoatResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBoatRepository _boatRepository;
        public GetBoatQueryHandler(IBoatRepository boatRepository, IMapper mapper)
        {
            _boatRepository = boatRepository;
            _mapper = mapper;
        }
        public async Task<GetBoatResponse> Handle(GetBoatQuery request, CancellationToken cancellationToken)
        {
            var result = await _boatRepository.GetBoatByIdAsync(request.Id);

            if (!result.result.Succeeded)
                return GetBoatResponse.Failure(result.result.Errors.ToList());

            return GetBoatResponse.Success(_mapper.Map<BoatDto>(result.boat));
        }
    }
}
