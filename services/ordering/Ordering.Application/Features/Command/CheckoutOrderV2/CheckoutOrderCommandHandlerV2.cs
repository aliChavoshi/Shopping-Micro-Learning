using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Features.Command.CheckoutOrderV2;

public class CheckoutOrderCommandHandlerV2(IOrderRepository orderRepository,IMapper mapper,ILogger<CheckoutOrderCommandHandlerV2> logger) : IRequestHandler<CheckoutOrderCommandV2,int>
{
    public async Task<int> Handle(CheckoutOrderCommandV2 request, CancellationToken cancellationToken)
    {
        var order = mapper.Map<Order>(request);
        var result =await orderRepository.AddAsync(order);
        logger.LogInformation("CheckoutOrderCommandHandlerV2 handled Result Order Id {OrderId}",result.Id);
        return result.Id;
    }
}