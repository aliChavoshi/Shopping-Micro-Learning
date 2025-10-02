using Microsoft.AspNetCore.Builder;

namespace Common.Logging.Correlations;

public static class AppBuilderExtension
{
    public static IApplicationBuilder AddCorrelationIdMiddleware(this IApplicationBuilder app) =>
        app.UseMiddleware<CorrelationMiddleware>();
}