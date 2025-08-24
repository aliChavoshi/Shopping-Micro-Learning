using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace Ordering.Api.Extensions;

public static class DbPollyExtension
{
    public static IHost MigrationDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder)
        where TContext : DbContext
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<TContext>>();
        var context = services.GetService<TContext>();

        try
        {
            logger.LogInformation($"Started DB Migration : {typeof(TContext).Name}");
            //retry
            var retry = Policy.Handle<SqlException>()
                .WaitAndRetry(
                    retryCount: 5,
                    sleepDurationProvider: retryAttempt =>
                        //2,4,8,16,32
                        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry:
                    (exception, timeSpan) => { logger.LogError($"Retrying because of {exception}{timeSpan}"); });
            //Execute
            retry.Execute(async () => await CallSeeder(seeder, context, services));
            logger.LogInformation($"Migration was completed :{typeof(TContext).Name}");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"An Error occured while migration on DB : {typeof(TContext).Name}");
        }

        return host;
    }

    private static async Task CallSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext? context,
        IServiceProvider
            services) where TContext : DbContext
    {
        if (context != null)
        {
            await context.Database.MigrateAsync();
            seeder(context, services);
        }
    }
}