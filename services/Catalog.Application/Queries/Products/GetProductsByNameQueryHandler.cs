using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Products;

public class GetProductsByNameQuery : IRequest<IEnumerable<ProductResponse>>
{
    public string Name { get; set; }

    public GetProductsByNameQuery(string name)
    {
        Name = name;
    }
}

public class GetProductsByNameQueryHandler(IProductRepository productRepository, IMapper mapper)
    : IRequestHandler<GetProductsByNameQuery, IEnumerable<ProductResponse>>
{
    public async Task<IEnumerable<ProductResponse>> Handle(GetProductsByNameQuery request,
        CancellationToken cancellationToken)
    {
        var result = await productRepository.GetProductsByName(request.Name);
        return mapper.Map<IEnumerable<ProductResponse>>(result);
    }
}