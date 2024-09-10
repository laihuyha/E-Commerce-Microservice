using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using Catalog.API.Request.Product;
using Mapster;
using MediatR;
using MongoDB.Entities;

namespace Catalog.API.Products.Update
{
    public class UpdateProductHandler : ICommandHandler<UpdateProductRequest, Unit>
    {
        public async Task<Unit> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = request.Adapt<Product>();

                var result = await DB.UpdateAndGet<Product>().MatchID(request.Id)
                .ModifyWith(entity)
                .ExecuteAsync(cancellationToken);

                if (result is null) throw new ProductNotFoundException(request.Id);

                return Unit.Value;
            }
            catch (ProductNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProductUpdateException($"Error while updating product with Id: {request.Id}", ex.InnerException);
            }
        }
    }
}