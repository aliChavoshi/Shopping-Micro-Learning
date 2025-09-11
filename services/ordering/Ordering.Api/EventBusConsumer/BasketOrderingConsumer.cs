using EventBus.Messages.Events;
using MapsterMapper;
using MassTransit;
using MediatR;
using Ordering.Application.Features.Command.CheckoutOrder;

namespace Ordering.Api.EventBusConsumer;

public class BasketOrderingConsumer(IMediator mediator, IMapper mapper, ILogger<BasketOrderingConsumer> logger)
    : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        logger.LogInformation("Consuming event : {EventId} {Event}", context.MessageId, context.Message);
        var command = mapper.Map<CheckoutOrderCommand>(context.Message);
        var result = await mediator.Send(command); //Create Order
        logger.LogInformation("CheckoutOrderCommand Result Order Id: {Result}", result);
    }
}