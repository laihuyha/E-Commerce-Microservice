using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Endpoints.Groups;
using Catalog.API.Request.Product;
using FastEndpoints;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Endpoints.Product
{
    public record GetProductByCategoryResponse(IEnumerable<Models.Product> Products);
    public class GetProductByCategoryEndPoint(ISender sender) : Endpoint<GetProductByCategoryRequest, GetProductByCategoryResponse>
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            Get("/by/cate/{@cate}", x => new { x.Category });
            Group<ProductGroupEndPoints>();
            Options(opt =>
            {
                opt.WithName("GetProductByCate");
                opt.WithSummary("Get products by Category");
                opt.WithDescription("Get products by Category.");
                opt.Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK);
                opt.ProducesProblem(StatusCodes.Status404NotFound);
            });
        }

        public override async Task HandleAsync(GetProductByCategoryRequest req, CancellationToken ct)
        {
            var result = await _sender.Send(new GetProductByCategoryRequest(req.Category), ct);
            if (!result.Products.Any()) await SendNotFoundAsync(ct);
            GetProductByCategoryResponse response = result.Adapt<GetProductByCategoryResponse>();
            await SendOkAsync(response, ct);
        }
    }
}