using AutoMapper;
using BoatApplication.Domain.Boats.DTOs;
using BoatApplication.Domain.Boats.Entitites;
using BoatApplication.Domain.Boats.Interfaces;
using BoatApplication.Domain.Common.Models;
using BoatApplication.Domain.Identity.Interfaces;
using BoatApplication.Infrastructure.Common.Extentions;
using BoatApplication.Infrastructure.DataBase.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BoatApplication.Infrastructure.DataBase.Repositories
{
    public class BoatRepository : IBoatRepository
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public BoatRepository(IApplicationDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<(Result result, Boat? boat)> CreateBoatAsync(BoatDto boatDto, CancellationToken cancellationToken)
        {
            var boat = new Boat()
            {
                Name = boatDto.Name,
                Description = boatDto.Description,
            };

            _context.Boats.Add(boat);

            int changes = await _context.SaveChangesAsync(cancellationToken);

            if (changes == 0)
                return (Result.Failure(Constants.BoatRepositoryErrors.BoatNotCreated), boat);

            boat.OnCreated();
            return (Result.Success(), boat);
        }
        public async Task<(Result result, Boat? boat)> UpdateBoatAsync(BoatDto boatDto, CancellationToken cancellationToken)
        {
            var boat = await _context.Boats.FindAsync(boatDto.Id);
            if (boat == null)
                return (Result.Failure(Constants.BoatRepositoryErrors.BoatNotFound), null);

            boat.Name = boatDto.Name;
            boat.Description = boatDto.Description;

            int changes = await _context.SaveChangesAsync(cancellationToken);

            if (changes == 0)
                return (Result.Failure(Constants.BoatRepositoryErrors.BoatNotUpdated), boat);

            boat.OnModified(boatDto);
            return (Result.Success(), boat);
        }

        public async Task<Result> DeleteBoatAsync(int id, CancellationToken cancellationToken)
        {
            var boat = await _context.Boats.FindAsync(id);
            if (boat == null)
                return Result.Failure(Constants.BoatRepositoryErrors.BoatNotFound);
            _context.Boats.Remove(boat);
            int changes = await _context.SaveChangesAsync(cancellationToken);

            if (changes == 0)
                return Result.Failure(Constants.BoatRepositoryErrors.BoatNotDeleted);

            boat.OnDeleted(_mapper.Map<BoatDto>(boat));
            return Result.Success();
        }

        public async Task<(Result result, Boat? boat)> GetBoatByIdAsync(int id)
        {
            var boat = await _context.Boats.FindAsync(id);
            return (boat != null ? Result.Success() : Result.Failure(Constants.BoatRepositoryErrors.BoatNotFound), boat);
        }

        public async Task<(Result result, PaginatedList<Boat> boats)> GetBoatsAsync(int pageNumber = 0, int pageSize = 10)
        {
            var boats = await _context.Boats.PaginatedListAsync(pageNumber, pageSize);

            return (Result.Success(), boats);
        }

        public async Task<(Result result, PaginatedList<Boat> boats)> GetBoatsByUserAsync(IUser user, int pageNumber = 0, int pageSize = 10)
        {
            var boats = await _context.Boats.Where(b => b.CreatedBy == user.Id).PaginatedListAsync(pageNumber, pageSize);

            return (Result.Success(), boats);
        }

        public async Task<(Result result, int purgedBoats)> PurgeBoatsAsync(CancellationToken cancellationToken)
        {
            _context.Boats.RemoveRange(_context.Boats);
            int changes = await _context.SaveChangesAsync(cancellationToken);
            return (changes > 0 ? Result.Success() : Result.Failure(Constants.BoatRepositoryErrors.BoatsNotPurged), changes);
        }
    }
}
