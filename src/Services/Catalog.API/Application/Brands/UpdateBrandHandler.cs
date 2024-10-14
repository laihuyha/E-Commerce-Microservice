using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.API.Application.Request;
using Catalog.API.Domain.Models;
using Mapster;
using MediatR;
using MongoDB.Entities;

namespace Catalog.API.Application.Brands
{
    public class UpdateBrandHandler : ICommandHandler<UpdateBrandRequest, Unit>
    {
        public async Task<Unit> Handle(UpdateBrandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = request.Adapt<Brand>();

                var result = await DB.UpdateAndGet<Brand>().MatchID(request.Id)
                .ModifyWith(entity)
                .ExecuteAsync(cancellationToken);

                if (result is null) throw new NotFoundException(nameof(Brand),request.Id);

                return Unit.Value;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DbErrorException($"Error while updating Brand with Id: {request.Id}", ex);
            }
        }
    }
}