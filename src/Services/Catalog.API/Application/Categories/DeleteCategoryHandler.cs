using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using BuildingBlocks.Enums;
using BuildingBlocks.Exceptions;
using Catalog.API.Application.Request;
using Catalog.API.Domain.Models;
using MediatR;
using MongoDB.Entities;

namespace Catalog.API.Application.Categories
{
    public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryRequest, Unit>
    {
        public async Task<Unit> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var cate = await DB.Find<Category>().OneAsync(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Category), request.Id);
                await cate.DeleteAsync();
                return Unit.Value;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch
            {
                throw new DbErrorException(Operations.Deleting, nameof(Category).ToLower());
            }
        }
    }
}