using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Endpoints.Groups
{
    public class ProductGroupEndPoints : SubGroup<BaseApiEndPoint>
    {
        public ProductGroupEndPoints()
        {
            Configure("products", endpoint =>
            {
                endpoint.Tags("Products");
                endpoint.Description(x =>
                {
                    x.WithDescription("Product APIs includes GET: (All/Single Product(s) - POST: Create Product - PUT: Update Product - DELETE: Remove Product")
                    .WithTags("Products");
                });
            });
        }
    }
}