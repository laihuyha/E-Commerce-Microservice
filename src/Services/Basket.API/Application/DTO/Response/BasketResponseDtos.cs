using Basket.API.Domain.Models;

namespace Basket.API.Application.DTO.Response;

public record GetCartResponse(Cart Cart);
public record StoreCartResponse(string UserId);
public record DeleteCartResponse(bool IsSuccess);