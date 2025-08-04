using AutoMapper;
using Discount.Core.Interfaces;
using MediatR;

namespace Discount.Application.Commands;

public class DeleteDiscountByNameCommand : IRequest<bool>
{
    public string ProductName { get; set; }

    public DeleteDiscountByNameCommand(string productName)
    {
        ProductName = productName;
    }
}

public class DeleteDiscountByNameCommandHandler(IDiscountRepository discountRepository)
    : IRequestHandler<DeleteDiscountByNameCommand, bool>
{
    public async Task<bool> Handle(DeleteDiscountByNameCommand request, CancellationToken cancellationToken)
    {
        return await discountRepository.DeleteDiscountByName(request.ProductName);
    }
}