using System;
using System.Collections.Generic;

namespace Discount.Grpc.Models;

public class SaleEvent
{
    public int      Id          { get; set; }
    public string   Description { get; set; }
    public decimal  SalePercent { get; set; }
    public DateTime StartDate   { get; set; }

    public DateTime EndDate { get; set; }

    // Optional navigation property - can be empty
    public virtual ICollection<SaleEventProduct> SaleEventProducts { get; set; } = new List<SaleEventProduct>();
}
