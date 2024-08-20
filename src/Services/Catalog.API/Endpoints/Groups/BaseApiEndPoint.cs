using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Endpoints.Groups
{
    public class BaseApiEndPoint : Group
    {
        public BaseApiEndPoint()
        {
            Configure("products", ep =>
            {
                ep.AllowAnonymous();
                ep.Description(x => x
                .WithSummary("Product APIs")
                .WithDescription("Product APIs includes GET: (All/Single Product(s) - POST: Create Product - PUT: Update Product - DELETE: Remove Product")
                .WithTags("Products"));
            });
        }
    }
}