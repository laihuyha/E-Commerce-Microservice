using Microsoft.Extensions.DependencyInjection;

namespace Basket.API.Extensions;

public static class BuilderServiceExtension
{
    public static IServiceCollection AddBuilderServices(this IServiceCollection services)
    {
        return services;
    }
}
