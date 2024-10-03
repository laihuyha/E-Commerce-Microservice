using BuildingBlocks.CQRS;
using Catalog.API.Response.Product;
using MediatR;

namespace Catalog.API.Application.Request;

public record CreateCategoryRequest(string Name, string ParentCateId, string Description) : ICommand<CreateProductResponse>;
public record UpdateCategoryRequest(string Id, string Name, string ParentCateId, string Description) : ICommand<Unit>;
public record DeleteCategoryRequest(string Id) : ICommand<Unit>;
public record GetCategoryRequest(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;
public record GetCategoryByIdRequest(string Id) : IQuery<GetProductByIdResult>;