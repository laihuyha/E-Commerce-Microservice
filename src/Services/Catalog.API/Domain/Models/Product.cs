using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Entities;

namespace Catalog.API.Domain.Models
{
    public class Product : Entity, ICreatedOn, IModifiedOn
    {
        public Product()
        {
            this.InitOneToMany(() => Attributes);
            this.InitManyToMany(() => Categories, categories => categories.Products);
        }

        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public decimal Price { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public string BrandId { get; set; }
        [OwnerSide]
        public Many<Category, Product> Categories { get; set; }
        public Many<ProductAttribute, Product> Attributes { get; set; }

        public async Task AddCategory(Category category)
        {
            await Categories.AddAsync(category);
        }

        public async Task AddCategories(IEnumerable<Category> categories)
        {
            foreach (var category in categories)
            {
                await AddCategory(category);
            }
        }

        public async Task AddAttribute(ProductAttribute attribute)
        {
            await Attributes.AddAsync(attribute);
        }

        public async Task AddAttributes(IEnumerable<ProductAttribute> attributes)
        {
            foreach (var attribute in attributes)
            {
                await AddAttribute(attribute);
            }
        }

        public Brand GetBrand()
        {
            var brand = DB.Queryable<Brand>().Where(e => e.ID == BrandId).Select(e => e).FirstOrDefault();
            return brand;
        }
    }
}