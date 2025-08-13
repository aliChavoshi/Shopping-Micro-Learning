namespace Ordering.Core.Entities;

public enum PaymentMethodEnum
{
    Cash = 1,           // پرداخت نقدی
    CreditCard = 2,     // کارت اعتباری یا بانکی
    BankTransfer = 3,   // انتقال بانکی
    PayPal = 4,         // پرداخت از طریق PayPal
    OnlineGateway = 5,  // درگاه پرداخت اینترنتی
    Crypto = 6          // ارز دیجیتال
}