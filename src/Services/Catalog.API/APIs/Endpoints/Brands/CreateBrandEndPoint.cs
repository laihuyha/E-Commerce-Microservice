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

namespace Catalog.API.Endpoints.Brands
{
    public class CreateBrandEndPoint(ISender sender) : Endpoint<CreateBrandRequest, CreateResponse>
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            AllowAnonymous();
            Post("/");
            // Require authorization if needed
            Group<BrandGroupEndPoints>();
            ThrowIfAnyErrors();
            Options(opt =>
            {
                opt.WithName("CreateBrand");
                opt.WithSummary("Create a new Brand");
                opt.WithDescription("Creates a new Brand using the provided details.");
                opt.Produces<CreateResponse>(StatusCodes.Status201Created);
                opt.ProducesProblem(StatusCodes.Status400BadRequest);
            });
        }

        public override async Task HandleAsync(CreateBrandRequest request, CancellationToken ct)
        {
            // Send the command using MediatR
            var result = await _sender.Send(request, ct);

            // Map the result to the response
            var response = result.Adapt<CreateResponse>();

            // Send the response with a 201 Created status
            await SendCreatedAtAsync("GetBrandById", response.Id, response, cancellation: ct);
        }
    }
}
