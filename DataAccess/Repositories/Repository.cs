﻿using DataAccess.Context;
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

    public async Task<TEntity?> GetAsync(Guid id, CancellationToken ct)
    {
        return await _entities
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<IEnumerable<TEntity>> GetByExpression(Expression<Func<TEntity, bool>>? predicate, CancellationToken ct)
    {
        var dbQuery = _entities
            .AsNoTracking()
            .AsQueryable();

        if (predicate != null)
        {
            dbQuery = dbQuery.Where(predicate);
        }

        return await dbQuery.ToListAsync(ct);
    }

    public async Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>> predicate,
    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include, CancellationToken ct)
    {
        var dbQuery = _entities
            .AsNoTracking()
            .AsQueryable();

        if (include != null)
        {
            dbQuery = include.Invoke(dbQuery);
        }

        return await dbQuery.FirstOrDefaultAsync(ct);
    }

    public async Task<TEntity> CreateAsync(TEntity item, CancellationToken ct)
    {
        _entities.Add(item);
        await _db.SaveChangesAsync(ct);

        return item;
    }

    public async Task<TEntity> UpdateAsync(TEntity item, CancellationToken ct)
    {
        _entities.Update(item);
        await _db.SaveChangesAsync(ct);
        return item;
    }

    public async Task DeleteAsync(TEntity item, CancellationToken ct)
    {
        _entities.Remove(item);
        await _db.SaveChangesAsync(ct);
    }
}
