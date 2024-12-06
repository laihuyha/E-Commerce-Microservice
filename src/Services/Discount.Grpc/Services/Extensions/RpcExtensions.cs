using System;
using Discount.Grpc.Models.Exceptions;
using Grpc.Core;

namespace Discount.Grpc.Services.Extensions;

public static class RpcExtensions
{
    public static void ValidateNotNull<T>(this T obj, string message = null) where T : class
    {
        if (obj == null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument,
                message ?? "Invalid request object"));
        }
    }

    public static RpcException ToRpcException(this Exception ex)
    {
        var statusCode = ex switch
        {
            NotFoundException    => StatusCode.NotFound,
            DbOperationException => StatusCode.Internal,
            _                    => StatusCode.Internal
        };

        return new RpcException(new Status(statusCode, ex.Message));
    }
}
