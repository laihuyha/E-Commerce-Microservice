using Catalog.API.Models;
using MongoDB.Entities;

namespace Catalog.API.Domain.Models;

public class Category : Entity
{
    public Category()
    {
        this.InitManyToMany(() => Products, products => products.Categories);
    }

    public string Name { get; set; } = default!;
    public string ParentCateId { get; set; } = default!;
    public string Description { get; set; } = default!;

    [InverseSide]
    public Many<Product, Category> Products { get; set; }
}
