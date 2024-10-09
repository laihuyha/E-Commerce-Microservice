using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Endpoints.Groups;
using Catalog.API.Request;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Endpoints.Products
{
    public class DeleteProductEndPoint(ISender sender) : EndpointWithoutRequest
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            Delete("/{id}");
            Group<ProductGroupEndPoints>();
            ThrowIfAnyErrors();
            Options(opt =>
            {
                _ = opt.WithName("DeleteProduct");
                _ = opt.WithSummary("Delete an exist product.");
                _ = opt.WithDescription("Delete an exist product.");
                _ = opt.Produces<Unit>(StatusCodes.Status200OK);
                _ = opt.ProducesProblem(StatusCodes.Status500InternalServerError);
            });
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var id = Route<string>("id");
            if (string.IsNullOrEmpty(id))
            {
                AddError("Id is null");
                await SendErrorsAsync(cancellation: ct);
            }
            else
            {
                _ = await _sender.Send(new DeleteProductRequest(id), ct);
                await SendOkAsync(ct);
            }
        }
    }
}