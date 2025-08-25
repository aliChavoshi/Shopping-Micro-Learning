using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace Ordering.Api.Extensions;

public static class DbPollyExtension
{
    public static async Task<IHost> MigrationDatabase<TContext>(
        this IHost host,
        Func<TContext, IServiceProvider, Task> seeder) // 👈 بهتره Action رو هم async کنید
        where TContext : DbContext
    
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<TContext>>();
        var context = services.GetService<TContext>();

        try
        {
            logger.LogInformation($"Started DB Migration : {typeof(TContext).Name}");

            var retry = Policy.Handle<SqlException>()
                .WaitAndRetryAsync(
                    retryCount: 2,
                    sleepDurationProvider: retryAttempt =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, timeSpan) =>
                    {
                        logger.LogError($"Retrying because of {exception} {timeSpan}");
                    });

            await retry.ExecuteAsync(async () =>
            {
                if (context != null)
                {
                    if (host.Services.GetRequiredService<IWebHostEnvironment>().IsDevelopment())
                    {
                        await context.Database.EnsureDeletedAsync();
                    }
                    await context.Database.MigrateAsync();
                    await seeder(context, services);
                }
            });

            logger.LogInformation($"Migration was completed :{typeof(TContext).Name}");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"An Error occured while migration on DB : {typeof(TContext).Name}");
        }

        return host;
    }
}