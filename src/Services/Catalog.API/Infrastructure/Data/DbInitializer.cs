using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Entities;

namespace Catalog.API.Data;

public static class DbInitializer
{
    public static async Task Init(IServiceProvider serviceProvider, IHostEnvironment hostEnvironment)
    {
        if (hostEnvironment.IsDevelopment())
        {
            using var scope = serviceProvider.CreateScope();
            var iLogger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            var itemCount = await DB.CountAsync<Product>();
            try
            {
                if (itemCount == 0)
                {
                    iLogger.LogInformation("No product found! Begin initializing........");
                    var products = new List<Product>()
                {
                    new() {
                        Name = "IPhone X",
                        Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                        ImageFile = "product-1.png",
                        Price = 950.00M,
                        Category = ["Smart Phone"]
                    },
                    new() {
                        Name = "Samsung 10",
                        Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                        ImageFile = "product-2.png",
                        Price = 840.00M,
                        Category = ["Smart Phone"]
                    },
                    new() {
                        Name = "Huawei Plus",
                        Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                        ImageFile = "product-3.png",
                        Price = 650.00M,
                        Category = ["White Appliances"]
                    },
                    new() {
                        Name = "Xiaomi Mi 9",
                        Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                        ImageFile = "product-4.png",
                        Price = 470.00M,
                        Category = ["White Appliances"]
                    },
                    new() {
                        Name = "HTC U11+ Plus",
                        Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                        ImageFile = "product-5.png",
                        Price = 380.00M,
                        Category = ["Smart Phone"]
                    },
                    new() {
                        Name = "LG G7 ThinQ",
                        Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                        ImageFile = "product-6.png",
                        Price = 240.00M,
                        Category = ["Home Kitchen"]
                    },
                    new() {
                        Name = "Panasonic Lumix",
                        Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                        ImageFile = "product-6.png",
                        Price = 240.00M,
                        Category = ["Camera"]
                    }
                };
                    await DB.SaveAsync(products);
                    iLogger.LogInformation("Products initialized!");
                }
                await Task.CompletedTask;
            }
            catch
            {
                iLogger.LogError("Products initialize failed!");
                throw;
            }
        }
    }
}
