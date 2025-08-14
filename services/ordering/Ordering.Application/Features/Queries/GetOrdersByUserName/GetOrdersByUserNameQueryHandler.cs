using MapsterMapper;
using MediatR;
using Ordering.Application.Responses;
using Ordering.Core.Repositories;

namespace Ordering.Application.Features.Queries.GetOrdersByUserName;

public class GetOrdersByUserNameQuery : IRequest<List<OrderResponse>>
{
    public string UserName { get; set; }

    public GetOrdersByUserNameQuery(string userName)
    {
        UserName = userName;
    }
}

public class GetOrdersByUserNameQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    : IRequestHandler<GetOrdersByUserNameQuery, List<OrderResponse>>
{
    public async Task<List<OrderResponse>> Handle(GetOrdersByUserNameQuery request, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetOrdersByUserName(request.UserName);
        return mapper.Map<List<OrderResponse>>(orders);
    }
}