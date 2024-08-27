using Catalog.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using MongoDB.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServiceExtension(builder.Configuration, builder.Environment); //Extension Method

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Config the lifetime of the application
_ = app.AddAppBuilderExtension(); //Extension Method
await DB.InitAsync("CatalogDb", MongoClientSettings.FromConnectionString(builder.Configuration.GetConnectionString("MongoDb")));

#region need to generate API
// await app.GenerateApiClientsAndExitAsync(
// c =>
// {
//     c.SwaggerDocumentName = "v1"; //must match doc name above
//     c.Language = GenerationLanguage.CSharp;
//     c.OutputPath = Path.Combine(app.Environment.WebRootPath, "ApiClients", "CSharp");
//     c.ClientNamespaceName = "MyCompanyName";
//     c.ClientClassName = "MyCsClient";
//     c.CreateZipArchive = true; //if you'd like a zip file as well
// });
#endregion

app.Run();