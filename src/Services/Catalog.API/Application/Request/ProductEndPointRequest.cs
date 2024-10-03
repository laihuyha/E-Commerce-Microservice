using System.Collections.Generic;
using BuildingBlocks.CQRS;
using Catalog.API.Domain.Models;
using Catalog.API.Response.Product;
using MediatR;

namespace Catalog.API.Request.Product
{
    public record CreateProductRequest(string Name, string Description, List<Category> Categories, string ImageFile, decimal Price) : ICommand<CreateProductResponse>;
    public record UpdateProductRequest(string Id, string Name, string Description, List<string> Category, string ImageFile, decimal Price) : ICommand<Unit>;
    public record DeleteProductRequest(string Id) : ICommand<Unit>;
    public record GetProductsRequest(List<string> CateIds, List<string> AttrIds, string BrandId, int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;
    public record GetProductByIdRequest(string Id) : IQuery<GetProductByIdResult>;
    public record GetProductByCategoryRequest(string Category) : IQuery<GetProductByCategoryResult>;
}