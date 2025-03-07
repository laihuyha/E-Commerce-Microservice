using System;
using System.Threading;
using System.Threading.Tasks;
using Discount.Grpc;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Basket.API.Application.Services;

public class GrpcDiscountServiceClient
{
    private readonly ILogger<GrpcDiscountServiceClient> _logger;
    private readonly IConfiguration _configuration;

    public GrpcDiscountServiceClient(ILogger<GrpcDiscountServiceClient> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }


    public async Task<Coupon> GetDiscountAsync(string productName, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Calling Grpc Service");
        var channel = GrpcChannel.ForAddress(_configuration["GrpcAuction"]);
        var client = new DiscountProtoService.DiscountProtoServiceClient(channel);
        var request = new GetDiscountRequest { ProductName = productName };

        try
        {
            var reply = await client.GetDiscountAsync(request, cancellationToken: cancellationToken);
            var coupon = new Coupon
            {
                Id = reply.Id,
                Amount = reply.Amount,
                Description = reply.Description,
                ExpiryDate = reply.ExpiryDate,
                ProductName = reply.ProductName,
                Type = reply.Type
            };
            return coupon;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Couldn't call Grpc Server");
            return null;
        }
    }
}
