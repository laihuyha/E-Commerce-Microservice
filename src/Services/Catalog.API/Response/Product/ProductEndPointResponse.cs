using System;
using System.Collections.Generic;

namespace Catalog.API.Response.Product
{
    public record CreateProductResponse(Guid Id);
    public record GetProductsResult(IEnumerable<Models.Product> Products);
}