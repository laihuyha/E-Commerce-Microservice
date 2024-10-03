using Basket.API.Application.DTO.Command;
using Basket.API.Application.DTO.Response;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Basket.API.UI.Endpoints.Basket;

public class StoreCartEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/basket", async (StoreCartCommand request, ISender sender) =>
        {
            var command = request.Adapt<StoreCartCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<StoreCartResponse>();
            return TypedResults.Created($"/basket/{response.UserId}", response);
        })
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithName("StoreCart")
        .WithSummary("Store Cart")
        .WithDescription("Store Cart and return cart's userId");
    }
}
