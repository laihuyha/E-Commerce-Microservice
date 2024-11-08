using System.Threading.Tasks;
using Grpc.Core;

namespace Discount.Grpc.Services;

public class SaleEventService : SaleEventProtoService.SaleEventProtoServiceBase
{
    public override Task<SaleEvent> GetSaleEvent(GetSaleEventRequest request, ServerCallContext context) =>
        base.GetSaleEvent(request, context);

    public override Task<SaleEvent> CreateSaleEvent(CreateSaleEventRequest request, ServerCallContext context) =>
        base.CreateSaleEvent(request, context);

    public override Task<SaleEvent> UpdateSaleEvent(UpdateSaleEventRequest request, ServerCallContext context) =>
        base.UpdateSaleEvent(request, context);

    public override Task<DeleteSaleEventResponse> DeleteSaleEvent(DeleteSaleEventRequest request,
        ServerCallContext context) => base.DeleteSaleEvent(request, context);
}
