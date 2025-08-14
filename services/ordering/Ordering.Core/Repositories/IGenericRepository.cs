using System.Linq.Expressions;
using Ordering.Core.Common;

namespace Ordering.Core.Repositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    IQueryable<T> GetAllAsync();
    IQueryable<T> GetAllAsync(Expression<Func<T, bool>> expression); //x=>x.title == "name"
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    Task<bool> SaveChangeAsync(CancellationToken cancellationToken = default);
}