using MapsterMapper;
using MediatR;
using Ordering.Core.Entities;
using Ordering.Core.Exceptions;
using Ordering.Core.Repositories;

namespace Ordering.Application.Features.Command.UpdateOrder;

public class UpdateOrderCommand : IRequest
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public decimal? TotalPrice { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmailAddress { get; set; }
    public string? AddressLine { get; set; }
    public string? State { get; set; }
    public PaymentMethodEnum PaymentMethod { get; set; }
}

public class UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
    : IRequestHandler<UpdateOrderCommand>
{
    public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = await orderRepository.GetByIdAsync(request.Id);
        if (entity == null) throw new GlobalNotFoundException(nameof(Order), request.Id);
        mapper.Map(request, entity);
        await orderRepository.UpdateAsync(entity);
    }
}