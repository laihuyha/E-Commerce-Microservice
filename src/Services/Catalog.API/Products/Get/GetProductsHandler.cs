using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Catalog.API.Request.Product;
using Catalog.API.Response.Product;
using Microsoft.Extensions.Logging;
using MongoDB.Entities;

namespace Catalog.API.Products.Get
{
    public class GetProductsQueryHandler(ILogger<GetProductsQueryHandler> logger) : IQueryHandler<GetProductsRequest, GetProductsResult>
    {
        private readonly ILogger<GetProductsQueryHandler> _logger = logger;

        public async Task<GetProductsResult> Handle(GetProductsRequest query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetProductsQueryHandler.Handle {Query}", query);
            var products = await DB.Find<Product>().ExecuteAsync(cancellationToken);
            return new GetProductsResult(products);
        }
    }
}