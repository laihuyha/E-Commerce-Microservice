using System;
using BuildingBlocks.Enums;
using BuildingBlocks.Exceptions;
using Catalog.API.Models;

namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException() : base("Product not found!") { }
        public ProductNotFoundException(object key) : base(nameof(Product), key) { }
    }

    public class ProductCreateException : DbErrorException
    {
        public ProductCreateException() : base(Operations.Creating, nameof(Product).ToLower()) { }
        public ProductCreateException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class ProductUpdateException : DbErrorException
    {
        public ProductUpdateException() : base(Operations.Updating, nameof(Product).ToLower()) { }
        public ProductUpdateException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class ProductDeleteException : DbErrorException
    {
        public ProductDeleteException() : base(Operations.Deleting, nameof(Product).ToLower()) { }
        public ProductDeleteException(string message, Exception innerException) : base(message, innerException) { }
    }
}