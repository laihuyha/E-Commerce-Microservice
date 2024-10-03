using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using Catalog.API.Request.Product;
using Catalog.API.Response.Product;
using MongoDB.Entities;

namespace Catalog.API.Products.Create
{
    internal class CreateProductCommandHandler : ICommandHandler<CreateProductRequest, CreateProductResponse>
    {
        public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var product = new Product
                {
                    Name = request.Name,
                    Description = request.Description,
                    ImageFile = request.ImageFile,
                    Price = request.Price,
                };

                await product.AddCategories(request.Categories);

                // Save to DB
                await DB.SaveAsync(product, cancellation: cancellationToken);
                return new CreateProductResponse(product.ID);
            }
            catch (Exception ex)
            {
                throw new ProductCreateException("Error while creating product!", ex);
            }
        }
    }
}