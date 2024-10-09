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
    public class GetProductsQueryHandler : IQueryHandler<GetProductsRequest, GetAllResponse<Product>>
    {
        public async Task<GetAllResponse<Product>> Handle(GetProductsRequest query, CancellationToken cancellationToken)
        {
            var products = await DB.PagedSearch<Product>()
                .Sort(prod => prod.Ascending("Name").Descending("CreatedOn")) // or Sort(prod => prod.Name, Order.Ascending) for simple usage.
                .PageSize(query.PageSize)
                .PageNumber(query.PageIndex)
                .ExecuteAsync(cancellationToken);


            return new GetAllResponse<Product>(products.Results);
        }
    }
}
