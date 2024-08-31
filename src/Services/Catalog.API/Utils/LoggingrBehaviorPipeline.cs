using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using BuildingBlocks.CQRS;
using System.Text;

namespace Catalog.API.Utils
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
            bool isQueryRequest = typeof(IQuery<TResponse>).IsAssignableFrom(typeof(TRequest));
            bool isCommandRequest = typeof(ICommand<TResponse>).IsAssignableFrom(typeof(TRequest)) || typeof(ICommand).IsAssignableFrom(typeof(TRequest));

            string requestName = typeof(TRequest).Name;
            // Create log message with a newline separator
            // https://gist.github.com/fnky/458719343aabd01cfb17a3a4f7296797
            StringBuilder logMessage = new StringBuilder().AppendLine("--------------------------------------")
                .AppendLine("\x1b[1;92m");

            if (isQueryRequest)
            {
                logMessage.AppendLine($"Handling {requestName}").AppendLine($"-----> Query: {@request}");
            }

            if (isCommandRequest)
            {
                logMessage.AppendLine($"Handling command {requestName}").AppendLine($"-----> Command: {@request}");
            }
            logMessage.AppendLine("\x1b[0m");
            logMessage.AppendLine("--------------------------------------");
            _logger.LogInformation(logMessage.ToString());
            TResponse response = await next();

            return response;
        }
    }
}