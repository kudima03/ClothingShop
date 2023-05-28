using ApplicationCore.Entities.Interfaces;
using ApplicationCore.Specifications;
using MediatR;

namespace DomainServices.Features.Templates.Queries.CollectionQueries;

public abstract class EntityCollectionQuery<TEntity> : IRequest<IEnumerable<TEntity>> where TEntity : IStorable
{
    protected EntityCollectionQuery(Specification<TEntity, TEntity> specification)
    {
        Specification = specification;
    }

    public Specification<TEntity, TEntity> Specification { get; init; }
}