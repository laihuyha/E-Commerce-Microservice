using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Application.Request;
using Catalog.API.Endpoints.Groups;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Endpoints.Categories
{
    public class DeleteCategoryEndPoint(ISender sender) : EndpointWithoutRequest
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            Delete("/{id}");
            Group<CategoryGroupEndPoints>();
            ThrowIfAnyErrors();
            Options(opt =>
            {
                _ = opt.WithName("DeleteCategory");
                _ = opt.WithSummary("Delete an exist Category.");
                _ = opt.WithDescription("Delete an exist Category.");
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
                _ = await _sender.Send(new DeleteCategoryRequest(id), ct);
                await SendOkAsync(ct);
            }
        }
    }
}