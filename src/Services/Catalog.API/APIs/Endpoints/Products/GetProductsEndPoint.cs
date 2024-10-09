using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Application.DTO;
using Catalog.API.Endpoints.Groups;
using Catalog.API.Request;
using FastEndpoints;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Endpoints.Products
{
    public record GetProductsResponse(IReadOnlyList<ProductDto> Products);
    public class GetProductsEndPoint(ISender sender) : Endpoint<GetProductsRequest, GetProductsResponse>
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
                opt.Produces<GetProductsResponse>(StatusCodes.Status200OK);
                opt.ProducesProblem(StatusCodes.Status500InternalServerError);
            });
        }

        public override async Task HandleAsync(GetProductsRequest request, CancellationToken ct)
        {
            var result = await _sender.Send(request, ct);
            var response = result.Results.Adapt<IReadOnlyList<ProductDto>>();
            await SendOkAsync(new GetProductsResponse(response), ct);
        }
    }
}