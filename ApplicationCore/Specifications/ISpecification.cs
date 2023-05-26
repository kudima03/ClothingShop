using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications;

public interface ISpecification<TEntity, TResult>
{
    public Expression<Func<TEntity, TResult>>? Selector { get; init; }
    public Expression<Func<TEntity, bool>>? Predicate { get; init; }
    public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? OrderBy { get; init; }
    public Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? Include { get; init; }
}