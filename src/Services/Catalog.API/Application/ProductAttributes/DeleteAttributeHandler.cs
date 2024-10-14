using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using BuildingBlocks.Enums;
using BuildingBlocks.Exceptions;
using Catalog.API.Application.Request;
using Catalog.API.Domain.Models;
using MediatR;
using MongoDB.Entities;

namespace Catalog.API.Application.ProductAttributes
{
    public class DeleteAttributeCommandHandler : ICommandHandler<DeleteAttributeRequest, Unit>
    {
        public async Task<Unit> Handle(DeleteAttributeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await DB.Find<ProductAttribute>().OneAsync(request.Id, cancellationToken)
                    ?? throw new NotFoundException(nameof(ProductAttribute), request.Id);
                await product.DeleteAsync();
                return Unit.Value;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch
            {
                throw new DbErrorException(Operations.Deleting, nameof(ProductAttribute).ToLower());
            }
        }
    }
}