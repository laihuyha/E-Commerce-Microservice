using System.Threading;
using System.Threading.Tasks;
using Basket.API.Application.DTO.Command;
using Basket.API.Application.DTO.Results;
using Basket.API.Application.Interfaces;
using BuildingBlocks.CQRS;

namespace Basket.API.Application.Basket.Store;

public class StoreCartHandler(IBasketRepository basketRepository) : ICommandHandler<StoreCartCommand, StoreCartResult>
{
    public async Task<StoreCartResult> Handle(StoreCartCommand request, CancellationToken cancellationToken)
    {
        var result = await basketRepository.StoreBasket(request.Cart, cancellationToken);
        return new StoreCartResult(result.UserId);
    }
}
