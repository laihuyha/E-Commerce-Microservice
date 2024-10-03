using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Catalog.API.Request.Product;
using Catalog.API.Response.Product;
using MongoDB.Entities;

namespace Catalog.API.Products.Get
{
    internal class GetProductByCategoryQueryHandler : IQueryHandler<GetProductByCategoryRequest, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryRequest query, CancellationToken cancellationToken)
        {
            List<string> categoryIdsToSearch = [query.Category];
            var products = string.IsNullOrEmpty(query.Category)
                ? await DB.Find<Product>().ExecuteAsync(cancellationToken)
                : await DB.Find<Product>().ManyAsync(x => x.Categories.Select(category => category.ID).Intersect(categoryIdsToSearch).Any(), cancellationToken);
            return new GetProductByCategoryResult(products);
        }
    }
}