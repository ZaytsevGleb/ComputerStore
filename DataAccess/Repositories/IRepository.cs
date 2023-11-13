using DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DataAccess.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetAsync(Guid id, CancellationToken ct);
    Task<IEnumerable<TEntity>> GetByExpression(Expression<Func<TEntity, bool>>? predicate, CancellationToken ct);
    Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include, CancellationToken ct);
    Task<TEntity> CreateAsync(TEntity item, CancellationToken ct);
    Task<TEntity> UpdateAsync(TEntity item, CancellationToken ct);
    Task DeleteAsync(TEntity item, CancellationToken ct);
}
