using System.Threading;
using System.Threading.Tasks;
using Basket.API.Application.DTO.Command;
using Basket.API.Application.DTO.Results;
using Basket.API.Application.Interfaces;
using BuildingBlocks.CQRS;

namespace Basket.API.Application.Basket.Delete;

public class DeleteCartHandler(IBasketRepository basketRepository) : ICommandHandler<DeleteCartCommand, DeleteCartResult>
{
    public async Task<DeleteCartResult> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        var result = await basketRepository.DeleteBasket(request.UserId, cancellationToken);
        return new DeleteCartResult(result);
    }
}
