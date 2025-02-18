using BoatApplication.Application.Boats.Attributes;
using BoatApplication.Application.Boats.Interfaces;
using BoatApplication.Application.Boats.Models;
using BoatApplication.Domain.Boats.DTOs;
using BoatApplication.Domain.Boats.Interfaces;
using BoatApplication.Domain.Common.Models;
using BoatApplication.Domain.Identity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Application.Boats.Commands.DeleteBoat
{
    [BoatAuthorize(Policy = "IsOwner")]
    public class DeleteBoatCommand() : IRequest<BaseAPIResponse>, ITargetBoatRequest
    {
        public int Id { get; init; }
    }

    public class DeleteBoatCommandHandler : IRequestHandler<DeleteBoatCommand, BaseAPIResponse>
    {
        private readonly IBoatRepository _boatRepository;
        private readonly IMapper _mapper;

        public DeleteBoatCommandHandler(IBoatRepository boatRepository, IMapper mapper)
        {
            _boatRepository = boatRepository;
            _mapper = mapper;
        }
        public async Task<BaseAPIResponse> Handle(DeleteBoatCommand request, CancellationToken cancellationToken)
        {
            var deleteResult = await _boatRepository.DeleteBoatAsync(request.Id, cancellationToken);

            return deleteResult.Succeeded ? BaseAPIResponse.Success() : BaseAPIResponse.Failure(deleteResult.Errors.ToList());
        }
    }
}
