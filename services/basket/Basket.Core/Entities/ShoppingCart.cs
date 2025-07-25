namespace Basket.Core.Entities;

public class ShoppingCart(string username)
{
    public Guid Guid { get; set; } = Guid.NewGuid();
    public string UserName { get; set; } = username;
    public List<ShoppingCartItem> Items { get; set; } = [];

    public decimal CalculateOriginalPrice()
    {
        return Items.Sum(x => x.Quantity * x.Price);
    }
}