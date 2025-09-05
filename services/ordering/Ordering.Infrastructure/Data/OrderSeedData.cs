using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Data;

public class OrderSeedData
{
    public static async Task SeedAsync(OrderContext context, ILogger<OrderSeedData>? logger)
    {
        if (!await context.Orders.AnyAsync())
        {
            var orders = GetOrders();
            context.Orders.AddRange(orders);
            await context.SaveChangesAsync();
            if (logger != null)
                logger.LogInformation("Seed database associated with context {context}", nameof(context));
        }
    }

    private static List<Order> GetOrders()
    {
        var orders = new List<Order>
        {
            new Order
            {
                UserName = "Ali Chavoshi",
                TotalPrice = 125_000,
                FirstName = "Ali",
                LastName = "Chavoshi",
                EmailAddress = "ali@example.com",
                AddressLine = "Tehran, Enghelab St, No. 12",
                State = "Tehran",
                PaymentMethod = PaymentMethodEnum.Cash
            },
            new Order
            {
                UserName = "Ali Chavoshi",
                TotalPrice = 850_000,
                FirstName = "Sara",
                LastName = "Moradi",
                EmailAddress = "sara@example.com",
                AddressLine = "Isfahan, Chaharbagh Abbasi, No. 45",
                State = "Isfahan",
                PaymentMethod = PaymentMethodEnum.Crypto
            },
            new Order
            {
                UserName = "Ali Chavoshi",
                TotalPrice = 500_000,
                FirstName = "Mohammad",
                LastName = "Karimi",
                EmailAddress = "mohammad@example.com",
                AddressLine = "Shiraz, Zand Blvd, No. 88",
                State = "Fars",
                PaymentMethod = PaymentMethodEnum.PayPal
            }
        };
        return orders;
    }
}