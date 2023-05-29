using ApplicationCore.Entities.Interfaces;
using ApplicationCore.Interfaces;
using DomainServices.Features.Templates.BusinessRulesValidators;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace DomainServices.Features.Templates.Commands.Update;

public class UpdateCommandHandler<TEntity> : IRequestHandler<UpdateCommand<TEntity>, Unit> where TEntity : IStorable
{
    protected readonly IBusinessRulesValidator<UpdateCommand<TEntity>>? BusinessValidator;
    protected readonly IRepository<TEntity> Repository;

    public UpdateCommandHandler(IRepository<TEntity> repository,
        IBusinessRulesValidator<UpdateCommand<TEntity>>? businessValidator = null)
    {
        Repository = repository;
        BusinessValidator = businessValidator;
    }

    public virtual async Task<Unit> Handle(UpdateCommand<TEntity> request, CancellationToken cancellationToken)
    {
        if (BusinessValidator is not null)
        {
            await BusinessValidator.ValidateAsync(request, cancellationToken);
        }

        bool exists = await Repository.ExistsAsync(x => x.Id == request.Entity.Id,
            cancellationToken);
        if (!exists)
        {
            throw new ValidationException("Validation error",
                new[] { new ValidationFailure(nameof(request.Entity.Id), "Entity to update doesn't exists") });
        }

        Repository.Update(request.Entity);
        await Repository.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}