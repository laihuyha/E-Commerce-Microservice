using BuildingBlocks.CQRS;
using Catalog.API.Application.Response;
using Catalog.API.Domain.Models;
using MediatR;

namespace Catalog.API.Application.Request;

public record CreateCategoryRequest(string Name, string ParentCateId, string Description) : ICommand<CreateResponse>;
public record UpdateCategoryRequest(string Id, string Name, string ParentCateId, string Description) : ICommand<Unit>;
public record DeleteCategoryRequest(string Id) : ICommand<Unit>;
public record GetCategoriesRequest : QueryBase, IQuery<GetAllResponse<Category>>;
public record GetCategoryByIdRequest(string Id) : IQuery<GetByIdResponse<Category>>;