using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using Catalog.API.Request.Product;
using Catalog.API.Response.Product;
using Marten;

namespace Catalog.API.Products.Create
{
    internal class CreateProductCommandHandler(IDocumentSession documentSession) : ICommandHandler<CreateProductRequest, CreateProductResponse>
    {
        private readonly IDocumentSession _documentSession = documentSession;

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
                _documentSession.Store(product);
                await _documentSession.SaveChangesAsync(cancellationToken);
                return new CreateProductResponse(product.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}