using BuildingBlocks.CQRS;
using Catalog.API.Response.Product;
using MediatR;

namespace Catalog.API.Application.Request;

public record CreateBrandRequest(string Name, string Description, string LogoUrl) : ICommand<CreateProductResponse>;
public record UpdateBrandRequest(string Id, string Name, string Description, string LogoUrl) : ICommand<Unit>;
public record DeleteBrandRequest(string Id) : ICommand<Unit>;
public record GetBrandRequest(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;
public record GetBrandByIdRequest(string Id) : IQuery<GetProductByIdResult>;