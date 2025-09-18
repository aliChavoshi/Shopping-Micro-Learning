using MediatR;

namespace Ordering.Application.Features.Command.CheckoutOrderV2;

public class CheckoutOrderCommandV2 : IRequest<int>
{
    public string UserName { get; set; }
    public decimal TotalPrice { get; set; }
}