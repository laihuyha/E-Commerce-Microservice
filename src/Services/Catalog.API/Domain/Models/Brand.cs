using Catalog.API.Models;
using MongoDB.Entities;

namespace Catalog.API.Domain.Models;

public class Brand : Entity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string LogoUrl { get; set; } = default!;

    public Many<Product, Brand> Products { get; set; }

    public Brand()
    {
        this.InitOneToMany(() => Products);
    }
}
