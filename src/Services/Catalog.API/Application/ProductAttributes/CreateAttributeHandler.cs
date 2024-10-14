using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.API.Application.Request;
using Catalog.API.Application.Response;
using Catalog.API.Domain.Enums;
using Catalog.API.Domain.Models;
using MongoDB.Entities;

namespace Catalog.API.Application.ProductAttributes
{
    internal class CreateAttributeCommandHandler : ICommandHandler<CreateAttributeRequest, CreateResponse>
    {
        public async Task<CreateResponse> Handle(CreateAttributeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var attribute = new ProductAttribute
                {
                    Type = (AttributeType)request.AttributeType,
                    Value = request.Value
                };

                // Save to DB
                await DB.SaveAsync(attribute, cancellation: cancellationToken);
                return new CreateResponse(attribute.ID);
            }
            catch (Exception ex)
            {
                throw new DbErrorException("Error while creating attribute!", ex);
            }
        }
    }
}