using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using Catalog.API.Products.CQRS.Commands;
using Catalog.API.Products.CQRS.Results;

namespace Catalog.API.Products.Create
{
    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //Create Product Entity from command Obj
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Category = request.Category,
                Description = request.Description,
                ImageFile = request.ImageFile,
                Price = request.Price,
            };
            // Save to DB
            return new CreateProductResult(product.Id);
        }
    }
}