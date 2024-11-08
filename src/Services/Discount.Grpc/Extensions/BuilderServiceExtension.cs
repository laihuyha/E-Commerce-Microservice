using System.Reflection;
using Discount.Grpc.Data;
using Discount.Grpc.Services.MappingProfile;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Grpc.Extensions;

public static class BuilderServiceExtension
{
    public static IServiceCollection AddAppService(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
        TypeAdapterConfig.GlobalSettings.Scan(typeof(MapsterRegister).Assembly);
        services.AddGrpc();
        services.AddDbContext<DataContext>(options => options.UseSqlite(configuration.GetConnectionString("Database")));
        return services;
    }
}
