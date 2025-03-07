using System.Threading;
using System.Threading.Tasks;
using Basket.API.Application.DTO.Command;
using Basket.API.Application.DTO.Results;
using Basket.API.Application.Interfaces;
using BuildingBlocks.CQRS;
using Discount.Grpc;

namespace Basket.API.Application.Basket.Store;

public class StoreCartHandler(IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient) : ICommandHandler<StoreCartCommand, StoreCartResult>
{
    public async Task<StoreCartResult> Handle(StoreCartCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.Cart.Items)
        {
            var coupon = await discountProtoServiceClient.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            item.Price -= (decimal)coupon.Amount;
        }
        var result = await basketRepository.StoreBasket(request.Cart, cancellationToken);
        return new StoreCartResult(result.UserId);
    }
}
