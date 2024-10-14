using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.API.Application.Request;
using Catalog.API.Domain.Enums;
using Catalog.API.Domain.Models;
using Mapster;
using MediatR;
using MongoDB.Entities;

namespace Catalog.API.Application.ProductAttributes
{
    public class UpdateAttrbuteCommandHanler : ICommandHandler<UpdateAttributeRequest, Unit>
    {
        public async Task<Unit> Handle(UpdateAttributeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = request.Adapt<ProductAttribute>();
                entity.Type = (AttributeType)request.AttributeType;

                var result = await DB.Update<ProductAttribute>().MatchID(request.Id)
                .ModifyWith(entity)
                .ExecuteAsync(cancellationToken);

                if (result.ModifiedCount == 0) throw new NotFoundException(nameof(ProductAttribute), request.Id);

                return Unit.Value;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DbErrorException($"Error while updating attribute with Id: {request.Id}", ex);
            }
        }
    }
}