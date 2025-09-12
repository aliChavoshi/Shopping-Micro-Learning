using Microsoft.AspNetCore.Http;

namespace Common.Logging.Correlations;

public class CorrelationIdGenerator(IHttpContextAccessor accessor) : ICorrelationIdGenerator
{
    private const string HeaderName = "X-Correlation-Id";
    private string? _cachedCorrelationId;

    public string? Get()
    {
        //EXIST
        if (_cachedCorrelationId != null)
            return _cachedCorrelationId;
        var context = accessor.HttpContext;
        if (context != null && context.Request.Headers.TryGetValue(HeaderName, out var correlationId))
        {
            _cachedCorrelationId = correlationId;
            return _cachedCorrelationId;
        }

        //NEW
        _cachedCorrelationId = Guid.NewGuid().ToString("D");
        if (context != null)
        {
            context.Request.Headers[HeaderName] = _cachedCorrelationId;
        }

        return _cachedCorrelationId;
    }

    public void Set(string correlationId)
    {
        //SET
        _cachedCorrelationId = correlationId;
        if (accessor.HttpContext != null) accessor.HttpContext.Request.Headers[HeaderName] = _cachedCorrelationId;
    }
}