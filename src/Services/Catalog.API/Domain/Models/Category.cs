using System;
using MongoDB.Entities;

namespace Catalog.API.Domain.Models;

public class Category : Entity, ICreatedOn, IModifiedOn
{
    public Category()
    {
        this.InitManyToMany(() => Products, products => products.Categories);
    }

    public string Name { get; set; } = default!;
    public string ParentCateId { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }

    [InverseSide]
    public Many<Product, Category> Products { get; set; }
}
