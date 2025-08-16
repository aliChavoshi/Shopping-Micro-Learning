namespace Ordering.Core.Exceptions;

public class OrderNotFound : ApplicationException
{
    public OrderNotFound(string name,object key) : base($"Entity {name} {key} was not found")
    {
        
    }
}