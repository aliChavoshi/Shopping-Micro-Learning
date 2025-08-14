using Microsoft.EntityFrameworkCore;
using Ordering.Core.Common;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Data;

public class OrderContext(DbContextOptions<OrderContext> options) : DbContext(options)
{
    public DbSet<Order> Orders => Set<Order>();

    //Instead of this use interceptors
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.Entity.DateModified = DateTime.Now;
                    entry.Entity.ModifiedBy = "Ali Chavoshi"; //TODO
                    entry.Entity.Version += 1;
                    break;
                case EntityState.Added:
                    entry.Entity.DateModified = DateTime.Now;
                    entry.Entity.ModifiedBy = "Ali Chavoshi"; //TODO
                    entry.Entity.CreatedBy = "Ali Chavoshi"; //TODO
                    entry.Entity.DateCreated = DateTime.Now;
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}