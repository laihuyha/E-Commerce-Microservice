using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Application.Request;
using Catalog.API.Endpoints.Groups;
using FastEndpoints;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Endpoints.Attributes
{
    public class UpdateAttributeEndPoint(ISender sender) : Endpoint<UpdateAttributeRequest, Unit>
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            Put("/");
            Group<AttributeGroupEndPoints>();
            ThrowIfAnyErrors();
            Options(opt =>
            {
                opt.WithName("UpdateAttribute");
                opt.WithSummary("Update a Attribute.");
                opt.WithDescription("Update a Attribute by Id.");
                opt.Produces<Unit>(StatusCodes.Status200OK);
                opt.ProducesProblem(StatusCodes.Status400BadRequest);
                opt.ProducesProblem(StatusCodes.Status500InternalServerError);
            });
        }

        public override async Task HandleAsync(UpdateAttributeRequest req, CancellationToken ct)
        {
            await _sender.Send(req, ct);
            await SendOkAsync(ct);
        }
    }
}