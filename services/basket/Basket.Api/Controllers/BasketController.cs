using AutoMapper;
using Basket.Application.Commands.CreateBasket;
using Basket.Application.Commands.DeleteBasket;
using Basket.Application.Queries.GetBasket;
using Basket.Application.Responses;
using Basket.Core.Entities;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers;

public class BasketController(
    IMediator mediator,
    IPublishEndpoint publishEndpoint,
    IMapper mapper,
    ILogger<BasketController> logger) : ApiController
{
    [HttpGet("{userName}")]
    public async Task<ActionResult<ShoppingCartResponse>> GetBasketByUserName(string userName,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetBasketByUserNameQuery(userName), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ShoppingCartResponse>> CreateBasket([FromBody] CreateBasketCommand request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{userName}")]
    public async Task<ActionResult<bool>> DeleteBasket(string userName, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new DeleteBasketCommand(userName), cancellationToken));
    }

    //RabbiMQ
    [HttpPost]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckout checkout)
    {
        var query = new GetBasketByUserNameQuery(checkout!.UserName!);
        var basket = await mediator.Send(query);
        //Publish Message in the RabbitMQ => Order Consumer
        var eventMsg = mapper.Map<BasketCheckoutEvent>(checkout);
        eventMsg.TotalPrice = basket.TotalPrice;
        await publishEndpoint.Publish(eventMsg);
        logger.LogInformation("BasketCheckoutEvent {EventId} {DateTime} {UserName}", eventMsg.CorrelationId,
            eventMsg.CreationDate, eventMsg.UserName);
        //Remove Basket
        var deleteCommand = new DeleteBasketCommand(checkout.UserName!);
        await mediator.Send(deleteCommand);
        //202
        return Accepted();
    }
}