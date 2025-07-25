using AutoMapper;
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

public class CreateBasketCommandHandler(IBasketRepository basketRepository, IMapper mapper)
    : IRequestHandler<CreateBasketCommand, ShoppingCartResponse>
{
    public async Task<ShoppingCartResponse> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
    {
        var shoppingCart = mapper.Map<ShoppingCart>(request);
        await basketRepository.UpdateBasket(shoppingCart);
        return mapper.Map<ShoppingCartResponse>(shoppingCart);
    }
}