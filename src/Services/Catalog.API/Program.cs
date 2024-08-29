using Catalog.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

_ = app.AddAppBuilderExtension(builder.Configuration); //Extension Method

// Config the lifetime of the application
app.Lifetime.ApplicationStarted.Register(async () =>
{
    await AppBuilderExtension.InitializeConnection(builder.Configuration);
});

app.Run();