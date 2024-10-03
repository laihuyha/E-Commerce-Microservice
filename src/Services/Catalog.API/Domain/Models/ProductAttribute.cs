using Catalog.API.Domain.Enums;
using Catalog.API.Models;
using MongoDB.Entities;

namespace Catalog.API.Domain.Models;

public class ProductAttribute : Entity
{
    public AttributeType Type { get; set; }
    public string Value { get; set; } = default!;
    public Many<Product, ProductAttribute> Products { get; set; }
}
