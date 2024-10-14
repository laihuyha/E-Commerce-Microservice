using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.API.Application.Request;
using Catalog.API.Application.Response;
using Catalog.API.Domain.Models;
using MongoDB.Entities;

namespace Catalog.API.Application.Brands
{
    public class GetBrandByIdQueryHandler : IQueryHandler<GetBrandByIdRequest, GetByIdResponse<Brand>>
    {
        public async Task<GetByIdResponse<Brand>> Handle(GetBrandByIdRequest query, CancellationToken cancellationToken)
        {
            var brand = await DB.Find<Brand>().OneAsync(query.Id, cancellationToken) ?? throw new NotFoundException(nameof(Brand), query.Id);
            return new GetByIdResponse<Brand>(brand);
        }
    }
}