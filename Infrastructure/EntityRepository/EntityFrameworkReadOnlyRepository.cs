using ApplicationCore.Entities.BaseEntity;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Infrastructure.EntityRepository;

public class EntityFrameworkReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity :  StorableEntity
{
    private readonly DbContext _dbContext;

    private readonly DbSet<TEntity> _dbSet;

    public EntityFrameworkReadOnlyRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
    }

    public IQueryable<TResult> ApplySpecification<TResult>(Specification<TEntity, TResult> specification)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        if (specification.Include is not null)
        {
            query = specification.Include(query);
        }

        if (specification.Predicate is not null)
        {
            query = query.Where(specification.Predicate);
        }

        return specification.OrderBy is not null
            ? specification.OrderBy(query).Select(specification.Selector)
            : query.Select(specification.Selector);
    }

    public async Task<TResult?> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        if (include is not null)
        {
            query = include(query);
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return orderBy is not null
            ? await orderBy(query).Select(selector).FirstOrDefaultAsync(cancellationToken)
            : await query.Select(selector).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        if (include is not null)
        {
            query = include(query);
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public TResult? GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        if (include is not null)
        {
            query = include(query);
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return query.Select(selector).FirstOrDefault();
    }

    public TEntity? GetFirstOrDefault(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        if (include is not null)
        {
            query = include(query);
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return query.FirstOrDefault();
    }

    public async Task<TResult?> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        if (include is not null)
        {
            query = include(query);
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return await query.Select(selector).FirstOrDefaultAsync(cancellationToken);
    }

    public TEntity? Find(params object[] keyValues)
    {
        TEntity? entity = _dbSet.Find(keyValues);
        if (entity is null)
        {
            return null;
        }

        _dbSet.Entry(entity).State = EntityState.Detached;
        return entity;
    }

    public async ValueTask<TEntity?> FindAsync(object[] keyValues,
        CancellationToken cancellationToken = default)
    {
        TEntity? entity = await _dbSet.FindAsync(keyValues, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        _dbSet.Entry(entity).State = EntityState.Detached;
        return entity;
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbSet.AsNoTracking();
    }

    public IQueryable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector)
    {
        return _dbSet.AsNoTracking().Select(selector);
    }

    public IQueryable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        if (predicate is not null)
        {
            query = _dbSet.Where(predicate);
        }

        return query.Select(selector);
    }

    public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        if (include is not null)
        {
            query = include(query);
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return orderBy is not null
            ? orderBy(query)
            : query;
    }

    public IQueryable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        if (include is not null)
        {
            query = include(query);
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return orderBy != null
            ? orderBy(query).Select(selector)
            : query.Select(selector);
    }

    public async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<IList<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().Select(selector).ToListAsync(cancellationToken);
    }

    public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        if (include is not null)
        {
            query = include(query);
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return orderBy is not null
            ? await orderBy(query).ToListAsync(cancellationToken)
            : await query.ToListAsync(cancellationToken);
    }

    public async Task<IList<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        if (include is not null)
        {
            query = include(query);
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return orderBy is not null
            ? await orderBy(query).Select(selector).ToListAsync(cancellationToken)
            : await query.Select(selector).ToListAsync(cancellationToken);
    }

    public int Count(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return predicate is null
            ? _dbSet.Count()
            : _dbSet.Count(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        return predicate is null
            ? await _dbSet.CountAsync(cancellationToken)
            : await _dbSet.CountAsync(predicate, cancellationToken);
    }

    public long LongCount(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return predicate is null
            ? _dbSet.LongCount()
            : _dbSet.LongCount(predicate);
    }

    public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        return predicate is null
            ? await _dbSet.LongCountAsync(cancellationToken)
            : await _dbSet.LongCountAsync(predicate, cancellationToken);
    }

    public bool Exists(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return predicate is null
            ? _dbSet.Any()
            : _dbSet.Any(predicate);
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>>? selector = null,
        CancellationToken cancellationToken = default)
    {
        return selector is null
            ? await _dbSet.AnyAsync(cancellationToken)
            : await _dbSet.AnyAsync(selector, cancellationToken);
    }

    public T? Max<T>(
        Expression<Func<TEntity, T>> selector,
        Expression<Func<TEntity, bool>>? predicate = null)
    {
        return predicate is null
            ? _dbSet.Max(selector)
            : _dbSet.Where(predicate).Max(selector);
    }

    public Task<T> MaxAsync<T>(
        Expression<Func<TEntity, T>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        return predicate is null
            ? _dbSet.MaxAsync(selector, cancellationToken)
            : _dbSet.Where(predicate).MaxAsync(selector, cancellationToken);
    }

    public T? Min<T>(
        Expression<Func<TEntity, T>> selector,
        Expression<Func<TEntity, bool>>? predicate = null)
    {
        return predicate is null
            ? _dbSet.Min(selector)
            : _dbSet.Where(predicate).Min(selector);
    }

    public Task<T> MinAsync<T>(
        Expression<Func<TEntity, T>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        return predicate is null
            ? _dbSet.MinAsync(selector, cancellationToken)
            : _dbSet.Where(predicate).MinAsync(selector, cancellationToken);
    }

    public decimal Average(
        Expression<Func<TEntity, decimal>> selector,
        Expression<Func<TEntity, bool>>? predicate = null)
    {
        return predicate is null
            ? _dbSet.Average(selector)
            : _dbSet.Where(predicate).Average(selector);
    }

    public Task<decimal> AverageAsync(
        Expression<Func<TEntity, decimal>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        return predicate is null
            ? _dbSet.AverageAsync(selector, cancellationToken)
            : _dbSet.Where(predicate).AverageAsync(selector, cancellationToken);
    }

    public decimal Sum(
        Expression<Func<TEntity, decimal>> selector,
        Expression<Func<TEntity, bool>>? predicate = null)
    {
        return predicate is null
            ? _dbSet.Sum(selector)
            : _dbSet.Where(predicate).Sum(selector);
    }

    public Task<decimal> SumAsync(
        Expression<Func<TEntity, decimal>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        return predicate is null
            ? _dbSet.SumAsync(selector, cancellationToken)
            : _dbSet.Where(predicate).SumAsync(selector, cancellationToken);
    }
}