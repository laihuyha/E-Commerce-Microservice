using System.Collections.Generic;
using BuildingBlocks.CQRS;
using Catalog.API.Application.Request;
using Catalog.API.Application.Response;
using Catalog.API.Domain.Models;
using MediatR;

namespace Catalog.API.Request
{
    public record CreateProductRequest(string Name, string Description, string ImageFile, decimal Price, string BrandId, List<string> CategoryIds, List<string> AttributeIds) : ICommand<CreateResponse>;
    public record UpdateProductRequest(string Id, string Name, string Description, string ImageFile, decimal Price, string BrandId, List<string> CategoryIds, List<string> AttributeIds) : ICommand<Unit>;
    public record DeleteProductRequest(string Id) : ICommand<Unit>;
    public record GetProductsRequest : QueryBase, IQuery<GetAllResponse<Product>>;
    public record GetProductByIdRequest(string Id) : IQuery<GetByIdResponse<Product>>;
    public record GetProductByFiltersRequest : ProductQueryFilter, IQuery<GetByFiltersReponse<Product>>;
}