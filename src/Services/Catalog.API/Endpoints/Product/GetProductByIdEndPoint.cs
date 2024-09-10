using System;
using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Endpoints.Groups;
using Catalog.API.Request.Product;
using FastEndpoints;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Endpoints.Product
{
    public record GetProductByIdResponse(Models.Product Product);
    public class GetProductByIdEndPoint(ISender sender) : EndpointWithoutRequest<GetProductByIdResponse>
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            Get("/{id}"); // Need filter parameters will be add in future
            Group<ProductGroupEndPoints>();
            Options(opt =>
            {
                opt.WithName("GetProductById");
                opt.WithSummary("Get product by Id");
                opt.WithDescription("Get a products with Id.");
                opt.Produces<GetProductByIdResponse>(StatusCodes.Status200OK);
                opt.ProducesProblem(StatusCodes.Status404NotFound);
                opt.ProducesProblem(StatusCodes.Status400BadRequest);
            });
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            /// Route<T>() method is only able to handle types that have a static TryParse() method.
            /// See here on how to add parsing support for your own types. (https://fast-endpoints.com/docs/model-binding#supported-dto-property-types)
            /// More about models binding via this link: https://fast-endpoints.com/docs/model-binding#built-in-request-binding

            string productId = Route<string>("id");
            if (string.IsNullOrEmpty(productId)) throw new ArgumentException($"{nameof(productId)} is null");

            var result = await _sender.Send(new GetProductByIdRequest(productId), ct);
            
            var response = result.Adapt<GetProductByIdResponse>();
            await SendOkAsync(response, ct);

        }
    }
}