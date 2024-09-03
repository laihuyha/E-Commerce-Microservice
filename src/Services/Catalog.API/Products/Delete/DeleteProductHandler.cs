using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using Catalog.API.Request.Product;
using MediatR;
using MongoDB.Entities;

namespace Catalog.API.Products.Delete
{
    public class DeleteProductHandler : ICommandHandler<DeleteProductRequest, Unit>
    {
        public async Task<Unit> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await DB.Find<Product>().OneAsync(request.Id, cancellationToken) ?? throw new ProductNotFoundException(request.Id);
                await product.DeleteAsync(cancellation: cancellationToken);
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