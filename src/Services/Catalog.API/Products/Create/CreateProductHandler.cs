using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
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
                //Create Product Entity from command Obj
                var product = new Product
                {
                    Name = request.Name,
                    Category = request.Category,
                    Description = request.Description,
                    ImageFile = request.ImageFile,
                    Price = request.Price,
                };
                // Save to DB
                await DB.SaveAsync(product, cancellation: cancellationToken);
                return new CreateProductResponse(product.ID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}