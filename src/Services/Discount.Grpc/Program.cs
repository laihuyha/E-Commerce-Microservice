using Discount.Grpc.Data.Seed;
using Discount.Grpc.Services;
using Discount.Grpc.Services.Extensions;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppService(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();
app.MapGrpcService<SaleEventService>();

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Lifetime.ApplicationStarted.Register(async () => { await DbInitializer.SeedData(app); });

app.Run();
return;
