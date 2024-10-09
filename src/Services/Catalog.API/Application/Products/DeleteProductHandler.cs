using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using Catalog.API.Domain.Models;
using Catalog.API.Exceptions;
using Catalog.API.Request;
using MediatR;
using MongoDB.Entities;

namespace Catalog.API.Application.Products
{
    public class DeleteProductHandler : ICommandHandler<DeleteProductRequest, Unit>
    {
        public async Task<Unit> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await DB.Find<Product>().OneAsync(request.Id, cancellationToken) ?? throw new ProductNotFoundException(request.Id);
                await product.DeleteAsync();

                return Unit.Value;
            }
            catch (ProductNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProductDeleteException("Error while Deleting product!", ex);
            }
        }
    }
}