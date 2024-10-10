using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.API.Domain.Models;
using Catalog.API.Exceptions;
using Catalog.API.Request;
using Mapster;
using MediatR;
using MongoDB.Entities;

namespace Catalog.API.Application.Products
{
    public class UpdateProductHandler : ICommandHandler<UpdateProductRequest, Unit>
    {
        public async Task<Unit> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var oldBrandId = DB.Queryable<Product>().Where(e => e.ID == request.Id).Select(a => a.BrandId).FirstOrDefault();

                var entity = request.Adapt<Product>();

                var result = await DB.UpdateAndGet<Product>().MatchID(request.Id)
                .ModifyWith(entity)
                .ExecuteAsync(cancellationToken) ?? throw new ProductNotFoundException(request.Id);

                // update references relationship
                // check valid of retrieved data
                if (request.BrandId is not null)
                {
                    //Remove existing relationship
                    await DB.Entity<Brand>(oldBrandId).Products.RemoveAsync(result, cancellation: cancellationToken);

                    var brand = await DB.Find<Brand>().OneAsync(request.BrandId, cancellationToken) ?? throw new NotFoundException("Brand not found.");
                    await brand.Products.AddAsync(result, cancellation: cancellationToken);
                }

                if (request.AttributeIds is not null && request.AttributeIds.Count != 0)
                {
                    var currentAttributes = result.Attributes.ChildrenQueryable().ToArray();
                    await result.Attributes.RemoveAsync(currentAttributes, cancellation: cancellationToken);
                    await result.SaveAsync(cancellation: cancellationToken);

                    var attributes = await DB.Find<ProductAttribute>().ManyAsync(x => request.AttributeIds.Contains(x.ID), cancellationToken);
                    await result.AddAttributes(attributes);
                }

                if (request.CategoryIds is not null && request.CategoryIds.Count != 0)
                {
                    var currentCategories = result.Categories.ChildrenQueryable().ToArray();
                    await result.Categories.RemoveAsync(currentCategories, cancellation: cancellationToken);
                    await result.SaveAsync(cancellation: cancellationToken);

                    var categories = await DB.Find<Category>().ManyAsync(x => request.CategoryIds.Contains(x.ID), cancellationToken);
                    await result.AddCategories(categories);
                }

                await result.SaveAsync(cancellation: cancellationToken);

                return Unit.Value;
            }
            catch (ProductNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProductUpdateException($"Error while updating product with Id: {request.Id}", ex.InnerException);
            }
        }
    }
}