using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Application.Request;
using Catalog.API.Endpoints.Groups;
using FastEndpoints;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Endpoints.Categories
{
    public class UpdateCategoryEndPoint(ISender sender) : Endpoint<UpdateCategoryRequest, Unit>
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            Put("/");
            Group<CategoryGroupEndPoints>();
            ThrowIfAnyErrors();
            Options(opt =>
            {
                opt.WithName("UpdateCategory");
                opt.WithSummary("Update a Category.");
                opt.WithDescription("Update a Category by Id.");
                opt.Produces<Unit>(StatusCodes.Status200OK);
                opt.ProducesProblem(StatusCodes.Status400BadRequest);
                opt.ProducesProblem(StatusCodes.Status500InternalServerError);
            });
        }

        public override async Task HandleAsync(UpdateCategoryRequest req, CancellationToken ct)
        {
            await _sender.Send(req, ct);
            await SendOkAsync(ct);
        }
    }
}