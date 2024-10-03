using Basket.API.Application.DTO.Command;
using Basket.API.Application.DTO.Response;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Basket.API.UI.Endpoints.Basket;

public class DeleteCartEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/basket", async (string userId, ISender sender) =>
        {
            var command = new DeleteCartCommand(userId);
            var result = await sender.Send(command);
            var response = result.Adapt<DeleteCartResponse>();
            return TypedResults.Ok(response);
        })
        .WithName("DeleteCart")
        .WithSummary("Delete all items in the cart of a user.")
        .WithDescription("This will delete all items in the cart of a user.")
        .WithTags("Basket");
    }
}
