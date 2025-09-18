using Asp.Versioning;
using AutoMapper;
using Basket.Application.Commands.DeleteBasket;
using Basket.Application.Queries.GetBasket;
using Basket.Core.Entities;
using EventBus.Messages.Events.CheckoutV2;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers.V2;

[ApiVersion("2.0")]
public class BasketController(
    IMediator mediator,
    IMapper mapper,
    IPublishEndpoint publishEndpoint,
    ILogger<BasketController> logger) : ApiController
{
    //RabbitMQ
    [HttpPost]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckoutV2 checkout)
    {
        var query = new GetBasketByUserNameQuery(checkout!.UserName!);
        var basket = await mediator.Send(query);
        //Publish Message in the RabbitMQ => Order Consumer
        var eventMsg = mapper.Map<BasketCheckoutEventV2>(checkout);
        eventMsg.TotalPrice = basket.TotalPrice;
        await publishEndpoint.Publish(eventMsg);
        logger.LogInformation("BasketCheckoutEventV2 BasketControllerV2 {DateTime} {UserName}", eventMsg.CreationDate, eventMsg.UserName);
        //Remove Basket
        var deleteCommand = new DeleteBasketCommand(checkout.UserName!);
        await mediator.Send(deleteCommand);
        //202
        return Accepted();
    }
}