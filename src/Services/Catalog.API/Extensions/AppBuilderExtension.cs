using System.Text.Json;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;

namespace Catalog.API.Extensions
{
    public static class AppBuilderExtension
    {
        public static IApplicationBuilder AddAppBuilderExtension(this IApplicationBuilder appBuilder)
        {
            // appBuilder.UseHttpsRedirection();
            appBuilder.UseFastEndpoints(cfg =>
            {
                cfg.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                // cfg.Endpoints.RoutePrefix = "api";
            }).UseSwaggerGen();
            return appBuilder;
        }
    }
}