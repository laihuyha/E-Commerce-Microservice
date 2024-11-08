using System;
using System.Linq;
using System.Threading.Tasks;
using Discount.Grpc.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Grpc.Data.Seed;

public static class DbInitializer
{
    public static async Task SeedData(IApplicationBuilder application)
    {
        using var scope       = application.ApplicationServices.CreateScope();
        var       dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

        await dataContext.Database.MigrateAsync();
        if (dataContext.SaleEvents.Any() && dataContext.Coupons.Any())
            Console.WriteLine("Database is already initialized ");
        if (!dataContext.Coupons.Any())
        {
            Models.Coupon[] coupons =
            [
                new()
                {
                    Id          = 1,
                    ProductName = "Product A",
                    Description = "Coupon for Product A",
                    Amount      = 10,
                    Type        = DiscountType.Percentage,
                    ExpiryDate  = DateTime.Now.AddDays(10)
                },
                new()
                {
                    Id          = 2,
                    ProductName = "Product B",
                    Description = "Coupon for Product B",
                    Amount      = 5,
                    Type        = DiscountType.Percentage,
                    ExpiryDate  = DateTime.Now.AddDays(5)
                },
                new()
                {
                    Id          = 3,
                    ProductName = "Product C",
                    Description = "Coupon for Product C",
                    Amount      = 5000,
                    Type        = DiscountType.FixAmount,
                    ExpiryDate  = DateTime.Now.AddDays(20)
                },
                new()
                {
                    Id          = 4,
                    ProductName = "Product D",
                    Description = "Coupon for Product D",
                    Amount      = 10000,
                    Type        = DiscountType.FixAmount,
                    ExpiryDate  = DateTime.Now.AddDays(10)
                },
                new()
                {
                    Id          = 5,
                    ProductName = "Product E",
                    Description = "Coupon for Product E",
                    Amount      = 56789,
                    Type        = DiscountType.FixAmount,
                    ExpiryDate  = DateTime.Now.AddDays(2)
                }
            ];
            dataContext.Coupons.AddRange(coupons);
        }

        if (!dataContext.SaleEvents.Any())
        {
            Models.SaleEvent[] saleEvents =
            [
                new()
                {
                    Id          = 1,
                    Description = "Lorem Ipsum",
                    SalePercent = 10,
                    StartDate   = DateTime.Now.AddDays(10),
                    EndDate     = DateTime.Now.AddDays(20)
                },
                new()
                {
                    Id          = 2,
                    Description = "Lorem Ipsum",
                    SalePercent = 5,
                    StartDate   = DateTime.Now.AddDays(21),
                    EndDate     = DateTime.Now.AddDays(41)
                },
                new()
                {
                    Id          = 3,
                    Description = "Lorem Ipsum",
                    SalePercent = 50,
                    StartDate   = DateTime.Now.AddDays(30),
                    EndDate     = DateTime.Now.AddDays(50)
                }
            ];
            dataContext.SaleEvents.AddRange(saleEvents);
        }

        await dataContext.SaveChangesAsync();
    }
}
