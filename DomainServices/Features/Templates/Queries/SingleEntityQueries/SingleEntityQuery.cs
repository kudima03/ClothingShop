using ApplicationCore.Entities.Interfaces;
using ApplicationCore.Specifications;
using MediatR;

namespace DomainServices.Features.Templates.Queries.SingleEntityQueries;

public abstract class SingleEntityQuery<TEntity> : IRequest<TEntity?> where TEntity : IStorable
{
    protected SingleEntityQuery(Specification<TEntity, TEntity> specification)
    {
        Specification = specification;
    }

    public Specification<TEntity, TEntity> Specification { get; init; }
}