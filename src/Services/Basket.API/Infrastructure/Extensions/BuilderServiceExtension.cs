using BuildingBlocks.Utils;
using Carter;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.API.Extensions;

public static class BuilderServiceExtension
{
    public static IServiceCollection AddBuilderServices(this IServiceCollection services)
    {
        services.AddCarter();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        return services;
    }
}
