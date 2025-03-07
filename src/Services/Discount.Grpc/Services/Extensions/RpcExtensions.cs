using System;
using System.Linq;
using System.Text.Json;
using Discount.Grpc.Models.Exceptions;
using FluentValidation.Results;
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

    public static void ValidateErrorHandler(this ValidationResult results)
    {
        if (results.IsValid) return;

        var errors = results.Errors.Select(failure =>
            new
            {
                Property     = failure.PropertyName,
                ErrorMessage = failure.ErrorMessage
            }).ToList();

        var response = new
        {
            Success = false,
            Errors  = errors
        };

        var serializeOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
        };

        var res = JsonSerializer.Serialize(response, serializeOptions);
        throw new RpcException(new Status(StatusCode.InvalidArgument, res));
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