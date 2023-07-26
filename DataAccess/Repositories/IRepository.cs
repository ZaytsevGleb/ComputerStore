using DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DataAccess.Repositories;

public interface IRepository<T> where T : IEntity
{
    Task<List<T>> FindAsync(Expression<Func<T, bool>>? predicate = null);
    Task<T> GetAsync(Guid id);
    Task<T> FirstAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    Task<T> CreateAsync(T item);
    Task<T> UpdateAsync(T item);
    Task DeleteAsync(Guid id);
}
