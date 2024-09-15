using Basket.API.Application.DTO.Response;
using BuildingBlocks.CQRS;

namespace Basket.API.Application.DTO.Request;

public record GetCartRequest(string UserId) : IQuery<GetCartResponse>;
