using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Application.Request;
using Catalog.API.Endpoints.Groups;
using FastEndpoints;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Endpoints.Brands
{
    public class UpdateBrandEndPoint(ISender sender) : Endpoint<UpdateBrandRequest, Unit>
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            Put("/");
            Group<BrandGroupEndPoints>();
            ThrowIfAnyErrors();
            Options(opt =>
            {
                opt.WithName("UpdateBrand");
                opt.WithSummary("Update a Brand.");
                opt.WithDescription("Update a Brand by Id.");
                opt.Produces<Unit>(StatusCodes.Status200OK);
                opt.ProducesProblem(StatusCodes.Status400BadRequest);
                opt.ProducesProblem(StatusCodes.Status500InternalServerError);
            });
        }

        public override async Task HandleAsync(UpdateBrandRequest req, CancellationToken ct)
        {
            await _sender.Send(req, ct);
            await SendOkAsync(ct);
        }
    }
}