using DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DataAccess.Repositories;

internal sealed class Repository<T> : IRepository<T> where T : IEntity
{
    public Task<T> CreateAsync(T item)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> FindAsync(Expression<Func<T, bool>>? predicate = null)
    {
        throw new NotImplementedException();
    }

    public Task<T> FirstAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<T> UpdateAsync(T item)
    {
        throw new NotImplementedException();
    }
}
