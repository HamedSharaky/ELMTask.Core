using ELM.Core.Application.Common.Exceptions;
using ELM.Core.Application.Common.Interfaces;
using FluentValidation;
using MediatR;
using ValidationException = ELM.Core.Application.Common.Exceptions.ValidationException;

namespace ELM.Core.Application.Common.Behaviours
{
    internal sealed class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILoggerService _logger;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILoggerService logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators.Select(v => v.Validate(context)).SelectMany(result => result.Errors)
                .Where(f => f != null);

            var validationFailures = failures.ToList();

            if (validationFailures.Any())
            {
                _logger.Error($"{typeof(TRequest).FullName} - Validation Failed", new { request, failures });

                throw new ValidationException(validationFailures);
            }

            return next();
        }
    }
}
