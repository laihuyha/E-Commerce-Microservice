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
    public record GetAttributeByIdResponse(AttributeDto Attribute);
    public class GetAttributeByIdEndPoint(ISender sender) : Endpoint<GetAttributeByIdRequest, GetAttributeByIdResponse>
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            Get("/with-id");
            Group<AttributeGroupEndPoints>();
            Options(opt =>
            {
                opt.WithName("GetAttributeById");
                opt.WithSummary("Get Attribute by Id");
                opt.WithDescription("Get an Attribute with Id.");
                opt.Produces<GetAttributeByIdResponse>(StatusCodes.Status200OK);
                opt.ProducesProblem(StatusCodes.Status404NotFound);
                opt.ProducesProblem(StatusCodes.Status400BadRequest);
            });
        }

        public override async Task HandleAsync(GetAttributeByIdRequest request, CancellationToken ct)
        {
            /// Route<T>() method is only able to handle types that have a static TryParse() method.
            /// See here on how to add parsing support for your own types. (https://fast-endpoints.com/docs/model-binding#supported-dto-property-types)
            /// More about models binding via this link: https://fast-endpoints.com/docs/model-binding#built-in-request-binding
            // string attrId = Query<string>("id");
            // if (string.IsNullOrEmpty(attrId)) throw new ArgumentException($"{nameof(attrId)} is null");

            var result = await _sender.Send(request, ct);

            var response = result.Object.Adapt<AttributeDto>();
            await SendOkAsync(new GetAttributeByIdResponse(response), ct);
        }
    }
}