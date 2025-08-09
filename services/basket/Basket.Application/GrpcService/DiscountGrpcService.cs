using Basket.Application.Queries.GetBasket;
using Discount.Application.Protos;

namespace Basket.Application.GrpcService;

public class DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient client)
{
    public async Task<CouponModel> GetDiscountTask(string productName)
    {
        var discountRequest = new GetDiscountRequest()
        {
            ProductName = productName
        };
        return await client.GetDiscountByNameAsync(discountRequest);
    }
}