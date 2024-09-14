using System.Collections.Generic;
using System.Linq;

namespace Basket.API.Domain.Models;

public class Cart
{
    public Cart() { }
    public Cart(string userId) { UserId = userId; }
    public Cart(string userId, string userName)
    {
        UserId = userId;
        UserName = userName;
    }

    public string UserId { get; set; }
    public string UserName { get; set; }
    public List<CartItem> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
}
