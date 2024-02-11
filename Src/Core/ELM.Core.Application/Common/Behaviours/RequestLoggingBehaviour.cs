using System.Diagnostics;
using ELM.Core.Application.Common.Interfaces;
using MediatR;

namespace ELM.Core.Application.Common.Behaviours
{
    internal sealed class RequestLoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILoggerService _logger;

        public RequestLoggingBehaviour(ILoggerService logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Information($"{typeof(TRequest).FullName} - Pre Processing", new { request });

                _timer.Start();

                var response = await next();

                _timer.Stop();

                if (_timer.ElapsedMilliseconds > 10000)
                {
                    _logger.Warning($"{typeof(TRequest).FullName} - Long Running Request", new { request, time = _timer.ElapsedMilliseconds });
                }

                _logger.Information($"{typeof(TRequest).FullName} - Post Processing", new { request, response });

                return response;
            }
            catch (Exception exception)
            {
                _logger.Error($"{typeof(TRequest).FullName} - Processing Failed", new { request, exception });
                throw;
            }
        }
    }
}
