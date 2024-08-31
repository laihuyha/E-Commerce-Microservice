using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Catalog.API.Request.Product;
using Catalog.API.Response.Product;
using MongoDB.Entities;

namespace Catalog.API.Products.Get
{
    public class GetProductsQueryHandler : IQueryHandler<GetProductsRequest, GetProductsResult>
    {

        public async Task<GetProductsResult> Handle(GetProductsRequest query, CancellationToken cancellationToken)
        {
            var products = await DB.Find<Product>().ExecuteAsync(cancellationToken);
            return new GetProductsResult(products);
        }
    }
}