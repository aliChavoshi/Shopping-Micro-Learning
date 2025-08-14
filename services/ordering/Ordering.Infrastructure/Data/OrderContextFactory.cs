using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Ordering.Infrastructure.Data;

public class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext>
{
    //Migrations
    public OrderContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();
        //appSettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var connectionString = configuration.GetConnectionString("OrderingConnectionString");
        optionsBuilder.UseSqlServer(connectionString);
        //Seed Data EF Core 9
        // optionsBuilder.UseAsyncSeeding(async (context, _, token) =>
        // {
        //     await OrderSeedData.SeedAsync(context);
        // });
        return new OrderContext(optionsBuilder.Options);
    }
}