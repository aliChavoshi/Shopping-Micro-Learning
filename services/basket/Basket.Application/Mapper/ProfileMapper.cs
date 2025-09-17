using AutoMapper;
using Basket.Application.Commands.CreateBasket;
using Basket.Application.Responses;
using Basket.Core.Entities;
using EventBus.Messages.Events;
using EventBus.Messages.Events.CheckoutV2;

namespace Basket.Application.Mapper;

public class ProfileMapper : Profile
{
    public ProfileMapper()
    {
        CreateMap<ShoppingCartItem, ShoppingCartItemResponse>().ReverseMap();
        CreateMap<ShoppingCart, ShoppingCartResponse>().ReverseMap();
        CreateMap<CreateBasketCommand, ShoppingCart>().ReverseMap();
        CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
        CreateMap<BasketCheckoutEventV2, BasketCheckoutV2>().ReverseMap();
    }
}