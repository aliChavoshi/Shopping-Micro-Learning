using Basket.Application.Queries.GetBasket;
using Discount.Application.Protos;

namespace Basket.Application.GrpcService;

public class DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient client)
{
    public async Task<CouponModel> GetDiscountByNameAsync(string productName)
    {
        var request = new GetDiscountRequest()
        {
            ProductName = productName
        };
        return await client.GetDiscountByNameAsync(request);
    }

    public async Task<CouponModel> GetDiscountByProductId(string productId)
    {
        var request = new GetDiscountByIdRequest()
        {
            ProductId = productId
        };
        var result = await client.GetDiscountByIdAsync(request);
        return result;
    }
}