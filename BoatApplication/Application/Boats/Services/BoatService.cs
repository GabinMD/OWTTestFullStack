using BoatApplication.Domain.Boats.Interfaces;
using BoatApplication.Domain.Common.Interfaces.Services;

namespace BoatApplication.Application.Boats.Services
{
    public class BoatService : IBoatService
    {
        private readonly IBoatRepository _boatRepository;

        public BoatService(IIdentityService identityService, IBoatRepository boatRepository )
        {
            _boatRepository = boatRepository;
        }

        public async Task<bool> IsBoatOwner(string userId, int boatId)
        {
            var boatResult = await _boatRepository.GetBoatByIdAsync(boatId);

            if (!boatResult.result.Succeeded || boatResult.boat == null)
                return false;

            return boatResult.boat.CreatedBy == userId;
        }
    }
}
