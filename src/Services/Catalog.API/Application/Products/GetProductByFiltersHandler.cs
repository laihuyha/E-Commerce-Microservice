using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using Catalog.API.Application.Response;
using Catalog.API.Domain.Models;
using Catalog.API.Request;
using MongoDB.Driver;
using MongoDB.Entities;

namespace Catalog.API.Application.Products
{
    internal class GetProductByFiltersQueryHandler : IQueryHandler<GetProductByFiltersRequest, GetByFiltersReponse<Product>>
    {
        public async Task<GetByFiltersReponse<Product>> Handle(GetProductByFiltersRequest query, CancellationToken cancellationToken)
        {
            HashSet<Product> products = [];
            if (query.CategoryIds.Any())
            {
                foreach (var id in query.CategoryIds)
                {
                    var x = DB.Entity<Category>(id).Products.ChildrenQueryable().ToArray();
                    products.UnionWith(x);
                }
            }

            products = products.DistinctBy(e => e.ID).ToHashSet();

            if (query.AttributeIds.Any())
            {
                products = products.Where(e => e.Attributes.ChildrenQueryable().Any(attr => query.AttributeIds.Contains(attr.ID))).ToHashSet();
            }

            if (query.BrandId is not null)
            {
                var brandProducts = DB.Entity<Brand>(query.BrandId).Products.ToArray();
                var intersectProducts = brandProducts.Intersect(products);
                products = intersectProducts.ToHashSet();
            }

            if (query.SearchText is not null)
            {
                products = products.Where(e => e.Name.Equals(query.SearchText, StringComparison.OrdinalIgnoreCase)).ToHashSet();
            }

            await Task.CompletedTask;

            // Order products
            products = [.. products.OrderBy(e => e.Name).ThenByDescending(e => e.CreatedOn)];

            return new GetByFiltersReponse<Product>(products.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize));
        }
    }
}