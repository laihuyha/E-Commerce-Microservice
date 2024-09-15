using System.Threading;
using System.Threading.Tasks;
using Basket.API.Application.DTO.Request;
using Basket.API.Application.DTO.Response;
using BuildingBlocks.CQRS;
using System;
using Basket.API.Domain.Models;
using Basket.API.Application.DTO.Results;

namespace Basket.API.Application.Basket.Get;

public class GetCartQueryHandler : IQueryHandler<GetCartRequest, GetCartResult>
{
    public async Task<GetCartResult> Handle(GetCartRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return new GetCartResult(new Cart(Guid.NewGuid().ToString()));
    }
}
