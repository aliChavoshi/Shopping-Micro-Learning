using Common.Logging.Correlations;

namespace Ocelot.ApiGateways.Handlers;

public class CorrelationDelegateHandler(
    ICorrelationIdGenerator correlationIdGenerator,
    ILogger<CorrelationDelegateHandler> logger) : DelegatingHandler
{
    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        logger.LogInformation("CorrelationDelegateHandler in Ocelot is executing");
        var correlationId = correlationIdGenerator.Get();
        if (!request.Headers.Contains(CorrelationCommon.CorrelationIdHeader))
        {
            request.Headers.Add(CorrelationCommon.CorrelationIdHeader, correlationId);
        }

        logger.LogInformation("[Ocelot] correlationId : {correlationId}", correlationId);
        return base.Send(request, cancellationToken);
    }
}