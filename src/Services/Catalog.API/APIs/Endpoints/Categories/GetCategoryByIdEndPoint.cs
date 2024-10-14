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
    public record GetCategoryByIdResponse(CategoryDto Category);
    public class GetCategoryByIdEndPoint(ISender sender) : Endpoint<GetCategoryByIdRequest, GetCategoryByIdResponse>
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            Get("/with-id"); // Need filter parameters will be add in future
            Group<CategoryGroupEndPoints>();
            Options(opt =>
            {
                opt.WithName("GetCategoryById");
                opt.WithSummary("Get Category by Id");
                opt.WithDescription("Get a Categorys with Id.");
                opt.Produces<GetCategoryByIdResponse>(StatusCodes.Status200OK);
                opt.ProducesProblem(StatusCodes.Status404NotFound);
                opt.ProducesProblem(StatusCodes.Status400BadRequest);
            });
        }

        public override async Task HandleAsync(GetCategoryByIdRequest request, CancellationToken ct)
        {
            /// Route<T>() method is only able to handle types that have a static TryParse() method.
            /// See here on how to add parsing support for your own types. (https://fast-endpoints.com/docs/model-binding#supported-dto-property-types)
            /// More about models binding via this link: https://fast-endpoints.com/docs/model-binding#built-in-request-binding
            // string CategoryId = Query<string>("id");
            // if (string.IsNullOrEmpty(CategoryId)) throw new ArgumentException($"{nameof(CategoryId)} is null");

            var result = await _sender.Send(request, ct);

            var response = result.Object.Adapt<CategoryDto>();
            await SendOkAsync(new GetCategoryByIdResponse(response), ct);
        }
    }
}