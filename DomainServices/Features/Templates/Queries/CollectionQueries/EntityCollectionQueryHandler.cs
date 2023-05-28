using ApplicationCore.Entities.Interfaces;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Templates.Queries.CollectionQueries;

public class
    EntityCollectionQueryHandler<TEntity> : IRequestHandler<EntityCollectionQuery<TEntity>, IEnumerable<TEntity>>
    where TEntity : IStorable
{
    private readonly IReadOnlyRepository<TEntity> _repository;

    public EntityCollectionQueryHandler(IReadOnlyRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TEntity>> Handle(EntityCollectionQuery<TEntity> request,
        CancellationToken cancellationToken)
    {
        return await _repository.ApplySpecification(request.Specification).ToListAsync(cancellationToken);
    }
}