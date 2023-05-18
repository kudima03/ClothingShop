using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ApplicationCore.Interfaces;

public interface IReadOnlyRepository<TEntity> : IDisposable, IAsyncDisposable where TEntity : class
{
    Task<TResult?> GetFirstOrDefaultNonTrackingAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

    Task<TEntity?> GetFirstOrDefaultNonTrackingAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

    TResult? GetFirstOrDefaultNonTracking<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

    TEntity? GetFirstOrDefaultNonTracking(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

    Task<TResult?> GetFirstOrDefaultNonTrackingAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

    TEntity? FindNonTracking(params object[] keyValues);

    ValueTask<TEntity?> FindNonTrackingAsync(params object[] keyValues);

    ValueTask<TEntity?> FindNonTrackingAsync(object[] keyValues, CancellationToken cancellationToken = default);


    IQueryable<TEntity> GetAllNonTracking();

    IQueryable<TResult> GetAllNonTracking<TResult>(
        Expression<Func<TEntity, TResult>> selector);

    IQueryable<TResult> GetAllNonTracking<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null);

    IQueryable<TEntity> GetAllNonTracking(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

    IQueryable<TResult> GetAllNonTracking<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

    Task<IList<TEntity>> GetAllNonTrackingAsync();

    Task<IList<TResult>> GetAllNonTrackingAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector);

    Task<IList<TEntity>> GetAllNonTrackingAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

    Task<IList<TResult>> GetAllNonTrackingAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

    int Count(Expression<Func<TEntity, bool>>? predicate = null);

    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    long LongCount(Expression<Func<TEntity, bool>>? predicate = null);

    Task<long> LongCountAsync(Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    bool Exists(Expression<Func<TEntity, bool>>? predicate = null);

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>>? selector = null,
        CancellationToken cancellationToken = default);

    void SaveChanges();
    Task SaveChangesAsync();
}