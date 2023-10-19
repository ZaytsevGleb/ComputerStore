using DataAccess.Context;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DataAccess.Repositories;

internal sealed class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationDbContext _db;
    private readonly DbSet<TEntity> _entities;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        _entities = _db.Set<TEntity>();
    }

    public async Task<TEntity?> GetAsync(Guid id)
    {
        return await _entities
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<TEntity>> GetByExpression(Expression<Func<TEntity, bool>>? predicate = null)
    {
        var dbQuery = _entities
            .AsNoTracking()
            .AsQueryable();

        if (predicate != null)
        {
            dbQuery = dbQuery.Where(predicate);
        }

        return await dbQuery.ToListAsync();
    }

    public async Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>> predicate,
    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        var dbQuery = _entities
            .AsNoTracking()
            .AsQueryable();

        if (include != null)
        {
            dbQuery = include.Invoke(dbQuery);
        }

        return await dbQuery
            .FirstOrDefaultAsync();
    }

    public async Task<TEntity> CreateAsync(TEntity item)
    {
        _entities.Add(item);
        await _db.SaveChangesAsync();

        return item;
    }

    public async Task<TEntity> UpdateAsync(TEntity item)
    {
        _entities.Update(item);
        await _db.SaveChangesAsync();
        return item;
    }

    public async Task DeleteAsync(TEntity item)
    {
        _entities.Remove(item);
        await _db.SaveChangesAsync();
    }
}
