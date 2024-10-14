using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.API.Application.Request;
using Catalog.API.Domain.Models;
using Catalog.API.Exceptions;
using MediatR;
using MongoDB.Entities;

namespace Catalog.API.Application.Brands
{
    public class DeleteBrandHandler : ICommandHandler<DeleteBrandRequest, Unit>
    {
        public async Task<Unit> Handle(DeleteBrandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await DB.Find<Brand>().OneAsync(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Brand), request.Id);
                await product.DeleteAsync();
                return Unit.Value;
            }
            catch (ProductNotFoundException)
            {
                throw;
            }
            catch
            {
                throw new ProductDeleteException();
            }
        }
    }
}