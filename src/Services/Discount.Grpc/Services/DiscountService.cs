// DiscountService.cs

using System;
using System.Threading.Tasks;
using Discount.Grpc.Data;
using Discount.Grpc.Models.Exceptions;
using Discount.Grpc.Services.Extensions;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Discount.Grpc.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly DataContext              _dbContext;
    private readonly ILogger<DiscountService> _logger;

    public DiscountService(DataContext dbContext, ILogger<DiscountService> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger    = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override async Task<Coupon> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        try
        {
            _logger.LogInformation("Getting discount for product: {ProductName}", request.ProductName);

            var coupon = await _dbContext.Coupons
                                         .FirstOrDefaultAsync(e => e.ProductName.Equals(request.ProductName));

            if (coupon == null)
            {
                throw new NotFoundException($"Coupon not found for product: {request.ProductName}");
            }

            return coupon.Adapt<Coupon>();
        }
        catch (Exception ex) when (ex is not RpcException)
        {
            _logger.LogError(ex, "Error getting discount for product: {ProductName}", request.ProductName);
            throw ex.ToRpcException();
        }
    }

    public override async Task<Coupon> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        try
        {
            request.ValidateNotNull();
            request.Coupon.ValidateNotNull();

            _logger.LogInformation("Creating discount for product: {ProductName}", request.Coupon.ProductName);

            var newCoupon = request.Coupon.Adapt<Models.Coupon>();
            _dbContext.Coupons.Add(newCoupon);

            await SaveChangesWithValidation();

            return request.Coupon;
        }
        catch (Exception ex) when (ex is not RpcException)
        {
            _logger.LogError(ex, "Error creating discount for product: {ProductName}", request.Coupon?.ProductName);
            throw ex.ToRpcException();
        }
    }

    public override async Task<Coupon> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        try
        {
            request.ValidateNotNull();
            request.Coupon.ValidateNotNull();

            _logger.LogInformation("Updating discount for product: {ProductName}", request.Coupon.ProductName);

            var newCoupon = request.Coupon.Adapt<Models.Coupon>();
            _dbContext.Coupons.Update(newCoupon);

            await SaveChangesWithValidation();

            return request.Coupon;
        }
        catch (Exception ex) when (ex is not RpcException)
        {
            _logger.LogError(ex, "Error updating discount for product: {ProductName}", request.Coupon?.ProductName);
            throw ex.ToRpcException();
        }
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request,
        ServerCallContext                                                                   context)
    {
        try
        {
            _logger.LogInformation("Deleting discount with ID: {Id}", request.Id);

            var coupon = await _dbContext.Coupons.FindAsync(request.Id)
                         ?? throw new NotFoundException($"Coupon with id: {request.Id} does not exist");

            _dbContext.Coupons.Remove(coupon);
            await SaveChangesWithValidation();

            return new DeleteDiscountResponse { Success = true };
        }
        catch (Exception ex) when (ex is not RpcException)
        {
            _logger.LogError(ex, "Error deleting discount with ID: {Id}", request.Id);
            throw ex.ToRpcException();
        }
    }

    private async Task SaveChangesWithValidation()
    {
        var saveResult = await _dbContext.SaveChangesAsync();
        if (saveResult <= 0)
        {
            throw new DbOperationException("Database operation failed");
        }
    }
}
