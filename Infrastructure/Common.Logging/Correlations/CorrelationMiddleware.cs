using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Common.Logging.Correlations;

public class CorrelationMiddleware(RequestDelegate next)
{
    private static readonly string CorrelationIdHeader = CorrelationCommon.CorrelationIdHeader;

    public async Task Invoke(HttpContext context, ICorrelationIdGenerator correlationIdGenerator)
    {
        var correlationId = GetCorrelationId(context, correlationIdGenerator);
        AddCorrelationHeader(context, correlationId);
        await next(context); //continue
    }

    private static void AddCorrelationHeader(HttpContext context, StringValues correlationId)
    {
        context.Response.OnStarting(() =>
        {
            context.Response.Headers[CorrelationIdHeader] = correlationId;
            return Task.CompletedTask;
        });
    }

    private static StringValues GetCorrelationId(HttpContext context, ICorrelationIdGenerator correlationIdGenerator)
    {
        if (context.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId))
        {
            correlationIdGenerator.Set(correlationId!);
            return correlationId;
        }

        return correlationIdGenerator.Get(); //Exist | NEW
    }
}