using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.API.Application.Request;
using Catalog.API.Application.Response;
using Catalog.API.Domain.Models;
using MongoDB.Entities;

namespace Catalog.API.Application.Categories
{
    public class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdRequest, GetByIdResponse<Category>>
    {
        public async Task<GetByIdResponse<Category>> Handle(GetCategoryByIdRequest query, CancellationToken cancellationToken)
        {
            var category = await DB.Find<Category>().OneAsync(query.Id, cancellationToken) ?? throw new NotFoundException(nameof(Category), query.Id);
            return new GetByIdResponse<Category>(category);
        }
    }
}