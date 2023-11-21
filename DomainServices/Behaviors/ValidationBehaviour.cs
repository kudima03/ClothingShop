using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DomainServices.Behaviors;

public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators,
                                                      ILogger<ValidationBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request,
                                        RequestHandlerDelegate<TResponse> next,
                                        CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            string typeName = request.GetType().Name;

            logger.LogInformation($"Validating command {typeName}");

            ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);

            ValidationResult[] validationResults =
                await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            IEnumerable<ValidationFailure> failures = validationResults.SelectMany(result => result.Errors)
                                                                       .Where(error => error != null)
                                                                       .ToList();

            if (failures.Any())
            {
                logger.LogWarning($"Validation errors occurred in request {typeName}");

                throw new ValidationException("Validation exception", failures);
            }
        }

        return await next();
    }
}