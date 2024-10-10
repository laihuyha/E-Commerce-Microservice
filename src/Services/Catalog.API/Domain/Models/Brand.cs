using System;
using MongoDB.Entities;

namespace Catalog.API.Domain.Models;

public class Brand : Entity, ICreatedOn, IModifiedOn
{
    public Brand()
    {
        this.InitOneToMany(() => Products);
    }

    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string LogoUrl { get; set; } = default!;
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public Many<Product, Brand> Products { get; set; }
}
