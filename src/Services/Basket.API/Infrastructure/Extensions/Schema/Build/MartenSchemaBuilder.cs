using Basket.API.Domain.Models;
using Marten;

namespace Basket.API.Infrastructure.Schema.Build;

public static class MartenSchemaBuilder
{
    public static void BuildSchema(this MartenRegistry schema)
    {
        schema.For<Cart>().Identity(e => e.UserId).Index(e => e.UserId);
    }
}
