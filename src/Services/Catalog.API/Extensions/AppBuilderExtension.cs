using System.Text.Json;
using System.Threading.Tasks;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Entities;

namespace Catalog.API.Extensions
{
    public static class AppBuilderExtension
    {
        public static IApplicationBuilder AddAppBuilderExtension(this IApplicationBuilder appBuilder, IConfiguration configuration)
        {
            // appBuilder.UseHttpsRedirection();
            appBuilder.UseFastEndpoints(cfg =>
            {
                cfg.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                // cfg.Endpoints.RoutePrefix = "api";
            }).UseSwaggerGen();
            return appBuilder;
        }
      
        public static async Task InitializeConnection(IConfiguration config)
        {
            var host = config["MongoDb:Host"];
            var credentials = config.GetSection("MongoDb:Credentials");
            var userName = credentials.GetValue<string>("UserName");
            var password = credentials.GetValue<string>("Password");
            var connectionString = $"mongodb://{userName}:{password}@{host}:27017/?authMechanism=SCRAM-SHA-256";
            var mongoClientSettings = MongoClientSettings.FromConnectionString(connectionString);

            await DB.InitAsync("CatalogDb", mongoClientSettings);
        }
    }
}