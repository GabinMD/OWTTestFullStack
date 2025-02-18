using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BoatApplication.Infrastructure.Identity;
using BoatApplication.Infrastructure.DataBase.Interfaces;
using BoatApplication.Domain.Boats.Entitites;

namespace BoatApplication.Infrastructure.DataBase;

public class ApplicationDbContext : IdentityDbContext<User>, IApplicationDbContext
{
    public DbSet<Boat> Boats => Set<Boat>();
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
