using MediatR;
using Microsoft.Extensions.Logging;

namespace Ordering.Application.Behavior;

public class UnHandledExceptionBehavior<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next(cancellationToken);
        }
        catch (Exception e)
        {
            var requestName = typeof(TRequest).Name;
            logger.LogError(e,$"Unhandled exception occured with TRequest Name : {requestName}");
            throw;
        }
    }
}