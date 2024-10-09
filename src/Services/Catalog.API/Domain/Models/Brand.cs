using System;
using MongoDB.Entities;

namespace Catalog.API.Domain.Models;

public class Brand : Entity, ICreatedOn, IModifiedOn
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string LogoUrl { get; set; } = default!;
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
}
