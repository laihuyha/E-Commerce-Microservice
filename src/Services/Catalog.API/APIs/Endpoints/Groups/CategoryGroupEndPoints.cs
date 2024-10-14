using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Endpoints.Groups;

public class CategoryGroupEndPoints : SubGroup<BaseApiEndPoint>
{
    public CategoryGroupEndPoints()
    {
        Configure("categories", endpoint =>
        {
            endpoint.Tags("Category");
            endpoint.Description(x =>
            {
                x.WithDescription("Category APIs includes GET: (All/Single Category(Categories) - POST: Create Category - PUT: Update Category - DELETE: Remove Category")
                .WithTags("Category");
            });
        });
    }
}
