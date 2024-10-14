using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using Catalog.API.Application.Request;
using Catalog.API.Application.Response;
using Catalog.API.Domain.Models;
using MongoDB.Driver;
using MongoDB.Entities;

namespace Catalog.API.Application.ProductAttributes
{
    public class GetAllAttributeQueryHandler : IQueryHandler<GetAttributesRequest, GetAllResponse<ProductAttribute>>
    {
        public async Task<GetAllResponse<ProductAttribute>> Handle(GetAttributesRequest query, CancellationToken cancellationToken)
        {
            var attributes = await DB.PagedSearch<ProductAttribute>()
                .Sort(attr => attr.Ascending("Type").Ascending("Value")) // or Sort(prod => prod.Name, Order.Ascending) for simple usage.
                .PageSize(query.PageSize)
                .PageNumber(query.PageIndex)
                .ExecuteAsync(cancellationToken);

            return new GetAllResponse<ProductAttribute>(attributes.Results);
        }
    }
}
