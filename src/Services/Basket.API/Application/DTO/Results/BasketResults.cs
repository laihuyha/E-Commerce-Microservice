using Basket.API.Domain.Models;

/// <summary>
/// Represents the result of a get cart operation.
/// </summary>
/// <param name="Cart">The retrieved cart.</param>
public record GetCartResult(Cart Cart);
