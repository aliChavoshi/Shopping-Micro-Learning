using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ordering.Infrastructure.Data;

public class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext>
{
    //Migrations
    public OrderContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();
        //1. find appSetting.json // TODO
        optionsBuilder.UseSqlServer("Data Source=OrderDb"); //TODO
        return new OrderContext(optionsBuilder.Options);
    }
}