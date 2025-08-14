using Ordering.Core.Entities;

namespace Ordering.Application.Responses;

public class OrderResponse
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public decimal? TotalPrice { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmailAddress { get; set; }
    public string? AddressLine { get; set; }
    public string? State { get; set; }
    public PaymentMethodEnum PaymentMethod { get; set; }
}