using FastEndpoints;
using FastEndpoints.Swagger;
using Marten;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Weasel.Core;

namespace Catalog.API.Extensions
{
    public static class BuilderServiceExtension
    {
        public static IServiceCollection AddServiceExtension(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
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
            });

            // This is the absolute, simplest way to integrate Marten into your
            // .NET application with Marten's default configuration

            services.AddMarten(options =>
            {
                // Establish the connection string to your Marten database
                options.Connection(configuration.GetConnectionString("Database")!);

                // Specify that we want to use STJ as our serializer
                options.UseSystemTextJsonForSerialization();

                options.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;

                // If we're running in development mode, let Marten just take care
                // of all necessary schema building and patching behind the scenes
                if (env.IsDevelopment())
                {
                    options.AutoCreateSchemaObjects = AutoCreate.All;
                }
            }).OptimizeArtifactWorkflow().UseLightweightSessions();
            return services;
        }
    }
}