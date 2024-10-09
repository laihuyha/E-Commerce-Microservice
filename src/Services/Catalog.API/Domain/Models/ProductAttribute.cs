using System;
using Catalog.API.Domain.Enums;
using MongoDB.Entities;

namespace Catalog.API.Domain.Models;

public class ProductAttribute : Entity, ICreatedOn, IModifiedOn
{
    public AttributeType Type { get; set; }
    public string Value { get; set; } = default!;
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public Many<Product, ProductAttribute> Products { get; set; }
}
