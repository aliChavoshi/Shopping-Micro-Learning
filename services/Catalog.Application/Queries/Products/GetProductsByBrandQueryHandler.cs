using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Products;

public class GetProductsByBrandQuery : IRequest<IEnumerable<ProductResponse>>
{
    public string Brand { get; set; }

    public GetProductsByBrandQuery(string brand)
    {
        Brand = brand;
    }
}

public class GetProductsByBrandQueryHandler(IProductRepository productRepository, IMapper mapper)
    : IRequestHandler<GetProductsByBrandQuery, IEnumerable<ProductResponse>>
{
    public async Task<IEnumerable<ProductResponse>> Handle(GetProductsByBrandQuery request,
        CancellationToken cancellationToken)
    {
        var result = await productRepository.GetProductsByBrand(request.Brand);
        return mapper.Map<IEnumerable<ProductResponse>>(result);
    }
}