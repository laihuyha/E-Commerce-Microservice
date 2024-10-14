using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Application.Request;
using Catalog.API.Endpoints.Groups;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Endpoints.Brands
{
    public class DeleteBrandEndPoint(ISender sender) : EndpointWithoutRequest
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            Delete("/{id}");
            Group<BrandGroupEndPoints>();
            ThrowIfAnyErrors();
            Options(opt =>
            {
                _ = opt.WithName("DeleteBrand");
                _ = opt.WithSummary("Delete an exist Brand.");
                _ = opt.WithDescription("Delete an exist Brand.");
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
                _ = await _sender.Send(new DeleteBrandRequest(id), ct);
                await SendOkAsync(ct);
            }
        }
    }
}