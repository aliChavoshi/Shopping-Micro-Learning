namespace Ordering.Core.Exceptions;

public class OrderNotFoundException(string name, object key)
    : ApplicationException($"Entity {name} {key} was not found");