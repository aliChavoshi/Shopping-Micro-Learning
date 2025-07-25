using Basket.Core.Repository;
using MediatR;

namespace Basket.Application.Commands.DeleteBasket;

public class DeleteBasketCommand : IRequest<bool>
{
    public string UserName { get; set; }

    public DeleteBasketCommand(string userName)
    {
        UserName = userName;
    }
}

public class DeleteBasketCommandHandler(IBasketRepository basketRepository) : IRequestHandler<DeleteBasketCommand, bool>
{
    public async Task<bool> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        await basketRepository.DeleteBasket(request.UserName);
        return true;
    }
}