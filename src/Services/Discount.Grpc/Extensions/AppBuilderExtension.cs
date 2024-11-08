using Microsoft.AspNetCore.Builder;

namespace Discount.Grpc.Extensions;

public static class AppBuilderExtension
{
    public static IApplicationBuilder UseAppBuilderExtension(this IApplicationBuilder appBuilder) => appBuilder;
}
