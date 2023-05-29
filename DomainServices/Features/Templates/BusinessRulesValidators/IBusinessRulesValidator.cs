namespace DomainServices.Features.Templates.BusinessRulesValidators;

public interface IBusinessRulesValidator<in TEntity>
{
    Task ValidateAsync(TEntity entity, CancellationToken cancellation = default);
}