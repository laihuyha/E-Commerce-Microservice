using BuildingBlocks.CQRS;
using Catalog.API.Application.Response;
using Catalog.API.Domain.Models;
using MediatR;

namespace Catalog.API.Application.Request;

public record CreateBrandRequest(string Name, string Description, string LogoUrl) : ICommand<CreateResponse>;
public record UpdateBrandRequest(string Id, string Name, string Description, string LogoUrl) : ICommand<Unit>;
public record DeleteBrandRequest(string Id) : ICommand<Unit>;
public record GetBrandsRequest : QueryBase, IQuery<GetAllResponse<Brand>>;
public record GetBrandByIdRequest(string Id) : IQuery<GetByIdResponse<Brand>>;