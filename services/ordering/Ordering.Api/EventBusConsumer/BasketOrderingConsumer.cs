using Common.Logging.Correlations;
using EventBus.Messages.Events;
using MapsterMapper;
using MassTransit;
using MediatR;
using Ordering.Application.Features.Command.CheckoutOrder;

namespace Ordering.Api.EventBusConsumer;

public class BasketOrderingConsumer(
    IMediator mediator,
    IMapper mapper,
    ILogger<BasketOrderingConsumer> logger,
    ICorrelationIdGenerator correlation)
    : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        logger.LogInformation("Consuming event : {EventId} {Event}", context.MessageId, context.Message);
        logger.LogInformation("CorrelationId {correlationId}", correlation.Get());
        var command = mapper.Map<CheckoutOrderCommand>(context.Message);
        var result = await mediator.Send(command); //Create Order
        logger.LogInformation("CheckoutOrderCommand Result Order Id: {Result}", result);
    }
}