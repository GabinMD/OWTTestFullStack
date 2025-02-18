using BoatApplication.Domain.Boats.DTOs;
using BoatApplication.Domain.Boats.Entitites;
using BoatApplication.Domain.Common.Models;
using BoatApplication.Domain.Identity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Domain.Boats.Interfaces
{
    public interface IBoatRepository
    {
        Task<(Result result, Boat? boat)> GetBoatByIdAsync(int id);
        Task<(Result result, Boat? boat)> CreateBoatAsync(BoatDto boat, CancellationToken cancellationToken);
        Task<(Result result, Boat? boat)> UpdateBoatAsync(BoatDto boat, CancellationToken cancellationToken);
        Task<Result> DeleteBoatAsync(int id, CancellationToken cancellationToken);
        Task<(Result result, PaginatedList<Boat> boats)> GetBoatsAsync(int pageNumber = 0, int pageSize = 10);
        Task<(Result result, PaginatedList<Boat> boats)> GetBoatsByUserAsync(IUser user, int pageNumber = 0, int pageSize = 10);
        Task<(Result result, int purgedBoats)> PurgeBoatsAsync(CancellationToken cancellationToken);
    }
}
