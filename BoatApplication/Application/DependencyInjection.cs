using System.Reflection;
using Microsoft.Extensions.Hosting;
using BoatApplication.Application.Common.Behaviours;
using BoatApplication.Application.Boats.Behaviours;
using BoatApplication.Application.Boats.Services;
using BoatApplication.Domain.Common.Interfaces.Services;
using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(BoatAuthorizationBehaviour<,>));
        });

        builder.Services.AddTransient<IBoatService, BoatService>();
    }
}
