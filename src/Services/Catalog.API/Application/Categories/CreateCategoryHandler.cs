using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.API.Application.Request;
using Catalog.API.Application.Response;
using Catalog.API.Domain.Models;
using MongoDB.Entities;

namespace Catalog.API.Application.Categories
{
    internal class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryRequest, CreateResponse>
    {
        public async Task<CreateResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var category = new Category
                {
                    Name = request.Name,
                    Description = request.Description,
                    ParentCateId = request.ParentCateId
                };

                // Save to DB
                await DB.SaveAsync(category, cancellation: cancellationToken);
                return new CreateResponse(category.ID);
            }
            catch (Exception ex)
            {
                throw new DbErrorException("Error while creating category!", ex);
            }
        }
    }
}