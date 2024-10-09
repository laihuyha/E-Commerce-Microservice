using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using Catalog.API.Application.Response;
using Catalog.API.Domain.Models;
using Catalog.API.Exceptions;
using Catalog.API.Request;
using MongoDB.Entities;

namespace Catalog.API.Application.Products
{
    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdRequest, GetByIdResponse<Product>>
    {
        public async Task<GetByIdResponse<Product>> Handle(GetProductByIdRequest query, CancellationToken cancellationToken)
        {
            var product = await DB.Find<Product>().OneAsync(query.Id, cancellationToken) ?? throw new ProductNotFoundException(query.Id);
            return new GetByIdResponse<Product>(product);
        }
    }
}