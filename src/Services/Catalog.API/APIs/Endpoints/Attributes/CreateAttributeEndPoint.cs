using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Application.Request;
using Catalog.API.Application.Response;
using Catalog.API.Endpoints.Groups;
using FastEndpoints;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Endpoints.Attributes
{
    public class CreateAttributeEndPoint(ISender sender) : Endpoint<CreateAttributeRequest, CreateResponse>
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            AllowAnonymous();
            Post("/");
            // Require authorization if needed
            Group<AttributeGroupEndPoints>();
            ThrowIfAnyErrors();
            Options(opt =>
            {
                opt.WithName("CreateAttribute");
                opt.WithSummary("Create a new attribute.");
                opt.WithDescription("Creates a new attribute using the provided details.");
                opt.Produces<CreateResponse>(StatusCodes.Status201Created);
                opt.ProducesProblem(StatusCodes.Status400BadRequest);
            });
        }

        public override async Task HandleAsync(CreateAttributeRequest request, CancellationToken ct)
        {
            // Send the command using MediatR
            var result = await _sender.Send(request, ct);

            // Map the result to the response
            var response = result.Adapt<CreateResponse>();

            // Send the response with a 201 Created status
            await SendCreatedAtAsync("GetAttributeById", response.Id, response, cancellation: ct);
        }
    }
}
