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
    public record GetBrandByIdResponse(BrandDto Brand);
    public class GetBrandByIdEndPoint(ISender sender) : Endpoint<GetBrandByIdRequest, GetBrandByIdResponse>
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            Get("/with-id"); // Need filter parameters will be add in future
            Group<BrandGroupEndPoints>();
            Options(opt =>
            {
                opt.WithName("GetBrandById");
                opt.WithSummary("Get Brand by Id");
                opt.WithDescription("Get a Brands with Id.");
                opt.Produces<GetBrandByIdResponse>(StatusCodes.Status200OK);
                opt.ProducesProblem(StatusCodes.Status404NotFound);
                opt.ProducesProblem(StatusCodes.Status400BadRequest);
            });
        }

        public override async Task HandleAsync(GetBrandByIdRequest request, CancellationToken ct)
        {
            /// Route<T>() method is only able to handle types that have a static TryParse() method.
            /// See here on how to add parsing support for your own types. (https://fast-endpoints.com/docs/model-binding#supported-dto-property-types)
            /// More about models binding via this link: https://fast-endpoints.com/docs/model-binding#built-in-request-binding
            // string BrandId = Query<string>("id");
            // if (string.IsNullOrEmpty(BrandId)) throw new ArgumentException($"{nameof(BrandId)} is null");

            var result = await _sender.Send(request, ct);

            var response = result.Object.Adapt<BrandDto>();
            await SendOkAsync(new GetBrandByIdResponse(response), ct);
        }
    }
}