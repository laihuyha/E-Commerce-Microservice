using System;
using System.Threading.Tasks;
using Discount.Grpc.Data;
using Discount.Grpc.Models.Exceptions;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Discount.Grpc.Services;

public class SaleEventService(DataContext dbContext, ILogger<SaleEventService> logger) : SaleEventProtoService.SaleEventProtoServiceBase
{
    private readonly DataContext               _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    private readonly ILogger<SaleEventService> _logger    = logger ?? throw new ArgumentNullException(nameof(logger));

    public override async Task<SaleEvent> GetSaleEvent(GetSaleEventRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Getting SaleEvent");
        var saleEvent = await _dbContext.SaleEvents.AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == request.Id);

        return saleEvent.Adapt<SaleEvent>() ?? throw new NotFoundException($"Not found");
    }

    public override async Task<SaleEvent> CreateSaleEvent(CreateSaleEventRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Creating SaleEvent");
        var saleEvent = request.Adapt<Models.SaleEvent>();
        _dbContext.SaleEvents.Add(saleEvent);
        var res = await _dbContext.SaveChangesAsync();
        return res > 0 ? request.Coupon : null;
    }

    public override async Task<SaleEvent> UpdateSaleEvent(UpdateSaleEventRequest request, ServerCallContext context)
    {
        var existingObject = await _dbContext.SaleEvents.AsNoTracking().FirstOrDefaultAsync(e => e.Id == request.Coupon.Id);
        if (existingObject != null)
        {
            var updateSaleEvent = request.Coupon.Adapt<Models.SaleEvent>();
            existingObject                         = updateSaleEvent;
            _dbContext.Entry(existingObject).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        return request.Coupon;
    }

    public override async Task<DeleteSaleEventResponse> DeleteSaleEvent(DeleteSaleEventRequest request, ServerCallContext context)
    {
        var saleEvent = await _dbContext.SaleEvents.FindAsync(request.Id);
        if (saleEvent != null)
        {
            _dbContext.SaleEvents.Remove(saleEvent);
        }
        var res = await _dbContext.SaveChangesAsync();
        return new DeleteSaleEventResponse{
            Success = res > 0
        };
    }
}