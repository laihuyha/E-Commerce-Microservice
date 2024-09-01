using System.Collections.Generic;
using BuildingBlocks.CQRS;
using Catalog.API.Response.Product;
using MediatR;

namespace Catalog.API.Request.Product
{
    public record CreateProductRequest(string Name, string Description, List<string> Category, string ImageFile, decimal Price) : ICommand<CreateProductResponse>;
    public record UpdateProductRequest(string Id, string Name, string Description, List<string> Category, string ImageFile, decimal Price) : ICommand<Unit>;
    public record GetProductsRequest() : IQuery<GetProductsResult>;
    public record GetProductByIdRequest(string Id) : IQuery<GetProductByIdResult>;
    public record GetProductByCategoryRequest(string Category) : IQuery<GetProductByCategoryResult>;
}