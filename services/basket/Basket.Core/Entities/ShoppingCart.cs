namespace Basket.Core.Entities;

public class ShoppingCart(string username, string userId)
{
    public Guid Guid { get; set; } = Guid.NewGuid();
    public string UserName { get; set; } = username;
    public string UserId { get; set; } = userId;
    public List<ShoppingCartItem> Items { get; set; } = [];

    public decimal CalculateOriginalPrice()
    {
        return Items.Sum(x => x.Quantity * x.Price);
    }
}