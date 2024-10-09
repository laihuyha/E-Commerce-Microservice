using System.Reflection;
using Catalog.API.Data;
using Catalog.API.Extensions;
using Mapster;
using Mapster.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServiceExtension(builder.Configuration); //Extension Method

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Mapping Configuration With "IMapFrom" Interface
TypeAdapterConfig.GlobalSettings.ScanInheritedTypes(Assembly.GetExecutingAssembly());

_ = app.AddAppBuilderExtension(); //Extension Method

// Config the lifetime of the application
app.Lifetime.ApplicationStarted.Register(async () =>
{
    await AppBuilderExtension.InitializeConnection(builder.Configuration);
    await DB.MigrationsAsync([
        new _001_DbInitializer(app.Services, app.Environment),
    ]);

});

await app.RunAsync();