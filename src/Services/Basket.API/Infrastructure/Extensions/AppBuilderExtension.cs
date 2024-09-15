using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Basket.API.Extensions;

public static class AppBuilderExtension
{
    public static WebApplication UseAppBuilderExtension(this WebApplication app)
    {
        app.MapCarter();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        return app;
    }
}
