namespace Discount.Grpc.Models;

public class SaleEventProduct
{
    public int    SaleEventId { get; set; }
    public string ProductId   { get; set; }

    // Navigation properties
    public virtual SaleEvent SaleEvent { get; set; }
    public virtual Product   Product   { get; set; }
}
