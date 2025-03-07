using System;

namespace Discount.Grpc.Models;

public class Coupon
{
	public int          Id          { get; set; }
	public string       ProductName { get; set; }
	public string       Description { get; set; }
	public decimal      Amount      { get; set; }
	public DiscountType Type        { get; set; }
	public DateTime?    ExpiryDate  { get; set; }
}

public enum DiscountType
{
	Percentage,
	FixAmount
}