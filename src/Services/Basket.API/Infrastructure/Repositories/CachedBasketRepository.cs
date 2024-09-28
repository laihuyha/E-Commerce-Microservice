using System;
using System.Threading;
using System.Threading.Tasks;
using Basket.API.Application.Interfaces;
using Basket.API.Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Infrastructure.Repositories;

/// <summary>
/// Acts as a proxy and forwards the calls to the <see cref="BasketRepository"/> interface
/// </summary>
/// <param name="repository"></param>
public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
{
    public async Task<bool> DeleteBasket(string userId, CancellationToken cancellationToken = default)
    {
        var result = await repository.DeleteBasket(userId, cancellationToken);
        await cache.RemoveAsync(userId, cancellationToken);
        return result;
    }

    public async Task<Cart> GetBasket(string userId, CancellationToken cancellationToken = default)
    {
        var cachedValue = await cache.GetStringAsync(userId, token: cancellationToken);
        if (cachedValue is not null)
        {
            return JsonConvert.DeserializeObject<Cart>(cachedValue);
        }

        // If cache miss, fetch from repository and cache for 3 minutes
        var basket = await repository.GetBasket(userId, cancellationToken);
        await cache.SetStringAsync(userId, JsonConvert.SerializeObject(basket), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3)
        }, token: cancellationToken);

        return basket;
    }

    public async Task<Cart> StoreBasket(Cart cart, CancellationToken cancellationToken = default)
    {
        await repository.StoreBasket(cart, cancellationToken);
        await cache.SetStringAsync(cart.UserId, JsonConvert.SerializeObject(cart), cancellationToken);
        return cart;
    }
}
