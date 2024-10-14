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

namespace Catalog.API.Endpoints.Attributes
{
    public record GetAttributesResponse(IReadOnlyList<AttributeDto> Attributes);
    public class GetAttributesEndPoint(ISender sender) : Endpoint<GetAttributesRequest, GetAttributesResponse>
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            Get("/"); // Need filter parameters will be add in future
            Group<AttributeGroupEndPoints>();
            Options(opt =>
            {
                opt.WithName("GetAttributes");
                opt.WithSummary("Get all Attributes");
                opt.WithDescription("Get all Attributes with filters.");
                opt.Produces<GetAttributesResponse>(StatusCodes.Status200OK);
                opt.ProducesProblem(StatusCodes.Status500InternalServerError);
            });
        }

        public override async Task HandleAsync(GetAttributesRequest request, CancellationToken ct)
        {
            var result = await _sender.Send(request, ct);
            var response = result.Results.Adapt<IReadOnlyList<AttributeDto>>();
            await SendOkAsync(new GetAttributesResponse(response), ct);
        }
    }
}