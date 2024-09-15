using System.Threading;
using System.Threading.Tasks;
using Basket.API.Application.DTO.Request;
using Basket.API.Application.DTO.Response;
using BuildingBlocks.CQRS;
using System;
using Basket.API.Domain.Models;

namespace Basket.API.Application.Basket.Get;

public class GetCartQueryHandler : IQueryHandler<GetCartRequest, GetCartResponse>
{
    public async Task<GetCartResponse> Handle(GetCartRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return new GetCartResponse(new Cart(Guid.NewGuid().ToString()));
    }
}
