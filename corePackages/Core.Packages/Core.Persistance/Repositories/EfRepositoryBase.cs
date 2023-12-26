using Core.Persistance.Dynamic;
using Core.Persistance.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Persistance.Repositories;

public class EfRepositoryBase<TEntity, TEntityId, TContext> : IAsyncRepository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : unmanaged
    where TContext : DbContext
{
    protected TContext _context { get; }

    public EfRepositoryBase(TContext context)
    {
        _context = context;
    }

    public Task<TEntity> AddAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entity, bool permanent = false)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? iclude = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Paginate<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Paginate<TEntity>> GetListByDynamicAsync(DynamicQuery dynamic, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TEntity> Query()
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entity)
    {
        throw new NotImplementedException();
    }
}
