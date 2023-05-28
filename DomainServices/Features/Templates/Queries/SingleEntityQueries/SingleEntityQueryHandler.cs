using ApplicationCore.Entities.Interfaces;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Templates.Queries.SingleEntityQueries;

public class SingleEntityQueryHandler<TEntity> : IRequestHandler<SingleEntityQuery<TEntity>, TEntity?>
    where TEntity : IStorable
{
    private readonly IReadOnlyRepository<TEntity> _repository;

    public SingleEntityQueryHandler(IReadOnlyRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public async Task<TEntity?> Handle(SingleEntityQuery<TEntity> request, CancellationToken cancellationToken)
    {
        TEntity? entity = await _repository.ApplySpecification(request.Specification)
            .FirstOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            throw new ValidationException("Validation exception",
                new[]
                {
                    new ValidationFailure($"{typeof(TEntity).Name}", "Entity with such parameters doesn't exist.")
                });
        }

        return entity;
    }
}