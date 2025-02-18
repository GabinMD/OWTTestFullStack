using BoatApplication.Application.Boats.Interfaces;
using BoatApplication.Domain.Boats.DTOs;
using BoatApplication.Domain.Boats.Interfaces;

namespace BoatApplication.Application.Boats.Queries.GetBoats
{
    public record class GetBoatsQuery : IRequest<GetBoatsResponse>, IBoatRequest
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetBoatsQueryHandler : IRequestHandler<GetBoatsQuery, GetBoatsResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBoatRepository _boatRepository;
        public GetBoatsQueryHandler(IBoatRepository boatRepository, IMapper mapper)
        {
            _boatRepository = boatRepository;
            _mapper = mapper;
        }
        public async Task<GetBoatsResponse> Handle(GetBoatsQuery request, CancellationToken cancellationToken)
        {
            var result = await _boatRepository.GetBoatsAsync(request.PageNumber, request.PageSize);

            if (!result.result.Succeeded)
                return GetBoatsResponse.Failure(result.result.Errors.ToList());

            return GetBoatsResponse.Success(result.boats.MapTo<BoatDto>(_mapper));
        }
    }
}
