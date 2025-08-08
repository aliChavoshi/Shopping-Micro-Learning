using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.Infrastructure.Extensions;

public static class DbExtension
{
    public static IHost MigrateDatabase<TContext>(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var logger = serviceProvider.GetRequiredService<ILogger<TContext>>();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        try
        {
            logger.LogInformation("Start Migration on {DbContextName}", typeof(TContext).Name);
            ApplyMigrateOnDb<TContext>(configuration, logger);
            logger.LogInformation("Database was migrated");
        }
        catch (Exception e)
        {
            logger.LogError(e, "an error occured in the database {DbContextName}", typeof(TContext).Name);
            throw;
        }

        return host;
    }

    private static void ApplyMigrateOnDb<TContext>(IConfiguration configuration, ILogger logger)
    {
        var retry = 5;
        while (retry > 0)
        {
            try
            {
                using var connection =
                    new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                connection.Open();
                using var cmd = new NpgsqlCommand();
                cmd.Connection = connection;
                //Drop Table
                cmd.CommandText = "DROP TABLE IF EXISTS Coupon";
                cmd.ExecuteNonQuery();
                //Create Table
                cmd.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                    ProductId TEXT,
                                                    ProductName VARCHAR(500) NOT NULL,
                                                    Description TEXT,
                                                    Amount INT)";
                cmd.ExecuteNonQuery();
                //Seed Data 1
                cmd.CommandText =
                    "INSERT INTO Coupon(ProductName, Description, Amount, ProductId) VALUES('Adidas Quick Force Indoor Badminton Shoes', 'Shoe Discount', 500, '602d2149e773f2a3990b47f5');";
                cmd.ExecuteNonQuery();
                //Seed Data 2
                cmd.CommandText =
                    "INSERT INTO Coupon(ProductName, Description, Amount, ProductId) VALUES('Yonex VCORE Pro 100 A Tennis Racquet (270gm, Strung)', 'Racquet Discount', 700, '992d2149e773f2a3990b47fa');";
                cmd.ExecuteNonQuery();
                break;
            }
            catch (Exception e)
            {
                retry--;
                if (retry == 0)
                {
                    throw;
                }

                logger.LogWarning(e, "Retrying database migration, attempts left: {Retry}", retry);

                Thread.Sleep(2000);
            }
        }
    }
}