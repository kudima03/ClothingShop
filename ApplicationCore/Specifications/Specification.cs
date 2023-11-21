using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications;

public class Specification<TEntity, TResult>(Expression<Func<TEntity, TResult>> selector,
                                             Expression<Func<TEntity, bool>>? predicate = null,
                                             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                             Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
{
    public Expression<Func<TEntity, TResult>> Selector { get; init; } = selector;

    public Expression<Func<TEntity, bool>>? Predicate { get; init; } = predicate;

    public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? OrderBy { get; init; } = orderBy;

    public Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? Include { get; init; } = include;
}