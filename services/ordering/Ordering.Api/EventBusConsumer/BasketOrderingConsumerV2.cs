using Common.Logging.Correlations;
using EventBus.Messages.Events.CheckoutV2;
using MapsterMapper;
using MassTransit;
using MediatR;
using Ordering.Application.Features.Command.CheckoutOrder;
using Ordering.Application.Features.Command.CheckoutOrderV2;

namespace Ordering.Api.EventBusConsumer;

public class BasketOrderingConsumerV2(
    IMediator mediator,
    IMapper mapper,
    ILogger<BasketOrderingConsumerV2> logger,
    ICorrelationIdGenerator correlation) : IConsumer<BasketCheckoutEventV2>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEventV2> context)
    {
        logger.LogInformation("Consuming event V2 : {EventId} {Event}", context.MessageId, context.Message);
        logger.LogInformation("CorrelationId V2 {correlationId}", correlation.Get());
        var command = mapper.Map<CheckoutOrderCommandV2>(context.Message);
        var result = await mediator.Send(command); //Create Order
        logger.LogInformation("CheckoutOrderCommand Result Order Id: {Result}", result);
    }
}