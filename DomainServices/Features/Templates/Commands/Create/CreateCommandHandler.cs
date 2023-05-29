using ApplicationCore.Entities.Interfaces;
using ApplicationCore.Interfaces;
using DomainServices.Features.Templates.BusinessRulesValidators;
using MediatR;

namespace DomainServices.Features.Templates.Commands.Create;

public class CreateCommandHandler<TEntity> : IRequestHandler<CreateCommand<TEntity>, TEntity> where TEntity : IStorable
{
    protected readonly IBusinessRulesValidator<CreateCommand<TEntity>>? BusinessValidator;
    protected readonly IRepository<TEntity> Repository;

    public CreateCommandHandler(IRepository<TEntity> repository,
        IBusinessRulesValidator<CreateCommand<TEntity>>? businessValidator = null)
    {
        Repository = repository;
        BusinessValidator = businessValidator;
    }

    public virtual async Task<TEntity> Handle(CreateCommand<TEntity> request, CancellationToken cancellationToken)
    {
        if (BusinessValidator is not null)
        {
            await BusinessValidator.ValidateAsync(request, cancellationToken);
        }

        TEntity? entity = await Repository.InsertAsync(request.Entity, cancellationToken);
        await Repository.SaveChangesAsync(cancellationToken);
        return entity;
    }
}