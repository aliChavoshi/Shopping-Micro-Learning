using Asp.Versioning;
using AutoMapper;
using Basket.Application.Commands.CreateBasket;
using Basket.Application.Commands.DeleteBasket;
using Basket.Application.Queries.GetBasket;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Common.Logging.Correlations;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers.V1;

[ApiVersion("1.0")]
// [ApiVersion("2")]
public class BasketController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;
    private readonly ILogger<BasketController> _logger;
    private readonly ICorrelationIdGenerator _correlation;

    public BasketController(IMediator mediator,
        IPublishEndpoint publishEndpoint,
        IMapper mapper,
        ILogger<BasketController> logger,ICorrelationIdGenerator correlation)
    {
        _mediator = mediator;
        _publishEndpoint = publishEndpoint;
        _mapper = mapper;
        _logger = logger;
        _correlation = correlation;
        _logger.LogInformation("CorrelationId {correlationId}", correlation.Get());
    }

    [HttpGet("{userName}")]
    // [MapToApiVersion("1")]
    public async Task<ActionResult<ShoppingCartResponse>> GetBasketByUserName(string userName,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetBasketByUserNameQuery(userName), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    // [MapToApiVersion("2")]
    public async Task<ActionResult<ShoppingCartResponse>> CreateBasket([FromBody] CreateBasketCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{userName}")]
    public async Task<ActionResult<bool>> DeleteBasket(string userName, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DeleteBasketCommand(userName), cancellationToken));
    }

    //RabbitMQ
    [HttpPost]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckout checkout)
    {
        var query = new GetBasketByUserNameQuery(checkout!.UserName!);
        var basket = await _mediator.Send(query);
        //Publish Message in the RabbitMQ => Order Consumer
        var eventMsg = _mapper.Map<BasketCheckoutEvent>(checkout);
        eventMsg.TotalPrice = basket.TotalPrice;
        await _publishEndpoint.Publish(eventMsg);
        _logger.LogInformation("BasketCheckoutEvent BasketController {EventId} {DateTime} {UserName}", _correlation.Get(),
            eventMsg.CreationDate, eventMsg.UserName);
        //Remove Basket
        var deleteCommand = new DeleteBasketCommand(checkout.UserName!);
        await _mediator.Send(deleteCommand);
        //202
        return Accepted();
    }
}