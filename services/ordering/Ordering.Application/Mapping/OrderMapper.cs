using Mapster;
using Ordering.Application.Responses;
using Ordering.Core.Entities;

namespace Ordering.Application.Mapping;

public class OrderMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OrderResponse, Order>();
        config.NewConfig<Order, OrderResponse>();
    }
}