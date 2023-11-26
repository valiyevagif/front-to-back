using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Bigon.Infrastructure.Middlewares
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<TRequest> logger;

        public LoggingBehaviour(ILogger<TRequest> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var response = await next();
            sw.Stop();

            logger.LogInformation($"{typeof(TRequest).Name} Elapsed: {sw.ElapsedMilliseconds}ms");
            return response;
        }
    }
}
