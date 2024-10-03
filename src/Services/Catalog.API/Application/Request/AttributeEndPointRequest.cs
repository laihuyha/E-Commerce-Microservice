using BuildingBlocks.CQRS;
using Catalog.API.Response.Product;
using MediatR;

namespace Catalog.API.Application.Request;

public record CreateAttributeRequest(int AttributeType, string Value) : ICommand<CreateProductResponse>;
public record UpdateAttributeRequest(string Id, string AttributeType, string Value) : ICommand<Unit>;
public record DeleteAttributeRequest(string Id) : ICommand<Unit>;
public record GetAttributeRequest(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;
public record GetAttributeByIdRequest(string Id) : IQuery<GetProductByIdResult>;