using Basket.API.Domain.Models;

namespace Basket.API.Application.DTO.Results;

public record GetCartResult(Cart Cart);
public record StoreCartResult(string UserId);
public record DeleteCartResult(bool IsSuccess);