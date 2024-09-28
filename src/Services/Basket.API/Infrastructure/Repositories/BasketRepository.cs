using System;
using System.Threading;
using System.Threading.Tasks;
using Basket.API.Application.Exceptions;
using Basket.API.Application.Interfaces;
using Basket.API.Domain.Models;
using Marten;

namespace Basket.API.Infrastructure.Repositories;

public class BasketRepository(IDocumentSession session) : IBasketRepository
{
    private readonly IDocumentSession _session = session;

    public async Task<bool> DeleteBasket(string userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var cart = await _session.LoadAsync<Cart>(userId, cancellationToken);
            if (cart is null)
            {
                throw new BasketNotFoundException(userId);
            }
            _session.Delete<Cart>(userId);
            await _session.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            return false;
            throw new BasketDeleteException("Error while deleting Cart", ex);
        }
    }

    public async Task<Cart> GetBasket(string userId, CancellationToken cancellationToken = default)
    {
        var basket = await _session.LoadAsync<Cart>(userId, cancellationToken);
        return basket is null ? throw new BasketNotFoundException(userId) : basket;
    }

    public async Task<Cart> StoreBasket(Cart cart, CancellationToken cancellationToken = default)
    {
        try
        {
            _session.Store(cart);
            await _session.SaveChangesAsync(cancellationToken);
            return cart;
        }
        catch (Exception ex)
        {
            throw new BasketStoreException("Error while storing Cart", ex);
        }
    }
}
