using System.Collections.Generic;

namespace Catalog.API.Response.Product
{
    public record CreateProductResponse(string Id);
    public record GetProductsResult(IEnumerable<Models.Product> Products);
    public record GetProductByIdResult(Models.Product Product);
}