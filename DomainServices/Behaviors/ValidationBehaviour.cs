using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DomainServices.Behaviors;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<ValidationBehaviour<TRequest, TResponse>> _logger;
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators,
                               ILogger<ValidationBehaviour<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request,
                                        RequestHandlerDelegate<TResponse> next,
                                        CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            string typeName = request.GetType().Name;

            _logger.LogInformation($"Validating command {typeName}");

            ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);

            ValidationResult[] validationResults =
                await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            IEnumerable<ValidationFailure> failures = validationResults.SelectMany(result => result.Errors)
                                                                       .Where(error => error != null)
                                                                       .ToList();

            if (failures.Any())
            {
                _logger.LogWarning($"Validation errors occurred in request {typeName}");

                throw new ValidationException("Validation exception", failures);
            }
        }

        return await next();
    }
}