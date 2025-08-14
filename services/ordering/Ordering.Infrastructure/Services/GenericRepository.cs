using System.Linq.Expressions;
using Ordering.Core.Common;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Services;

public class GenericRepository<T>(OrderContext context) : IGenericRepository<T> where T : BaseEntity
{
    public Task<IReadOnlyList<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<T> AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
    {
        throw new NotImplementedException();
    }
}