using Discount.Core.Interfaces;
using MediatR;

namespace Discount.Application.Commands;

public class DeleteDiscountByProductIdCommand : IRequest<bool>
{
    public string ProductId { get; set; }

    public DeleteDiscountByProductIdCommand(string productId)
    {
        ProductId = productId;
    }
}

public class DeleteDiscountByProductIdCommandHandler(IDiscountRepository discountRepository)
    : IRequestHandler<DeleteDiscountByProductIdCommand, bool>
{
    public async Task<bool> Handle(DeleteDiscountByProductIdCommand request, CancellationToken cancellationToken)
    {
        return await discountRepository.DeleteDiscount(request.ProductId);
    }
}