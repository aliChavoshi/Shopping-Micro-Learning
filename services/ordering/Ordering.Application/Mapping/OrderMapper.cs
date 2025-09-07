using EventBus.Messages.Events;
using Mapster;
using Ordering.Application.Features.Command.CheckoutOrder;
using Ordering.Application.Features.Command.UpdateOrder;
using Ordering.Application.Responses;
using Ordering.Core.Entities;

namespace Ordering.Application.Mapping;

public class OrderMapper : IRegister
{
    //IMapper
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OrderResponse, Order>();
        config.NewConfig<CheckoutOrderCommand, Order>();
        config.NewConfig<Order, OrderResponse>();
        config.NewConfig<UpdateOrderCommand, Order>();
        config.NewConfig<BasketCheckoutEvent, CheckoutOrderCommand>(); //RabbitMQ
    }
}