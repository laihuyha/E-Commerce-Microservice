using System;
using System.Collections.Generic;
using MongoDB.Entities;

namespace Catalog.API.Products
{
    public class Product : Entity, ICreatedOn, IModifiedOn
    {
        public string Name { get; set; }
        public List<string> Category { get; set; } = [];
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public decimal Price { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}