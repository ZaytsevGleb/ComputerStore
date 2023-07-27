using DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DataAccess.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>>? predicate = null);
    Task<TEntity?> GetAsync(Guid id);
    Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
    Task<TEntity> CreateAsync(TEntity item);
    Task<TEntity> UpdateAsync(TEntity item);
    Task DeleteAsync(TEntity item);
}
