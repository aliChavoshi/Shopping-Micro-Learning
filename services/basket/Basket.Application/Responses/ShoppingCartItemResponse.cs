namespace Basket.Application.Responses;

public class ShoppingCartItemResponse
{
    public int Quantity { get; set; } = 1;
    public string? ProductId { get; set; }
    public string? ImageFile { get; set; }
    public string? ProductName { get; set; }
    public decimal Price { get; set; }
}