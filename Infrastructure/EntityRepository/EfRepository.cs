using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Infrastructure.EntityRepository;

public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbContext _dbContext;

    private readonly DbSet<TEntity> _dbSet;

    public EfRepository(DbContext context)
    {
        _dbContext = context;
        _dbSet = context.Set<TEntity>();
    }

    #region GetFirstOrDefault

    #region Public

    public async Task<TResult?> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
    Expression<Func<TEntity, bool>>? predicate = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        return await GetFirstOrDefaultAsync(selector, predicate, orderBy, include, disableTracking: false);
    }

    public async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        return await GetFirstOrDefaultAsync(predicate, include, disableTracking: false);
    }

    public TResult? GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        return GetFirstOrDefault(selector, predicate, include, disableTracking: false);
    }

    public TEntity? GetFirstOrDefault(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        return GetFirstOrDefault(predicate, include, disableTracking: false);
    }

    public async Task<TResult?> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        return await GetFirstOrDefaultAsync(selector, predicate, include, disableTracking: false);
    }

    #endregion

    #region Private

    private TResult? GetFirstOrDefault<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

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

    private async Task<TResult?> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (include is not null)
        {
            query = include(query);
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return orderBy is not null
            ? await orderBy(query).Select(selector).FirstOrDefaultAsync()
            : await query.Select(selector).FirstOrDefaultAsync();
    }

    #endregion

    #endregion

    #region GetFirstOrDefaultAsync

    #region Private

    private async Task<TResult?> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (include is not null)
        {
            query = include(query);
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return await query.Select(selector).FirstOrDefaultAsync();
    }

    private TEntity? GetFirstOrDefault(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

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

    private async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (include is not null)
        {
            query = include(query);
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return await query.FirstOrDefaultAsync();
    }
    #endregion

    #endregion

    #region GetAll

    #region Public

    public IQueryable<TEntity> GetAll()
    {
        return GetAll(disableTracking: false);
    }

    public IQueryable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector)
    {
        return GetAll(selector, disableTracking: false);
    }

    public IQueryable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null)
    {
        return GetAll(selector, predicate, disableTracking: false);
    }

    public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        return GetAll(predicate, orderBy, include, disableTracking: false);
    }

    public IQueryable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        return GetAll(selector, predicate, orderBy, include, disableTracking: false);
    }

    #endregion

    #region Private

    private IQueryable<TEntity> GetAll(bool disableTracking = true)
    {
        return disableTracking
            ? _dbSet.AsNoTracking()
            : _dbSet;
    }

    private IQueryable<TResult> GetAll<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        bool disableTracking = true)
    {
        return disableTracking
            ? _dbSet.AsNoTracking().Select(selector)
            : _dbSet.Select(selector);
    }

    private IQueryable<TResult> GetAll<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        bool disableTracking = true)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return query.Select(selector);
    }

    private IQueryable<TEntity> GetAll(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

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

    private IQueryable<TResult> GetAll<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

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

    #endregion

    #endregion

    #region GetAllAsync

    #region Public

    public async Task<IList<TEntity>> GetAllAsync()
    {
        return await GetAllAsync(disableTracking: false);
    }

    public async Task<IList<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector)
    {
        return await GetAllAsync(selector, disableTracking: false);
    }

    public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        return await GetAllAsync(predicate, orderBy, include, disableTracking: false);
    }

    public async Task<IList<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        return await GetAllAsync(selector, predicate, orderBy, include, disableTracking: false);
    }

    #endregion

    #region Private

    private async Task<IList<TEntity>> GetAllAsync(bool disableTracking = true)
    {
        return disableTracking
            ? await _dbSet.AsNoTracking().ToListAsync()
            : await _dbSet.ToListAsync();
    }

    private async Task<IList<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        bool disableTracking = true)
    {
        return disableTracking
            ? await _dbSet.AsNoTracking().Select(selector).ToListAsync()
            : await _dbSet.Select(selector).ToListAsync();
    }

    private async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool disableTracking = true)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (include is not null)
        {
            query = include(query);
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return orderBy is not null
            ? await orderBy(query).ToListAsync()
            : await query.ToListAsync();
    }

    private async Task<IList<TResult>> GetAllAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (include is not null)
        {
            query = include(query);
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return orderBy is not null
            ? await orderBy(query).Select(selector).ToListAsync()
            : await query.Select(selector).ToListAsync();
    }

    #endregion

    #endregion

    #region GetAllNonTracking

    #region Public

    public IQueryable<TEntity> GetAllNonTracking()
    {
        return GetAll(disableTracking: true);
    }

    public IQueryable<TResult> GetAllNonTracking<TResult>(Expression<Func<TEntity, TResult>> selector)
    {
        return GetAll(selector, disableTracking: true);
    }

    public IQueryable<TResult> GetAllNonTracking<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null)
    {
        return GetAll(selector, predicate, disableTracking: true);
    }

    public IQueryable<TEntity> GetAllNonTracking(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        return GetAll(predicate, orderBy, include, disableTracking: true);
    }

    public IQueryable<TResult> GetAllNonTracking<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        return GetAll(selector, predicate, orderBy, include, disableTracking: true);
    }

    #endregion

    #endregion

    #region GetAllNonTrackingAsync

    public async Task<IList<TEntity>> GetAllNonTrackingAsync()
    {
        return await GetAllAsync(disableTracking: true);
    }

    public async Task<IList<TResult>> GetAllNonTrackingAsync<TResult>(Expression<Func<TEntity, TResult>> selector)
    {
        return await GetAllAsync(selector, disableTracking: true);
    }

    public async Task<IList<TEntity>> GetAllNonTrackingAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        return await GetAllAsync(predicate, orderBy, include, disableTracking: true);
    }

    public async Task<IList<TResult>> GetAllNonTrackingAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        return await GetAllAsync(selector, predicate, orderBy, include, disableTracking: true);
    }

    #endregion

    #region Insert

    #region Public

    TEntity IRepository<TEntity>.Insert(TEntity entity)
    {
        return Insert(entity);
    }

    void IRepository<TEntity>.Insert(IEnumerable<TEntity> entities)
    {
        Insert(entities);
    }

    #endregion

    #region Private

    private TEntity Insert(TEntity entity)
    {
        return _dbSet.Add(entity).Entity;
    }

    private void Insert(IEnumerable<TEntity> entities)
    {
        _dbSet.AddRange(entities);
    }

    #endregion

    #endregion

    #region InsertAsync

    #region Public

    async ValueTask<EntityEntry<TEntity>> IRepository<TEntity>.InsertAsync(TEntity entity,
    CancellationToken cancellationToken)
    {
        return await InsertAsync(entity, cancellationToken);
    }

    async Task IRepository<TEntity>.InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await InsertAsync(entities, cancellationToken);
    }

    #endregion

    #region Private

    private ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return _dbSet.AddAsync(entity, cancellationToken);
    }

    private Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        return _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    #endregion

    #endregion

    #region Find

    #region Public

    public TEntity? Find(params object[] keyValues)
    {
        return Find(disableTracking: false, keyValues);
    }

    #endregion


    #region Private

    private TEntity? Find(bool disableTracking = true, params object[] keyValues)
    {
        if (disableTracking)
        {
            TEntity? entity = _dbSet.Find(keyValues);
            if (entity is null)
            {
                return null;
            }
            _dbSet.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        else
        {
            return _dbSet.Find(keyValues);
        }
    }

    #endregion

    #endregion

    #region FindAsync

    #region Public

    public async ValueTask<TEntity?> FindAsync(params object[] keyValues)
    {
        return await FindAsync(disableTracking: false, keyValues);
    }

    async ValueTask<TEntity?> IRepository<TEntity>.FindAsync(object[] keyValues, CancellationToken cancellationToken)
    {
        return await FindAsync(disableTracking: false, keyValues, cancellationToken);
    }

    #endregion

    #region Private

    private async ValueTask<TEntity?> FindAsync(bool disableTracking = true, params object[] keyValues)
    {
        if (disableTracking)
        {
            TEntity? entity = await _dbSet.FindAsync(keyValues);
            if (entity is null)
            {
                return null;
            }
            _dbSet.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        else
        {
            return await _dbSet.FindAsync(keyValues);
        }
    }

    private async ValueTask<TEntity?> FindAsync(object[] keyValues, bool disableTracking = true, CancellationToken cancellationToken = default)
    {
        if (disableTracking)
        {
            TEntity? entity = await _dbSet.FindAsync(keyValues, cancellationToken);
            if (entity is null)
            {
                return null;
            }
            _dbSet.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        else
        {
            return await _dbSet.FindAsync(keyValues, cancellationToken);
        }
    }

    #endregion

    #endregion

    #region FindNonTracking

    public TEntity? FindNonTracking(params object[] keyValues)
    {
        return Find(disableTracking: true, keyValues);
    }

    #endregion

    #region FindNonTrackingAsync

    public async ValueTask<TEntity?> FindNonTrackingAsync(params object[] keyValues)
    {
        return await FindAsync(disableTracking: true, keyValues);
    }

    public async ValueTask<TEntity?> FindNonTrackingAsync(object[] keyValues,
        CancellationToken cancellationToken = default)
    {
        return await FindAsync(disableTracking: true, keyValues, cancellationToken);
    }

    #endregion

    #region Update

    #region Public

    void IRepository<TEntity>.Update(TEntity entity)
    {
        Update(entity);
    }

    void IRepository<TEntity>.Update(IEnumerable<TEntity> entities)
    {
        Update(entities);
    }

    #endregion

    #region Private

    private void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    private void Update(IEnumerable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    #endregion

    #endregion

    #region Delete

    #region Public

    void IRepository<TEntity>.Delete(TEntity entity)
    {
        Delete(entity);
    }

    void IRepository<TEntity>.Delete(IEnumerable<TEntity> entities)
    {
        Delete(entities);
    }

    #endregion

    #region Private
    private void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    private void Delete(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    #endregion

    #endregion

    #region GetFirstOrDefaultNonTrackingAsync

    public async Task<TResult?> GetFirstOrDefaultNonTrackingAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        return await GetFirstOrDefaultAsync(selector, predicate, orderBy, include, disableTracking: true);
    }

    public async Task<TEntity?> GetFirstOrDefaultNonTrackingAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        return await GetFirstOrDefaultAsync(predicate, include, disableTracking: true);
    }
    public async Task<TResult?> GetFirstOrDefaultNonTrackingAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        return await GetFirstOrDefaultAsync(selector, predicate, include, disableTracking: true);
    }
    #endregion

    #region GetFirstOrDefaultNonTracking

    public TResult? GetFirstOrDefaultNonTracking<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        return GetFirstOrDefault(selector, predicate, include, disableTracking: true);
    }

    public TEntity? GetFirstOrDefaultNonTracking(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        return GetFirstOrDefault(predicate, include, disableTracking: true);
    }

    #endregion

    #region Aggregates

    #region Public

    int IReadOnlyRepository<TEntity>.Count(Expression<Func<TEntity, bool>>? predicate)
    {
        return Count(predicate);
    }

    async Task<int> IReadOnlyRepository<TEntity>.CountAsync(Expression<Func<TEntity, bool>>? predicate,
        CancellationToken cancellationToken)
    {
        return await CountAsync(predicate, cancellationToken);
    }

    long IReadOnlyRepository<TEntity>.LongCount(Expression<Func<TEntity, bool>>? predicate)
    {
        return LongCount(predicate);
    }

    async Task<long> IReadOnlyRepository<TEntity>.LongCountAsync(Expression<Func<TEntity, bool>>? predicate,
        CancellationToken cancellationToken)
    {
        return await LongCountAsync(predicate, cancellationToken);
    }

    bool IReadOnlyRepository<TEntity>.Exists(Expression<Func<TEntity, bool>>? predicate)
    {
        return Exists(predicate);
    }

    async Task<bool> IReadOnlyRepository<TEntity>.ExistsAsync(Expression<Func<TEntity, bool>>? selector,
        CancellationToken cancellationToken)
    {
        return await ExistsAsync(selector, cancellationToken);
    }

    #endregion

    #region Private

    private int Count(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return predicate is null
            ? _dbSet.Count()
            : _dbSet.Count(predicate);
    }

    private async Task<int> CountAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        return predicate is null
            ? await _dbSet.CountAsync(cancellationToken)
            : await _dbSet.CountAsync(predicate, cancellationToken);
    }

    private long LongCount(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return predicate is null
            ? _dbSet.LongCount()
            : _dbSet.LongCount(predicate);
    }

    private async Task<long> LongCountAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        return predicate is null
            ? await _dbSet.LongCountAsync(cancellationToken)
            : await _dbSet.LongCountAsync(predicate, cancellationToken);
    }

    private bool Exists(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return predicate is null
            ? _dbSet.Any()
            : _dbSet.Any(predicate);
    }

    private async Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>>? selector = null,
        CancellationToken cancellationToken = default)
    {
        return selector is null
            ? await _dbSet.AnyAsync(cancellationToken)
            : await _dbSet.AnyAsync(selector, cancellationToken);
    }

    #endregion

    #endregion

    #region SaveChanges

    #region Public

    void IReadOnlyRepository<TEntity>.SaveChanges()
    {
        SaveChanges();
    }

    async Task IReadOnlyRepository<TEntity>.SaveChangesAsync()
    {
        await SaveChangesAsync();
    }

    #endregion

    #region Private

    private void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    private async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    #endregion

    #endregion

    #region Dispose

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
    }

    #endregion
}