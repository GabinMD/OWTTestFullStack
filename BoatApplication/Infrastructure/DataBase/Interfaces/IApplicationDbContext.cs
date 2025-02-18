using BoatApplication.Domain.Boats.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Infrastructure.DataBase.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Boat> Boats { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
