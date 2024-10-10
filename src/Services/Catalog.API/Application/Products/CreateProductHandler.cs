using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.API.Application.Response;
using Catalog.API.Domain.Models;
using Catalog.API.Exceptions;
using Catalog.API.Request;
using MongoDB.Entities;

namespace Catalog.API.Application.Products
{
    internal class CreateProductCommandHandler : ICommandHandler<CreateProductRequest, CreateResponse>
    {
        public async Task<CreateResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var product = new Product
                {
                    Name = request.Name,
                    Description = request.Description,
                    ImageFile = request.ImageFile,
                    Price = request.Price,
                    BrandId = request.BrandId
                };

                var categories = await DB.Find<Category>().ManyAsync(x => request.CategoryIds.Contains(x.ID), cancellationToken);
                var attributes = request.AttributeIds is not null && request.AttributeIds.Count != 0
                    ? await DB.Find<ProductAttribute>().ManyAsync(x => request.AttributeIds.Contains(x.ID), cancellationToken)
                    : [];

                await product.SaveAsync(cancellation: cancellationToken);

                // Add references to Brand
                var brand = await DB.Find<Brand>().OneAsync(request.BrandId, cancellationToken);
                if (brand is not null)
                {
                    await brand.Products.AddAsync(product, cancellation: cancellationToken);
                }
                else
                {
                    throw new NotFoundException("This brand is not available.");
                }

                // Cuz a product need to belong to at least one category so we check the validation of category
                if (categories.Count == 0) throw new NotFoundException("Invalid Category.");
                await product.AddCategories(categories);

                await product.AddAttributes(attributes);

                // Save to DB again
                await product.SaveAsync(cancellation: cancellationToken);
                return new CreateResponse(product.ID);
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)

            {
                throw new ProductCreateException("Error while creating product!", ex);
            }
        }
    }
}