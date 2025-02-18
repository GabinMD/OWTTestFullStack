using BoatApplication.Domain.Common.Models;
using BoatApplication.Domain.Identity.Interfaces;

namespace BoatApplication.Domain.Common.Interfaces.Services
{
    public interface IBoatService
    {
        Task<bool> IsBoatOwner(string userId, int boatId);
        Task<bool> IsBoatOwner(IUser user, int boatId) { return IsBoatOwner(user.Id ?? "", boatId); }
    }
}
