using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using Catalog.API.Request.Product;
using Catalog.API.Response.Product;
using Marten;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Products.Get
{
    internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) : IQueryHandler<GetProductsRequest, GetProductsResult>
    {
        private readonly IDocumentSession _session = session;
        private readonly ILogger<GetProductsQueryHandler> _logger = logger;

        public async Task<GetProductsResult> Handle(GetProductsRequest query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetProductsQueryHandler.Handle {Query}", query);
            var products = await _session.Query<Models.Product>().ToListAsync(cancellationToken);
            return new GetProductsResult(products);
        }
    }
}