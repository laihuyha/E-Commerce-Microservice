using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Endpoints.Groups
{
    public class BaseApiEndPoint : Group
    {
        public BaseApiEndPoint()
        {
            Configure("/api", ep =>
            {
                ep.AllowAnonymous();
                ep.Description(x => x
                .WithSummary("General APIs"));
            });
        }
    }
}