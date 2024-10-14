using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Application.DTO;
using Catalog.API.Application.Request;
using Catalog.API.Endpoints.Groups;
using FastEndpoints;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Endpoints.Categories
{
    public record GetCategorysResponse(IReadOnlyList<CategoryDto> Categorys);
    public class GetCategorysEndPoint(ISender sender) : Endpoint<GetCategoriesRequest, GetCategorysResponse>
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            Get("/"); // Need filter parameters will be add in future
            Group<CategoryGroupEndPoints>();
            Options(opt =>
            {
                opt.WithName("GetCategorys");
                opt.WithSummary("Get all Categorys");
                opt.WithDescription("Get all Categorys with filters.");
                opt.Produces<GetCategorysResponse>(StatusCodes.Status200OK);
                opt.ProducesProblem(StatusCodes.Status500InternalServerError);
            });
        }

        public override async Task HandleAsync(GetCategoriesRequest request, CancellationToken ct)
        {
            var result = await _sender.Send(request, ct);
            var response = result.Results.Adapt<IReadOnlyList<CategoryDto>>();
            await SendOkAsync(new GetCategorysResponse(response), ct);
        }
    }
}