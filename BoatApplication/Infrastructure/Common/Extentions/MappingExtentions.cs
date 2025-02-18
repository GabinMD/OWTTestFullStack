using BoatApplication.Domain.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Infrastructure.Common.Extentions
{
    public static class MappingExtentions
    {
        public static async Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(
            this IQueryable<TDestination> queryable,
            int pageNumber,
            int pageSize) where TDestination : class
        {
            int count = await queryable.CountAsync();
            List<TDestination> items = await queryable.AsNoTracking().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<TDestination>(items, count, pageNumber, pageSize);
        }

    }
}
