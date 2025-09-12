using Common.Logging.Correlations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Command.CheckoutOrder;
using Ordering.Application.Features.Command.DeleteOrder;
using Ordering.Application.Features.Command.UpdateOrder;
using Ordering.Application.Features.Queries.GetOrdersByUserName;
using Ordering.Application.Responses;

namespace Ordering.Api.Controllers;

public class OrderController : ApiController
{
    private readonly IMediator _mediator;
    private readonly ILogger<OrderController> _logger;
    private readonly ICorrelationIdGenerator _correlationIdGenerator;


    public OrderController(IMediator mediator, ILogger<OrderController> logger,
        ICorrelationIdGenerator correlationIdGenerator)
    {
        _mediator = mediator;
        _logger = logger;
        _correlationIdGenerator = correlationIdGenerator;
        _logger.LogInformation("CorrelationId {correlationId}", correlationIdGenerator.Get());
    }

    [HttpGet("{userName}")]
    public async Task<ActionResult<List<OrderResponse>>> GetOrdersByUserName(string userName,
        CancellationToken cancellationToken)
    {
        var query = new GetOrdersByUserNameQuery(userName);
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand command,
        CancellationToken cancellationToken)
    {
        var orderId = await _mediator.Send(command, cancellationToken);
        return Ok(orderId);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(); //200
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOrder(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteOrderCommand
        {
            Id = id
        }, cancellationToken);
        return NoContent(); //204
    }
}