using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Application.DTO;
using Catalog.API.Application.Request;
using Catalog.API.Endpoints.Groups;
using FastEndpoints;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Endpoints.Brands
{
    public record GetBrandsResponse(IReadOnlyList<BrandDto> Brands);
    public class GetBrandsEndPoint(ISender sender) : Endpoint<GetBrandsRequest, GetBrandsResponse>
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            Get("/"); // Need filter parameters will be add in future
            Group<BrandGroupEndPoints>();
            Options(opt =>
            {
                opt.WithName("GetBrands");
                opt.WithSummary("Get all Brands");
                opt.WithDescription("Get all Brands with filters.");
                opt.Produces<GetBrandsResponse>(StatusCodes.Status200OK);
                opt.ProducesProblem(StatusCodes.Status500InternalServerError);
            });
        }

        public override async Task HandleAsync(GetBrandsRequest request, CancellationToken ct)
        {
            var result = await _sender.Send(request, ct);
            var response = result.Results.Adapt<IReadOnlyList<BrandDto>>();
            await SendOkAsync(new GetBrandsResponse(response), ct);
        }
    }
}