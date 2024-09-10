using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using System.Text;
using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Utils
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            Stopwatch watch = new();
            StringBuilder logMessage = new StringBuilder().AppendLine("--------------------------------------");
            string requestName = typeof(TRequest).Name;

            try
            {
                bool isQueryRequest = typeof(IQuery<TResponse>).IsAssignableFrom(typeof(TRequest));
                bool isCommandRequest = typeof(ICommand<TResponse>).IsAssignableFrom(typeof(TRequest)) || typeof(ICommand).IsAssignableFrom(typeof(TRequest));

                // Create log message with a newline separator
                // https://gist.github.com/fnky/458719343aabd01cfb17a3a4f7296797
                logMessage.AppendLine("\x1b[1;92m");

                if (isQueryRequest)
                {
                    watch.Start();
                    logMessage.AppendLine($"Handling {requestName}").AppendLine($"-----> Query: {@request}");
                }

                if (isCommandRequest)
                {
                    watch.Start();
                    logMessage.AppendLine($"Handling command {requestName}").AppendLine($"-----> Command: {@request}");
                }

                TResponse response = await next();

                watch.Stop();
                logMessage.AppendLine("--------------------------------------");
                logMessage.AppendLine($"Request Ellapsed Time: ======> {watch.ElapsedMilliseconds} ms");
                logMessage.AppendLine("\x1b[0m");
                logMessage.AppendLine("--------------------------------------");
                _logger.LogInformation(logMessage.ToString());

                return response;
            }
            catch (Exception ex)
            {
                logMessage.AppendLine("\x1b[1;91m");
                logMessage.AppendLine($"Handling failed{requestName}").AppendLine($"-----> Query: {@request}");
                logMessage.AppendLine($"Exception: ===>{ex}");
                return await next();
            }
        }
    }
}