using Basket.Application.Commands.CreateBasket;
using Basket.Application.Commands.DeleteBasket;
using Basket.Application.Queries.GetBasket;
using Basket.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers;

public class BasketController(IMediator mediator) : ApiController
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
}