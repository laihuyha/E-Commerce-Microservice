using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Endpoints.Groups;
using Catalog.API.Request.Product;
using Catalog.API.Response.Product;
using FastEndpoints;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Endpoints.Product
{
    public record GetProductsResponse(IEnumerable<Models.Product> Products);
    public class GetProductEndPoint(ISender sender) : EndpointWithoutRequest<GetProductsResponse>
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            Get("/"); // Need filter parameters will be add in future
            Group<ProductGroupEndPoints>();
            Options(opt =>
            {
                opt.WithName("GetProducts");
                opt.WithSummary("Get all products");
                opt.WithDescription("Get all products with filters.");
                opt.Produces<GetProductsResult>(StatusCodes.Status200OK);
                opt.ProducesProblem(StatusCodes.Status500InternalServerError);
            });
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetProductsRequest(), cancellationToken);
            var response = result.Adapt<GetProductsResponse>();
            await SendOkAsync(response, cancellationToken);
        }
    }
}