using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Endpoints.Groups;

public class AttributeGroupEndPoints : SubGroup<BaseApiEndPoint>
{
    public AttributeGroupEndPoints()
    {
        Configure("attributes", endpoint =>
        {
            endpoint.Tags("Attributes");
            endpoint.Description(x =>
            {
                x.WithDescription("Attribute APIs includes GET: (All/Single Attribute(s) - POST: Create Attribute - PUT: Update Attribute - DELETE: Remove Attribute")
                .WithTags("Attributes");
            });
        });
    }
}
