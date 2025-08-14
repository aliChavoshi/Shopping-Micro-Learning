using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace Ordering.Infrastructure.Data;

public class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext>
{
    //Migrations
    public OrderContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();
        //1. find appSetting.json // TODO
        optionsBuilder.UseSqlServer("Data Source=OrderDb"); //TODO
        //Seed Data EF Core 9
        // optionsBuilder.UseAsyncSeeding(async (context, _, token) =>
        // {
        //     await OrderSeedData.SeedAsync(context);
        // });
        return new OrderContext(optionsBuilder.Options);
    }
}