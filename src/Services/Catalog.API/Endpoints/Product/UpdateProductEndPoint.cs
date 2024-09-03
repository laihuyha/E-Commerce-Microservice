using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Endpoints.Groups;
using Catalog.API.Request.Product;
using FastEndpoints;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Endpoints.Product
{
    public class UpdateProductEndPoint(ISender sender) : Endpoint<UpdateProductRequest, Unit>
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            Put("/");
            Group<ProductGroupEndPoints>();
            ThrowIfAnyErrors();
            Options(opt =>
            {
                opt.WithName("UpdateProduct");
                opt.WithSummary("Update a product.");
                opt.WithDescription("Update a product by Id.");
                opt.Produces<Unit>(StatusCodes.Status200OK);
                opt.ProducesProblem(StatusCodes.Status400BadRequest);
                opt.ProducesProblem(StatusCodes.Status500InternalServerError);
            });
        }

        public override async Task HandleAsync(UpdateProductRequest req, CancellationToken ct)
        {
            await _sender.Send(req, ct);
            await SendOkAsync(ct);
        }
    }
}