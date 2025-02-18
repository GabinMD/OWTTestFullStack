using System.Reflection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BoatApplication.Domain;

public static class DependencyInjection
{
    public static void AddDomainServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}
