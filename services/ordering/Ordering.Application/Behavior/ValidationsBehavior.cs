using FluentValidation;
using MediatR;
using Ordering.Core.Exceptions;

namespace Ordering.Application.Behavior;

public class ValidationsBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) :
    IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any()) return await next(cancellationToken);
        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(validators
            .Select(x => x.ValidateAsync(context, cancellationToken)));
        var failures = validationResults.SelectMany(e => e.Errors)
            .Where(x => x != null).ToList();
        if (failures != null && failures.Count != 0) throw new CustomValidationException(failures);
        return await next(cancellationToken);
    }
}