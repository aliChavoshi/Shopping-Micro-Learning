using AutoMapper;
using Basket.Application.Commands.CreateBasket;
using Basket.Application.Responses;
using Basket.Core.Entities;

namespace Basket.Application.Mapper;

public class ProfileMapper : Profile
{
    public ProfileMapper()
    {
        CreateMap<ShoppingCartItem, ShoppingCartItemResponse>().ReverseMap();
        CreateMap<ShoppingCart, ShoppingCartResponse>().ReverseMap();
        CreateMap<CreateBasketCommand, ShoppingCart>().ReverseMap();
    }
}