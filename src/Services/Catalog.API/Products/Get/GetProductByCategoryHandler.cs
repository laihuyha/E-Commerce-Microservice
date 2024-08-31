using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Catalog.API.Request.Product;
using Catalog.API.Response.Product;
using MongoDB.Entities;

namespace Catalog.API.Products.Get
{
    public class GetProductByCategoryQueryHandler : IQueryHandler<GetProductByCategoryRequest, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryRequest query, CancellationToken cancellationToken)
        {
            var products = string.IsNullOrEmpty(query.Category)
                ? await DB.Find<Product>().ExecuteAsync(cancellationToken)
                : await DB.Find<Product>().ManyAsync(x => x.Category.Contains(query.Category), cancellationToken);
            return new GetProductByCategoryResult(products);
        }
    }
}