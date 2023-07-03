using ApplicationCore.Entities.BaseEntity;
using ApplicationCore.Specifications;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ApplicationCore.Interfaces;

public interface IReadOnlyRepository<TEntity> : IDisposable, IAsyncDisposable where TEntity : StorableEntity
{
    IQueryable<TResult> ApplySpecification<TResult>(Specification<TEntity, TResult> specification);

    Task<TResult?> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                   Expression<Func<TEntity, bool>>? predicate = null,
                                                   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                   Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include =
                                                       null,
                                                   CancellationToken cancellationToken = default);

    Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                          CancellationToken cancellationToken = default);

    TResult? GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
                                        Expression<Func<TEntity, bool>>? predicate = null,
                                        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

    TEntity? GetFirstOrDefault(Expression<Func<TEntity, bool>>? predicate = null,
                               Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

    Task<TResult?> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                   Expression<Func<TEntity, bool>>? predicate = null,
                                                   Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include =
                                                       null,
                                                   CancellationToken cancellationToken = default);

    TEntity? Find(params object[] keyValues);

    ValueTask<TEntity?> FindAsync(object[] keyValues, CancellationToken cancellationToken = default);

    IQueryable<TEntity> GetAll();

    IQueryable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector);

    IQueryable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector,
                                        Expression<Func<TEntity, bool>>? predicate = null);

    IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? predicate = null,
                               Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                               Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

    IQueryable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector,
                                        Expression<Func<TEntity, bool>>? predicate = null,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

    Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IList<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                              CancellationToken cancellationToken = default);

    Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                     Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                     Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                     CancellationToken cancellationToken = default);

    Task<IList<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                              Expression<Func<TEntity, bool>>? predicate = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                              CancellationToken cancellationToken = default);

    int Count(Expression<Func<TEntity, bool>>? predicate = null);

    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null,
                         CancellationToken cancellationToken = default);

    long LongCount(Expression<Func<TEntity, bool>>? predicate = null);

    Task<long> LongCountAsync(Expression<Func<TEntity, bool>>? predicate = null,
                              CancellationToken cancellationToken = default);

    bool Exists(Expression<Func<TEntity, bool>>? predicate = null);

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>>? selector = null,
                           CancellationToken cancellationToken = default);

    T? Max<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>>? predicate = null);

    Task<T> MaxAsync<T>(Expression<Func<TEntity, T>> selector,
                        Expression<Func<TEntity, bool>>? predicate = null,
                        CancellationToken cancellationToken = default);

    T? Min<T>(Expression<Func<TEntity, T>> selector,
              Expression<Func<TEntity, bool>>? predicate = null);

    Task<T> MinAsync<T>(Expression<Func<TEntity, T>> selector,
                        Expression<Func<TEntity, bool>>? predicate = null,
                        CancellationToken cancellationToken = default);

    decimal Average(Expression<Func<TEntity, decimal>> selector,
                    Expression<Func<TEntity, bool>>? predicate = null);

    Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> selector,
                               Expression<Func<TEntity, bool>>? predicate = null,
                               CancellationToken cancellationToken = default);

    decimal Sum(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>>? predicate = null);

    Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> selector,
                           Expression<Func<TEntity, bool>>? predicate = null,
                           CancellationToken cancellationToken = default);
}