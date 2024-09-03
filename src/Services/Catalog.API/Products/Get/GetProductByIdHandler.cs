using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using Catalog.API.Request.Product;
using Catalog.API.Response.Product;
using MongoDB.Entities;

namespace Catalog.API.Products.Get
{
    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdRequest, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdRequest query, CancellationToken cancellationToken)
        {
            var product = await DB.Find<Product>().OneAsync(query.Id, cancellationToken) ?? throw new ProductNotFoundException(query.Id);
            return new GetProductByIdResult(product);
        }
    }
}