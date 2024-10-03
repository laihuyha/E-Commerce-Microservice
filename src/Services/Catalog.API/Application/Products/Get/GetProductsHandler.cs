using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Catalog.API.Request.Product;
using Catalog.API.Response.Product;
using MongoDB.Driver;
using MongoDB.Entities;

namespace Catalog.API.Products.Get
{
    public class GetProductsQueryHandler : IQueryHandler<GetProductsRequest, GetProductsResult>
    {
        private const int DefaultPageSize = 10;
        private const int DefaultPageNumber = 1;
        private const int MaxPageSize = 30;

        public async Task<GetProductsResult> Handle(GetProductsRequest query, CancellationToken ct)

        {
            int pageNumber = query.PageNumber ?? DefaultPageNumber;
            int pageSize = query.PageSize.HasValue
                ? (query.PageSize.Value > MaxPageSize ? MaxPageSize : query.PageSize.Value)
                : DefaultPageSize;

            Expression<Func<Product, bool>> matchQueryExpression = x => true;

            if (query.BrandId is not null)
            {
                matchQueryExpression = x => x.Brand.ID == query.BrandId;
            }
            if (query.CateIds.Count > 0)
            {
                // Continue add into existing matchQueryExpression && x.Categories.Select(e => e.ID).Intersect(query.CategoryIds).Any();
                matchQueryExpression = x => matchQueryExpression.Compile()(x) && x.Categories.Select(e => e.ID).Intersect(query.CateIds).Any();
            }
            if (query.AttrIds.Count > 0)
            {
                // Continue add into existing matchQueryExpression && x.Attributes.Select(e => e.ID).Intersect(query.AttrIds).Any();
                matchQueryExpression = x => matchQueryExpression.Compile()(x) && x.Attributes.Select(e => e.ID).Intersect(query.AttrIds).Any();
            }

            var (products, totalCount, pageCount) = await DB.PagedSearch<Product>()
                .Match(matchQueryExpression)
                .Sort(prod => prod.Ascending("Name").Descending("CreatedOn")) // or Sort(prod => prod.Name, Order.Ascending) for simple usage.
                .PageSize(pageSize)
                .PageNumber(pageNumber)
                .ExecuteAsync(ct);


            return new GetProductsResult(products);
        }
    }
}
