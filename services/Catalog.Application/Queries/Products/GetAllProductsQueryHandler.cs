using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Products;

public class GetAllProductsQuery : IRequest<IEnumerable<ProductResponse>>
{
}

public class GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResponse>>
{
    public async Task<IEnumerable<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var result = await productRepository.GetProducts();
        return mapper.Map<IEnumerable<ProductResponse>>(result);
    }
}