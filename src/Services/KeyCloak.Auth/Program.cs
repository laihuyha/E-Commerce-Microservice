using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using KeyCloak.Auth.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuth(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// Helper method for deserializing claims
object DeserializeClaim(Claim claim)
{
    return claim.ValueType != "JSON"
        ? claim.Value
        : JsonSerializer.Deserialize<object>(claim.Value);
}

app.MapGet("users/me", (ClaimsPrincipal claimsPrincipal) =>
{
    var claimsGrouped = claimsPrincipal.Claims.GroupBy(c => c.Type);
    var dict = new Dictionary<string, object>();

    foreach (var item in claimsGrouped)
    {
        dict[item.Key] = item.Count() switch
        {
            1 => DeserializeClaim(item.First()),
            _ => item.Select(c => c.Value).ToList()
        };
    }

    return Results.Json(dict); // Return the custom dictionary as JSON
}).RequireAuthorization();

app.UseAuthentication();

app.UseAuthorization();

await app.RunAsync();

