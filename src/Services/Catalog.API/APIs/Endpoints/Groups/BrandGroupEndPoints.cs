using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Endpoints.Groups;

public class BrandGroupEndPoints : SubGroup<BaseApiEndPoint>
{
    public BrandGroupEndPoints()
    {
        Configure("brands", endpoint =>
        {
            endpoint.Tags("Brands");
            endpoint.Description(x =>
            {
                x.WithDescription("Brand APIs includes GET: (All/Single Brand(s) - POST: Create Brand - PUT: Update Brand - DELETE: Remove Brand")
                .WithTags("Brands");
            });
        });
    }
}
