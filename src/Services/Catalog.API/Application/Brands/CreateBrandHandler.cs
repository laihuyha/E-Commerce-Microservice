using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.API.Application.Request;
using Catalog.API.Application.Response;
using Catalog.API.Domain.Models;
using MongoDB.Entities;

namespace Catalog.API.Application.Brands
{
    internal class CreateBrandCommandHandler : ICommandHandler<CreateBrandRequest, CreateResponse>
    {
        public async Task<CreateResponse> Handle(CreateBrandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var brand = new Brand
                {
                    Name = request.Name,
                    Description = request.Description,
                    LogoUrl = request.LogoUrl
                };

                // Save to DB
                await DB.SaveAsync(brand, cancellation: cancellationToken);
                return new CreateResponse(brand.ID);
            }
            catch (Exception ex)
            {
                throw new DbErrorException("Error while creating brand!", ex);
            }
        }
    }
}