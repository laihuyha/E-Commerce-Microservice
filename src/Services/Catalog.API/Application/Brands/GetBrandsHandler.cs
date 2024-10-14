using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using Catalog.API.Application.Request;
using Catalog.API.Application.Response;
using Catalog.API.Domain.Models;
using MongoDB.Driver;
using MongoDB.Entities;

namespace Catalog.API.Application.Brands
{
    public class GetBrandsQueryHandler : IQueryHandler<GetBrandsRequest, GetAllResponse<Brand>>
    {
        public async Task<GetAllResponse<Brand>> Handle(GetBrandsRequest query, CancellationToken cancellationToken)
        {

            var brands = await DB.PagedSearch<Brand>()
                .Sort(prod => prod.Ascending("Name").Descending("CreatedOn")) // or Sort(prod => prod.Name, Order.Ascending) for simple usage.
                .PageSize(query.PageSize)
                .PageNumber(query.PageIndex)
                .ExecuteAsync(cancellationToken);

            return new GetAllResponse<Brand>(brands.Results);
        }
    }
}
