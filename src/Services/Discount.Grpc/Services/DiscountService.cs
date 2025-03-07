using System;
using System.Threading.Tasks;
using Discount.Grpc.Data;
using Discount.Grpc.Models.Exceptions;
using Discount.Grpc.Models.Validators;
using Discount.Grpc.Services.Extensions;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Discount.Grpc.Services
{
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
            return await ExecuteServiceOperation(async () =>
            {
                _logger.LogInformation("Getting discount for product: {ProductName}", request.ProductName);
                var coupon = await GetCouponByProductNameAsync(request.ProductName);
                return coupon.Adapt<Coupon>();
            }, request.ProductName);
        }

        public override async Task<Coupon> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            return await ExecuteServiceOperation(async () =>
            {
                var newCoupon = request.Coupon.Adapt<Models.Coupon>();
                var res       = new Models.Coupon();
                await SaveEntityWithValidation(request, () =>
                {
                    var res1 = _dbContext.Coupons.Add(newCoupon);
                    res = res1.Entity;
                });
                return res.Adapt<Coupon>();
            }, request.Coupon?.ProductName);
        }

        public override async Task<Coupon> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            return await ExecuteServiceOperation(async () =>
            {
                var updatedCoupon = request.Coupon.Adapt<Models.Coupon>();
                await SaveEntityWithValidation(request, () => _dbContext.Coupons.Update(updatedCoupon));

                return request.Coupon;
            }, request.Coupon?.ProductName);
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request,
            ServerCallContext                                                                   context)
        {
            return await ExecuteServiceOperation(async () =>
            {
                var coupon = await _dbContext.Coupons.FindAsync(request.Id)
                             ?? throw new NotFoundException($"Coupon with id: {request.Id} does not exist");

                await SaveEntityWithValidation(request, () => _dbContext.Coupons.Remove(coupon));

                return new DeleteDiscountResponse { Success = true };
            }, request.Id.ToString());
        }

        private async Task<T> ExecuteServiceOperation<T>(Func<Task<T>> operation, string identifier)
        {
            try
            {
                return await operation();
            }
            catch (Exception ex) when (ex is not RpcException)
            {
                _logger.LogError(ex, "Operation failed for identifier: {Identifier}", identifier);
                throw ex.ToRpcException();
            }
        }

        private async Task SaveEntityWithValidation(object request, Action action)
        {
            ValidateDiscountRequest(request);
            action();
            var saveResult = await _dbContext.SaveChangesAsync();
            if (saveResult <= 0)
            {
                throw new DbOperationException("Database operation failed");
            }
        }

        private async Task<Models.Coupon> GetCouponByProductNameAsync(string productName)
        {
            var coupon = await _dbContext.Coupons.AsNoTracking()
                                         .FirstOrDefaultAsync(e => e.ProductName.Equals(productName));

            return coupon ?? throw new NotFoundException($"Coupon not found for product: {productName}");
        }

        private static void ValidateDiscountRequest(object request)
        {
            request.ValidateNotNull();

            // Validate based on the type of the request
            switch (request)
            {
                case CreateDiscountRequest createRequest:
                    createRequest.Coupon.ValidateNotNull();
                    var createValidator = new CreateDiscountRequestValidator();
                    var createResults   = createValidator.Validate(createRequest);
                    createResults.ValidateErrorHandler();
                    break;

                case UpdateDiscountRequest updateRequest:
                    updateRequest.Coupon.ValidateNotNull();
                    var updateValidator = new UpdateDiscountValidator();
                    var updateResults   = updateValidator.Validate(updateRequest);
                    updateResults.ValidateErrorHandler();
                    break;
                case DeleteDiscountRequest deleteRequest:
                    if (deleteRequest.Id <= 0)
                        throw new RpcException(new Status(StatusCode.InvalidArgument, "Id is required"));
                    break;
                default:
                    throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request type"));
            }
        }
    }
}
