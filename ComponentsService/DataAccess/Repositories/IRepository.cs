using System.Linq.Expressions;
using ComputerStore.Services.CS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace ComputerStore.Services.CS.DataAccess.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetAsync(Guid id);
    Task<IEnumerable<TEntity>> GetByExpression(Expression<Func<TEntity, bool>>? predicate = null);
    Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
    Task<TEntity> CreateAsync(TEntity item);
    Task<TEntity> UpdateAsync(TEntity item);
    Task DeleteAsync(TEntity item);
}
