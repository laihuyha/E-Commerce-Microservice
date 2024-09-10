using BuildingBlocks.Utils;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Extensions
{
    public static class BuilderServiceExtension
    {
        public static IServiceCollection AddServiceExtension(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<Program>), typeof(Logger<Program>));
            services.AddFastEndpoints().SwaggerDocument(o =>
            {
                o.DocumentSettings = s =>
                {
                    s.Title = "Catalog.API";
                    s.Version = "v1";
                };
                o.AutoTagPathSegmentIndex = 0;
            });
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            return services;
        }
    }
}