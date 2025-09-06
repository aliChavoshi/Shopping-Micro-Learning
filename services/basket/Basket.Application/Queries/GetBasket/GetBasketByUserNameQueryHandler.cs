﻿using AutoMapper;
using Basket.Application.Responses;
using Basket.Core.Repository;
using MediatR;

namespace Basket.Application.Queries.GetBasket;

public class GetBasketByUserNameQuery(string userName) : IRequest<ShoppingCartResponse>
{
    public string UserName { get; set; } = userName;
}

public class GetBasketByUserNameQueryHandler(IBasketRepository basketRepository, IMapper mapper)
    : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
{
    public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request,
        CancellationToken cancellationToken)
    {
        var basket = await basketRepository.GetBasket(request.UserName);
        if (basket != null)
        {
            var response = mapper.Map<ShoppingCartResponse>(basket);
            response.TotalPrice = response.CalculateOriginalPrice();
            return response;
        }
        return new ShoppingCartResponse(request.UserName);
    }
}