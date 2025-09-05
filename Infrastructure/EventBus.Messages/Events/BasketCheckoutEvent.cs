namespace EventBus.Messages.Events;

public class BasketCheckoutEvent : BaseIntegrationEvent
{
    //Identity
    public Guid Guid { get; set; }
    public string? UserName { get; set; }

    //Customer
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmailAddress { get; set; }
    public string? BuyerPhoneNumber { get; set; }

    //Address
    public string? AddressLine { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }

    // Payment
    public decimal TotalPrice { get; set; }
    public int PaymentMethod { get; set; }
    public string? PaymentTrackingCode { get; set; } //bank

    //Shipping
    public string? TrackingCode { get; set; }
    public string? ShippingTrackingCode { get; set; } //post 
}