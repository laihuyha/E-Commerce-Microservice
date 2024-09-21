using System;
using Basket.API.Domain.Models;
using BuildingBlocks.Enums;
using BuildingBlocks.Exceptions;

namespace Basket.API.Application.Exceptions;

public class BasketNotFoundException : NotFoundException
{
    public BasketNotFoundException() : base("Cart not found!") { }
    public BasketNotFoundException(object key) : base(nameof(Cart), key) { }
}

public class BasketDeleteException : DbErrorException
{
    public BasketDeleteException() : base(Operations.Deleting, nameof(Cart).ToLower()) { }
    public BasketDeleteException(string message, Exception innerException) : base(message, innerException) { }
}

public class BasketStoreException : DbErrorException
{
    public BasketStoreException(Operations operations) : base(operations, nameof(Cart).ToLower()) { }
    public BasketStoreException(Operations operations, string details) : base(operations, nameof(Cart).ToLower(), details) { }
    public BasketStoreException(string message, Exception innerException) : base(message, innerException) { }
}