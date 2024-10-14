using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.API.Application.Request;
using Catalog.API.Application.Response;
using Catalog.API.Domain.Models;
using MongoDB.Entities;

namespace Catalog.API.Application.ProductAttributes
{
    public class GetAttributeByIdQueryHandler : IQueryHandler<GetAttributeByIdRequest, GetByIdResponse<ProductAttribute>>
    {
        public async Task<GetByIdResponse<ProductAttribute>> Handle(GetAttributeByIdRequest query, CancellationToken cancellationToken)
        {
            var attribute = await DB.Find<ProductAttribute>().OneAsync(query.Id, cancellationToken) ?? throw new NotFoundException(nameof(ProductAttribute), query.Id);
            return new GetByIdResponse<ProductAttribute>(attribute);
        }
    }
}