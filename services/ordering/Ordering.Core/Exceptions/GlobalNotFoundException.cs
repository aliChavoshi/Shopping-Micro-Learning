namespace Ordering.Core.Exceptions;

public class GlobalNotFoundException(string name, object key)
    : ApplicationException($"Entity {name} {key} was not found");