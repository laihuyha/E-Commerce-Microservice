using System;
using System.Threading.Tasks;
using Discount.Grpc.Data;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService(DataContext _dbContext)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<Coupon> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _dbContext.Coupons.FirstOrDefaultAsync(e =>
            e.ProductName.Equals(request.ProductName));
        return coupon?.Adapt<Coupon>();
    }

    public override async Task<Coupon> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        if (request is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
        }

        var newCounpon = request.Coupon.Adapt<Models.Coupon>();
        _dbContext.Coupons.Add(newCounpon);
        var res = await _dbContext.SaveChangesAsync();
        return res > 0 ? request.Coupon : null;
    }

    public override Task<Coupon> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context) =>
        base.UpdateDiscount(request, context);

    public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request,
        ServerCallContext context) => base.DeleteDiscount(request, context);
}
