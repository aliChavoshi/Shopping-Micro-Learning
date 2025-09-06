using AutoMapper;
using Basket.Application.GrpcService;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Basket.Core.Repository;
using MediatR;

namespace Basket.Application.Commands.CreateBasket;

public class CreateBasketCommand(string userName, List<ShoppingCartItem> items) : IRequest<ShoppingCartResponse>
{
    public string UserName { get; set; } = userName;
    public List<ShoppingCartItem> Items { get; set; } = items;
}

public class CreateBasketCommandHandler(
    IBasketRepository basketRepository,
    IMapper mapper,
    DiscountGrpcService discountGrpcService)
    : IRequestHandler<CreateBasketCommand, ShoppingCartResponse>
{
    public async Task<ShoppingCartResponse> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
    {
        //Add and push Discount By GRPC
        foreach (var product in request.Items)
        {
            var discount = await discountGrpcService.GetDiscountByProductId(product.ProductId!);
            product.Price -= discount.Amount;
        }
        //Create Basket
        var shoppingCart = mapper.Map<ShoppingCart>(request);
        await basketRepository.UpdateBasket(shoppingCart);
        var response = mapper.Map<ShoppingCartResponse>(shoppingCart);
        response.TotalPrice = response.CalculateOriginalPrice();
        return response;
    }
}