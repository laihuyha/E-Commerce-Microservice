using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Catalog.API.Request.Product;
using Catalog.API.Response.Product;
using MongoDB.Driver;
using MongoDB.Entities;

namespace Catalog.API.Products.Get
{
    public class GetProductsQueryHandler : IQueryHandler<GetProductsRequest, GetProductsResult>
    {
        private const int DefaultPageSize = 10;
        private const int DefaultPageNumber = 1;
        private const int MaxPageSize = 30;

        public async Task<GetProductsResult> Handle(GetProductsRequest query, CancellationToken cancellationToken)
        {
            int pageNumber = query.PageNumber ?? DefaultPageNumber;
            int pageSize = query.PageSize.HasValue
                ? (query.PageSize.Value > MaxPageSize ? MaxPageSize : query.PageSize.Value)
                : DefaultPageSize;

            var (products, totalCount, pageCount) = await DB.PagedSearch<Product>()
                .Sort(prod => prod.Ascending("Name").Descending("CreatedOn")) // or Sort(prod => prod.Name, Order.Ascending) for simple usage.
                .PageSize(pageSize)
                .PageNumber(pageNumber)
                .ExecuteAsync(cancellationToken);

            return new GetProductsResult(products);
        }
    }
}
