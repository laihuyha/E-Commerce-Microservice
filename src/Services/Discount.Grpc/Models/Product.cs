using System.Collections.Generic;

namespace Discount.Grpc.Models;

public class Product
{
    public         string ProductId { get; set; }
    public         string Name      { get; set; }
    public virtual ICollection<SaleEventProduct> SaleEventProducts { get; set; }
}
