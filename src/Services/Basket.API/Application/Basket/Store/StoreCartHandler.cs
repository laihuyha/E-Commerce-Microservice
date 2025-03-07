using System.Threading;
using System.Threading.Tasks;
using Basket.API.Application.DTO.Command;
using Basket.API.Application.DTO.Results;
using Basket.API.Application.Interfaces;
using Basket.API.Application.Services;
using BuildingBlocks.CQRS;

namespace Basket.API.Application.Basket.Store;

public class StoreCartHandler(IBasketRepository basketRepository, GrpcDiscountServiceClient grpcDiscountServiceClient) : ICommandHandler<StoreCartCommand, StoreCartResult>
{
    public async Task<StoreCartResult> Handle(StoreCartCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.Cart.Items)
        {
            var coupon = await grpcDiscountServiceClient.GetDiscountAsync(item.ProductName, cancellationToken);
            item.Price -= (decimal)coupon.Amount;
        }
        var result = await basketRepository.StoreBasket(request.Cart, cancellationToken);
        return new StoreCartResult(result.UserId);
    }
}
