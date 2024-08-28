using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Endpoints.Groups;
using Catalog.API.Request.Product;
using Catalog.API.Response.Product;
using FastEndpoints;
using FluentValidation.Results;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Endpoints.Product
{
    public class CreateProductEndPoint(ISender sender) : Endpoint<CreateProductRequest, CreateProductResponse>
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            Post("/");
            // Require authorization if needed
            Group<ProductGroupEndPoints>();

            Options(opt =>
            {
                opt.WithName("CreateProduct");
                opt.WithSummary("Create a new product");
                opt.WithDescription("Creates a new product using the provided details.");
                opt.Produces<CreateProductResponse>(StatusCodes.Status201Created);
                opt.ProducesProblem(StatusCodes.Status400BadRequest);
            });
        }

        public override async Task HandleAsync(CreateProductRequest request, CancellationToken ct)
        {
            if (ValidationFailed)
            {
                foreach (ValidationFailure failure in ValidationFailures)
                {
                    var propertyName = failure.PropertyName;
                    var errorMessage = failure.ErrorMessage;
                }
                ThrowIfAnyErrors();
            }

            try
            {
                // Map the request to the command
                var command = request.Adapt<CreateProductRequest>();

                // Send the command using MediatR
                var result = await _sender.Send(command, ct);

                // Map the result to the response
                var response = result.Adapt<CreateProductResponse>();

                // Send the response with a 201 Created status
                await SendCreatedAtAsync("GetProductById", response.Id, response, cancellation: ct);
            }
            catch
            {
                // Log the exception and return a 500 Internal Server Error
                // _logger.LogError(ex, "Error creating product");
                // await SendAsync(new ProblemDetails(ValidationFailures, statusCode: StatusCodes.Status500InternalServerError));
            }
        }
    }
}
