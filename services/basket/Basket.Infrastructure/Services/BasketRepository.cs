using Basket.Core.Entities;
using Basket.Core.Repository;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Newtonsoft.Json;

namespace Basket.Infrastructure.Services;

public class BasketRepository(IDistributedCache redis) : IBasketRepository
{
    public async Task<ShoppingCart?> GetBasket(string userName)
    {
        var basket = await redis.GetStringAsync(userName);
        return string.IsNullOrEmpty(basket) ? null : JsonConvert.DeserializeObject<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart?> UpdateBasket(ShoppingCart shoppingCart)
    {
        await redis.SetStringAsync(shoppingCart.UserName, JsonConvert.SerializeObject(shoppingCart));
        return await GetBasket(shoppingCart.UserName);
    }

    public async Task DeleteBasket(string userName)
    {
        await redis.RemoveAsync(userName);
    }
}