using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Utils
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            Stopwatch watch = new();
            StringBuilder logMessage = new();
            _ = logMessage.AppendLine("--------------------------------------");
            string requestName = typeof(TRequest).Name;

            try
            {
                bool isQueryRequest = typeof(IQuery<TResponse>).IsAssignableFrom(typeof(TRequest));
                bool isCommandRequest = typeof(ICommand<TResponse>).IsAssignableFrom(typeof(TRequest)) || typeof(ICommand).IsAssignableFrom(typeof(TRequest));

                _ = logMessage.AppendLine("\x1b[1;92m");

                if (isQueryRequest)
                {
                    watch.Start();
                    _ = logMessage.AppendLine($"Handling {requestName}").AppendLine($"-----> Query: {@request}");
                }

                if (isCommandRequest)
                {
                    watch.Start();
                    _ = logMessage.AppendLine($"Handling command {requestName}").AppendLine($"-----> Command: {@request}");
                }

                TResponse response = await next();

                watch.Stop();
                _ = logMessage.AppendLine("--------------------------------------")
                .AppendLine($"Request Ellapsed Time: ======> {watch.ElapsedMilliseconds} ms")
                .AppendLine("\x1b[0m")
                .AppendLine("--------------------------------------");
                logger.LogInformation("{LogMessage}", logMessage.ToString());

                return response;
            }
            catch (Exception ex)
            {
                _ = logMessage.AppendLine("\x1b[1;91m")
                .AppendLine($"Handling failed{requestName}")
                .AppendLine($"-----> Query: {@request}")
                .AppendLine($"Exception: ===>{ex}");
                return await next();
            }
        }
    }
}