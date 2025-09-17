namespace EventBus.Messages.Events.CheckoutV2;

public class BasketCheckoutEventV2 : BaseIntegrationEvent
{
    public string UserName { get; set; }
    public decimal TotalPrice { get; set; }
}