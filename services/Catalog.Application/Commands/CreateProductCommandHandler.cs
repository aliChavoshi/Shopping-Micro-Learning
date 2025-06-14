using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Commands;

public class CreateProductCommand : IRequest<ProductResponse>
{

}
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand,ProductResponse>
{
    public Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}