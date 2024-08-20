using Catalog.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServiceExtension(); //Extension Method

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.AddAppBuilderExtension(); //Extension Method

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