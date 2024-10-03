using System.Threading;
using System.Threading.Tasks;
using Basket.API.Domain.Models;

namespace Basket.API.Application.Interfaces;

public interface IBasketRepository
{
    Task<Cart> GetBasket(string userId, CancellationToken cancellationToken = default);
    Task<Cart> StoreBasket(Cart cart, CancellationToken cancellationToken = default);
    Task<bool> DeleteBasket(string userId, CancellationToken cancellationToken = default);
}
