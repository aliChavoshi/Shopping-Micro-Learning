using Discount.Core.Entities;

namespace Discount.Core.Interfaces;

public interface IDiscountRepository
{
    Task<Coupon> GetDiscount(string productId);
    Task<Coupon> GetDiscountByName(string productName);
    Task<bool> CreateDiscount(Coupon coupon);
    Task<bool> UpdateDiscount(Coupon coupon);
    Task<bool> DeleteDiscount(string productId);
    Task<bool> DeleteDiscountByName(string productName);
}