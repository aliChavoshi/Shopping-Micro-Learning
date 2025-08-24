using FluentValidation.Results;

namespace Ordering.Core.Exceptions;

public class CustomValidationException() : ApplicationException("one or many validation errors were occured")
{
    public Dictionary<string, string[]> Errors { get; set; } = new();

    public CustomValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures.GroupBy(x => x.PropertyName, e => e.ErrorMessage)
            .ToDictionary(k => k.Key, group => group.ToArray());
    }
}