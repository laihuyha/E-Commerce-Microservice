using Basket.API.Application.DTO.Request;
using Basket.API.Application.DTO.Response;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Basket.API.UI.Endpoints;

public class GetCartEndPoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		/// This should be get user id from BearerToken but now just passed id into endpoint for get cart.
		app.MapGet("/api/basket", async (string userId, ISender sender) =>
		{
			var result = await sender.Send(new GetCartRequest(userId));
			var response = result.Adapt<GetCartResponse>();
			return TypedResults.Ok(response);
		})
		.WithName("GetCartByUserId")
		.WithSummary("Get Cart by user id")
		.WithDescription("Get Cart by user id");
	}
}
