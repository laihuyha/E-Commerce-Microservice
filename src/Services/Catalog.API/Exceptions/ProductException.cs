using System;
using System.Data.Common;

namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() : base("Product not found!") { }
    }

    public class ProductUpdateException : DbException
    {
        public ProductUpdateException() : base("Error while updating product!") { }
        public ProductUpdateException(string message, Exception innerException) : base(message, innerException) { }
    }

}