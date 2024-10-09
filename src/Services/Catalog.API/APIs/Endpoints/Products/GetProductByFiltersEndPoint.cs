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
    public record GetProductByFilterResponse(IReadOnlyList<ProductDto> Products);
    public class GetProductByCategoryEndPoint(ISender sender) : Endpoint<GetProductByFiltersRequest, GetProductByFilterResponse>
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            Get("/by/filters");
            Group<ProductGroupEndPoints>();
            Options(opt =>
            {
                opt.WithName("GetProductByFilters");
                opt.WithSummary("Get products by filters");
                opt.WithDescription("Get products by filters.");
                opt.Produces<GetProductByFilterResponse>(StatusCodes.Status200OK);
                opt.ProducesProblem(StatusCodes.Status404NotFound);
            });
        }

        public override async Task HandleAsync(GetProductByFiltersRequest req, CancellationToken ct)
        {
            var result = await _sender.Send(req, ct);
            var response = result.Results.Adapt<IReadOnlyList<ProductDto>>();
            await SendOkAsync(new GetProductByFilterResponse(response), ct);
        }
    }
}