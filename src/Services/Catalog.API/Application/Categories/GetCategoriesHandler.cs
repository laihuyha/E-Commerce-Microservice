using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using Catalog.API.Application.Request;
using Catalog.API.Application.Response;
using Catalog.API.Domain.Models;
using MongoDB.Driver;
using MongoDB.Entities;

namespace Catalog.API.Application.Categories
{
    public class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesRequest, GetAllResponse<Category>>
    {
        public async Task<GetAllResponse<Category>> Handle(GetCategoriesRequest query, CancellationToken cancellationToken)
        {
            var categories = await DB.PagedSearch<Category>()
                .Sort(cate => cate.Ascending("Name").Descending("CreatedOn")) // or Sort(prod => prod.Name, Order.Ascending) for simple usage.
                .PageSize(query.PageSize)
                .PageNumber(query.PageIndex)
                .ExecuteAsync(cancellationToken);
                
            return new GetAllResponse<Category>(categories.Results);
        }
    }
}
