using Basket.API.Application.DTO.Response;
using Basket.API.Application.DTO.Results;
using BuildingBlocks.CQRS;

namespace Basket.API.Application.DTO.Request;

public record GetCartRequest(string UserId) : IQuery<GetCartResult>;
public record DeleteCartRequest(string UserId) : ICommand<DeleteCartResponse>;