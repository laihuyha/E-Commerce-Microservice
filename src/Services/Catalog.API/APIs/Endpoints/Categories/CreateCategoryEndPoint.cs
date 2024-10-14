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

namespace Catalog.API.Endpoints.Categories
{
    public class CreateCategoryEndPoint(ISender sender) : Endpoint<CreateCategoryRequest, CreateResponse>
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            AllowAnonymous();
            Post("/");
            // Require authorization if needed
            Group<CategoryGroupEndPoints>();
            ThrowIfAnyErrors();
            Options(opt =>
            {
                opt.WithName("CreateCategory");
                opt.WithSummary("Create a new Category");
                opt.WithDescription("Creates a new Category using the provided details.");
                opt.Produces<CreateResponse>(StatusCodes.Status201Created);
                opt.ProducesProblem(StatusCodes.Status400BadRequest);
            });
        }

        public override async Task HandleAsync(CreateCategoryRequest request, CancellationToken ct)
        {
            // Send the command using MediatR
            var result = await _sender.Send(request, ct);

            // Map the result to the response
            var response = result.Adapt<CreateResponse>();

            // Send the response with a 201 Created status
            await SendCreatedAtAsync("GetCategoryById", response.Id, response, cancellation: ct);
        }
    }
}
