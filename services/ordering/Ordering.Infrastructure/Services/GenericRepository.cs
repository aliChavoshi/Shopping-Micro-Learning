using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ordering.Core.Common;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Services;

public class GenericRepository<T>(OrderContext context) : IGenericRepository<T> where T : BaseEntity
{
    public IQueryable<T> GetAllAsync()
    {
        return context.Set<T>().AsQueryable();
    }

    public IQueryable<T> GetAllAsync(Expression<Func<T, bool>> expression)
    {
        return context.Set<T>().Where(expression);
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        context.Set<T>().Add(entity);
        await SaveChangeAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        await SaveChangeAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        context.Set<T>().Remove(entity);
        await SaveChangeAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
    {
        return await context.Set<T>().AnyAsync(expression);
    }

    public async Task<bool> SaveChangeAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }
}