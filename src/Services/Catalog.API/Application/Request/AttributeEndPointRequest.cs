using BuildingBlocks.CQRS;
using Catalog.API.Application.Response;
using Catalog.API.Domain.Models;
using MediatR;

namespace Catalog.API.Application.Request;

public record CreateAttributeRequest(int AttributeType, string Value) : ICommand<CreateResponse>;
public record UpdateAttributeRequest(string Id, int AttributeType, string Value) : ICommand<Unit>;
public record DeleteAttributeRequest(string Id) : ICommand<Unit>;
public record GetAttributesRequest : QueryBase, IQuery<GetAllResponse<ProductAttribute>>;
public record GetAttributeByIdRequest(string Id) : IQuery<GetByIdResponse<ProductAttribute>>;