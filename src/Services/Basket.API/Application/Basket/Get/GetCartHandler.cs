using System.Threading;
using System.Threading.Tasks;
using Basket.API.Application.DTO.Request;
using BuildingBlocks.CQRS;
using Basket.API.Application.DTO.Results;
using Basket.API.Application.Interfaces;

namespace Basket.API.Application.Basket.Get;

public class GetCartQueryHandler(IBasketRepository basketRepository) : IQueryHandler<GetCartRequest, GetCartResult>
{
    public async Task<GetCartResult> Handle(GetCartRequest request, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.GetBasket(request.UserId, cancellationToken);
        return new GetCartResult(basket);
    }
}
