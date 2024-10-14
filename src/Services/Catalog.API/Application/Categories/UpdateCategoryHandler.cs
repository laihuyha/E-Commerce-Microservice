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

namespace Catalog.API.Application.Categories
{
    public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryRequest, Unit>
    {
        public async Task<Unit> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = request.Adapt<Category>();

                var result = await DB.UpdateAndGet<Category>().MatchID(request.Id)
                .ModifyWith(entity)
                .ExecuteAsync(cancellationToken);

                if (result is null) throw new NotFoundException(nameof(Category), request.Id);

                return Unit.Value;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DbErrorException($"Error while updating category with Id: {request.Id}", ex);
            }
        }
    }
}