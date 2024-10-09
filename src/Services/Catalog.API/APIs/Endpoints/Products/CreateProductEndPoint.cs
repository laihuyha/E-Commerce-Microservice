using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Application.Response;
using Catalog.API.Endpoints.Groups;
using Catalog.API.Request;
using FastEndpoints;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Endpoints.Products
{
    public class CreateProductEndPoint(ISender sender) : Endpoint<CreateProductRequest, CreateResponse>
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            AllowAnonymous();
            Post("/");
            // Require authorization if needed
            Group<ProductGroupEndPoints>();
            ThrowIfAnyErrors();
            Options(opt =>
            {
                opt.WithName("CreateProduct");
                opt.WithSummary("Create a new product");
                opt.WithDescription("Creates a new product using the provided details.");
                opt.Produces<CreateResponse>(StatusCodes.Status201Created);
                opt.ProducesProblem(StatusCodes.Status400BadRequest);
            });
        }

        public override async Task HandleAsync(CreateProductRequest request, CancellationToken ct)
        {
            // Map the request to the command
            var command = request.Adapt<CreateProductRequest>();

            // Send the command using MediatR
            var result = await _sender.Send(command, ct);

            // Map the result to the response
            var response = result.Adapt<CreateResponse>();

            // Send the response with a 201 Created status
            await SendCreatedAtAsync("GetProductById", response.Id, response, cancellation: ct);
        }
    }
}
