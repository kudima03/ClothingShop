using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications;

public class Specification<TEntity, TResult>
{
    public Specification(Expression<Func<TEntity, TResult>> selector,
                         Expression<Func<TEntity, bool>>? predicate = null,
                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        Selector = selector;
        Predicate = predicate;
        OrderBy = orderBy;
        Include = include;
    }

    public Expression<Func<TEntity, TResult>> Selector { get; init; }

    public Expression<Func<TEntity, bool>>? Predicate { get; init; }

    public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? OrderBy { get; init; }

    public Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? Include { get; init; }
}