using Basket.API.Application.Interfaces;
using Basket.API.Infrastructure.Repositories;
using Basket.API.Infrastructure.Schema.Build;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using BuildingBlocks.Utils;
using Carter;
using Marten;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Weasel.Core;

namespace Basket.API.Extensions;

public static class BuilderServiceExtension
{
    public static IServiceCollection AddBuilderServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment host)
    {
        _ = services.AddCarter();
        _ = services.AddMediatR(cfg =>
        {
            _ = cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
            _ = cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            _ = cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        _ = services.AddMarten(opt =>
        {
            // Establish the connection string to your Marten database
            opt.Connection(configuration.GetConnectionString("Marten")!);

            // Specify that we want to use STJ as our serializer
            opt.UseSystemTextJsonForSerialization();

            opt.Schema.BuildSchema();

            // If we're running in development mode, let Marten just take care
            // of all necessary schema building and patching behind the scenes
            if (host.IsDevelopment())
            {
                opt.AutoCreateSchemaObjects = AutoCreate.All;
            }
        }).UseLightweightSessions();

        //Repositories
        _ = services.AddScoped<IBasketRepository, BasketRepository>();

        // Exception Handler
        _ = services.AddExceptionHandler<CustomExceptionHandler>();
        return services;
    }
}
