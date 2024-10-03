using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Domain.Enums;
using Catalog.API.Domain.Models;
using Catalog.API.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Entities;

namespace Catalog.API.Data;

public class _001_DbInitializer(IServiceProvider serviceProvider, IHostEnvironment hostEnvironment) : IMigration
{
    private static readonly List<Brand> brands = [
        new(){
            Name = "Apple",
            LogoUrl = "apple.png",
            Description = "Apple Inc. is an American multinational technology company headquartered in Cupertino, California, that designs, manufactures, and sells consumer electronics, personal computers, and smartphones."
        },
        new(){
            Name = "Samsung",
            LogoUrl = "samsung.png",
            Description = "Samsung Electronics Co., Ltd. is a South Korean multinational semiconductor company headquartered in Samsung Town, Seoul, South Korea."
        },
        new(){
            Name = "Google",
            LogoUrl = "google.png",
            Description = "Google LLC is an American multinational technology company headquartered in Mountain View, California, specializing in online services, cloud computing, and artificial intelligence."
        },
        new(){
            Name = "Xiaomi",
            LogoUrl = "xiaomi.png",
            Description = "Xiaomi Inc. is a Chinese multinational technology company based in Beijing."
        },
        new(){
            Name = "Huawei",
            LogoUrl = "huawei.png",
            Description = "Huawei Technologies Co., Ltd. is a Chinese multinational technology company headquartered in Shanghai."
        },
        new(){
            Name = "OPPO",
            LogoUrl = "oppo.png",
            Description = "OPPO is a Chinese multinational technology company based in Shanghai."
        },
        new(){
            Name = "Honor",
            LogoUrl = "honor.png",
            Description = "Honor is a Chinese multinational technology company based in Shanghai."
        },
        new(){
            Name = "Panasonic",
            LogoUrl = "panasonic.png",
            Description = "Panasonic Corporation is a Japanese multinational consumer electronics and smartphone maker."
        },
        new(){
            Name = "Vivo",
            LogoUrl = "vivo.png",
            Description = "Vivo Corporation is a Chinese multinational technology company based in Beijing."
        },
        new(){
            Name = "Nokia",
            LogoUrl = "nokia.png",
            Description = "Nokia Corporation is a Swedish multinational technology company based in Stockholm."
        },
        new(){
            Name = "OnePlus",
            LogoUrl = "oneplus.png",
            Description = "OnePlus Corporation is a Chinese multinational technology company based in Shanghai."
        },
        new(){
            Name = "Asus",
            LogoUrl = "asus.png",
            Description = "ASUS Corporation is a South Korean multinational technology company headquartered in Seoul, South Korea."
        },
        new(){
            Name = "Lenovo",
            LogoUrl = "lenovo.png",
            Description = "Lenovo Corporation is a Chinese multinational technology company based in Beijing."
        },
        new(){
            Name = "Realme",
            LogoUrl = "realme.png",
            Description = "Realme is a Chinese multinational technology company based in Shenzhen."
        },
        new(){
            Name = "Sony",
            LogoUrl = "sony.png",
            Description = "Sony Corporation is a Japanese multinational technology company based in Tokyo."
        },
        new(){
            Name = "Xiaomi Mi",
            LogoUrl = "xiaomi-mi.png",
            Description = "Xiaomi Mi is a Chinese multinational technology company based in Shanghai."
        },
        new(){
            Name = "Infinix",
            LogoUrl = "infinix.png",
            Description = "Infinix Corporation is a Chinese multinational technology company based in Shenzhen."
        },
        new(){
            Name = "LG",
            LogoUrl = "lg.png",
            Description = "LG Corporation is a South Korean multinational technology company headquartered in Seoul, South Korea."
        },
        new(){
            Name = "DELL",
            LogoUrl = "dell.png",
            Description = "Dell Inc. is an American multinational technology company headquartered in Round Rock, Texas, United States."
        },
        new(){
            Name = "Acer",
            LogoUrl = "acer.png",
            Description = "Acer is a South Korean multinational technology company based in Seoul, South Korea."
        },
        new(){
            Name = "HP",
            LogoUrl = "hp.png",
            Description = "HP Inc. is an American multinational technology company headquartered in Santa Monica, California, United States."
        },
    ];
    private static readonly List<ProductAttribute> attributes = [
        new ProductAttribute{ Type = AttributeType.Color , Value= "Red"},
        new ProductAttribute{ Type = AttributeType.Color , Value= "Green"},
        new ProductAttribute{ Type = AttributeType.Color , Value= "Blue"},
        new ProductAttribute{ Type = AttributeType.FrameRate , Value= "60FPS"},
        new ProductAttribute{ Type = AttributeType.FrameRate , Value= "90FPS"},
        new ProductAttribute{ Type = AttributeType.FrameRate , Value= "120FPS"},
        new ProductAttribute{ Type = AttributeType.FrameRate , Value= "144FPS"},
        new ProductAttribute{ Type = AttributeType.RAM , Value= "4GB"},
        new ProductAttribute{ Type = AttributeType.RAM , Value= "6GB"},
        new ProductAttribute{ Type = AttributeType.RAM , Value= "8GB"},
        new ProductAttribute{ Type = AttributeType.RAM , Value= "16GB"},
        new ProductAttribute{ Type = AttributeType.RAM , Value= "32GB"},
        new ProductAttribute{ Type = AttributeType.RAM , Value= "64GB"},
        new ProductAttribute{ Type = AttributeType.HardDrive , Value= "HDD"},
        new ProductAttribute{ Type = AttributeType.HardDrive , Value= "SSD"},
        new ProductAttribute{ Type = AttributeType.Storage , Value= "64GB"},
        new ProductAttribute{ Type = AttributeType.Storage , Value= "128GB"},
        new ProductAttribute{ Type = AttributeType.Storage , Value= "256GB"},
        new ProductAttribute{ Type = AttributeType.Storage , Value= "1TB"},
        new ProductAttribute{ Type = AttributeType.Storage , Value= "2TB"},
        new ProductAttribute{ Type = AttributeType.Resolution , Value= "1280x720"},
        new ProductAttribute{ Type = AttributeType.Resolution , Value= "1920x1080"},
        new ProductAttribute{ Type = AttributeType.Resolution , Value= "2560x1440"},
        new ProductAttribute{ Type = AttributeType.Resolution , Value= "3840x2160"},
        new ProductAttribute{ Type = AttributeType.Resolution , Value= "7680x4320"},
    ];

    private static readonly List<Category> categories = [
        new Category{Name="Smart Phone", ParentCateId="", Description="Smart Phone Description"},
        new Category{Name = "Camera", ParentCateId="", Description="Camera Description"},
        new Category{Name = "Accessories", ParentCateId="", Description="Accessories Description"},
    ];
    public async Task UpgradeAsync()
    {
        if (hostEnvironment.IsDevelopment())
        {
            using var scope = serviceProvider.CreateScope();
            var iLogger = scope.ServiceProvider.GetRequiredService<ILogger<_001_DbInitializer>>();
            var itemCount = await DB.CountAsync<Product>();
            try
            {
                if (itemCount == 0)
                {
                    iLogger.LogInformation("No product found! Begin initializing........");
                    await SeedAttributes();
                    await SeedBrand();
                    await SeedCategories();
                    await SeedProducts();
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

    private static async Task SeedBrand() => await DB.SaveAsync(brands);
    private static async Task SeedAttributes() => await DB.SaveAsync(attributes);
    private static async Task SeedCategories() => await DB.SaveAsync(categories);

    private static async Task SeedProducts()
    {
        const string description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.";

        List<ProductOmittedForeignKey> list =
        [
            new ProductOmittedForeignKey ("IPhone X", description, "product-1.png",  950.00M ),
            new ProductOmittedForeignKey ("Samsung Galaxy S21 Ultra", description, "product-2.png",  1200.00M ),
            new ProductOmittedForeignKey ("OnePlus 9 Pro", description, "product-3.png",  850.00M ),
            new ProductOmittedForeignKey ("Google Pixel 5", description, "product-4.png",  950.00M ),
            new ProductOmittedForeignKey ("Xiaomi Mi 11", description, "product-5.png",  800.00M ),
            new ProductOmittedForeignKey ("Xiaomi Redmi Note 8 Pro", description, "product-6.png",  450.00M ),
            new ProductOmittedForeignKey ("Huawei P40 Pro", description, "product-7.png",  850.00M ),
            new ProductOmittedForeignKey ("Vivo V20 Pro", description, "product-8.png",  700.00M ),
            new ProductOmittedForeignKey ("OPPO Reno 3 Pro", description, "product-9.png",  700.00M ),
            new ProductOmittedForeignKey ("Honor 20 Pro", description, "product-10.png",  750.00M ),
            new ProductOmittedForeignKey ("Panasonic Lumix", description, "product-11.png",  240.00M ),
        ];

        Random random = new();

        for (int i = 0; i < list.Count; i++)
        {
            // Randomly select attributes
            var selectedAttributes = attributes.OrderBy(x => random.Next()).Take(random.Next(1, 4));
            var selectedCategories = categories.OrderBy(x => random.Next()).Take(random.Next(1, 3));
            var product = new Product
            {
                // Cập nhật thuộc tính cho sản phẩm từ list[i]
                Name = list[i].Name,
                Description = list[i].Description,
                ImageFile = list[i].ImageFile,
                Price = list[i].Price
            };

            // Lưu product lần đầu
            await product.SaveAsync();

            // Thêm danh mục cho sản phẩm
            if (i < list.Count - 1)
            {
                await product.AddCategories(selectedCategories);
                await product.AddAttributes(selectedAttributes);
                await product.SaveAsync();
            }
            else
            {
                var cameraCate = categories.Find(e => e.Name.StartsWith("Cam"));
                await product.AddCategories([cameraCate]);

                var panasonicBrand = brands.Find(e => e.Name.StartsWith("Pana"));
                product.Brand = panasonicBrand.ToReference();
                product.Brand = new(panasonicBrand.ID);
                await product.SaveAsync(); // Save để lưu BrandID

                // Thêm sản phẩm vào Brand
                await panasonicBrand.Products.AddAsync(product);
                await panasonicBrand.SaveAsync();
            }
        }
    }


    public record ProductOmittedForeignKey(string Name, string Description, string ImageFile, decimal Price);
}
